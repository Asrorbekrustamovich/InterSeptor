using InterseptorSample.data;
using InterseptorSample.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace InterseptorSample.ServiceLog
{
    public class AuditLogInterceptor : SaveChangesInterceptor
    {
        private readonly Mydbcontext _context;

        public AuditLogInterceptor(Mydbcontext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            Console.WriteLine("SavingChanges Interceptor Executed");
            UpdateEntities(_context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("SavingChangesAsync Interceptor Executed");
            UpdateEntitiesAsync(_context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        private void UpdateEntities(Mydbcontext context)
        {
            if (context == null)
                return;

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

                    context.AuditLogs.Add(auditlog);
                }
            }
        }

        private async ValueTask UpdateEntitiesAsync(Mydbcontext context)
        {
            if (context == null)
                return;

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

                    context.AuditLogs.Add(auditlog);
                }
            }
        }
    }
}
