using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingTest.Common.Parser
{
    public interface IParser<TData>
    {
        string Status { get; }

        Task<List<TData>> Process(string filename);
    }
}
