namespace DataAccess.Entities
{
	public class UploadTransaction : BaseEntity
	{
		private UploadTransaction() { }

		public UploadTransaction(
			int classroomPlayListId, 
			string filePath,
			string fileName,
			string owner,
			decimal fileSizeMb)

		{
			ClassroomPlayListId = classroomPlayListId;
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
			FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
			Owner = owner ?? throw new ArgumentNullException(nameof(owner));
			FileSizeMb = fileSizeMb;
		}

		public int ClassroomPlayListId { get; private set; }
		public ClassroomPlaylist ClassroomPlaylist { get; private set; } = null!;
		public string FilePath { get; private set; } = null!;
		public string FileName { get; private set; } = null!;
		public string Owner { get; private set; } = null!;
		public decimal FileSizeMb { get; private set; }
	}
}
