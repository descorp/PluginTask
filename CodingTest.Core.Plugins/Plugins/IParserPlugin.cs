namespace CodingTest.Common.Plugins
{
    using CodingTest.Common.Parser;

    public interface IParserPlugin<TData> : IPlugin
    {
        string FileFormat { get; }

        IParser<TData> Parser { get; }
    }
}