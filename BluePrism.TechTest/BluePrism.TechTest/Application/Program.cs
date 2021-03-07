using BluePrism.TechTest.Business.Services;
using BluePrism.TechTest.Domain;
using BluePrism.TechTest.Infrastruture;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BluePrism.TechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args?.Length != 4)
            //    throw new ArgumentException("Please add 4 arguments(start,end, sourceFileLocation, outputFileLocation).");
            args = new string[4];
            var start = args[0] = "abbe";
            var end = args[1] = "zoom";
            var sourceLocation = args[2] = "words-english.txt";
            var outPutLocation = args[3] = "words-english.result.txt";

            var services = new ServiceCollection()
                .AddMediatR(Assembly.GetExecutingAssembly());

            services
                .AddSingleton(new WordFilesStoredSettings(sourceLocation, outPutLocation))
                .AddScoped<IWordsRepository, WordsFileRepository>()
                .AddScoped<ISearchShortestPath<Word>, SearchWordsPath>()
                //.AddScoped<ISorterWords, SorterWords>()
                .AddScoped<IDocumentListWriter<Word>, WordsFileListWriter>()
                .AddScoped<ITreeNodeBuilder<Word>, WordTreeNodeBuilder>()
                .AddScoped<IWordDistanceComparer, WordComparer>()
                .AddScoped<IDocumentWriter<Word>, WordsWriter>();

            var mediator = services.BuildServiceProvider()
                .GetRequiredService<IMediator>();

            mediator.Send(new WriteShortestPathToFileCommand(start, end))
                .GetAwaiter().GetResult();

        }
    }
}
