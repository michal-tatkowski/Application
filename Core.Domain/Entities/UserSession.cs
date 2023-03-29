namespace Core.Domain.Entities
{
    public partial class UserSession
    {
        public int UserId { get; set; }
        public bool IsLogged { get; set; }
        public DateTime? LastActivity { get; set; }
        public virtual User User { get; set; }
    }
}
