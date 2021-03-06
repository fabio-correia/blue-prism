using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    public class WriteFasterPathToFileHandler : IRequestHandler<WriteShortestPathToFileCommand, Unit>
    {
        private readonly IWordsRepository _wordsRepository;
        private readonly ISearchWordsPath _searchWordsPath;
        private readonly IDocumentListWriter<Word> _documentListWriter;
        private readonly IWordDistanceComparer _wordComparer;

        public WriteFasterPathToFileHandler(IWordsRepository wordsRepository,
            ISearchWordsPath searchWordsPath,
            IDocumentListWriter<Word> documentListWriter,
            IWordDistanceComparer wordComparer)
        {
            _wordsRepository = wordsRepository;
            _searchWordsPath = searchWordsPath;
            _documentListWriter = documentListWriter;
            _wordComparer = wordComparer;
        }
        public async Task<Unit> Handle(WriteShortestPathToFileCommand request, CancellationToken cancellationToken)
        {
            var words = (await _wordsRepository.GetAll())
                .ToList();

            var start = new Word(request.Start);
            var end = new Word(request.End);

            var wordsTree = new WordTreeNode(start, end, words, _wordComparer);

            var shortestPath = await _searchWordsPath.GetShortestPath(start, end, wordsTree);

            _documentListWriter.WriteText(shortestPath);

            return Unit.Value;
        }
    }
}