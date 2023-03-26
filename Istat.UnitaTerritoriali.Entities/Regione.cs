using System.Dynamic;

namespace Istat.UnitaTerritoriali.Entities
{
    public class Regione
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? NomeEstero { get; set; }
    }
    public static class RegioneEx 
    {
        public static Regione ToRegione(this IstatComuneDataRow row) 
        {
            Regione regione = new();
            regione.Id = row.RegioneId?.Trim();
            string? nome = row.Regione?.Trim();
            if (nome != null && nome.Contains('/'))
            {
                string[] nomeSplit = nome.Split('/');
                string nomeIt = nomeSplit[0].Trim();
                string nomeEe = nomeSplit[1].Trim();
                regione.Nome = nomeIt;
                regione.NomeEstero = nomeEe;
            }
            else regione.Nome = nome;
            return regione;
        }
    }
}
