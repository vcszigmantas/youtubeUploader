using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class ClassroomPlaylist : BaseEntity
	{
		private ClassroomPlaylist() { }

		public ClassroomPlaylist(
			string playlistId, 
			string classroomId)
		{
			PlaylistId = playlistId ?? throw new ArgumentNullException(nameof(playlistId));
			ClassroomId = classroomId ?? throw new ArgumentNullException(nameof(classroomId));
		}

		[Required]
		public string PlaylistId { get; private set; } = null!;

		[Required]
		public string ClassroomId { get; private set; } = null!;
	}
}
