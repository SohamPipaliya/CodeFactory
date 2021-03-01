using CodeFactoryAPI.DAL;
using CodeFactoryAPI.Extra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CodeFactoryAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  configurePolicy: builder =>
                                  {
                                      builder.WithOrigins("https://localhost:44366")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                                  });
            });
            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.IgnoreNullValues = true;
                option.JsonSerializerOptions.PropertyNamingPolicy = new PascalCase();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeFactoryAPI", Version = "v1" });
            });

            services.AddDbContextPool<Context>(x => x.UseSqlServer(Configuration.GetConnectionString("CodeFactory")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeFactoryAPI"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(context => context.Response.WriteAsync("CodeFactory"));
        }
    }
}
