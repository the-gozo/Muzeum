using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Muzeum.UzletiLogika;
using Muzeum.UzletiLogika.Muzeumok;

namespace Muzeum
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var m1 = new HadtortenetiMuzeum();
                var csucs1 = new Csucs(m1);

                var m2 = new IparmuveszetiMuzeum();
                var csucs2 = new Csucs(m2);

                var m3 = new NeprajziMuzeum();
                var csucs3 = new Csucs(m3);

                var m4 = new SzepmuveszetiMuzeum();
                var csucs4 = new Csucs(m4);

                var m5 = new Babamuzeum();
                var csucs5 = new Csucs(m5);

                m1.HasonloMuzeumokElejereBeszur(m2);
                m1.HasonloMuzeumokElejereBeszur(m3);
                m1.HasonloMuzeumokElejereBeszur(m4);

                var hasonlo = m1.HasonloMuzeumok();

                m1.HasonloMuzeumTorol(m3);

                var hasonlo2 = m1.HasonloMuzeumok();


                var graf = new Graf();
                graf.CsucsHozzaad(csucs1);
                graf.CsucsHozzaad(csucs2);
                graf.CsucsHozzaad(csucs3);
                graf.CsucsHozzaad(csucs4);
                graf.CsucsHozzaad(csucs5);

                var el1_2 = new El(csucs1, csucs2, 10);
                var el1_3 = new El(csucs1, csucs3, 1);
                var el1_4 = new El(csucs1, csucs4, 1);
                var el2_3 = new El(csucs3, csucs2, 2);
                var el2_4 = new El(csucs2, csucs2, 4);
                var el3_4 = new El(csucs3, csucs4, 1);
                var el4_5 = new El(csucs4, csucs5, 1);

                graf.ElHozzaad(el1_2);
                graf.ElHozzaad(el1_3);
                graf.ElHozzaad(el1_4);
                graf.ElHozzaad(el2_3);
                graf.ElHozzaad(el2_4);
                graf.ElHozzaad(el3_4);
                graf.ElHozzaad(el4_5);

                var utvonalGraf = graf.UtvonalGraf(csucs1, csucs5, new[] { csucs3 });

                graf.EleketFrissit(RandomEl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        public static float RandomEl(Csucs honnan, Csucs hova, List<El> elek)
        {
            var el = elek.FirstOrDefault(e => e.Honnan == honnan && e.Hova == hova);
            if (el == null)
            {
                throw new ArgumentException("Random élbeállítás hiba. Nincs él a két csúcs között");
            }

            var rdn = new Random();
            el.Tavolsag = rdn.Next(1, 11);
            return el.Tavolsag;
        }
    }
}
