using DataAccess;
using DataClasses;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Workflow.Activities;

namespace WebAPI
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
            #region Web
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            services.AddApiVersioning(config =>
            {
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Net5WithElsa", Version = "v1" });
            });
            #endregion

            #region Database
            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("LocalContext"));
            });
            services.AddSingleton(Configuration);
            #endregion

            #region Stores
            services
                  .AddTransient<LibraryStore>()
                  ;
            #endregion

            #region Elsa
            var elsaSection = Configuration.GetSection("Elsa");

            services.AddElsa(elsa => elsa
                .UseEntityFrameworkPersistence(ef =>
                {
                    ef.UseSqlServer(Configuration.GetConnectionString("LocalContext"));
                }, true)
                .AddConsoleActivities()
                .AddActivity<GetBookGraphActivity>()
                .AddHttpActivities(elsaSection.GetSection("Http").Bind)
                .AddQuartzTemporalActivities()
                .AddJavaScriptActivities()
                .AddWorkflowsFrom<Startup>()
            );

            services.AddElsaApiEndpoints();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net5WithElsa v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition")
            );

            app.UseAuthorization();

            app.UseHttpActivities(); // Elsa

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
