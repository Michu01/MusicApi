using Microsoft.AspNetCore.StaticFiles;

using MusicApi.DTOs;

using NAudio.Wave;

namespace MusicApi.Services
{
    public class SongFileManager : ISongFileManager
    {
        private readonly FileExtensionContentTypeProvider typeProvider = new();

        public void Delete(Guid id)
        {
            if (Find(id) is string filename)
            {
                Directory.Delete(filename);
            }
        }

        public string? Find(Guid id)
        {   
            string[] files = Directory.GetFiles(@"StaticFiles\Songs", $"{id}.*");

            if (files.Length == 0)
            {
                return null;
            }

            return files[0];
        }

        public (FileStream, string) Get(string filename)
        {
            FileStream file = File.OpenRead(filename);

            string extension = Path.GetExtension(filename);

            string contentType = typeProvider.Mappings[extension];

            return (file, contentType);
        }

        public TimeSpan? GetDuration(Guid id)
        {
            if (Find(id) is not string filename)
            {
                return null;
            }

            AudioFileReader audioFileReader = new(filename);

            return audioFileReader.TotalTime;
        }

        public async Task Save(Guid id, IFormFile formFile)
        {
            string path = $"StaticFiles/Songs/{id}{Path.GetExtension(formFile.FileName)}";

            using FileStream file = File.OpenWrite(path);

            await formFile.CopyToAsync(file);
        }
    }
}
