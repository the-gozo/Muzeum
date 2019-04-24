using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public abstract class Muzeum : IMuzeum
    {
        string _nev;
        int _baratokSzamaAkitErdekelAProgram;
        ErdekessegiSzint _erdekessegiSzint;
        MuzeumLancoltLista _hasonloMuzeumokLancoltLista;

        public int BaratokSzamaAkitErdekelAProgram
        {
            get => _baratokSzamaAkitErdekelAProgram;
            set => _baratokSzamaAkitErdekelAProgram = value;
        }

        public ErdekessegiSzint Erdekesseg
        {
            get => _erdekessegiSzint; 
            set=> _erdekessegiSzint = value;
        }
        public MuzeumLancoltLista HasonloMuzeumokLancoltLista
        {
            get => _hasonloMuzeumokLancoltLista;
        }
        
        public Muzeum(string nev, ErdekessegiSzint erdekessegiSzint)
        {
            _nev = nev;
            _erdekessegiSzint = erdekessegiSzint;
            _hasonloMuzeumokLancoltLista = new MuzeumLancoltLista();
        }

        public IMuzeum[] HasonloMuzeumok()
        {
            throw new NotImplementedException();
        }

        public void HasonloMuzeumokElejereBeszur(IMuzeum muzeum)
        {
            _hasonloMuzeumokLancoltLista.BeszurasElejere(muzeum);
        }

        public void HasonloMuzeumokvegereeBeszur(IMuzeum muzeum)
        {
            _hasonloMuzeumokLancoltLista.BeszurasVegere(muzeum);
        }

        public override string ToString()
        {
            return _nev;
        }

        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case IMuzeum elem when elem.Erdekesseg > Erdekesseg:
                    return 1;
                case IMuzeum elem when elem.Erdekesseg < Erdekesseg:
                    return -1;
                default: return 0;
            }
        }
    }
}
