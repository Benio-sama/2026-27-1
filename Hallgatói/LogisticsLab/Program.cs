using System;
using System.Collections.Generic;
using System.Linq;

namespace Logistics
{
    class Program
    {
        static void Main(string[] args)
        {
            var raktar = new Raktar("Fő raktár");
            var z1 = new Zona("Hűtött zóna", true);
            var z2 = new Zona("Normál zóna", false);
            raktar.AddZona(z1);
            raktar.AddZona(z2);

            var tipus1 = new TermekTipus("Boci 450gr tejföl", 450, true);
            var tipus2 = new TermekTipus("MagicPen 200 toll", 120, false);

            // create some product instances
            var p1 = new Termek(tipus1, "BOC-00001", DateTime.UtcNow.AddDays(10));
            var p2 = new Termek(tipus1, "BOC-00002", DateTime.UtcNow.AddDays(-1)); // expired
            var p3 = new Termek(tipus2, "MAG-12345", DateTime.UtcNow.AddDays(30));
            var p4 = new Termek(tipus2, "bad-000", DateTime.UtcNow.AddDays(30)); // bad id format intentionally

            // place on shelves
            var polc1 = new Polchely(z1,"Z1-P1", 10000); // weight limit 10kg
            var polc2 = new Polchely(z1,"Z1-P2", 10000);

            polc1.AddTermek(p1);
            polc1.AddTermek(p2);
            polc2.AddTermek(p3);
            polc2.AddTermek(p4);

            var robot = new Robot( raktar);

            Console.WriteLine($"Polc1 összes termék száma: {polc1.GetTermekek().Count}");
            Console.WriteLine($"P2 lejárt? {p2.Lejart()}");

            Console.WriteLine("Polc1 -> polc2 csoportos áthelyezés");
            polc1.AthelyezCsoportos(polc2);

            Console.WriteLine($"Áthelyezés után, polc1: {polc1.GetTermekek().Count}, polc2: {polc2.GetTermekek().Count}");

            Console.WriteLine("Robot: az '123' azonosítóra keresés és polc1-re mozgatás");
            robot.KeresEsPakol("123", tipus2, polc1);

            Console.WriteLine("Audit:");
            Console.WriteLine($"Konzisztecia Ok? {raktar.Ellenoriz()}");
        }
    }
}
