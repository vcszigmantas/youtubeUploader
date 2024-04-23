using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YoutubeAppV1.Models;
using YoutubeAppV1.Repositories;
using YoutubeAppV1.Services;

namespace YoutubeAppV1
{
	public class Startup
	{
		public ServiceCollection ConfigureServices()
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile(
					"appsettings.json", 
					optional: false, 
					reloadOnChange: true
				);

			var configuration = builder.Build();

			var services = new ServiceCollection();

			services
				.AddDbContext<YoutubeAppDbContext>(
					options =>
						options.UseSqlServer(configuration.GetConnectionString("Default"))
				);

			var appSettings = configuration
				.GetSection("ApplicationSettings")
				.Get<ApplicationParameter>();

			if(appSettings == null)
			{
				throw new Exception("Might be ApplicationSettings not defined in appsettings.json file.");
			}

			var appParams = new ApplicationParameter(
				appSettings.OriginalPath,
				appSettings.UploadedPath,
				appSettings.FailedPath,
				appSettings.ThumbnailPath
			);

			services.AddSingleton(appParams);

			services.AddScoped<IApplication, Application>();
			services.Decorate<IApplication, ApplicationDecorator>();

			services.AddScoped<IRepository<ClassroomPlaylist>, ClassroomPlaylistRepository>();
			services.AddScoped<IRepository<UploadTransaction>, UploadTransactionRepository>();
			services.AddScoped<IFileSelectorRepository, FileSelectorRepository>();

			services.AddScoped<ILogger, Logger>();

			return services;
		}
	}
}
