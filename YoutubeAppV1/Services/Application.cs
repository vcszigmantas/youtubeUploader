using DataAccess.Entities;
using DataAccess.Repositories;
using YoutubeAppV1.Models;
using YoutubeAppV1.Repositories;

namespace YoutubeAppV1.Services
{
	public class Application : IApplication
	{
		private readonly ApplicationParameter _parameter;
		private readonly IRepository<ClassroomPlaylist> _classroomPlaylistRepository;
		private readonly IRepository<UploadTransaction> _uploadTransactionRepository;
		private readonly IFileSelectorRepository _fileSelectorRepository;

		public Application(
			IRepository<ClassroomPlaylist> classroomPlaylistRepository,
			IRepository<UploadTransaction> uploadTransactionRepository,
			ApplicationParameter parameter,
			IFileSelectorRepository fileSelectorRepository)
		{
			_classroomPlaylistRepository = classroomPlaylistRepository;
			_uploadTransactionRepository = uploadTransactionRepository;
			_parameter = parameter;
			_fileSelectorRepository = fileSelectorRepository;
		}

		public async Task RunAsync()
		{
			throw new NotImplementedException("Actually it is implemented, but not for public eyes. ha ha!");
		}
	}
}
