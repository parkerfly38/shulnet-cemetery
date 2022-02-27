namespace cemetery.Models
{
    public class Internment
    {
        public int id { get; set; }

        public int SectionId { get; set; }

        public string Lot { get; set; } = "";

        public string Book { get; set; } = "";

        public int? PageNumber { get; set; }

        public DateTime? DeceasedDate { get; set; }

        public string FirstName { get; set; } = "";
        
        public string LastName { get; set; } = "";

        public string MiddleName { get; set; } = "";
        
        public string BirthPlace { get; set; } = "";

        public string LastResidence { get; set; } = "";

        public int? Age { get; set; }

        public string Sex { get; set; } = "";

        public int CemeteryId { get; set; }

        public string Notes { get; set; } = "";

        public string Lot2 { get; set; } = "";

        public Section? Section { get; set; }

        public Cemetery? Cemetery { get; set; }
    }
}