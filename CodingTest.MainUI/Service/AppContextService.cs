using System;

namespace CodingTest.MainUI.Service
{
    using Core.Services.Interfaces;

    public class AppContextService : IAppContextService
    {
        public string LocalPath => Environment.CurrentDirectory;
    }
}
