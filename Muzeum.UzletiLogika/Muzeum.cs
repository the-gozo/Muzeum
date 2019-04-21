using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public abstract class Muzeum : IMuzeum
    {
        MuzeumLancoltLista _hasonloMuzeumok;

        public int BaratokSzamaAkitErdekelAProgram { get; set; }
        public ErdekessegiSzint Erdekesseg { get; set; }
        public IMuzeum[] HasonloMuzeumok()
        {
            throw new NotImplementedException();
        }
    }
}
