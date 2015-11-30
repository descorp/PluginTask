namespace CodingTest.Core.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IParserService<TData>
    {
        Task<IEnumerable<TData>> Parse(string stream);
    }
}