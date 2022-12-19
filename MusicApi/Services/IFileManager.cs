namespace MusicApi.Services
{
    public interface IFileManager
    {
        Task<string> Save(Guid id, IFormFile formFile);

        void Delete(Guid id);

        bool Exists(Guid id);

        public (FileStream, string)? Get(Guid id);
    }
}
