using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OAuth.Business.Contract.Interface;
using OAuth.Business.Service.AuthenticationManager;
using OAuth.Business.Service.Interfaces;
using OAuth.Business.Service.Service;
using OAuth.Data.Contract.Data;
using OAuth.Data.Contract.Interface;
using OAuth.Data.Service.Service;
using System.Text;

namespace OAuth.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ITokenManagerService tokenManagerService { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<userContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("user_master")));
            services.AddTransient<IUserService, UserService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OAuth.Server", Version = "v1" });
            });
            services.AddControllers();
            string tokenKey = Configuration.GetValue<string>("refreshKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            services.AddTransient<ITokenRefresher>(x => new TokenRefresher(key, x.GetService<IJwtAuthenticationManager>()));
            services.AddTransient<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddTransient<IJwtAuthenticationManager>(x => new JwtAuthenticationManager(
                tokenKey, x.GetService<IRefreshTokenGenerator>(), x.GetService<IUserService>(), x.GetService<IConfiguration>()));
            services.AddTransient<IUserDataService, UserDataService>();
            services.AddTransient<IUserCredentialDataService, UserCredentialDataService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OAuth.Server v1"));
            }

            app.UseHttpsRedirection();

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
