using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BluePrism.TechTest.UnitTests
{
    public class SearchWordsPathTests
    {
        [Fact]
        public async Task GivenTreeNode_WhenGetShortestPath_ExpectSpecificPath()
        {
            var wordTree = new WordTreeNode(new Word("side"))
                .AddChildren(
                    new WordTreeNode(new Word("size"))
                ).AddChildren(
                    new WordTreeNode(new Word("tize"))
                    .AddChildren(
                        new WordTreeNode(new Word("ture"))
                    )
                    .AddChildren(
                        new WordTreeNode(new Word("tore"))
                        .AddChildren(
                            new WordTreeNode(new Word("sore"))
                            .AddChildren(
                                new WordTreeNode(new Word("sort"))
                            )
                        )
                    )
                ).AddChildren(
                    new WordTreeNode(new Word("sode"))
                    .AddChildren(
                        new WordTreeNode(new Word("sodt"))
                        .AddChildren(
                            new WordTreeNode(new Word("sort"))
                        )
                    )
                );

            var shortestPath = await new SearchWordsPath()
                .GetShortestPath(new Word("side"), new Word("sort"), wordTree);

            Assert.Equal(4, shortestPath.Count());
            Assert.Equal("side", shortestPath.ElementAt(0).Value);
            Assert.Equal("sode", shortestPath.ElementAt(1).Value);
            Assert.Equal("sodt", shortestPath.ElementAt(2).Value);
            Assert.Equal("sort", shortestPath.ElementAt(3).Value);
        }
    }
}
