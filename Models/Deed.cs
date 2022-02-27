namespace cemetery.Models
{
    public class Deed
    {
        public int id { get; set; }

        public int SectionId { get; set; }

        public string Lot { get; set; } = "";

        public string LastName1 { get; set; } = "";

        public string FirstName1 { get; set; } = "";

        public string MiddleName1 { get; set; } = "";

        public string LastName2 { get; set; } = "";

        public string FirstName2 { get; set; } = "";

        public string MiddleName2 { get; set; } = "";

        public DateTime IssueDate { get; set; }

        public string Notes { get; set; } = "";

        public int CemeteryId { get; set; }

        public Cemetery? Cemetery { get; set; }

        public Section? Section { get;set; }

    }
}