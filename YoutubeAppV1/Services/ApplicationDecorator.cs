namespace YoutubeAppV1.Services
{
	public class ApplicationDecorator : IApplication
	{
		private IApplication _application;
		private ILogger _logger;

		public ApplicationDecorator(
			IApplication application, 
			ILogger logger)
		{
			_application = application;
			_logger = logger;
		}

		public async Task RunAsync()
		{
			throw new NotImplementedException("Actually it is implemented, but not for public eyes. ha ha!");
		}
	}
}
