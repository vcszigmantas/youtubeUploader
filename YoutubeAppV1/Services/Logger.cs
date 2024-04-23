namespace YoutubeAppV1.Services
{
	public class Logger : ILogger
	{
		public void Log(string content)
		{
			var formattedContent = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {content}";

			Console.WriteLine(formattedContent);
			File.AppendAllText($"log_{DateTime.Now:yyyyMMdd}.txt", $"{formattedContent}{Environment.NewLine}");
		}
	}
}
