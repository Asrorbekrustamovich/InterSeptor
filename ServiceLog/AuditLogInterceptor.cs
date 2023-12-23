using InterseptorSample.data;
using InterseptorSample.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace InterseptorSample.ServiceLog
{
    public class AuditLogInterceptor : SaveChangesInterceptor
    {

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            Console.WriteLine("SavingChanges Interceptor Executed");
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }


        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("SavingChangesAsync Interceptor Executed");
            await UpdateEntitiesAsync(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }



        private void UpdateEntities(DbContext context)
        {
            if (context == null)
                return;

            var auditLogsToAdd = new List<AuditLog>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted ||
                    entry.State == EntityState.Added)
                {
                    var auditlog = new AuditLog()
                    {
                        EntityName = entry.Entity.GetType().Name,
                        Date = DateTime.Now,
                        OperationType = entry.State,
                        UpdatedValueAsJson = entry.OriginalValues.ToObject().ToString(),
                        UserName = "Asror"
                    };

                    auditLogsToAdd.Add(auditlog);
                }
            }

            // Add the audit logs after the loop
            context.AddRange(auditLogsToAdd);
        }


        private async ValueTask UpdateEntitiesAsync(DbContext context)
        {
            if (context == null)
                return;

            var auditLogsToAdd = new List<AuditLog>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted ||
                    entry.State == EntityState.Added)
                {
                    var auditlog = new AuditLog()
                    {
                        EntityName = entry.Entity.GetType().Name,
                        Date = DateTime.Now,
                        OperationType = entry.State,
                        UpdatedValueAsJson = entry.OriginalValues.ToObject().ToString(),
                        UserName = "Asror"
                    };

                    auditLogsToAdd.Add(auditlog);
                }
            }

            // Add the audit logs after the loop
            context.AddRange(auditLogsToAdd);
        }
    }
}
