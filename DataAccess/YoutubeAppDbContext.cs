using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class YoutubeAppDbContext : DbContext
	{
		public YoutubeAppDbContext(DbContextOptions<YoutubeAppDbContext> options) : base(options)
		{
			
		}

		public DbSet<ClassroomPlaylist> ClassroomPlaylists { get; set; }
		public DbSet<UploadTransaction> UploadTransactions { get; set; }
	}
}
