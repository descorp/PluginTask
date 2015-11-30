namespace CodingTest.Core.Services.Interfaces
{
    using System;

    public interface IFolderObserver
    {
        #region Properties

        string CurrentFolder { get; set; }

        TimeSpan TimeSpan { get; set; }

        #endregion

        #region Public Methods

        void Start();

        void Stop();

        #endregion
    }
}