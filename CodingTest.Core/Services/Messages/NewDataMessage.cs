namespace CodingTest.Core.Services
{
    using MvvmCross.Plugins.Messenger;

    public class NewDataMessage : MvxMessage
    {
        public string Path { get; set; }

        public object Data { get; set; }

        public NewDataMessage(object sender)
            : base(sender)
        {
        }
    }
}