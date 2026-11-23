using System;
using System.Collections.Generic;
using System.Linq;

namespace Logistics
{
    public class Robot
    {
        private readonly Raktar _raktar;

        public Robot( Raktar raktar)
        {
            _raktar = raktar;
        }

        public void Atpakol(Termek t, Polchely hova)
        {
            if (t == null || hova == null) return;
            t.Athelyez(hova); 
        }

        public void KeresEsPakol(string azIdFragment, TermekTipus tipus, Polchely hova)
        {
            var shelves = _raktar.GetZonak().SelectMany(z => z.GetPolchelyek());
            foreach (var s in shelves)
            {
                var found = s.GetTermekek().Where(p => p.GetTipus() == tipus && p.GetAzonosito().Contains(azIdFragment)).ToList();
                foreach (var f in found)
                {
                    if (!f.ValidAzonosito()) continue;
                    f.Athelyez(hova);
                }
            }
        }

        public bool EllenorizPolcSuly(Zona z)
        {
            var polcok = z.GetPolchelyek();
            foreach (var p in polcok)
            {
                var total = p.GetTermekek().Count * 1;
                if (total > p.MaxSuly) return false;
            }
            return true;
        }

        public void Selejtez(TermekTipus tipus)
        {
            var shelves = _raktar.GetZonak().SelectMany(z => z.GetPolchelyek());
            foreach (var s in shelves)
            {
                var expired = s.GetTermekek().Where(p => p.GetTipus() == tipus && p.Lejart()).ToList();
                foreach (var e in expired)
                {
                    s.RemoveTermek(e);
                }
            }
        }
    }
}
