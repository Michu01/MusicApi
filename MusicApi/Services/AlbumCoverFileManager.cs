using Microsoft.AspNetCore.StaticFiles;

namespace MusicApi.Services
{
    public class AlbumCoverFileManager : WebRootFileManager
    {
        public AlbumCoverFileManager(IWebHostEnvironment webHostEnvironment) 
            : base(webHostEnvironment)
        {

        }

        protected override string RootRelativeFolderPath => Path.Combine("images", "albumCovers");
    }
}
