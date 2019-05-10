using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muzeum.UzletiLogika
{
    public class Graf
    {
        private List<Csucs> Csucsok { get; }
        private List<El> Elek { get; }
        public MuzeumLancoltLista NagyonJoMuzeumLancoltLista { get; set; }

        public Graf()
        {
            Csucsok = new List<Csucs>();
            Elek = new List<El>();
            NagyonJoMuzeumLancoltLista = new MuzeumLancoltLista();
        }

        private bool VanElKözte(Csucs honnan, Csucs hova)
        {
            return Elek.Any(el => el.Honnan == honnan && el.Hova == hova);
        }

        private bool VezetElIde(Csucs csucs) => Elek.Any(el => el.Hova == csucs);
        private bool VezetElIde(Csucs ide, Csucs innen, Dictionary<int, (Csucs csucs, float tavolsagStarttol, Csucs honnanJottem)> dijkstra)
        {
            bool vezetel = false;

            var innenRekord = dijkstra[innen.GetHashCode()];

            if (innenRekord.honnanJottem!=null)
            {
                if (innenRekord.honnanJottem == ide)
                {
                    vezetel = true;
                }
                else
                {
                    vezetel = VezetElIde(ide, innenRekord.honnanJottem, dijkstra);
                }
            }

            return vezetel;
        }
        private bool VezetElInnen(Csucs csucs) => Elek.Any(el => el.Honnan == csucs);

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
                if (csucs.Muzeum.Erdekesseg == ErdekessegiSzint.NagyonJo)
                {
                    NagyonJoMuzeumLancoltLista.BeszurasElejere(csucs.Muzeum);
                }
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
                if (csucs.Muzeum.Erdekesseg == ErdekessegiSzint.NagyonJo && NagyonJoMuzeumLancoltLista.Contains(csucs.Muzeum))
                {
                    NagyonJoMuzeumLancoltLista.Torles(csucs.Muzeum);
                }

            }
            else
            {
                Console.WriteLine($"A paraméter null vagy nincs ilyen csúcs a gráfban: {nameof(csucs)}");
            }
        }

        public void ElHozzaadas(El el)
        {
            if (!Elek.Any(e => (el.Hova == e.Hova && el.Honnan == e.Honnan) || (el.Honnan == e.Hova && el.Hova == e.Honnan)))
            {
                Elek.Add(el);
            }
            else
            {
                Console.WriteLine($"Már van él a két múzeum között: {el.Honnan}, {el.Hova}");
            }
        }

        public void ElTorles(El el)
        {
            if (el != null && Elek.Contains(el))
            {
                Elek.Remove(el);
            }
            else
            {
                Console.WriteLine($"A paraméter null vagy nincs ilyen él a gráfban: {nameof(el)}");
            }
        }

        public void EleketFrissit(EleketFrissit deleg)
        {
            Elek.ForEach(el => deleg(el.Honnan, el.Hova)); ;
        }

        private Dictionary<int, (Csucs csucs, float tavolsagStarttol, Csucs honnanJottem)> DijkstraElemek(Csucs start)
        {
            var dijkstraAdatszerkezet = new Dictionary<int, (Csucs csucs, float tavolsagStarttol, Csucs honnanJottem)>();
            var csucsPrioritasosSor = new Dictionary<int, Csucs>();

            Csucsok.ForEach(csucs =>
            {
                dijkstraAdatszerkezet.Add(csucs.GetHashCode(), (csucs, int.MaxValue, null));
                csucsPrioritasosSor.Add(csucs.GetHashCode(), csucs);
            });

            var kiindulo = dijkstraAdatszerkezet[start.GetHashCode()];
            kiindulo.tavolsagStarttol = 0;
            dijkstraAdatszerkezet[start.GetHashCode()] = kiindulo;

            while (csucsPrioritasosSor.Any())
            {
                var vizsgalandoMaradek = dijkstraAdatszerkezet
                    .Where(rekord => csucsPrioritasosSor.ContainsValue(rekord.Value.csucs));
                var kivalasztottLegkisebb = vizsgalandoMaradek
                    .FirstOrDefault(rekord => rekord.Value.tavolsagStarttol == vizsgalandoMaradek.Min(elem => elem.Value.tavolsagStarttol))
                    .Value;

                csucsPrioritasosSor.Remove(kivalasztottLegkisebb.csucs.GetHashCode());

                var szomszedok = Csucsok.Where(csucs => Elek.Any(el => VanElKözte(kivalasztottLegkisebb.csucs, csucs))).ToList();

                foreach (var szomszedCsucs in szomszedok)
                {
                    var ossztavolsag = dijkstraAdatszerkezet[kivalasztottLegkisebb.csucs.GetHashCode()].tavolsagStarttol +
                    GetEl(kivalasztottLegkisebb.csucs, szomszedCsucs).Tavolsag;

                    if (
                    ossztavolsag <
                    dijkstraAdatszerkezet[szomszedCsucs.GetHashCode()].tavolsagStarttol
                    )
                    {
                        var szomszed = dijkstraAdatszerkezet[szomszedCsucs.GetHashCode()];
                        szomszed.tavolsagStarttol = ossztavolsag;
                        szomszed.honnanJottem = kivalasztottLegkisebb.csucs;
                        dijkstraAdatszerkezet[szomszedCsucs.GetHashCode()] = szomszed;
                    }
                }

            }

            return dijkstraAdatszerkezet;
        }

        public Graf UtatSzamol(Csucs honnan, Csucs hova, Csucs[] allomasok)
        {
            if (!VezetElInnen(honnan))
            {
                throw new ArgumentException("Az indulási múzeumból nem lehet máshová eljutni");
            }

            if (!VezetElIde(hova))
            {
                throw new ArgumentException("A cél múzeumba nem lehet eljutni, mert nincs odavezető él");
            }

            var dijsktraKEzdo = DijkstraElemek(honnan);

            var honnanBenneVan = dijsktraKEzdo.Any(rekord => rekord.Value.csucs == honnan);
            var hovaBenneVan = dijsktraKEzdo.Any(rekord => rekord.Value.csucs == hova);
            var allomasokBenneVannak = allomasok.All(allomas => VezetElIde(allomas, hova, dijsktraKEzdo));

            if (honnanBenneVan && hovaBenneVan && allomasokBenneVannak)
            {
                return new Graf();
            }

            throw new ArgumentException("A megadott útvonalon nem lehet eljutni a célba");

        }

        private El GetEl(Csucs honnan, Csucs hova)
        {
            return Elek.SingleOrDefault(el => el.Honnan == honnan && el.Hova == hova);
        }
    }

    public delegate void EleketFrissit(Csucs honnan, Csucs hova);

    public class Csucs
    {
        public IMuzeum Muzeum { get; set; }

        public Csucs(IMuzeum muzeum)
        {
            Muzeum = muzeum;
        }
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
