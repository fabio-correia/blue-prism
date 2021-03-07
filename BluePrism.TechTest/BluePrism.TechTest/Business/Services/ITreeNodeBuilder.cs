using BluePrism.TechTest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BluePrism.TechTest.Business.Services
{
    public interface ITreeNodeBuilder<T>
    {
        TreeNode<T> BuildTree(T start, T end, IList<T> words);
    }
}
