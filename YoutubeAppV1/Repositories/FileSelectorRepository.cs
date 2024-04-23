namespace YoutubeAppV1.Repositories
{
	public class FileSelectorRepository : IFileSelectorRepository
	{
		/// <summary>
		/// Selects files in provided directory
		/// Accepts following extensions: .mp4, .mov, .avi, .wmv, .flv, .mkv
		/// </summary>
		/// <param name="path">Path to specific folder</param>
		/// <returns>List of file paths to specific video file</returns>
		public List<string> SelectFiles(string path)
		{
			throw new NotImplementedException("Actually it is implemented, but not for public eyes. ha ha!");
		}
	}
}
