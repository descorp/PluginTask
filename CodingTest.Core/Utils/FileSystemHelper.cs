namespace CodingTest.Core.Utils
{
    using Cirrious.CrossCore;

    using CodingTest.Core.Services.Interfaces;

    public class FileSystemHelper
    {
        public static string AppLocalFolder => Mvx.Resolve<IAppContextService>().LocalPath;

        public static string FileName(string filepath)
        {
            return filepath.Substring(filepath.LastIndexOf('\\') + 1);
        }
    }
}
