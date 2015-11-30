namespace CodingTest.Core.Utils
{
    using Cirrious.CrossCore;

    using CodingTest.Core.Services.Interfaces;

    public class FileSystemHelper
    {
        public static string AppLocalFolder => Mvx.Resolve<IAppContextService>().LocalPath;
    }
}
