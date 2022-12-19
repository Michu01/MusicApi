namespace MusicApi.Services
{
    public abstract class WebRootFileManager : FileManager
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public WebRootFileManager(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        protected override string RootPath => webHostEnvironment.WebRootPath;
    }
}
