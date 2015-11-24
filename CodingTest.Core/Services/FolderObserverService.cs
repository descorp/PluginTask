namespace CodingTest.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MvvmCross.Plugins.File;
    using MvvmCross.Plugins.Messenger;

    public class FolderObserverService : IFolderObserver
    {
        #region Fields

        private readonly IMvxFileStore store;

        private readonly IMvxMessenger hug;

        private string currentFolder = "Files";

        private Timer timer;

        private readonly Dictionary<string, DateTime> knownFiles;

        private TimeSpan timeSpan;

        #endregion

        #region Constructors

        public FolderObserverService(IMvxFileStore store, IMvxMessenger hug)
        {
            this.store = store;
            this.hug = hug;
            this.knownFiles = new Dictionary<string, DateTime>(64);
        }

        #endregion

        #region Nested type: Timer

        internal sealed class Timer : CancellationTokenSource, IDisposable
        {
            #region Constructors

            internal Timer(TimerCallback callback, object state, int dueTime, TimeSpan period)
            {
                Task.Delay(dueTime, this.Token).ContinueWith(
                    async (t, s) =>
                        {
                            var tuple = (Tuple<TimerCallback, object>)s;

                            while (true)
                            {
                                if (this.IsCancellationRequested)
                                {
                                    break;
                                }
                                Task.Run(() => tuple.Item1(tuple.Item2));
                                await Task.Delay(period);
                            }
                        },
                    Tuple.Create(callback, state),
                    CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.Default);
            }

            #endregion

            #region IDisposable Members

            public new void Dispose()
            {
                this.Cancel();
            }

            #endregion
        }

        #endregion

        #region Nested type: TimerCallback

        internal delegate void TimerCallback(object state);

        #endregion

        #region Implementation of IFolderObserver

        public TimeSpan TimeSpan
        {
            get
            {
                return this.timeSpan;
            }
            set
            {
                this.Stop();
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
                this.Stop();
                this.currentFolder = value;
            }
        }

        public void Start()
        {
            this.timer = new Timer(this.Callback, this, this.TimeSpan.Milliseconds, this.TimeSpan);
        }

        private void Callback(object a)
        {
            this.store.EnsureFolderExists(this.CurrentFolder);

            foreach (var file in this.store.GetFilesIn(this.CurrentFolder))
            {
                if (this.knownFiles.ContainsKey(file) && (DateTime.UtcNow - this.knownFiles[file]).Minutes < 5)
                {
                    continue;
                }

                this.knownFiles.Add(file, DateTime.UtcNow);
                this.hug.Publish(new NewFileMessage(this) {Path = file});
            }
        }

        public void Stop()
        {
            var timer1 = this.timer;
            if (timer1 == null)
            {
                return;
            }

            timer1.Cancel();
            timer1.Dispose();
        }

        #endregion
    }
}