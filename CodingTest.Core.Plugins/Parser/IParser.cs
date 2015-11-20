using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Common.Parser
{
    public interface IParser<TData>
    {
        string Status { get; }

        List<TData> Process(string raw);
    }
}
