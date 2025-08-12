using Data.Contexts;
using Data.Definitions;
using Data.Helpers;
using Data.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DataExtension
    {
        public static IServiceCollection RegisterData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<ICsvLoader, CsvLoader>();

            services.AddDbContext<Context>(options =>
                options.UseSqlite("Data Source=prueba.db"));

            services.AddTransient<IContext, Context>();

            return services;
        }
    }
}

