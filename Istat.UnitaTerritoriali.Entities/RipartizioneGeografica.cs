namespace Istat.UnitaTerritoriali.Entities
{
    public class RipartizioneGeografica
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
    }
    public static class RipartizioneGeograficaEx
    {
        public static RipartizioneGeografica ToRipartizioneGeografica(this IstatComuneDataRow row)
        {
            RipartizioneGeografica ripartizione = new();
            ripartizione.Id = row.RipartizioneGeograficaId?.Trim();
            ripartizione.Nome = row.RipartizioneGeografica?.Trim();
            return ripartizione;
        }
    }
}
