namespace cemetery.Models
{
    public class Person
    {
        public int id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}