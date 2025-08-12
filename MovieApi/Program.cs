using Data.Contexts;
using Data.Helpers;
using Microsoft.EntityFrameworkCore;
using MovieApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            context.Database.Migrate();

            var loader = scope.ServiceProvider.GetRequiredService<ICsvLoader>();
            await loader.LoadMoviesAsync("db/movies.csv");

        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("https://localhost:8080");
            });
}
