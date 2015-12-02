namespace CodingTest.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodingTest.Common.Model;
    using CodingTest.Core.Services.Interfaces;
    using CodingTest.Core.Utils;

    using MvvmCross.Plugins.Messenger;

    public class DataStorageService : IDataStorage<DataSample>
    {
        private readonly IParserService<DataSample> parser;

        private readonly IMvxMessenger hub;

        private MvxSubscriptionToken mvxSubscriptionToken;

        public DataStorageService(IParserService<DataSample> parser, IMvxMessenger hub)
        {
            this.parser = parser;
            this.hub = hub;
            this.mvxSubscriptionToken = hub.Subscribe<NewFileMessage>(this.ProcessNewFile);
        }

        private async void ProcessNewFile(NewFileMessage a)
        {
            try
            {
                var data = await this.parser.Parse(a.Path);

                this.ObservedFiles.Add(
                    new ObservedFileItem<DataSample>()
                        {
                            Name = FileSystemHelper.FileName(a.Path),
                            LastUpdate = DateTime.Now.ToString("G"),
                            Data = data.ToList()
                        });
            }
            catch (Exception exc)
            {
                this.ObservedFiles.Add(new ObservedFileItem<DataSample>() { Name = FileSystemHelper.FileName(a.Path), LastUpdate = exc.Message });
            }
            finally
            {
                this.hub.Publish<NewDataMessage>(new NewDataMessage(this));
            }
        }

        public List<ObservedFileItem<DataSample>> ObservedFiles { get; set; } = new List<ObservedFileItem<DataSample>>();
    }

    public interface IDataStorage<TData>
    {

        List<ObservedFileItem<TData>> ObservedFiles { get; set; }
    }
}
