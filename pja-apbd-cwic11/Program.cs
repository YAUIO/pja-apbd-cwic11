using Microsoft.EntityFrameworkCore;
using pja_apbd_cwic11.Data;
using pja_apbd_cwic11.Services;

namespace pja_apbd_cwic11;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddOpenApi();

        builder.Services.AddControllers();

        builder.Services.AddScoped<IDbService, DbService>();

        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("Default")
            );
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.MapControllers();

        app.UseAuthorization();

        app.Run();
    }
}