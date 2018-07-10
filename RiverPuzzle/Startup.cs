using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverPuzzle.Services;
using RiverPuzzle1.Data;
using RiverPuzzle1.Services;
using System;

namespace RiverPuzzle1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>            {                options.AddPolicy("CorsPolicy",                    builder =>                    builder.AllowAnyOrigin()                    .AllowAnyMethod()                    .WithExposedHeaders("content-disposition")                    .AllowAnyHeader()                    .AllowCredentials()                    .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));            });

            services.AddDbContext<GameContext>(options => options.UseInMemoryDatabase("RiverPuzzle"));
            //services.AddDbContext<GameContext>(options => options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")
            //        ));

            // configuring services
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameStateService, GameStateService>();
            services.AddScoped<ICharacterService, CharacterService>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
