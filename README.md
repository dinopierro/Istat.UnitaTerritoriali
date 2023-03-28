# Istat.UnitaTerritoriali
API .Net Core delle unità territoriali italiane. L'API provvede ad aggiornare i dati automaticamente e contestualmente all'aggiornamento del sito ISTAT.
## Modalità di funzionamento API
#### Origine dati
Sul sito [Istat][1] sono resi disponibili i link permanenti dell'elenco dei comuni italiani in formato csv e in formato xls. Tali indirizzi resteranno immutati ad ogni aggiornamento dei file da parte dell'Istat.
#### Modalità di elaborazione 
Ad ogni richiesta ricevuta, verrà effettuata una interrogazione al file .csv sul sito Istat e recuperata la sola data di ultima modifica del file contenente i dati aggiornati. Nel caso la data di ultima modifica del file csv, differisca da quella utilizzata per l'ultimo aggiornamento dei dati del servizio, questi verranno aggiornati per restituire i risultati richiesti tramite API. 

L'elaborazione dei dati è effettuata mediante la classe **UnitaTerritorialiService** che estende l'interfaccia **IUnitaTerritorialiService**. Quest'ultima, registrata nella WebApplication come servizio **Singleton** e integrata, mediante **Dependency Injection**, in tutti i controllers:

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

## Entità
Di seguito sono indicate le strutture delle entità utilizzate come risposta alle interrogazioni effettuate sull'API.

**Regione**

    public class Regione
    {
		public string? Id { get; set; }
		public string? Nome { get; set; }
		public string? NomeEstero { get; set; }
    }
	
**Unità sovracomunale**

    public class UnitaSovracomunale
    {
    	public string? Id { get; set; }
    	public string? RegioneId { get; set; }
    	public string? TipologiaId { get; set; }
    	public string? Nome { get; set; }
    	public string? NomeEstero { get; set; }
    	public string? Sigla { get; set; }
    	public string? CodiceProvincia { get; set; }
    }
**Tipologia unità sovracomunale**


    public class TipologiaUnitaSovracomunale
    {
    	public string? Id { get; set; }
    	public string? Tipologia { get; set; }
    }
**Comune**


    public class Comune
    {
    	public string? Id { get; set; }
    	public string? UnitaSovracomunaleId { get; set; }
    	public string? RegioneId { get; set; }
    	public string? RipartizioneGeograficaId { get; set; }
    	public string? TipologiaComuneId { get; set; }
    	public string? CodiceCatastale { get; set; }
    	public string? Nome { get; set; }
    	public string? NomeEstero { get; set; }
    	public string? Progressivo { get; set; }
    	public string? CodiceNumerico { get; set; }
    	public string? CodiceNumerico110 { get; set; }
    	public string? CodiceNumerico107 { get; set; }
    	public string? CodiceNumerico103 { get; set; }
    	public string? CodiceNUTS12010 { get; set; }
    	public string? CodiceNUTS22010 { get; set; }
    	public string? CodiceNUTS32010 { get; set; }
    	public string? CodiceNUTS12021 { get; set; }
    	public string? CodiceNUTS22021 { get; set; }
    	public string? CodiceNUTS32021 { get; set; }
    }
**Tipologia comune**


    public class TipologiaComune
    {
    	public string? Id { get; set; }
    	public string? Tipologia { get; set; }
    }
**Ripartizione geografica**


    public class RipartizioneGeografica
    {
    	public string? Id { get; set; }
    	public string? Nome { get; set; }
    }

## Consumo API
Di seguito sono elencati gli endpoint disponibili e il tipo di dato fornito in risposta. Si fa presente che l'API è attualmente pubblicata su **Azure** e disponibile al seguente indirizzo: [https://istat.azurewebsites.net](https://istat.azurewebsites.net "https://istat.azurewebsites.net"). Disponibile pagina swagger OpenAPI da utilizzare come documentazione API al seguente indirizzo [https://istat.azurewebsites.net/swagger/index.html](https://istat.azurewebsites.net/swagger/index.html "https://istat.azurewebsites.net/swagger/index.html")

|  EndPoint | Tipo di dato  | Note |
| ------------ | ------------ | -|
|  **/Regioni** | ` List<Regione>` | Tutte le regioni|
|  /Regioni/{id} | ` Regione` | Regione da id (dove {id} è un valore stringa compreso tra 01 e 20)|
|  /Regioni/Like/{like} | ` List<Regione>` | Tutte le regioni che contengono il valore stringa {like} nel nome o nel nome estero|
|  **/UnitaSovracomunali** | ` List<UnitaSovracomunale>` | Tutte le unità sovracomunali|
|  /UnitaSovracomunali/Tipologie | ` List<TipologiaUnitaSovracomunale>` | Tutte le tipologie di unità sovracomunali|
|  /UnitaSovracomunali/ById/{id} | ` UnitaSovracomunale` | Unità sovracomunale avente {id} passato|
|  /UnitaSovracomunali/ByRegioneId/{regioneId} | ` List<UnitaSovracomunale>` | Unità sovracomunali appartenenti alla regione con {regioneId} passato|
|  /UnitaSovracomunali/ByTipologiaId/{tipologiaId} | ` List<UnitaSovracomunale>` | Unità sovracomunali appartenenti alla tipologia con {tipologiaId} passato|
|  /UnitaSovracomunali/Like/{like} | ` List<UnitaSovracomunale>` | Unità sovracomunali che contengono il valore {like} passato nel nome o nel nome estero|
|  **/Comuni** | ` List<Comune>` | Tutti i comuni italiani|
|  **/Comuni/RipartizioniGeografiche** | ` List<RipartizioneGeografica>` | Tutte le ripartizioni geografiche previste|
|  **/Comuni/Tipologie** | ` List<TipologiaComune>` | Tutte le ripartizioni geografiche previste|
|  /Comuni/ById/{id} | ` Comune` | Comune il cui {id} corrisponde a quello fornito. L'id del comune corrisponde al codice alfanumerico Istat|
|  /Comuni/ByUnitaSovracomunaleId/{id} | ` List<Comune>` | Lista di tutti i comuni appartenenti all'unità sovracomunale avente {id} passato|
|  /Comuni/ByRegioneId/{id} | ` List<Comune>` | Lista di tutti i comuni appartenenti alla regione avente {id} passato|
|  /Comuni/ByRipartizioneGeograficaId/{id} | ` List<Comune>` | Lista di tutti i comuni appartenenti alla ripartizione geografica avente {id} passato|
|  /Comuni/ByTipologiaId/{id} | ` List<Comune>` | Lista di tutti i comuni appartenenti alla tipologia avente {id} passato|
|  /Comuni/Like/{like} | ` List<Comune>` | Lista di tutti i comuni avente la stringa {like} nel nome o nell'eventuale nome estero|
