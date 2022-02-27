namespace cemetery.Models
{
    public class User
    {
        public int id { get; set; }

        public int PersonId { get; set; }

        public string UserName { get; set; } = "";

        public string Password { get; set; } = "";

        public string AuthenticationMethod { get; set; } = "";

        public Person? Person { get; set; }

        public List<Role>? Roles { get; set; }

    }
}