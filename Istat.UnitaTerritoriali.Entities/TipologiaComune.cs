namespace Istat.UnitaTerritoriali.Entities
{
    public class TipologiaComune
    {
        public string? Id { get; set; }
        public string? Tipologia { get; set; }
        public TipologiaComune(string id, string tipologia)
        {
            Id = id;
            Tipologia = tipologia;
        }
        public static TipologiaComune GetItem(string? id)
        {
            return TIPOLOGIE.First(i => string.Equals(i.Id, id, StringComparison.InvariantCultureIgnoreCase));
        }
        public static List<TipologiaComune> GetItems()
        {
            return TIPOLOGIE.ToList();
        }
        private static TipologiaComune[] TIPOLOGIE
        {
            get
            {
                if (tipologie.Any()) return tipologie;
                tipologie = new TipologiaComune[2];
                tipologie[0] = new TipologiaComune("0", "Comune non capoluogo");
                tipologie[1] = new TipologiaComune("1", "Comune capoluogo");
                return tipologie;
            }
        }
        private static TipologiaComune[] tipologie = Array.Empty<TipologiaComune>();
    }
}
