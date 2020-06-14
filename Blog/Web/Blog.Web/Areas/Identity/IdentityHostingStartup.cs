using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Blog.Web.Areas.Identity.IdentityHostingStartup))]
namespace Blog.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
