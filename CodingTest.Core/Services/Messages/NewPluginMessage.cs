namespace CodingTest.Core.Services
{
    using MvvmCross.Plugins.Messenger;

    public class NewPluginMessage : MvxMessage
    {
        public string Path { get; set; }

        public NewPluginMessage(object sender)
            : base(sender)
        {
        }
    }
}