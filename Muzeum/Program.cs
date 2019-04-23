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
            var m1 = new HadtortenetiMuzeum("1");
            var m2 = new IparmuveszetiMuzeum("2");
            var m3 = new NeprajziMuzeum("3");
            var m4 = new SzepmuveszetiMuzeum("4");

            var lancoltLista = new MuzeumLancoltLista();
            lancoltLista.BeszurasElejere(m1);
            lancoltLista.BeszurasElejere(m2);
            lancoltLista.BeszurasVegere(m3);
            lancoltLista.BeszurasVegere(m4);


            Console.ReadKey();
        }
    }
}
