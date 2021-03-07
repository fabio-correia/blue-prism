using System.Collections.Generic;

namespace BluePrism.TechTest.ValueObjects
{
    public class TreeNode<T>
    {
        public readonly T Item;
        public TreeNode<T> Parent { get; protected set; }

        protected readonly Dictionary<string, TreeNode<T>> _children =
                                            new Dictionary<string, TreeNode<T>>();
        public TreeNode(T item)
        {
            Item = item;
        }

        public TreeNode<T> GetChild(string key)
        {
            return this._children[key];
        }
        public TreeNode<T> AddChildren(string key, TreeNode<T> node)
        {
            node.Parent = this;
            _children.Add(key, node);
            return this;
        }

        public TreeNode<T> RemoveChildren(string key)
        {
            _children.Remove(key);
            return this;
        }
        public IReadOnlyDictionary<string, TreeNode<T>> GetChildrens() => _children;
    }
}