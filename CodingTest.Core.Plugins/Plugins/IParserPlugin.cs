namespace CodingTest.Common.Plugins
{
    using CodingTest.Common.Model;
    using CodingTest.Common.Parser;

    public interface IParserPlugin : IPlugin
    {
        string FileFormat { get; }

        IParser<DataSample> Parser { get; }
    }
}