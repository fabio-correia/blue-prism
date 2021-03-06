using BluePrism.TechTest.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    public interface IWordsRepository
    {
        Task<IEnumerable<Word>> GetAll();
    }
}