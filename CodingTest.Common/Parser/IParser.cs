using System.Threading.Tasks;

namespace CodingTest.Common.Parser
{
    public interface IParser<TData>
    {
        string Status { get; }

        Task<TData> Process(string raw);
    }
}
