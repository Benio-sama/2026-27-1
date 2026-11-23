using System.Collections.Generic;
using System.Linq;

namespace Logistics
{
    public class Polchely : IDisposable
    {
        public string Id { get; }
        public Zona Zona { get; set; }
        public int MaxSuly { get; } 
        private readonly List<Termek> _termekek = new();

        internal Polchely(Zona Zona, string id, int maxSuly)
        {
            this.Zona = Zona;
            Id = id;
            MaxSuly = maxSuly;
        }

        public void RemoveTermek(Termek t)
        {
            _termekek.Remove(t);
            t.Polchely = null;
        }

        public IReadOnlyList<Termek> GetTermekek() => _termekek;

        public void AthelyezCsoportos(Polchely hova)
        {
            if (hova.GetJelenlegiSuly() + GetJelenlegiSuly() > MaxSuly) return;
            foreach (var t in _termekek.ToList())
            {
                hova.AddTermek(t);
            }
        }

        public int GetJelenlegiSuly()
        {
            return _termekek.Sum(t => t.GetTipus().Suly);
        }

        public Termek? Kereses(string frag) => _termekek.FirstOrDefault(p => p.GetAzonosito().Contains(frag));
        
        public void AddTermek(Termek t)
        {
            if (t == null) return;

            _termekek.Add(t);
            t.Polchely = this;
        }


        public void Dispose()
        {
            foreach (var t in _termekek)
                RemoveTermek(t);
        }
    }
}
