namespace Muzeum.UzletiLogika
{
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
