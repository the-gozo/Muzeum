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

        public void CsucsHozzaad(Csucs csucs)
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

        public void CsucsTorol(Csucs csucs)
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

        public void ElHozzaad(El el)
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

        public void ElTorol(El el)
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
            var csucs1 = Csucsok[0];
            var csucs2 = Csucsok[4];
            deleg(csucs1, csucs2, Elek);
        }

        public Graf UtvonalGraf(Csucs honnan, Csucs hova, Csucs[] allomasok)
        {
            if (!VezetElInnen(honnan))
            {
                throw new ArgumentException("Az indulási múzeumból nem lehet máshová eljutni");
            }

            if (!VezetElIde(hova))
            {
                throw new ArgumentException("A cél múzeumba nem lehet eljutni, mert nincs odavezető él");
            }

            var megnezendoMuzeumok = new List<Csucs>();
            megnezendoMuzeumok.Add(honnan);
            megnezendoMuzeumok.AddRange(allomasok);
            megnezendoMuzeumok.Add(hova);

            bool vegcelElerheto = true;
            var dijkstraMindenKivalasztottMuzeumhoz = new List<Dictionary<int, (Csucs csucs, float tavolsagStarttol, Csucs honnanJottem)>>();

            //megnézzük, hogy minden kiválasztott múzeumból elérhető-e a sorban következő
            for (int i = 0; i <= megnezendoMuzeumok.Count-2 && vegcelElerheto; i++)
            {
                var aktualis = megnezendoMuzeumok[i];
                var kovetkezo = megnezendoMuzeumok[i+1];

                var dijkstraAktualisMuzeumhoz = DijkstraElemek(aktualis);
                dijkstraMindenKivalasztottMuzeumhoz.Add(dijkstraAktualisMuzeumhoz);
                vegcelElerheto = dijkstraAktualisMuzeumhoz.Any(rekord => rekord.Value.csucs == kovetkezo && rekord.Value.honnanJottem!=null);
                if (i == megnezendoMuzeumok.Count - 2)
                {
                    dijkstraMindenKivalasztottMuzeumhoz.Add(DijkstraElemek(kovetkezo));
                }
            }

            if (vegcelElerheto)
            {
                var graf = new Graf();

                List<Csucs> csucsok = new List<Csucs>();
                List<El> elek = new List<El>();

                for (int i = 0; i <= megnezendoMuzeumok.Count - 2; i++)
                {
                    var visszafejtes = UtatVisszafejt(megnezendoMuzeumok[i + 1], megnezendoMuzeumok[i]);
                    csucsok.AddRange(visszafejtes.csucsok);
                    elek.AddRange(visszafejtes.elek);
                }

                graf.Csucsok.AddRange(csucsok.Distinct());
                graf.Elek.AddRange(elek.Distinct());

                return graf; 
            }

            throw new ArgumentException("A megadott útvonalon nem lehet eljutni a célba");

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

                var szomszedok = Csucsok.Where(csucs => Elek.Any(el => GetEl(kivalasztottLegkisebb.csucs, csucs) != null)).ToList();

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

        private (List<Csucs> csucsok, List<El> elek)  UtatVisszafejt(Csucs vegpont, Csucs kezdopont)
        {
            var dijkstra = DijkstraElemek(kezdopont);
            var cel = dijkstra.SingleOrDefault(rekord => rekord.Value.csucs == vegpont);

            var csucsok = new List<Csucs>();
            var elek = new List<El>();

            while (cel.Value.honnanJottem!=null)
            {
                csucsok.Add(cel.Value.csucs);
                cel = dijkstra.SingleOrDefault(rekord => rekord.Value.csucs == cel.Value.honnanJottem);
            }

            csucsok.Add(dijkstra.SingleOrDefault(rekord => rekord.Value.tavolsagStarttol == 0).Value.csucs);
            csucsok.Reverse();
            for (int i = 0; i <= csucsok.Count-2; i++)
            {
                elek.Add(Elek.SingleOrDefault(el => el.Honnan == csucsok[i] && el.Hova == csucsok[i + 1]));
            }

            return (csucsok, elek);
        }

        private bool VezetElIde(Csucs csucs) => Elek.Any(el => el.Hova == csucs);

        private bool VezetElInnen(Csucs csucs) => Elek.Any(el => el.Honnan == csucs);

        private El GetEl(Csucs honnan, Csucs hova)
        {
            return Elek.SingleOrDefault(el => el.Honnan == honnan && el.Hova == hova);
        }
    }

    public delegate float EleketFrissit(Csucs honnan, Csucs hova, List<El> elek);
}
