using BluePrism.TechTest.Domain;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BluePrism.TechTest
{
    public class WordsFileListWriter : IDocumentListWriter<Word>
    {
        private readonly WordFilesStoredSettings _wordFilesStoredSettings;
        private readonly IDocumentWriter<Word> _documentWriter;

        public WordsFileListWriter(WordFilesStoredSettings wordFilesStoredSettings, IDocumentWriter<Word> documentWriter)
        {
            _wordFilesStoredSettings = wordFilesStoredSettings;
            _documentWriter = documentWriter;
        }
        public void WriteText(IEnumerable<Word> words)
        {
            if (words is null) throw new System.ArgumentNullException(nameof(words));

            var sb = new StringBuilder();
            foreach (var word in words)
            {
                sb.AppendLine(_documentWriter.GetText(word));
            }

            File.WriteAllTextAsync(_wordFilesStoredSettings.DestinyLocation, sb.ToString());
        }
    }
}