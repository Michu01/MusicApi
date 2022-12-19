namespace MusicApi.Services
{
    public class PlaylistCoverFileManager : WebRootFileManager
    {
        public PlaylistCoverFileManager(IWebHostEnvironment webHostEnvironment) 
            : base(webHostEnvironment)
        {
        }

        protected override string RootRelativeFolderPath => Path.Combine("images", "playlistCovers");
    }
}
