using BluePrism.TechTest.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            var start = args[0] = "Spin";
            var end = args[1] = "Spot";
            var sourceLocation = args[2] = "words-english.txt";
            var outPutLocation = args[3] = "words-english.result.txt";

            var services = new ServiceCollection()
                .AddMediatR(Assembly.GetExecutingAssembly());

            services
                .AddSingleton(new WordFilesStoredSettings(sourceLocation, outPutLocation))
                .AddScoped<IWordsRepository, WordsFileRepository>()
                .AddScoped<ISearchWordsPath, SearchWordsPath>()
                //.AddScoped<ISorterWords, SorterWords>()
                .AddScoped<IDocumentListWriter<Word>, WordsFileListWriter>()
                .AddScoped<IWordDistanceComparer, WordComparer>()
                .AddScoped<IDocumentWriter<Word>, WordsWriter>();

            var mediator = services.BuildServiceProvider()
                .GetRequiredService<IMediator>();

            mediator.Send(new WriteShortestPathToFileCommand(start, end))
                .GetAwaiter().GetResult();

        }
    }
}
