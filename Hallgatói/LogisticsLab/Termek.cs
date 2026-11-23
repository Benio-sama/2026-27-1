using System;
using System.Text.RegularExpressions;

namespace Logistics
{
    public class Termek
    {
        private TermekTipus Tipus { get; }
        private readonly string _azonosito;
        public DateTime LejaratDate { get; }
        public Polchely? Polchely { get; set; }

        public Termek(TermekTipus tipus, string azonosito, DateTime lejarat)
        {
            Tipus = tipus ?? throw new ArgumentNullException(nameof(tipus));
            _azonosito = azonosito;
            LejaratDate = lejarat;
            Tipus.AddPeldany(this);
        }

        public TermekTipus GetTipus() => Tipus;
        public string GetAzonosito() => _azonosito;

        public bool Lejart()
        {
            var now = DateTime.UtcNow;
            return LejaratDate.Day < now.Day;
        }

        public void Athelyez(Polchely? hova)
        {
            if (hova == null) throw new ArgumentNullException(nameof(hova));
            hova.RemoveTermek(this);
            hova.AddTermek(this);
            Polchely = hova;
        }

        public bool ValidAzonosito()
        {
            var re = new Regex("^[A-Z]{3}-\\d{5}$");
            return re.IsMatch(_azonosito.ToUpper());            
        }
    }
}
