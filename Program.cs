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
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            // Register AuditLogInterceptor
            builder.Services.AddScoped<ISaveChangesInterceptor, AuditLogInterceptor>();
            // Register CrudService
            builder.Services.AddScoped<CrudService>();
            builder.Services.AddDbContext<Mydbcontext>((sp, option) => {
                option.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

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
