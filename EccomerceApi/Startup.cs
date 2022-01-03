using EcommerceCore;
using EcommerceCore.IRepository;
using EcommerceCore.Repository;
using EcommerceCore.Services;
using EcommerceData.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EccomerceApi
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
            services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("lukasConnection")));

            services.AddAuthentication();
            services.ConfigureJWT(Configuration);
            // adding Cors policy. so user from other networks could access our API. just adding policy with name
            //basically allowing here anybody and everybody to access this API
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
            });

            // Add autoMapper. For type providing MapperInitializer that i created in Configurations
            services.ConfigureAutoMapper();

            //adding service IAuthManager(interface) and its implementation. so i can use it in whole project\
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EccomerceApi", Version = "v1" });
            });
            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EccomerceApi v1"));
            //addding exception handler from service extensions. to not write try catch all time
            app.ConfigurExceptionHandler();

            app.UseHttpsRedirection();

            // letting app know that it should use CORS policy with name "AllowAll" that i created 
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
