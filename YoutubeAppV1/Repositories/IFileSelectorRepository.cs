namespace YoutubeAppV1.Repositories
{
	public interface IFileSelectorRepository
	{
		List<string> SelectFiles(string path);
	}
}
