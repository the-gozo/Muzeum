using System;

namespace Muzeum.UzletiLogika
{
    public class MuzeumLancoltListaElem 
    {
        public IMuzeum Muzeum { get; set; }
        public MuzeumLancoltListaElem Elozo { get; set; }
        public MuzeumLancoltListaElem Kovetkezo { get; set; }

        public MuzeumLancoltListaElem()
        {
            Muzeum = default(IMuzeum);
            Elozo = null;
            Kovetkezo = null;
        }

        public MuzeumLancoltListaElem(IMuzeum muzeum, MuzeumLancoltListaElem kovetkezo, MuzeumLancoltListaElem elozo)
        {
            Muzeum = muzeum;
            Kovetkezo = kovetkezo;
            Elozo = elozo;
        }
    }
}