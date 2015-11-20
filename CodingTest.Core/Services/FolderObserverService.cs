namespace CodingTest.Core.Services
{
    using System;
    using System.Collections.Generic;

    public class FolderObserverService : IFolderObserver
    {
        private TimeSpan timeSpan;

        private string currentFolder;

        private List<ObservedFile> observedFiles;

        #region Implementation of IFolderObserver

        public TimeSpan TimeSpan
        {
            get
            {
                return this.timeSpan;
            }
            set
            {
                this.timeSpan = value;
            }
        }

        public string CurrentFolder
        {
            get
            {
                return this.currentFolder;
            }
            set
            {
                this.currentFolder = value;
            }
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public List<ObservedFile> ObservedFiles
        {
            get
            {
                return this.observedFiles;
            }
            set
            {
                this.observedFiles = value;
            }
        }

        #endregion
    }

    public interface IFolderObserver
    {
        TimeSpan TimeSpan { get; set; }

        string CurrentFolder { get; set; }

        void Start();

        void Stop();

        List<ObservedFile> ObservedFiles { get; set; }
    }
}
