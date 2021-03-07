using BluePrism.TechTest.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BluePrism.TechTest.ValueObjects
{
    public class WordTreeNode : TreeNode<Word>
    {
        //public WordTreeNode(Word start, Word end, IList<Word> words, IComparer<Word> comparer)
        //    :base(start)
        //{
        //    //Word = start;

        //    //var linkedWords = words
        //    //    //1(characters different) is the value that we should consider as a closest word
        //    //    .Where(x => comparer.Compare(x, start) == 1);

        //    //if (linkedWords.Count() == 0)
        //    //    return;

        //    //if (linkedWords.Contains(end))
        //    //{
        //    //    this.AddChildren(new WordTreeNode(end));
        //    //    return;
        //    //}
        //    ////remove from list on work
        //    //var nextPossibleWords = words
        //    //    .Where(x => !linkedWords.Contains(x) && !x.Equals(start))
        //    //    //.Where(x => !x.Equals(start))
        //    //    .ToList();

        //    //if (nextPossibleWords.Count == 0)
        //    //    return;


        //    //linkedWords.ToList().ForEach(x =>
        //    //    this.AddChildren(new WordTreeNode(x, end, nextPossibleWords, comparer)));

        //}
        public WordTreeNode(Word word)
            : base(word)
        {
            //Word = word;
        }

        //public IReadOnlyDictionary<string, WordTreeNode> GetChildrens() => _children;
        //public WordTreeNode GetChild(Word word)
        //{
        //    return this._children[word.Value];
        //}

        public WordTreeNode AddChildren(WordTreeNode node)
        {

            ReplaceParent(node)
                .AddChildren(node.Item.Value, node);
                //._children.Add(item.GetHashCode(), item);

            return this;
        }

        private WordTreeNode ReplaceParent(WordTreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.RemoveChildren(node.Item.Value);
            }

            node.Parent = this;

            return this;
        }
    }
}
