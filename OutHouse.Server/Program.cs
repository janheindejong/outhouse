using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Domain;
using Microsoft.AspNetCore.Identity;

namespace OutHouse.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

            // Build app
            WebApplication app = builder.Build();

            // Serve front-end
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Add Identity routes
            app.MapGroup("/api").MapIdentityApi<User>();

            // Enable Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            // Run, Forest!
            app.Run();
        }
    }
}
