using System.Web.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace WebApi1
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            const string rootfolder = ".";
            var fiesystem = new PhysicalFileSystem(rootfolder);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fiesystem
            };
            appBuilder
                .UseFileServer(options)
                .UseWebApi(config);

            appBuilder.MapSignalR();
        }
    }
}
