using BluePrism.TechTest.Domain;
using BluePrism.TechTest.Infrastruture;
using BluePrism.TechTest.ValueObjects;
using Xunit;

namespace BluePrism.TechTest.UnitTests
{
    public class WordTreeNodeTests
    {
        [Fact]
        public void GivenList_WhenGetTreeNode_ExpectSpecificPath()
        {
            var list = new[]
            {
                new Word("aide"),
                new Word("dire"),
                new Word("eire"),
                new Word("bide"),
                new Word("fide"),
                new Word("fire"),
                new Word("hike"),
                new Word("hire"),
                new Word("hide"),
                new Word("hyde"),
                new Word("ride"),
                new Word("side"),
                new Word("site"),
                new Word("size"),
                new Word("sire"),
                new Word("sure"),
                new Word("sore"),
                new Word("sort"),
                new Word("tide"),
                new Word("tire"),
                new Word("vide"),
                new Word("wide"),
                new Word("wire"),
            };

            var wordTreeResult = new WordTreeNodeBuilder(new WordComparer())
                .BuildTree(new Word("hide"), new Word("sort"), list);

            Assert.Equal("hide", wordTreeResult.Item.Value);

            var end = wordTreeResult.GetChild("hire")
                .GetChild("sire")
                .GetChild("sore")
                .GetChild("sort");

            Assert.Equal("sort", end.Item.Value);
        }
    }
}
