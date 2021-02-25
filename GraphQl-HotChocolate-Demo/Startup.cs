using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQl_HotChocolate_Demo.DataBase;
using GraphQl_HotChocolate_Demo.GraphQlServices;
using GraphQl_HotChocolate_Demo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GraphQl_HotChocolate_Demo
{
    public class Startup
    {
        
        private readonly string AllowedOrigin = "allowedOrigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllers();
         
            services.AddDbContext<GraphQlDemoDbContext>(
                options => 
                    options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString")
                    ));
                    
            services.AddInMemorySubscriptions();

            services
                .AddGraphQLServer()
                .AddQueryType<BookQuery>()
                .AddMutationType<BooksMutation>()
                .AddSubscriptionType<BookSubscription>();            

            services.AddScoped<BooksRepo, BooksRepo>();

            
            services.AddCors(option => {
                option.AddPolicy("allowedOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });
            
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo {Title = "GraphQl_HotChocolate_Demo", Version = "v1"});
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQl_HotChocolate_Demo v1"));
            }
            // app.UseCors(AllowedOrigin);
            // app.UseHttpsRedirection();
            //
            // app.UseRouting();
            //
            // app.UseAuthorization();
            //
            // app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseCors(AllowedOrigin);
            app.UseWebSockets();
            app
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                }); 
        }
    }
}