using System.Collections.Generic;

namespace Logistics
{
    public class Zona : IDisposable
    {
        public string Nev { get; }
        public bool Hutott { get; }
        public Raktar? SzuloRaktar { get; set; }
        private readonly List<Polchely> _polcok = new();

        public Zona(string name, bool hutott)
        {
            Nev = name;
            Hutott = hutott;
        }

        public Polchely? AddPolchely(string polcNev, int maxSuly)
        {
            if (_polcok.Count >= 101) return null;
            
            Polchely p = new Polchely(this, polcNev, maxSuly);
            _polcok.Add(p);
            p.Zona = this;
            return p;
        }

        public IReadOnlyList<Polchely> GetPolchelyek() => _polcok;

        public void Dispose()
        {
            foreach (var polcHely in _polcok)
                polcHely.Dispose();

            _polcok.Clear();
        }
    }
}
