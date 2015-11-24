namespace CodingTest.Core.ViewModels.Tabs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using CodingTest.Core.Services;
    using CodingTest.Core.ViewModels.Items;

    using MvvmCross.Plugins.Messenger;

    public class FolderControllerViewModel : MvxViewModel
    {
        private readonly IFolderObserver observer;

        private readonly IMvxMessenger hub;

        private Dictionary<string, DateTime> files;

        public FolderControllerViewModel(IFolderObserver observer, IMvxMessenger hub)
        {
            this.observer = observer;
            this.hub = hub;
            this.files = new Dictionary<string, DateTime>(64);
            this.mvxSubscriptionToken = hub.Subscribe<NewFileMessage>(this.NewFileReceived);
        }

        private void NewFileReceived(NewFileMessage a)
        {
            if (this.files.ContainsKey(a.Path))
            {
                this.files[a.Path] = DateTime.Now;
            }
            else
            {
                this.files.Add(a.Path, DateTime.Now);
            }

            this.RaisePropertyChanged(() => this.FilesScaned);
        }

        private string folder = "Files";

        public string Folder
        {
            get
            {
                return this.folder;
            }
            set
            {
                this.folder = value;
                this.RaisePropertyChanged(() => this.Folder);
            }
        }

        private int timeSpan = 15000;

        public string TimeSpan
        {
            get
            {
                return this.timeSpan.ToString();
            }
            set
            {
                var timespan = 0;
                if (int.TryParse(value, out timespan))
                {
                    this.timeSpan = timespan;
                    this.RaisePropertyChanged(() => this.TimeSpan);
                }
            }
        }

        private MvxCommand chooseFolderCommand;

        private MvxSubscriptionToken mvxSubscriptionToken;

        public ICommand ChooseFolderCommand => this.chooseFolderCommand ?? (this.chooseFolderCommand = new MvxCommand(this.DoChooseFolderCommand));

        private void DoChooseFolderCommand()
        {
            this.observer.CurrentFolder = this.Folder;
            this.observer.TimeSpan = System.TimeSpan.FromMilliseconds(this.timeSpan);
            this.observer.Start();
        }

        public List<FileInfoItem> FilesScaned
        {
            get
            {
                return
                    this.files.Select(n =>
                    {
                        var name = n.Key.Substring(n.Key.LastIndexOf('/') + 1);
                        return new FileInfoItem { NAME = name, Update = DateTime.Now.ToString("G") };
                    })
                        .ToList();
            }
        }
    }
}
