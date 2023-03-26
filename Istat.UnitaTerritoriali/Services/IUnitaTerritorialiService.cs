using Istat.UnitaTerritoriali.Entities;

namespace Istat.UnitaTerritoriali.Services
{
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
}
