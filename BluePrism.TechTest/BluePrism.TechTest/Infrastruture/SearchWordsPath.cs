using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    public class SearchWordsPath : ISearchWordsPath
    {
        private readonly IList<WordTreeNode> _endPossibilities = new List<WordTreeNode>();
        public SearchWordsPath()
        {
        }

        public async Task<IEnumerable<Word>> GetShortestPath(Word start, Word end, WordTreeNode root)
        {
            await Task.CompletedTask;

            AddEndNode(end, root);

            var possiblePaths = new List<Word>[_endPossibilities.Count];
            for (var i = 0; i < _endPossibilities.Count; i++)
            {
                possiblePaths[i] = new List<Word>();
                var currentNode = _endPossibilities[i];
                while (currentNode.Parent != null)
                {
                    possiblePaths[i].Insert(0, currentNode.Word);
                    currentNode = currentNode.Parent;
                }
                possiblePaths[i].Insert(0, currentNode.Word);
            }

            var shortestPath = possiblePaths
                .OrderBy(x => x.Count)
                .FirstOrDefault();

            return shortestPath;
        }

        private void AddEndNode(Word end, WordTreeNode node)
        {
            if (node.Word.Equals(end))
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