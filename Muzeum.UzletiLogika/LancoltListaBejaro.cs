using System.Collections;
using System.Collections.Generic;

namespace Muzeum.UzletiLogika
{
    public class LancoltListaBejaro : IEnumerator<IMuzeum>
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