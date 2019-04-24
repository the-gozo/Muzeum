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
            var m2 = new IparmuveszetiMuzeum();
            var m3 = new NeprajziMuzeum();
            var m4 = new SzepmuveszetiMuzeum();

            m1.HasonloMuzeumokElejereBeszur(m2);
            m1.HasonloMuzeumokElejereBeszur(m3);
            m1.HasonloMuzeumokElejereBeszur(m4);

            foreach (var muz in m1.HasonloMuzeumokLancoltLista)
            {
                Console.WriteLine(muz.ToString());
            }



            var rendezettTomb = m1.HasonloMuzeumokLancoltLista.MuzeumokatTombbeRendez();

            Console.ReadKey();
        }
    }
}
