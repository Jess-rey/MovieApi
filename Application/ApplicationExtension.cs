using Application.Definitions;
using Application.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data;

namespace Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IMovieService, MovieService>();

            services.RegisterData(configuration);


            return services;
        }

    }
}
