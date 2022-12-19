namespace MusicApi.Services
{
    public interface ISongFileManager : IFileManager
    {
        TimeSpan? GetDuration(Guid id);
    }
}
