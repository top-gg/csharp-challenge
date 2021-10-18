using System.Collections.Generic;

namespace Earthquake.Data.CSV
{
    public interface ICsvParser
    {
        IEnumerable<TClass> Read<TClass, TMap>();
    }
}