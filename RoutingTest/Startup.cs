using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace RoutingTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            static async Task EmptyResponse(HttpContext context)
            {
                await context.Response.WriteAsync($"Hello World from {context.GetEndpoint().DisplayName}!");
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("blog/{**blog}", endpoints.CreateApplicationBuilder().Use(next => EmptyResponse).Build()).WithDisplayName("Blog");
                endpoints.MapControllerRoute("HomePage", "/", new { controller = "Home", action = "Index" }).WithDisplayName("HomePage");
                endpoints.MapControllerRoute("TwoConstraints", "{a:regex(^a$)}/{b:regex(^b$)}", new { controller = "Home", action = "Index" }).WithDisplayName("TwoConstaints");
            });
        }
    }
}
