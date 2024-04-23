using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Text.RegularExpressions;

namespace YoutubeUploadPrototype
{
    internal class Program
    {
        private static readonly Dictionary<string, string> playlists = new Dictionary<string, string>()
        {
            { "PL5DQNw1Q67GuPCBJwrK68SeDXp_uGcrXa", "231119TES001VAK" },
            { "PL5DQNw1Q67Gso5NmS8g1KEO2BVg-fqCxo", "231129WED160DIE" },
            { "PL5DQNw1Q67GtbpOF-PzPlu9NFEf5lAvPT", "231213WED051VAK" },
            { "PL5DQNw1Q67GsaVUacamQjL2p3xR7nFWFl", "231023JSM492DIE" },
            { "PL5DQNw1Q67GtglZQZCxIpsQY7TWkRzTf6", "231204DMA160DIE" },
            { "PL5DQNw1Q67Gugt9eSBqI9WYHO8lQlVogW", "231206DMA160DIE" },
            { "PL5DQNw1Q67GtZb3bieVrFrGL824nmL30u", "231218PYT160DIE" },
            { "PL5DQNw1Q67GsIP-V9rbpZdvO6gtiLCDdo", "231019SIX081VAK" },
            { "PL5DQNw1Q67GtG1KcvzfQY1qZTO-GhTZW4", "231106ITS160VAK" },
            { "PL5DQNw1Q67GvgXYm0jYxBT6wPYqQ7LFVF", "240103DMA160DIE" },
            { "PL5DQNw1Q67GuRWevvnD_FJKdIMKl0QAzo", "240105WEB160DIE" },
            { "PL5DQNw1Q67GswLJv8iXfvvCO9bBLHdWrR", "231220DMA075VAK" },
            { "PL5DQNw1Q67Gtp1zUDdHOf2N78Qsv3ffPR", "231121DMA160VAK" },
            { "PL5DQNw1Q67GvBTcbT1bqTQp7DEC8d6pbu", "231108JSM492VAK" },
            { "PL5DQNw1Q67GuFZCwUq5G1a5jyP-J27F8_", "231108WED160VAK" },
            { "PL5DQNw1Q67Gs5Tg4PszijL8AVpge6vOHE", "231227PBI020VAK" },
            { "PL5DQNw1Q67Gu4V5YPxzWD77cmr1BNqROT", "231030DMA160VAK" }
            //https://www.youtube.com/playlist?list=PL5DQNw1Q67GuRWevvnD_FJKdIMKl0QAzo 240105WEB160DIE Web Kūrimas, 160 val.
            //https://studio.youtube.com/playlist/PL5DQNw1Q67GswLJv8iXfvvCO9bBLHdWrR/edit
            //https://studio.youtube.com/playlist/PL5DQNw1Q67Gtp1zUDdHOf2N78Qsv3ffPR/edit
            //https://studio.youtube.com/playlist/PL5DQNw1Q67GvBTcbT1bqTQp7DEC8d6pbu/edit
            //https://studio.youtube.com/playlist/PL5DQNw1Q67GuFZCwUq5G1a5jyP-J27F8_/edit
            //https://studio.youtube.com/playlist/PL5DQNw1Q67Gs5Tg4PszijL8AVpge6vOHE/edit
            //https://studio.youtube.com/playlist/PL5DQNw1Q67Gu4V5YPxzWD77cmr1BNqROT/edit
        };

        private static readonly string videoFolderPath = @"G:\My Drive\video\";
        private static readonly string uploadedVideosPath = @"G:\My Drive\uploaded\";
        private static readonly string failedVideosPath = @"G:\My Drive\failed\";
        private static readonly string thumbnailImagePath = @"G:\My Drive\logo.png"; // Adjust this path as necessary

        static async Task Main(string[] args)
        {
            UserCredential credential;

            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                var dataStore = new FileDataStore("Store", true);
                Console.WriteLine($"Data store path: {dataStore.FolderPath}");

                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.YoutubeUpload, YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeForceSsl },
                    "user",
                    CancellationToken.None,
                    dataStore
                );
            }

            var youtubeService = new YouTubeService(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "YouTubeUploader"
                }
            );

            // Define the supported video file extensions
            var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".mp4", ".mov", ".avi", ".wmv", ".flv", ".mkv" };

            // Get all files in the folder
            var allFiles = Directory.GetFiles(videoFolderPath).OrderBy(filename => filename);

            // Filter out files that match the supported video file extensions and sort them alphabetically
            var videoFiles = allFiles.Where(file => supportedExtensions.Contains(Path.GetExtension(file))).ToList();

            foreach (var filePath in videoFiles)
            {
                var fileName = Path.GetFileName(filePath);
                

                if (IsFileNameValid(fileName))
                {
                    string playlistId = GetPlaylistIdFromFilename(fileName);

                    if(playlistId == null)
                    {
                        Console.WriteLine($"Nėra playlisto sistemoje: {fileName}");
                        File.AppendAllText("no_playlist.txt", fileName + Environment.NewLine);
                        continue;
                    }

                    // Proceed with uploading the file as before
                    var video = new Video
                    {
                        Snippet = new VideoSnippet
                        {
                            Title = fileName,
                            Description = fileName,
                            //Tags = new string[] { "tag1", "tag2", "tag3" },
                            CategoryId = "27"
                        },
                        Status = new VideoStatus
                        {
                            PrivacyStatus = "unlisted",
                            SelfDeclaredMadeForKids = false
                        }
                    };

                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                        videosInsertRequest.ProgressChanged += VideosInsertRequest_ProgressChanged;
                        videosInsertRequest.ResponseReceived += async (uploadedVideo) =>
                        {
                            await VideosInsertRequest_ResponseReceived(uploadedVideo, youtubeService, playlistId, filePath);
                        };

                        await videosInsertRequest.UploadAsync();
                    }
                }
                else
                {
                    // Move the file to the failed directory
                    MoveFileToFailedFolder(filePath);
                }
            }

            Console.WriteLine("All videos processed.");
            Console.ReadKey();
        }

        static void VideosInsertRequest_ProgressChanged(IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine($"{progress.BytesSent} bytes sent.");
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine($"An error prevented the upload from completing.\n{progress.Exception}");
                    break;
            }
        }

        static async Task VideosInsertRequest_ResponseReceived(Video video, YouTubeService youtubeService, string playlistId, string filePath)
        {
            Console.WriteLine($"Video id '{video.Id}' was successfully uploaded.");

            if (!string.IsNullOrEmpty(playlistId))
            {
                var playlistItem = new PlaylistItem
                {
                    Snippet = new PlaylistItemSnippet
                    {
                        PlaylistId = playlistId,
                        ResourceId = new ResourceId
                        {
                            Kind = "youtube#video",
                            VideoId = video.Id
                        }
                    }
                };

                CancellationTokenSource cts = new CancellationTokenSource();

                try
                {
                    var request = youtubeService.PlaylistItems.Insert(playlistItem, "snippet");
                    var task = request.ExecuteAsync(cts.Token);

                    if (await Task.WhenAny(task, Task.Delay(10000)) == task) // 10 seconds timeout
                    {
                        if (task.IsCompletedSuccessfully)
                        {
                            var response = await task;
                            Console.WriteLine($"Video added to playlist {playlistId}.");

                            // Now move the file to the uploaded videos path
                            MoveFileToUploadedFolder(filePath);
                        }
                        else if (task.IsFaulted)
                        {
                            Console.WriteLine($"Task failed: {task.Exception?.InnerException?.Message}");
                        }
                        else
                        {
                            Console.WriteLine("Task did not complete successfully.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Request timed out. Cancelling the request.");
                        cts.Cancel();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error adding video to playlist: {e.Message}");
                }
                finally
                {
                    cts?.Dispose();
                }
            }

            await SetVideoThumbnail(video.Id, youtubeService);
        }

        static void MoveFileToUploadedFolder(string filePath)
        {
            try
            {
                var targetPath = Path.Combine(uploadedVideosPath, Path.GetFileName(filePath));
                if (!Directory.Exists(uploadedVideosPath))
                {
                    Directory.CreateDirectory(uploadedVideosPath);
                }
                if (File.Exists(targetPath))
                {
                    File.Delete(targetPath); // Optional: Overwrite existing file
                }
                File.Move(filePath, targetPath);
                Console.WriteLine($"File moved to {targetPath}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error moving the file: {e.Message}");
            }
        }

        static string GetPlaylistIdFromFilename(string fileName)
        {
            foreach (var item in playlists)
            {
                if (fileName.Contains(item.Value))
                {
                    return item.Key;
                }
            }
            return null;
        }

        static bool IsFileNameValid(string fileName)
        {
            var pattern = @"^\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])[A-Z]{3}\d{3}(VAK|DIE|IND|B2B|SAV)";
            return Regex.IsMatch(fileName, pattern, RegexOptions.IgnoreCase);
        }

        static void MoveFileToFailedFolder(string filePath)
        {
            try
            {
                var targetPath = Path.Combine(failedVideosPath, Path.GetFileName(filePath));
                if (!Directory.Exists(failedVideosPath))
                {
                    Directory.CreateDirectory(failedVideosPath);
                }
                File.Move(filePath, targetPath);
                Console.WriteLine($"File moved to failed directory: {targetPath}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error moving the file to failed directory: {e.Message}");
            }
        }

        static async Task SetVideoThumbnail(string videoId, YouTubeService youtubeService)
        {
            using (var fileStream = new FileStream(thumbnailImagePath, FileMode.Open))
            {
                var thumbnailSetRequest = youtubeService.Thumbnails.Set(videoId, fileStream, "image/png");
                thumbnailSetRequest.ProgressChanged += ThumbnailSetRequest_ProgressChanged;
                thumbnailSetRequest.ResponseReceived += ThumbnailSetRequest_ResponseReceived;

                await thumbnailSetRequest.UploadAsync();
            }
        }

        static void ThumbnailSetRequest_ProgressChanged(IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine($"Thumbnail {progress.BytesSent} bytes sent.");
                    break;
                case UploadStatus.Completed:
                    Console.WriteLine("Thumbnail uploaded.");
                    break;
                case UploadStatus.Failed:
                    Console.WriteLine($"An error prevented the thumbnail from uploading: {progress.Exception}");
                    break;
            }
        }

        static void ThumbnailSetRequest_ResponseReceived(ThumbnailSetResponse response)
        {
            Console.WriteLine("Thumbnail set successfully.");
        }
    }
}