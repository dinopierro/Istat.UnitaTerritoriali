namespace Istat.UnitaTerritoriali.Entities
{
    public class IstatComuneDataRow
    {
        public string? RegioneId { get; set; }
        public string? UnitaTerritorialeSovracomunaleId { get; set; }
        public string? ProvinciaId { get; set; }
        public string? Progressivo { get; set; }
        public string? Id { get; set; }
        public string? DenominazioneItEe { get; set; }
        public string? DenominazioneIt { get; set; }
        public string? DenominazioneEe { get; set; }
        public string? RipartizioneGeograficaId { get; set; }
        public string? RipartizioneGeografica { get; set; }
        public string? Regione { get; set; }
        public string? UnitaTerritorialeSovracomunale { get; set; }
        public string? TipoUnitaTerritorialeSovracomunaleId { get; set; }
        public string? FlagId { get; set; }
        public string? SiglaAutomobilistica { get; set; }
        public string? IdNumerico { get; set; }
        public string? Id110 { get; set; }
        public string? Id107 { get; set; }
        public string? Id103 { get; set; }
        public string? CodiceCatastale { get; set; }
        public string? CodiceNUTS12010 { get; set; }
        public string? CodiceNUTS22010 { get; set; }
        public string? CodiceNUTS32010 { get; set; }
        public string? CodiceNUTS12021 { get; set; }
        public string? CodiceNUTS22021 { get; set; }
        public string? CodiceNUTS32021 { get; set; }
    }
    public static class IstatComuneDataRowEx 
    {
        /// <summary>
        /// Estensione che converte una row del sorgente delle unità territoriali istat in una entità IstatComuneDataRow
        /// </summary>
        /// <param name="row">Array dei valori di una row del file sorgente Istat</param>
        /// <returns>Entità con i dati della row data o null se row è null o non contiene esattamente 26 elementi</returns>
        public static IstatComuneDataRow? ToIstatComuneDataRow(this string[] row)
        {
            if (row == null || row.Length != 26) return null; 
            return new IstatComuneDataRow()
            {
                RegioneId = row[0],
                UnitaTerritorialeSovracomunaleId = row[1],
                ProvinciaId = row[2],
                Progressivo = row[3],
                Id = row[4],
                DenominazioneItEe = row[5],
                DenominazioneIt = row[6],
                DenominazioneEe = row[7],
                RipartizioneGeograficaId = row[8],
                RipartizioneGeografica = row[9],
                Regione = row[10],
                UnitaTerritorialeSovracomunale = row[11],
                TipoUnitaTerritorialeSovracomunaleId = row[12],
                FlagId = row[13],
                SiglaAutomobilistica = row[14],
                IdNumerico = row[15],
                Id110 = row[16],
                Id107 = row[17],
                Id103 = row[18],
                CodiceCatastale = row[19],
                CodiceNUTS12010 = row[20],
                CodiceNUTS22010 = row[21],
                CodiceNUTS32010 = row[22],
                CodiceNUTS12021 = row[23],
                CodiceNUTS22021 = row[24],
                CodiceNUTS32021 = row[25]
            };
        }
    }
}
