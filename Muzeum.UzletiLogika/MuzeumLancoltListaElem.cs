using System;

namespace Muzeum.UzletiLogika
{
    public class MuzeumLancoltListaElem : IComparable
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

        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case MuzeumLancoltListaElem elem when elem.Muzeum.Erdekesseg < Muzeum.Erdekesseg:
                    return 1;
                case MuzeumLancoltListaElem elem when elem.Muzeum.Erdekesseg > Muzeum.Erdekesseg:
                    return -1;
                default: return 0;
            }
        }
    }
}