using BluePrism.TechTest.Domain;
using BluePrism.TechTest.Settings;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BluePrism.TechTest
{
    public class WordsFileListWriter : IDocumentListWriter<Word>
    {
        private readonly WordFilesStoredSettings _wordFilesStoredSettings;
        private readonly IDocumentWriter<Word> _documentWriter;
        private readonly ILogger<WordsFileListWriter> _logger;

        public WordsFileListWriter(WordFilesStoredSettings wordFilesStoredSettings, IDocumentWriter<Word> documentWriter
            , ILogger<WordsFileListWriter> logger)
        {
            _wordFilesStoredSettings = wordFilesStoredSettings;
            _documentWriter = documentWriter;
            _logger = logger;
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
            _logger.LogInformation("Words file written on {destinyLocation}", Path.GetFullPath(_wordFilesStoredSettings.DestinyLocation));
        }
    }
}