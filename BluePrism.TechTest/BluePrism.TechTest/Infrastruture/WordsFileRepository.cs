using BluePrism.TechTest.Domain;
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

        public WordsFileRepository(WordFilesStoredSettings wordFilesStoredSettings)
        {
            _wordFilesStoredSettings = wordFilesStoredSettings;
        }

        public async Task<IEnumerable<Word>> GetAll()
        {
            var words = await File.ReadAllLinesAsync(_wordFilesStoredSettings.SourceLocation);

            return words
                .Where(x => x.Length == ALLOWED_CHARACTED_NUMBERS)
                .Select(x => new Word(x));
        }
    }
}