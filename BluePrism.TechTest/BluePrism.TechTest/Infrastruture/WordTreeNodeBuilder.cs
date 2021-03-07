using BluePrism.TechTest.Business.Services;
using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace BluePrism.TechTest.Infrastruture
{
    public class WordTreeNodeBuilder : ITreeNodeBuilder<Word>
    {
        //private TreeNode<Word> _rootNode;
        private Word _end;
        //private IDictionary<string, bool> _verified;
        private IDictionary<string, TreeNode<Word>> _currentList = new Dictionary<string, TreeNode<Word>>();
        private IList<Word> _possibleWordsMissing;
        private readonly IComparer<Word> _comparer;

        public WordTreeNodeBuilder(IWordDistanceComparer comparer)
        {
            _comparer = comparer;
        }
        public TreeNode<Word> BuildTree(Word start, Word end, IList<Word> words)
        {
            //_rootNode = new TreeNode<Word>(start);
            _end = end;
            _possibleWordsMissing = words;

            AddNode(new TreeNode<Word>(start))
            .FillTree();

            return _currentList[start.Value];
            //return _rootNode;
        }

        private WordTreeNodeBuilder AddNode(TreeNode<Word> node)
        {

            _currentList.Add(node.Item.Value, node);

            _possibleWordsMissing = _possibleWordsMissing
                .Where(x => !(x.Value == node.Item.Value))
                .ToList();

            return this;
        }

        private void FillTree()
        {
            for (int i = 0; i < _currentList.Count; i++)
            {
                AddNextNodes(_currentList.ElementAt(i).Value.Item);
            }
        }

        private void AddNextNodes(Word word)
        {
            var closestWords = GetClosestWords(word)
                .ToList();

            if (closestWords.Count == 0)
                return;

            //When contains end, no need to add the other options
            if (closestWords.Contains(_end))
            {
                //_currentList[_end.Value].AddChildren(new WordTreeNode(_end));
                AddChildren(word, _end);
                return;
            }

            RemoveClosestFromFromPossibleWords(word, closestWords)
            .AddClosetChildNodesToParent(word, closestWords);

        }

        private void AddClosetChildNodesToParent(Word parent, IEnumerable<Word> closestWords)
        {
            foreach (var node in closestWords)
            {
                AddNode(
                    AddChildren(parent, node));
            }
        }

        private TreeNode<Word> AddChildren(Word parent, Word node)
        {
            var children = new TreeNode<Word>(node);
            _currentList[parent.Value]
                .AddChildren(node.Value, children);

            return children;
        }

        private WordTreeNodeBuilder RemoveClosestFromFromPossibleWords(Word word, IEnumerable<Word> closestWords)
        {
            _possibleWordsMissing = _possibleWordsMissing
                .Where(x => !closestWords.Contains(x) && !x.Equals(word))
                .ToList();

            return this;
            //.Where(x => !x.Equals(start))
            //.ToList();
        }

        private IEnumerable<Word> GetClosestWords(Word word)
        {
            return _possibleWordsMissing
                                //1(characters different) is the value that we should consider as a closest word
                                .Where(x => _comparer.Compare(x, word) == 1);
        }


        //var linkedWords = words
        //        //1(characters different) is the value that we should consider as a closest word
        //        .Where(x => comparer.Compare(x, start) == 1);

        //    if (linkedWords.Count() == 0)
        //        return null;

        //    if (linkedWords.Contains(end))
        //    {
        //        this.AddChildren(new WordTreeNode(end));
        //        return;
        //    }
        //    //remove from list on work
        //    var nextPossibleWords = words
        //        .Where(x => !linkedWords.Contains(x) && !x.Equals(start))
        //        //.Where(x => !x.Equals(start))
        //        .ToList();

        //    if (nextPossibleWords.Count == 0)
        //        return;


        //    linkedWords.ToList().ForEach(x =>
        //        this.AddChildren(new WordTreeNode(x, end, nextPossibleWords, comparer)));
    }
}
