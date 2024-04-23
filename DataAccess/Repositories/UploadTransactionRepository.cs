using DataAccess.Entities;

namespace DataAccess.Repositories
{
	public class UploadTransactionRepository : BaseRepository<UploadTransaction>
	{
		public UploadTransactionRepository(YoutubeAppDbContext context) : base(context)
		{
		}
	}
}
