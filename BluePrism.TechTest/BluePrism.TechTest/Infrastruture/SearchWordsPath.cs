using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    public class SearchWordsPath : ISearchShortestPath<Word>
    {
        private readonly IList<TreeNode<Word>> _endPossibilities = new List<TreeNode<Word>>();
        public SearchWordsPath()
        {
        }

        public async Task<IEnumerable<Word>> GetShortestPath(Word start, Word end, TreeNode<Word> root)
        {
            await Task.CompletedTask;

            AddEndNode(end, root);

            var possiblePaths = new List<Word>[_endPossibilities.Count];
            for (var i = 0; i < _endPossibilities.Count; i++)
            {
                possiblePaths[i] = new List<Word>();
                var currentNode = _endPossibilities[i];
                while (currentNode.Parent != null && !currentNode.Item.Equals(start))
                {
                    possiblePaths[i].Insert(0, currentNode.Item);
                    currentNode = currentNode.Parent;
                }
                possiblePaths[i].Insert(0, currentNode.Item);
            }

            var shortestPath = possiblePaths
                .OrderBy(x => x.Count)
                .FirstOrDefault();

            return shortestPath;
        }

        private void AddEndNode(Word end, TreeNode<Word> node)
        {
            if (node.Item.Equals(end))
            {
                _endPossibilities.Add(node);
                return;
            }
            var childrens = node?.GetChildrens();
            if (childrens is null)
                return;

            foreach (var childrenNode in childrens)
            {
                AddEndNode(end, childrenNode.Value);
            }
        }
    }
}