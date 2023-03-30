# Istat.UnitaTerritoriali
API .Net Core delle unità territoriali italiane. L'API provvede ad aggiornare i dati automaticamente e contestualmente all'aggiornamento del sito ISTAT.
## Modalità di funzionamento API
#### Origine dati
Sul sito [Istat][1] sono resi disponibili i link permanenti dell'elenco dei comuni italiani in formato csv e in formato xls. Tali indirizzi resteranno immutati ad ogni aggiornamento dei file da parte dell'Istat.
#### Modalità di elaborazione 
Ad ogni richiesta ricevuta, verrà effettuata una interrogazione al file .csv sul sito Istat e recuperata la sola data di ultima modifica del file contenente i dati aggiornati. Nel caso la data di ultima modifica del file csv, differisca da quella utilizzata per l'ultimo aggiornamento dei dati del servizio, questi verranno aggiornati per restituire i risultati richiesti tramite API. 

L'elaborazione dei dati è effettuata mediante la classe **UnitaTerritorialiService** che estende l'interfaccia **IUnitaTerritorialiService**. Questa é registrata nella WebApplication come servizio **Singleton** e integrata, mediante **Dependency Injection**, in tutti i controllers:

`builder.Services.AddSingleton<IUnitaTerritorialiService, UnitaTerritorialiService>();`

A seguito della transcodifica dei dati Istat, l'interfaccia di servizio esporrà, in tutti i controlli, i seguenti metodi:

    public interface IUnitaTerritorialiService
    {
    	Task<List<IstatComuneDataRow>> GetDataSource();
    	Task<List<Regione>> GetRegioni();
    	Task<List<UnitaSovracomunale>> GetUnitaSovracomunali();
    	Task<List<TipologiaUnitaSovracomunale>> GetTipologieUnitaSovracomunali();
    	Task<List<Comune>> GetComuni();
    	Task<List<TipologiaComune>> GetTipologieComuni();
    	Task<List<RipartizioneGeografica>> GetRipartizioniGeografiche();
    }
[1]: https://www.istat.it/it/archivio/6789 "Istat"

## Consumo API
L'Api è attualmente pubblicata su **Azure** e disponibile al seguente indirizzo: [https://istat.azurewebsites.net](https://istat.azurewebsites.net "https://istat.azurewebsites.net")
