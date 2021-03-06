using BluePrism.TechTest.Domain;

namespace BluePrism.TechTest
{
    public class WordsWriter : IDocumentWriter<Word>
    {
        public string GetText(Word word)
        {
            if (word is null) throw new System.ArgumentNullException(nameof(word));

            return $"{word.Value}";
        }
    }
}