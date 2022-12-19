using Microsoft.AspNetCore.StaticFiles;

namespace MusicApi.Services
{
    public abstract class FileManager : IFileManager
    {
        private readonly FileExtensionContentTypeProvider typeProvider = new();

        protected abstract string RootRelativeFolderPath { get; }

        protected abstract string RootPath { get; }

        private string FolderPath => Path.Combine(RootPath, RootRelativeFolderPath);

        private IEnumerable<string> FindAll(Guid id)
        {
            return Directory.EnumerateFiles(FolderPath, $"{id}.*");
        }

        protected string? Find(Guid id)
        {
            return FindAll(id).FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            foreach (string filename in FindAll(id))
            {
                File.Delete(filename);
            }
        }

        public async Task<string> Save(Guid id, IFormFile formFile)
        {
            Delete(id);

            string extension = Path.GetExtension(formFile.FileName);

            string relativeFilePath = Path.Combine(RootRelativeFolderPath, $"{id}{extension}");

            string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);

            using FileStream file = File.OpenWrite(absoluteFilePath);

            await formFile.CopyToAsync(file);

            return relativeFilePath;
        }

        public bool Exists(Guid id)
        {
            return Find(id) is not null;
        }

        public (FileStream, string)? Get(Guid id)
        {
            if (Find(id) is not string filename)
            {
                return null;
            }

            FileStream file = File.OpenRead(filename);

            string extension = Path.GetExtension(filename);

            string contentType = typeProvider.Mappings[extension];

            return (file, contentType);
        }
    }
}
