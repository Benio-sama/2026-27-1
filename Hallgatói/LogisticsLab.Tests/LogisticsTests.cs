using System;
using Xunit;
using Logistics;
using System.Linq;

namespace Logistics.Tests
{
    public class LogisticsTests
    {
        [Fact(DisplayName = "1. A zonában max 100 polchely engedélyezett")]
        public void Test_AddPolchely()
        {
            var z = new Zona("Z", false);
            for (int i = 0; i < 101; i++)
            {
                z.AddPolchely("P" + i, 1000);
            }

            Assert.True(z.GetPolchelyek().Count <= 100);
        }

        [Fact(DisplayName = "2. Hűtendő áru csak hűtőtt polchelyhez adható")]
        public void Test_Polchely_AddTermek()
        {
            Raktar r = new Raktar("r");
            Zona z1 = new Zona("Z1", false);
            r.AddZona(z1);
            Zona z2 = new Zona("Z2", true);
            r.AddZona(z2);

            var p1 = z1.AddPolchely("p1", 200);
            var p2 = z2.AddPolchely("p2", 200);

            var tipus = new TermekTipus("T", 100, true);
            var termek = new Termek(tipus, "ABC-12345", DateTime.UtcNow.AddDays(1));

            p1.AddTermek(termek);
            p2.AddTermek(termek);

            Assert.False(p1.GetTermekek().Any(), "Hűtést igénylő termék nem hűtött polcra került");
            Assert.NotEmpty(p2.GetTermekek());
        }

        
        [Fact(DisplayName = "3. A termékazonosító validációja a megadott formátum szerinti")]
        public void Test_ValidAzonosito()
        {
            var tipus = new TermekTipus("T", 100, false);
            var good = new Termek(tipus, "ABC-12345", DateTime.UtcNow.AddDays(1));
            var bad = new Termek(tipus, "bad-123", DateTime.UtcNow.AddDays(1));
            var bad2 = new Termek(tipus, "bad-12345", DateTime.UtcNow.AddDays(1));

            Assert.True(good.ValidAzonosito(), "Érvényes azonosító érvénytelennek minősítve");
            Assert.False(bad.ValidAzonosito(), "Érvénytelen azonosító érvényesnek minősítve");
            Assert.False(bad2.ValidAzonosito(), "Érvénytelen azonosító érvényesnek minősítve");
        }

        [Fact(DisplayName = "4. A lejárt példányok lekérdezése mindig helyesen működik")]
        public void Test_GetLejartPeldanyok()
        {
            var tipus = new TermekTipus("T", 10, false);
            var t1 = new Termek(tipus, "AAA-00001", DateTime.UtcNow.AddDays(-40));
            var t2 = new Termek(tipus, "AAA-00002", DateTime.UtcNow.AddDays(10));

            var expired = tipus.GetLejartPeldanyok();
            Assert.Contains(t1, expired);
            Assert.DoesNotContain(t2, expired);
        }


        [Fact(DisplayName = "5. A termékáthelyezés eltávolítja a forrásból")]
        public void Test_Athelyez()
        {
            Zona z = new Zona("z1", false);
            var polc1 = z.AddPolchely("P1", 10000);
            var polc2 = z.AddPolchely("P2", 10000);
            var tipus = new TermekTipus("T", 100, false);
            var t1 = new Termek(tipus, "ABC-00001", DateTime.UtcNow.AddDays(1));
            polc1.AddTermek(t1);

            t1.Athelyez(polc2);

            Assert.Empty(polc1.GetTermekek());
            Assert.Contains(t1, polc2.GetTermekek());
        }


        [Fact(DisplayName = "6. A csoportos áthelyezés után üres polc marad")]
        public void Test_AthelyezCsoportos()
        {
            Zona z = new Zona("z1", false);
            var polc1 = z.AddPolchely("P1", 10000);
            var polc2 = z.AddPolchely("P2", 1000);
            var polc3 = z.AddPolchely("P3", 100);
            var tipus = new TermekTipus("T", 100, false);
            var t1 = new Termek(tipus, "ABC-00001", DateTime.UtcNow.AddDays(1));
            polc1.AddTermek(t1);
            var t2 = new Termek(tipus, "ABC-00002", DateTime.UtcNow.AddDays(1));
            polc1.AddTermek(t2);

            polc1.AthelyezCsoportos(polc2);
            polc2.AthelyezCsoportos(polc3);

            // Expect source to be empty after group move
            Assert.Empty(polc1.GetTermekek());
            Assert.False(polc3.GetTermekek().Any(), "A MaxSuly határ túl lett lépve");
            Assert.Contains(t1, polc2.GetTermekek());
            Assert.Contains(t2, polc2.GetTermekek());
            Assert.Empty(polc3.GetTermekek());
        }
       
        [Fact(DisplayName = "7. A robotok keresés-és-pakolás függvénye helyesen pakolja át a terméket")]
        public void Test_Robot_KeresEsPakol()
        {
            Raktar r = new Raktar("r");
            Zona z1 = new Zona("Z1", true);
            Zona z2 = new Zona("z2", false);
            r.AddZona(z1);
            r.AddZona(z2);

            var p1 = z1.AddPolchely("p1", 200);
            var p2 = z2.AddPolchely("p2", 200);

            var tipus = new TermekTipus("T", 100, false);
            var termek = new Termek(tipus, "ABC-12345", DateTime.UtcNow.AddDays(1));
            p1.AddTermek(termek);

            Robot robot = new Robot(r);
            robot.KeresEsPakol("ab", tipus, p2);

            Assert.Empty(p1.GetTermekek());
            Assert.Contains(termek, p2.GetTermekek());
        }

        [Fact(DisplayName = "8. A termékek lejárata helyesen van kezelve")]
        public void Test_Lejart()
        {
            var tipus = new TermekTipus("T", 100, false);
            var notExpired = new Termek(tipus, "ABC-00002", DateTime.UtcNow.AddDays(10));
            var expired = new Termek(tipus, "ABC-00003", DateTime.UtcNow.AddDays(-10));
            
            Assert.False(notExpired.Lejart(), "Nem lejárt lejártként jelenik meg");
            Assert.True(expired.Lejart(), "Lejárt termék nem lejártként jelenik meg");
        }


        [Fact(DisplayName = "9. A selejtezés minden lejárt terméket eltávolít")]
        public void Test_SelejtezLejart()
        {
            var raktar = new Raktar("R");
            var z = new Zona("Z", false);
            raktar.AddZona(z);
            var polc = z.AddPolchely("P", 10000);

            var tipus = new TermekTipus("T", 10, false);
            var expired1 = new Termek(tipus, "EEE-00001", DateTime.UtcNow.AddDays(-5));
            var expired2 = new Termek(tipus, "EEE-00002", DateTime.UtcNow.AddDays(-6));
            var good = new Termek(tipus, "EEE-00003", DateTime.UtcNow.AddDays(5));

            polc.AddTermek(expired1);
            polc.AddTermek(expired2);
            polc.AddTermek(good);

            var robot = new Robot(raktar);
            robot.Selejtez(tipus);

            Assert.DoesNotContain(expired1, polc.GetTermekek());
            Assert.DoesNotContain(expired2, polc.GetTermekek());
            Assert.Contains(good, polc.GetTermekek());
        }

        [Fact(DisplayName = "10. A polchelyeken a keresés megtalálja a terméket")]
        public void Test_Polchely_Keres()
        {
            Raktar r = new Raktar("r");
            Zona z1 = new Zona("Z1", true);
            r.AddZona(z1);

            var p1 = z1.AddPolchely("p1", 200);
            var p2 = z1.AddPolchely("p2", 200);

            var tipus = new TermekTipus("T", 100, false);
            var termek = new Termek(tipus, "ABC-12345", DateTime.UtcNow.AddDays(1));
            p1.AddTermek(termek);

            Assert.NotNull(p1.Kereses("bc"));
            Assert.Null(p1.Kereses("Cbc"));
            Assert.Null(p2.Kereses("bc"));
        }

        [Fact(DisplayName = "11. A raktárellenőrzés megtalálja a duplikátumokat")]
        public void Test_Raktar_Ellenoriz_DetectsDuplicates()
        {
            var raktar = new Raktar("R");
            var z = new Zona("Z", false);
            raktar.AddZona(z);
            var polc1 = z.AddPolchely("P1", 1000);
            var polc2 = z.AddPolchely("P2", 1000);

            var tipus = new TermekTipus("T", 10, false);
            var t = new Termek(tipus, "DDD-00001", DateTime.UtcNow.AddDays(1));
            polc1.AddTermek(t);

            polc2.AddTermek(t);

            Assert.False(raktar.Ellenoriz(), "A duplikált terméket nem találta meg az ellenőrzés");
        }

        [Fact(DisplayName = "12. Az EllenorizPolcSuly függvény ellenőrzi a polc súlyterhelését")]
        public void Test_EllenorizPolcSuly()
        {
            var raktar = new Raktar("R");
            var z = new Zona( "Z", false);
            raktar.AddZona(z);
            var polc = z.AddPolchely("P", 200); 

            var tipus = new TermekTipus("T", 150, false); 
            var t1 = new Termek(tipus, "ABC-00010", DateTime.UtcNow.AddDays(1));
            var t2 = new Termek(tipus, "ABC-00011", DateTime.UtcNow.AddDays(1));

            polc.AddTermek(t1);
            polc.AddTermek(t2);

            var robot = new Robot(raktar);
            
            Assert.False(polc.GetTermekek().Count==2, "Túlsúlyos polcot hoztunk létre");
            Assert.True(robot.EllenorizPolcSuly(z), "Túlsúlyos polcot nem vettünk észre");
        }

        [Fact(DisplayName = "13. Zóna - raktár kapcsolat")]
        public void Test_Zonak()
        {
            Raktar r = new Raktar("r1");
            r.AddZona(new Zona("z1", true));
            r.AddZona(new Zona("z2", true));
            r.AddZona(new Zona("z3", true));

            Raktar r2 = new Raktar("r2");
            Zona z4 = new Zona("z4", true);
            r2.AddZona(z4);

            r.RemoveZona(z4);
            r.RemoveZona(r.GetZonak().First());
            r.RemoveZona(r.GetZonak().First());

            Assert.True(r.GetZonak().Count >= 2, "Legalább 2 zóna kell egy raktárban");
            Assert.True(z4.SzuloRaktar == r2, "Nem vehetünk ki egy zónát egy raktárból, ha nincs benne");
        }
    }
}
