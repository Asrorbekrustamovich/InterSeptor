using Microsoft.EntityFrameworkCore;

namespace InterseptorSample.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public DateTime Date { get; set; }
        public EntityState? OperationType { get; set; }
        public string UpdatedValueAsJson { get; set; }
        public string UserName { get; set; }

    }
}
