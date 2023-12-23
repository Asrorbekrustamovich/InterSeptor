using InterseptorSample.data;
using InterseptorSample.ServiceLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterseptorSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register CrudService
            builder.Services.AddScoped<CrudService>();

            // Register AuditLogInterceptor
            builder.Services.AddScoped<ISaveChangesInterceptor, AuditLogInterceptor>();

            // Register DbContext
            builder.Services.AddDbContext<Mydbcontext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<Mydbcontext>((sp, options) =>
            {
                // Add all registered ISaveChangesInterceptor instances
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
