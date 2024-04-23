namespace YoutubeUploadPrototype.Services
{
    public interface IService<TParam, TResult>
    {
        Task<TResult> CallAsync(TParam parameter);
    }
}
