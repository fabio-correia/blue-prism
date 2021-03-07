using BluePrism.TechTest.Business.Services;
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
        private readonly ISearchShortestPath<Word> _searchWordsPath;
        private readonly IDocumentListWriter<Word> _documentListWriter;
        private readonly ITreeNodeBuilder<Word> _treeNodeBuilder;

        public WriteFasterPathToFileHandler(IWordsRepository wordsRepository,
            ISearchShortestPath<Word> searchWordsPath,
            IDocumentListWriter<Word> documentListWriter,
            ITreeNodeBuilder<Word> treeNodeBuilder)
        {
            _wordsRepository = wordsRepository;
            _searchWordsPath = searchWordsPath;
            _documentListWriter = documentListWriter;
            _treeNodeBuilder = treeNodeBuilder;
        }
        public async Task<Unit> Handle(WriteShortestPathToFileCommand request, CancellationToken cancellationToken)
        {
            var words = (await _wordsRepository.GetAll())
                .ToList();

            var start = new Word(request.Start);
            var end = new Word(request.End);

            var wordsTree = _treeNodeBuilder.BuildTree(start, end, words);

            var shortestPath = await _searchWordsPath.GetShortestPath(start, end, wordsTree);

            _documentListWriter.WriteText(shortestPath);

            return Unit.Value;
        }
    }
}