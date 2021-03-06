using BluePrism.TechTest.Domain;
using System.Collections.Generic;

namespace BluePrism.TechTest
{
    public interface IDocumentListWriter<T>
    {
        void WriteText(IEnumerable<T> rows);
    }
}