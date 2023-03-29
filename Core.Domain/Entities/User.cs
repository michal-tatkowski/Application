namespace Core.Domain.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
