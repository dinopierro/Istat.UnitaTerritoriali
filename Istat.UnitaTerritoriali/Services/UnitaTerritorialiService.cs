using Istat.UnitaTerritoriali.Entities;
using static System.Net.WebRequestMethods;

namespace Istat.UnitaTerritoriali.Services
{
    public class UnitaTerritorialiService : IUnitaTerritorialiService
    {
        /// <summary>
        /// Permalink Istat, in formato csv, di tutti i comuni italiani. Il link è costantemente aggiornato
        /// e distribuito dall'Istituto Nazionale di Statistica e contiene le informazioni aggiornate di comuni,
        /// province, regioni e unità territoriali sovracomunali.
        /// </summary>
        const string permalink = "https://www.istat.it/storage/codici-unita-amministrative/Elenco-comuni-italiani.csv";
        /// <summary>
        /// Data di ultima modifica del file csv Istat. Questa data viene utilizzata per verificare quando il file è stato
        /// aggiornato per consentire di aggiornare contestualmente anche il servizio corrente
        /// </summary>
        DateTime lastModifiedSource = DateTime.MinValue;
        /// <summary>
        /// Locker utilizzato per impedire più aggiornamenti contestuali. La prima chiamata che verifica
        /// la necessità di aggiornare il servizio, procede normalmente. Eventuali altre chiamate che 
        /// arriveranno nel frattempo, resteranno in attesa del completamento e al termine di questo, procederanno 
        /// senza eseguire ulteriori aggiornamenti del servizio
        /// </summary>
        object updateLocker = new object();
        /// <summary>
        /// Dati transcodificati del sorgente Istat delle unità territoriali
        /// </summary>
        List<IstatComuneDataRow> dataSource = new List<IstatComuneDataRow>();
        /// <summary>
        /// Dati transcodificati del sorgente Istat delle regioni
        /// </summary>
        List<Regione> regioni = new List<Regione>();
        /// <summary>
        /// Dati transcodificati del sorgente Istat delle unità sovracomunali
        /// </summary>
        List<UnitaSovracomunale> unita = new List<UnitaSovracomunale>();
        /// <summary>
        /// Dati transcodificati del sorgente Istat delle tipologie di unità sovracomunali
        /// </summary>
        List<TipologiaUnitaSovracomunale> tipologieUnitaSovracomunali = new List<TipologiaUnitaSovracomunale>();
        /// <summary>
        /// Dati transcodificati del sorgente Istat dei comuni
        /// </summary>
        List<Comune> comuni = new List<Comune>();
        /// <summary>
        /// Dati transcodificati del sorgente Istat delle tipologie dei comuni
        /// </summary>
        List<TipologiaComune> tipologieComuni = new List<TipologiaComune>();
        /// <summary>
        /// Dati transcodificati del sorgente Istat delle ripartizioni geografiche
        /// </summary>
        List<RipartizioneGeografica> ripartizioniGeografiche = new List<RipartizioneGeografica>();
        /// <summary>
        /// Metodo che verifica se occorre aggiornare il servizio poichè il file sorgente distribuito dall'Istat
        /// è stato modificato. Se la data di ultima modifica del file Istat risulta più aggiornata di quella 
        /// utilizzata per valorizzare i dati del servizio, questi verranno aggiornati con i nuovi dati ufficiali.
        /// </summary>
        private async Task UpdateIfNeeded() 
        {
            DateTime currentLastModifiedSource = await GetLastModifiedSource();
            bool needUpdateService = lastModifiedSource < currentLastModifiedSource;
            if (!needUpdateService) return;
            else await Update();
        }
        /// <summary>
        /// Metodo che aggiorna i dati del servizio
        /// </summary>
        private async Task Update()
        {
            HttpResponseMessage response = await new HttpClient().GetAsync(permalink);
            if (!response.IsSuccessStatusCode) return;
            lock (updateLocker) 
            {
                if (lastModifiedSource < response.Content.Headers.LastModified.Value.UtcDateTime &&
                    UpdateServiceData(response.Content.ReadAsStream()))
                    lastModifiedSource = response.Content.Headers.LastModified.Value.UtcDateTime;
            }
        }
        /// <summary>
        /// Metodo che riceve in input lo stream del file sorgente dei dati Istat, lo legge e 
        /// lo converte in oggetti utilizzati dal servizio
        /// </summary>
        /// <param name="stream">Stream del file sorgente dei dati Istat</param>
        /// <returns>True se l'aggiornamento dei dati viene eseguito con successo, altrimenti false</returns>
        private bool UpdateServiceData(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new(stream, System.Text.Encoding.UTF7)) 
            {
                int count = 0;
                List<IstatComuneDataRow> data = new();
                while (!reader.EndOfStream) 
                {
                    string? line = reader.ReadLine();
                    if (line == null) continue;
                    if (count > 0) //salto la prima riga con le intestazioni 
                    {
                        string[] items = line.Split(';');
                        IstatComuneDataRow? item = items.ToIstatComuneDataRow();
                        if (item == null) continue;
                        data.Add(item);
                    }
                    count++;
                }
                if (count > 0)
                {
                    UpdateRegioni(data);
                    UpdateUnitaSovracomunali(data);
                    UpdateTipologieUnitaSovracomunali(data);
                    UpdateComuni(data);
                    UpdateTipologieComuni(data);
                    UpdateRipartizioniGeografiche(data);
                    dataSource = data;
                }
            }
            return true;
        }
        /// <summary>
        /// Metodo che aggiorna il datasource delle ripartizioni geografiche
        /// </summary>
        /// <param name="data">DataSource ISTAT da cui aggregare i dati</param>
        private void UpdateRipartizioniGeografiche(List<IstatComuneDataRow> data)
        {
            var tipologie = data.DistinctBy(i => i.RipartizioneGeograficaId).ToList();
            var result = new List<RipartizioneGeografica>();
            tipologie.ForEach(t =>
            {
                result.Add(t.ToRipartizioneGeografica());
            });
            this.ripartizioniGeografiche = result.OrderBy(i => i.Id).ToList();
        }
        /// <summary>
        /// Metodo che aggiorna il datasource delle tipologie dei comuni
        /// </summary>
        /// <param name="data">DataSource ISTAT da cui aggregare i dati</param>
        private void UpdateTipologieComuni(List<IstatComuneDataRow> data)
        {
            var tipologie = data.DistinctBy(i => i.FlagId).Select(i => i.FlagId).ToList();
            var result = new List<TipologiaComune>();
            tipologie.ForEach(t =>
            {
                result.Add(TipologiaComune.GetItem(t));
            });
            this.tipologieComuni = result.OrderBy(i => i.Id).ToList();
        }
        /// <summary>
        /// Metodo che aggiorna il datasource dei comuni
        /// </summary>
        /// <param name="data">DataSource ISTAT da cui aggregare i dati</param>
        private void UpdateComuni(List<IstatComuneDataRow> data)
        {
            var result = new List<Comune>();
            data.ForEach(r =>
            {
                result.Add(r.ToComune());
            });
            this.comuni = result;
        }
        /// <summary>
        /// Metodo che aggiorna il datasource delle tipologie di unità sovracomunali
        /// </summary>
        /// <param name="data">DataSource ISTAT da cui aggregare i dati</param>
        private void UpdateTipologieUnitaSovracomunali(List<IstatComuneDataRow> data)
        {
            var tipologie = data.DistinctBy(i => i.TipoUnitaTerritorialeSovracomunaleId).Select(i => i.TipoUnitaTerritorialeSovracomunaleId).ToList();
            var result = new List<TipologiaUnitaSovracomunale>();
            tipologie.ForEach(t =>
            {
                result.Add(TipologiaUnitaSovracomunale.GetItem(t));
            });
            this.tipologieUnitaSovracomunali = result.OrderBy(i => i.Id).ToList();
        }
        /// <summary>
        /// Metodo che aggiorna il datasource delle unità sovracomunali
        /// </summary>
        /// <param name="data">DataSource ISTAT da cui aggregare i dati</param>
        private void UpdateUnitaSovracomunali(List<IstatComuneDataRow> data)
        {
            var unita = data.DistinctBy(i => i.UnitaTerritorialeSovracomunaleId).ToList();
            var result = new List<UnitaSovracomunale>();
            unita.ForEach(u =>
            {
                result.Add(u.ToUnitaSovracomunale());
            });
            this.unita = result;
        }
        /// <summary>
        /// Metodo che aggiorna il datasource delle regioni
        /// </summary>
        /// <param name="data">DataSource ISTAT da cui aggregare i dati</param>
        private void UpdateRegioni(List<IstatComuneDataRow> data)
        {
            var regioni = data.DistinctBy(i => i.RegioneId).ToList();
            var result = new List<Regione>();
            regioni.ForEach(r =>
            {
                result.Add(r.ToRegione());
            });
            this.regioni = result;
        }
        /// <summary>
        /// Metodo che restituisce la data di ultima modifica del file csv distribuito dall'Istat
        /// contenete i dati necessari al servizio di consultazione delle unità territoriali
        /// </summary>
        /// <returns>Data di ultimo aggiornamento del file Istat o, DateTime.MinValue se non è possibile
        /// consultare o recuperare dal link la data di ultima modifica</returns>
        private async Task<DateTime> GetLastModifiedSource()
        {
            HttpResponseMessage response = await new HttpClient().GetAsync(permalink, HttpCompletionOption.ResponseHeadersRead);
            if (!response.IsSuccessStatusCode) return DateTime.MinValue;
            var lastModified = response.Content.Headers.LastModified;
            return lastModified.Value.UtcDateTime;
        }
        /// <summary>
        /// Restituisce la lista degli item transcodificati dal file sorgente Istat
        /// </summary>
        /// <returns></returns>
        public async Task<List<IstatComuneDataRow>> GetDataSource()
        {
            await UpdateIfNeeded();
            return dataSource;
        }
        /// <summary>
        /// Restituisce la lista delle regioni dal file sorgente Istat
        /// </summary>
        /// <returns></returns>
        public async Task<List<Regione>> GetRegioni()
        {
            await UpdateIfNeeded();
            return regioni;
        }
        /// <summary>
        /// Restituisce la lista delle unità sovracomunali dal file sorgente Istat
        /// </summary>
        /// <returns></returns>
        public async Task<List<UnitaSovracomunale>> GetUnitaSovracomunali()
        {
            await UpdateIfNeeded();
            return unita;
        }
        /// <summary>
        /// Restituisce la lista delle tipologie di unità sovracomunali dal file sorgente Istat
        /// </summary>
        /// <returns></returns>
        public async Task<List<TipologiaUnitaSovracomunale>> GetTipologieUnitaSovracomunali()
        {
            await UpdateIfNeeded();
            return tipologieUnitaSovracomunali;
        }
        /// <summary>
        /// Restituisce la lista dei comuni dal file sorgente Istat
        /// </summary>
        /// <returns></returns>
        public async Task<List<Comune>> GetComuni()
        {
            await UpdateIfNeeded();
            return comuni;
        }
        /// <summary>
        /// Restituisce la lista delle tipologie di comune dal file sorgente Istat
        /// </summary>
        /// <returns></returns>
        public async Task<List<TipologiaComune>> GetTipologieComuni()
        {
            await UpdateIfNeeded();
            return tipologieComuni;
        }
        /// <summary>
        /// Restituisce la lista delle ripartizioni geografiche dal file sorgente Istat
        /// </summary>
        /// <returns></returns>
        public async Task<List<RipartizioneGeografica>> GetRipartizioniGeografiche()
        {
            await UpdateIfNeeded();
            return ripartizioniGeografiche;
        }
    }
}
