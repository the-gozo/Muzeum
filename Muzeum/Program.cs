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

            var lancoltLista = new MuzeumLancoltLista();
            lancoltLista.BeszurasElejere(m1);
            lancoltLista.BeszurasElejere(m2);
            lancoltLista.BeszurasVegere(m3);
            lancoltLista.BeszurasVegere(m4);

            foreach (var muz in lancoltLista)
            {
                Console.WriteLine(muz.ToString());
            }


            Console.ReadKey();
        }
    }
}
