namespace Muzeum.UzletiLogika
{
    public class Csucs
    {
        public IMuzeum Muzeum { get; set; }

        public Csucs(IMuzeum muzeum)
        {
            Muzeum = muzeum;
        }
    }
}
