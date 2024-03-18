using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Domain;

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
            
            // Configure SPA front-end
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Add endpoints
            app.MapGroup("/api").MapIdentityApi<User>();
            app.MapControllers();

            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger
                app.UseSwagger();
                app.UseSwaggerUI();

                // Seed development database
                using IServiceScope scope = app.Services.CreateScope();
                ApplicationDbContext dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dataContext.Database.EnsureCreated();
            }

            // Do some more stuff...
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapFallbackToFile("/index.html");

            // Run, Forest!
            app.Run();
        }
    }
}
