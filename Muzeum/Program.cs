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



            var graf = new Graf();
            graf.CsucsHozzaadas(csucs1);
            graf.CsucsHozzaadas(csucs2);
            graf.CsucsHozzaadas(csucs3);
            graf.CsucsHozzaadas(csucs4);



            graf.CsucsTorles(csucs4);
            graf.CsucsTorles(csucs4);





            //m1.HasonloMuzeumokElejereBeszur(m2);
            //m1.HasonloMuzeumokElejereBeszur(m3);
            //m1.HasonloMuzeumokElejereBeszur(m4);

            //foreach (var muz in m1.HasonloMuzeumokLancoltLista)
            //{
            //    Console.WriteLine(muz.ToString());
            //}



            //var rendezettTomb = m1.HasonloMuzeumokLancoltLista.MuzeumokatTombbeRendez();

            Console.ReadKey();
        }
    }
}
