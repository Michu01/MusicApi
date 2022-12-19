using NAudio.Wave;

namespace MusicApi.Services
{
    public class SongFileManager : FileManager, ISongFileManager
    {
        protected override string RootRelativeFolderPath => "Songs";

        protected override string RootPath => "StaticFiles";

        public TimeSpan? GetDuration(Guid id)
        {
            if (Find(id) is not string filename)
            {
                return null;
            }

            AudioFileReader audioFileReader = new(filename);

            return audioFileReader.TotalTime;
        }
    }
}
