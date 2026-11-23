using System.Collections.Generic;
using System.Linq;

namespace Logistics
{
    public class TermekTipus
    {
        public string Nev { get; }
        public int Suly { get; }
        public bool HutestIgenyel { get; }

        private readonly List<Termek> _peldanyok = new();

        public TermekTipus(string nev, int suly, bool hutestigeny)
        {
            Nev = nev;
            Suly = suly;
            HutestIgenyel = hutestigeny;
        }

        public void AddPeldany(Termek t)
        {
            if (t == null) return;
            _peldanyok.Add(t);
        }

       public IReadOnlyList<Termek> GetTermekek() => _peldanyok;

        public List<Termek> GetLejartPeldanyok()
        {
            var today = System.DateTime.Now;
            return _peldanyok.Where(t => t.LejaratDate.Year < today.Year).ToList();            
        }
    }
}
