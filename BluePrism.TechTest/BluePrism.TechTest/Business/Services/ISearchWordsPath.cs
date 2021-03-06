using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    public interface ISearchWordsPath
    {
        Task<IEnumerable<Word>> GetShortestPath(Word start, Word end, WordTreeNode wordTreeNode);
    }
}