using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    class MuzeumLancoltLista
    {
        MuzeumLancoltListaElem _fej;

        public MuzeumLancoltLista()
        {
            _fej = null;
        }

        public void BeszurasElejere(IMuzeum muzeum)
        {
            MuzeumLancoltListaElem uj = new MuzeumLancoltListaElem(muzeum, _fej, _fej.Kovetkezo);
        }
        public void BeszurasVegere(IMuzeum muzeum)
        {
            MuzeumLancoltListaElem p = _fej;

            while (p.Kovetkezo != null)
            {
                p = p.Kovetkezo;
            }

            p.Kovetkezo = new MuzeumLancoltListaElem(muzeum, p, null);
        }
    }

    public class MuzeumLancoltListaElem
    {
        public IMuzeum Muzeum { get; set; }
        public MuzeumLancoltListaElem Elozo { get; set; }
        public MuzeumLancoltListaElem Kovetkezo { get; set; }

        public MuzeumLancoltListaElem()
        {
        }

        public MuzeumLancoltListaElem(IMuzeum muzeum, MuzeumLancoltListaElem elozo, MuzeumLancoltListaElem kovezkezo)
        {
            Muzeum = muzeum;
            Elozo = elozo;
            Kovetkezo = kovezkezo;
        }
        
    }

}
