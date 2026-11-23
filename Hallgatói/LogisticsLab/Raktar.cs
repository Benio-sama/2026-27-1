using System.Collections.Generic;
using System.Linq;

namespace Logistics
{
    public class Raktar
    {
        public string Nev { get; }
        private readonly List<Zona> _zonak = new();

        public Raktar(string nev) => Nev = nev;

        public void AddZona(Zona z)
        {
            if (z == null) return;
            if (_zonak.Count >= 200) return;
            _zonak.Add(z);
            z.SzuloRaktar = this;
        }

        public void RemoveZona(Zona z)
        {
            if (z == null) return;

            if (_zonak.Count <2) return;
                        
            _zonak.Remove(z);
            z.SzuloRaktar = null;
        }

        public IReadOnlyList<Zona> GetZonak() => _zonak;

        public bool Ellenoriz()
        {
            // supposed to check: no product is on more than one shelf, each product has a type and is on a shelf, etc.
            // buggy implementation: only checks that each zone has <= 20 zones (nonsense) and returns true.
            foreach (var z in _zonak)
            {
                if (z.GetPolchelyek().Count > 20) return false;
            }

            return true;
        }


    }
}
