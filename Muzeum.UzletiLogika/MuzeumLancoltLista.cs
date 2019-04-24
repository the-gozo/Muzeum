using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public class MuzeumLancoltLista: IEnumerable
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

        public IEnumerator GetEnumerator()
        {
            return new LancoltListaBejaro(Fej);
        }
    }

    public class LancoltListaBejaro  : IEnumerator<IMuzeum>
    {
        MuzeumLancoltListaElem _elsoElem;
        MuzeumLancoltListaElem _elsoElottiElem;

        public LancoltListaBejaro(MuzeumLancoltListaElem elsoElem)
        {
            _elsoElem = elsoElem;
        }

        public void Dispose()
        {
            _elsoElem = null;
            _elsoElottiElem = null;
        }

        public bool MoveNext()
        {
            if (_elsoElem == null)
            {
                _elsoElem = _elsoElottiElem;
            }
            else
            {
                _elsoElem = _elsoElem.Kovetkezo;
            }

            return _elsoElem != null;
        }

        public void Reset()
        {
            _elsoElem = null;
        }

        public IMuzeum Current { get => _elsoElem.Muzeum ; }

        object IEnumerator.Current => Current;
    }
}
