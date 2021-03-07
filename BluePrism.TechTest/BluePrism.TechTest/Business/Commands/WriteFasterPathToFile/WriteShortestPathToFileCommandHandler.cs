using BluePrism.TechTest.Business.Services;
using BluePrism.TechTest.Domain;
using BluePrism.TechTest.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<WriteFasterPathToFileHandler> _logger;

        public WriteFasterPathToFileHandler(IWordsRepository wordsRepository,
            ISearchShortestPath<Word> searchWordsPath,
            IDocumentListWriter<Word> documentListWriter,
            ITreeNodeBuilder<Word> treeNodeBuilder,
            ILogger<WriteFasterPathToFileHandler> logger)
        {
            _wordsRepository = wordsRepository;
            _searchWordsPath = searchWordsPath;
            _documentListWriter = documentListWriter;
            _treeNodeBuilder = treeNodeBuilder;
            _logger = logger;
        }
        public async Task<Unit> Handle(WriteShortestPathToFileCommand request, CancellationToken cancellationToken)
        {
            var words = (await _wordsRepository.GetAll())
                .ToList();
            
            _logger.LogDebug($"Find {words.Count} words");

            var start = new Word(request.Start);
            var end = new Word(request.End);

            _logger.LogDebug($"Build tree node");
            var wordsTree = _treeNodeBuilder.BuildTree(start, end, words);

            _logger.LogDebug($"search shortest path");
            var shortestPath = await _searchWordsPath
                .GetShortestPath(start, end, wordsTree);

            _logger.LogDebug($"write file output");
            _documentListWriter.WriteText(shortestPath);
            

            return Unit.Value;
        }
    }
}