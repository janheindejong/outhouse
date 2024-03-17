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

            // Apply migrations; when we go live this will need to be taken out for sure! 
            using (IServiceScope scope = app.Services.CreateScope())
            {
                ApplicationDbContext dataContext = scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();
                dataContext.Database.Migrate();
            }

            // Configure SPA front-end
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Add endpoints
            app.MapGroup("/api").MapIdentityApi<User>();
            app.MapControllers();

            // Enable Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
