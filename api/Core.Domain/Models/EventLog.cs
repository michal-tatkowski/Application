namespace Core.Domain.Models
{
    public class ApiEventLog
    {
        public string Message { get; set; }
        public int PermissionTypeEnum { get; set; }
        public int EventLogInformationTypeEnum { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
