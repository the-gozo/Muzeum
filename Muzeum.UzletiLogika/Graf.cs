using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public class Graf
    {
        public List<Csucs> Csucsok { get; set; }
        public List<El> Elek { get; set; }

        public Graf()
        {
            Csucsok = new List<Csucs>();
            Elek = new List<El>();
        }

        public bool VezetEl(Csucs hova)
        {
            return Elek.Any(el =>el.Hova == hova);
        }

        public IEnumerable<Csucs> Szomszedok(Csucs csucs)
        {
            //todo implementálni
            return new List<Csucs>();
        }

        public void CsucsHozzaadas(Csucs csucs)
        {
            if (csucs != null && !Csucsok.Contains(csucs))
            {
                Csucsok.Add(csucs);
            }
            else
            {
                Console.WriteLine($"A paraméter null vagy már szerepel a gráfban: {nameof(csucs)}");
            }
        }

        public void CsucsTorles(Csucs csucs)
        {
            if (csucs != null && Csucsok.Contains(csucs))
            {
                Csucsok.Remove(csucs);
                Elek.RemoveAll(el => el.Honnan == csucs || el.Hova == csucs);
            }
            else
            {
                Console.WriteLine($"A paraméter null vagy nincs ilyen csúcs a gráfban: {nameof(csucs)}");
            }
        }



    }

    public class Csucs
    {
        public IMuzeum Muzeum { get; set; }
    }

    public class El
    {
        public Csucs Honnan { get; set; }
        public Csucs Hova { get; set; }
        public float Tavolsag { get; set; }

        public El(Csucs honnan, Csucs hova, float tavolsag)
        {
            Honnan = honnan;
            Hova = hova;
            Tavolsag = tavolsag;
        }
    }
}
