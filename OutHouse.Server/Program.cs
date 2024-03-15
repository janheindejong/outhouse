using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.DataAccess;
using OutHouse.Server.Domain;

namespace OutHouse.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("OuthouseDb")));

            // Add Identity Management 
            builder.Services
                .AddAuthorization()
                .AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            // Azure Logging; this is necessary for logs to show up in the Azure App Service
            builder.Logging.AddAzureWebAppDiagnostics();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Add Identity routes
            app.MapIdentityApi<User>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
