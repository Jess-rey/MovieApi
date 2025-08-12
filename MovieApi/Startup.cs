using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Data.Helpers;
using Microsoft.OpenApi.Models;

namespace MovieApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Movie API",
                    Version = "v1"
                });
            });

            services.RegisterApplication(Configuration);


        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
            }
            else
            {

                app.UseExceptionHandler(config =>
                {
                    config.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionFeature != null)
                        {
                            var err = exceptionFeature.Error;

                            var result = System.Text.Json.JsonSerializer.Serialize(new
                            {
                                Status = 500,
                                Message = "Ha ocurrido un error inesperado",
                                Detail = err.Message
                            });

                            await context.Response.WriteAsync(result);
                        }
                    });
                });


                app.UseHsts();
            }



            loggerFactory.AddFile("logs/log_{Date}.txt");
            app.UseHttpsRedirection();

            //app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseSwagger(); // Genera /swagger/v1/swagger.json

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
                // No pongas RoutePrefix = string.Empty si quieres que esté en /swagger/index.html
                // Por defecto, Swagger UI se sirve en /swagger
            });

            app.UseRouting();

            app.UseCors("MyPolicy");
            app.UseResponseCaching();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }

    }


}

