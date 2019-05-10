using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public class MuzeumLancoltLista: IEnumerable<IMuzeum>
    {
        public MuzeumLancoltListaElem Fej { get; }

        public MuzeumLancoltLista()
        {
            Fej = new MuzeumLancoltListaElem();
        }

        public void BeszurasElejere(IMuzeum muzeum)
        {
            MuzeumLancoltListaElem uj = new MuzeumLancoltListaElem(muzeum, Fej.Kovetkezo, null);
           
            if (Fej.Kovetkezo != null) Fej.Kovetkezo.Elozo = uj;
            
            Fej.Kovetkezo = uj;
        }

        public void BeszurasVegere(IMuzeum muzeum)
        {
            MuzeumLancoltListaElem p = Fej;

            while (p.Kovetkezo != null)
            {
                p = p.Kovetkezo;
            }

            p.Kovetkezo = new MuzeumLancoltListaElem(muzeum, null, p);
        }

        public void Torles(IMuzeum muzeum)
        {
            MuzeumLancoltListaElem p = Fej;

            while (p.Kovetkezo != null && p.Muzeum != muzeum)
            {
                p = p.Kovetkezo;
            }

            if (p.Muzeum == muzeum)
            {
                p.Elozo.Kovetkezo = p.Kovetkezo;
                p = null;
            }
            else
            {
                throw new ArgumentException("Nincs ilyen múzeum a hasonló múzeumok között");
            }

        }

        public IMuzeum[] MuzeumokatTombbeRendez()
        {
            List<IMuzeum> muzeumLista = new List<IMuzeum>();

            foreach (var muzeum in this)
            {
                muzeumLista.Add(muzeum);
            }

            muzeumLista.Sort();

            return muzeumLista.ToArray();
        }


        public IEnumerator<IMuzeum> GetEnumerator()
        {
            return new LancoltListaBejaro(Fej);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
