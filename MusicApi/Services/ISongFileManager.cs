namespace MusicApi.Services
{
    public interface ISongFileManager
    {
        string? Find(Guid id);

        (FileStream, string) Get(string filename);

        Task Save(Guid id, IFormFile file);

        void Delete(Guid id);

        TimeSpan? GetDuration(Guid id);
    }
}
