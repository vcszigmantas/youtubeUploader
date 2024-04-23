using DataAccess.Entities;

namespace DataAccess.Repositories
{
	public class ClassroomPlaylistRepository : BaseRepository<ClassroomPlaylist>
	{
		public ClassroomPlaylistRepository(YoutubeAppDbContext context) : base(context)
		{
		}
	}
}
