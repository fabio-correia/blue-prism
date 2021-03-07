using BluePrism.TechTest.Domain;
using BluePrism.TechTest.Settings;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    public class WordsFileRepository : IWordsRepository
    {
        short ALLOWED_CHARACTED_NUMBERS = 4;
        private readonly WordFilesStoredSettings _wordFilesStoredSettings;
        private readonly ILogger<WordsFileRepository> _logger;

        public WordsFileRepository(WordFilesStoredSettings wordFilesStoredSettings, ILogger<WordsFileRepository> logger)
        {
            _wordFilesStoredSettings = wordFilesStoredSettings;
            _logger = logger;
        }

        public async Task<IEnumerable<Word>> GetAll()
        {
            var words = await File.ReadAllLinesAsync(_wordFilesStoredSettings.SourceLocation);
            _logger.LogInformation("Words file read from {destiny}", Path.GetFullPath(_wordFilesStoredSettings.DestinyLocation));

            return words
                .Where(x => x.Length == ALLOWED_CHARACTED_NUMBERS)
                .Select(x => new Word(x));
        }
    }
}