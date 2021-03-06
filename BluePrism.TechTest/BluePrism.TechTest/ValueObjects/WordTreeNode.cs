using BluePrism.TechTest.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BluePrism.TechTest.ValueObjects
{
    public class WordTreeNode
    {
        private readonly Dictionary<string, WordTreeNode> _children =
                                            new Dictionary<string, WordTreeNode>();

        public readonly Word Word;
        public WordTreeNode Parent { get; private set; }

        public WordTreeNode(Word start, Word end, IList<Word> words, IComparer<Word> comparer)
        {
            Word = start;

            var linkedWords = words
                //1(characters different) is the value that we should consider as a closest word
                .Where(x => comparer.Compare(x, start) == 1);

            if (linkedWords.Count() == 0)
                return;

            if (linkedWords.Contains(end))
            {
                this.AddChildren(new WordTreeNode(end));
                return;
            }
            //remove from list on work
            var nextPossibleWords = words
                .Where(x => !linkedWords.Contains(x) && !x.Equals(start))
                //.Where(x => !x.Equals(start))
                .ToList();

            if (nextPossibleWords.Count == 0)
                return;


            linkedWords.ToList().ForEach(x =>
                this.AddChildren(new WordTreeNode(x, end, nextPossibleWords, comparer)));

        }
        public WordTreeNode(Word word)
        {
            Word = word;
        }

        public IReadOnlyDictionary<string, WordTreeNode> GetChildrens() => _children;
        public WordTreeNode GetChild(Word word)
        {
            return this._children[word.Value];
        }

        public WordTreeNode AddChildren(WordTreeNode item)
        {

            ReplaceParent(item)
                ._children.Add(item.Word.Value, item);

            return this;
        }

        private WordTreeNode ReplaceParent(WordTreeNode item)
        {
            if (item.Parent != null)
            {
                item.Parent._children.Remove(item.Word.Value);
            }

            item.Parent = this;

            return this;
        }
    }
}
