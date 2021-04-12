using Blogging.Api.Extensions;
using Blogging.Business.Contract.Interface;
using Blogging.Business.Contract.ModelMapper;
using Blogging.Business.Service.Services;
using Blogging.Data.Contract.Data;
using Blogging.Data.Contract.Interface;
using Blogging.Data.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Blogging.Api
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

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<blogContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("blog_master")));
            string tokenKey = Configuration.GetValue<string>("accessKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            services.AddSwaggerDocumentation();
            services.JwtTokenService(key);
            services.AddTransient<ILogger>(x => new Logger<PostDataService>(new LoggerFactory()));
            services.AddTransient<IBlogPostService, BlogPostService>();
            services.AddTransient<IPostDataService, PostDataService>();
            services.AddTransient<IModelBuilder, Business.Contract.ModelMapper.ModelBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blogging.Api v1"));
            }
            app.ConfigureCustomExceptionMiddleware();
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
