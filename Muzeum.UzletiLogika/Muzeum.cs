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
        MuzeumLancoltLista _hasonloMuzeumokLancoltLista;
        
        public int BaratokSzamaAkitErdekelAProgram { get; set; }
        public ErdekessegiSzint Erdekesseg { get; set; }
        
        public Muzeum(string nev)
        {
            _nev = nev;
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
    }
}
