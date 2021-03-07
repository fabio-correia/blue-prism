using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    public interface ISearchShortestPath<T>
    {
        Task<IEnumerable<T>> GetShortestPath(T start, T end, TreeNode<T> wordTreeNode);
    }
}