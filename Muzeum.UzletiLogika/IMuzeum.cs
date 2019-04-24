using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public interface IMuzeum: IComparable
    {
        int BaratokSzamaAkitErdekelAProgram { get; set; }
        ErdekessegiSzint Erdekesseg { get; set; }

        IMuzeum[] HasonloMuzeumok();
    }
}
