namespace CodingTest.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodingTest.Common.Model;

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
            var data = await this.parser.Parse(a.Path);

            if (data != null)
            {
                this.ObservedFiles.Add(
                    new ObservedFileItem<DataSample>()
                        {
                            Name = a.Path.Substring(a.Path.LastIndexOf('/') + 1),
                            LastUpdate = DateTime.Now.ToString("G"),
                            Data = data.ToList()
                        });
            }
            else
            {
                this.ObservedFiles.Add(
                    new ObservedFileItem<DataSample>()
                    {
                        Name = a.Path.Substring(a.Path.LastIndexOf('/') + 1),
                        LastUpdate = "n/a"
                    });
            }
        }

        public List<ObservedFileItem<DataSample>> ObservedFiles { get; set; }
    }

    public interface IDataStorage<TData>
    {

        List<ObservedFileItem<TData>> ObservedFiles { get; set; }
    }
}
