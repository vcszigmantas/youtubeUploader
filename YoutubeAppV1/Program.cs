using Microsoft.Extensions.DependencyInjection;
using YoutubeAppV1;
using YoutubeAppV1.Services;

namespace YoutubeUploadPrototype
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var startup = new Startup();
			var services = startup.ConfigureServices();

			var serviceProvider = services.BuildServiceProvider();

			var application = serviceProvider.GetService<IApplication>();

			if(application != null)
			{
				await application.RunAsync();
			}

			Environment.Exit(0);
		}
	}
}
