﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using TripTracker.Backservice.Data;

namespace TripTracker.Backservice
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
            //Api repository model
            //services.AddTransient<Models.Repository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Entity framework dbcontext
            services.AddDbContext<TripContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TripConnection")) //TripConnection comes from appsettings.json
            
            );

            //Adding Swashbuckle swagger service which allows to see informations about our API
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Trip Tracker", Version = "v1" })

            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Using swashbuckle swagger in our application.
            app.UseSwagger();

            if(env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Trip Tracker v1")
                );
            }
            

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

            TripContext.SeedData(app.ApplicationServices);

        }
    }
}
