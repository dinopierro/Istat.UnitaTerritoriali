namespace Istat.UnitaTerritoriali.Entities
{
    public class TipologiaUnitaSovracomunale
    {
        public string? Id { get; set; }
        public string? Tipologia { get; set; }
        public TipologiaUnitaSovracomunale(string id, string tipologia)
        {
            Id = id;
            Tipologia = tipologia;
        }
        public static TipologiaUnitaSovracomunale GetItem(string? id) 
        {
            return TIPOLOGIE.First(i => string.Equals(i.Id, id, StringComparison.InvariantCultureIgnoreCase));
        }
        public static List<TipologiaUnitaSovracomunale> GetItems()
        {
            return TIPOLOGIE.ToList();
        }
        private static TipologiaUnitaSovracomunale[] TIPOLOGIE
        {
            get
            {
                if (tipologie.Any()) return tipologie;
                tipologie = new TipologiaUnitaSovracomunale[5];
                tipologie[0] = new TipologiaUnitaSovracomunale("1", "Provincia");
                tipologie[1] = new TipologiaUnitaSovracomunale("2", "Provincia autonoma");
                tipologie[2] = new TipologiaUnitaSovracomunale("3", "Città metropolitana");
                tipologie[3] = new TipologiaUnitaSovracomunale("4", "Libero consorzio di comuni");
                tipologie[4] = new TipologiaUnitaSovracomunale("5", "Unità non amministrativa (ex-province del Friuli-Venezia Giulia)");
                return tipologie;
            }
        }
        private static TipologiaUnitaSovracomunale[] tipologie = Array.Empty<TipologiaUnitaSovracomunale>();
    }
}
