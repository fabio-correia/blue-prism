using BluePrism.TechTest.Domain;
using Xunit;

namespace BluePrism.TechTest.UnitTests
{
    public class WordsListWriterTests
    {
        [Fact]
        public void GivenList_ThenWriteList_ShouldExpectCorrecPrintt()
        {
            var writer = new WordsWriter();

            var result = writer.GetText(new Word("hey"));

            Assert.Equal("hey", result);
        }
    }
}
