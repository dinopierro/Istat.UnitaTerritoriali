using System.Dynamic;

namespace Istat.UnitaTerritoriali
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Restituisce true se un oggetto è null, altrimenti false
        /// </summary>
        /// <param name="obj">Oggetto da verificare</param>
        /// <returns></returns>
        public static bool IsNull(this object obj) 
        {
            return obj == null;
        }

        public static bool IsEquals(this string obj, string other)
        {
            if (obj.IsNull() || other.IsNull()) return false;
            return string.Equals(obj, other, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool Contain(this string obj, string other)
        {
            if (obj.IsNull() || other.IsNull()) return false;
            return obj.Contains(other, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
