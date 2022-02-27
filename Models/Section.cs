namespace cemetery.Models
{
    public class Section
    {
        public int id { get; set; }

        public int CemeteryId { get; set; }

        public string Code { get; set; } = "";

        public string Name { get; set; } = "";

        public Cemetery? Cemetery { get; set; }
    }
}