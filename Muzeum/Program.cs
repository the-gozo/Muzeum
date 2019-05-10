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



            var graf = new Graf();
            graf.CsucsHozzaadas(csucs1);
            graf.CsucsHozzaadas(csucs2);
            graf.CsucsHozzaadas(csucs3);
            graf.CsucsHozzaadas(csucs4);
            graf.CsucsHozzaadas(csucs5);

            var el1_2 = new El(csucs1, csucs2, 1);
            var el1_3 = new El(csucs1, csucs3, 4);
            var el1_4 = new El(csucs1, csucs4, 6);

            var el2_3 = new El(csucs2, csucs3, 2);
            var el2_4 = new El(csucs2, csucs2, 4);

            var el3_4 = new El(csucs3, csucs4, 1);

            var el4_5 = new El(csucs4, csucs5, 1);



            graf.ElHozzaadas(el1_2);
            graf.ElHozzaadas(el1_3);
            //graf.ElHozzaadas(el1_4);
            graf.ElHozzaadas(el2_3);
            //graf.ElHozzaadas(el2_4);
            //graf.ElHozzaadas(el3_4);
            graf.ElHozzaadas(el4_5);


            var dij = graf.UtatSzamol(csucs1, csucs5, new[]{csucs2});

            Console.ReadKey();

            //graf.CsucsTorles(csucs4);
            //graf.CsucsTorles(csucs4);





            //m1.HasonloMuzeumokElejereBeszur(m2);
            //m1.HasonloMuzeumokElejereBeszur(m3);
            //m1.HasonloMuzeumokElejereBeszur(m4);

            //foreach (var muz in m1.HasonloMuzeumokLancoltLista)
            //{
            //    Console.WriteLine(muz.ToString());
            //}



            //var rendezettTomb = m1.HasonloMuzeumokLancoltLista.MuzeumokatTombbeRendez();

        }
    }
}
