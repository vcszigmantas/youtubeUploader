namespace YoutubeAppV1.Models
{
	public class ApplicationParameter
	{
		public ApplicationParameter(
			string originalPath, 
			string uploadedPath, 
			string failedPath, 
			string thumbnailPath)
		{
			OriginalPath = originalPath;
			UploadedPath = uploadedPath;
			FailedPath = failedPath;
			ThumbnailPath = thumbnailPath;
		}

		public string OriginalPath { get; }
		public string UploadedPath { get; }
		public string FailedPath { get; }
		public string ThumbnailPath { get; }
	}
}
