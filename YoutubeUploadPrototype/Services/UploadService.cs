namespace YoutubeUploadPrototype.Services
{
    internal class UploadService<TParam, TResult> : IService<TParam, TResult>
    {
        public Task<TResult> CallAsync(TParam parameter)
        {
            throw new NotImplementedException();
        }
    }
}
