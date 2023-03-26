using System.Dynamic;

namespace Istat.UnitaTerritoriali.Entities
{
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
    public static class ComuneEx
    {
        public static Comune ToComune(this IstatComuneDataRow row)
        {
            Comune comune = new();                        
            comune.Id = row.Id?.Trim();
            comune.CodiceCatastale = row.CodiceCatastale?.Trim();
            comune.Nome = row.DenominazioneIt.Trim();
            comune.NomeEstero = row.DenominazioneEe.Trim();
            comune.UnitaSovracomunaleId = row.UnitaTerritorialeSovracomunaleId.Trim();
            comune.RegioneId = row.RegioneId.Trim();
            comune.RipartizioneGeograficaId = row.RipartizioneGeograficaId.Trim();
            comune.TipologiaComuneId = row.FlagId.Trim();
            comune.Progressivo = row.Progressivo?.Trim();
            comune.CodiceNumerico = row.IdNumerico?.Trim();
            comune.CodiceNumerico110 = row.Id110?.Trim();
            comune.CodiceNumerico107 = row.Id107?.Trim();
            comune.CodiceNumerico103 = row.Id103?.Trim();
            comune.CodiceNUTS12010 = row.CodiceNUTS12010?.Trim();
            comune.CodiceNUTS22010 = row.CodiceNUTS22010?.Trim();
            comune.CodiceNUTS32010 = row.CodiceNUTS32010?.Trim();
            comune.CodiceNUTS12021 = row.CodiceNUTS12021?.Trim();
            comune.CodiceNUTS22021 = row.CodiceNUTS22021?.Trim();
            comune.CodiceNUTS32021 = row.CodiceNUTS32021?.Trim();
            return comune;
        }
    }
}
