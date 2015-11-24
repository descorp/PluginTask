namespace CodingTest.Core.Services
{
    using MvvmCross.Plugins.Messenger;

    public class NewFileMessage : MvxMessage
    {
        public string Path { get; set; }

        public NewFileMessage(object sender)
            : base(sender)
        {
        }
    }
}