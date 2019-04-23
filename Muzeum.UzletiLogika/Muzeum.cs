using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public abstract class Muzeum : IMuzeum
    {
        public string Nev { get; }
        MuzeumLancoltLista _hasonloMuzeumokLancoltLista;
        
        public int BaratokSzamaAkitErdekelAProgram { get; set; }
        public ErdekessegiSzint Erdekesseg { get; set; }
        public MuzeumLancoltLista HasonloMuzeumokLancoltLista
        {
            get => _hasonloMuzeumokLancoltLista;
            set => _hasonloMuzeumokLancoltLista = value;
        }
        
        public Muzeum(string nev)
        {
            Nev = nev;
        }

        public IMuzeum[] HasonloMuzeumok()
        {
       

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Nev;
        }
    }
}
