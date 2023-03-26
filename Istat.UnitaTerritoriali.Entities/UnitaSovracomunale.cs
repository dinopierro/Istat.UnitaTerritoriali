using System.Dynamic;

namespace Istat.UnitaTerritoriali.Entities
{
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
    public static class UnitaSovracomunaleEx
    {
        public static UnitaSovracomunale ToUnitaSovracomunale(this IstatComuneDataRow row)
        {
            UnitaSovracomunale unita = new();
            unita.Id = row.UnitaTerritorialeSovracomunaleId?.Trim();
            unita.CodiceProvincia = row.ProvinciaId?.Trim();
            unita.RegioneId = row.RegioneId;
            unita.TipologiaId = row.TipoUnitaTerritorialeSovracomunaleId;
            string? nome = row.UnitaTerritorialeSovracomunale?.Trim();
            if (nome != null && nome.Contains('/'))
            {
                string[] nomeSplit = nome.Split('/');
                string nomeIt = nomeSplit[0].Trim();
                string nomeEe = nomeSplit[1].Trim();
                unita.Nome = nomeIt;
                unita.NomeEstero = nomeEe;
            }
            else unita.Nome = nome;
            unita.Sigla = row.SiglaAutomobilistica;
            return unita;
        }
    }
}
