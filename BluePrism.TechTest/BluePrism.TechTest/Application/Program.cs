using BluePrism.TechTest.Application.Extensions;
using BluePrism.TechTest.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace BluePrism.TechTest
{
    class Program
    {
        private const int NumberExpectedArguments = 4;

        static async Task Main(string[] args)
        {
            if (args?.Length != NumberExpectedArguments)
                throw new ArgumentException($"Please add {NumberExpectedArguments} arguments\n" +
                    $"Arguments expected: start word, end word, sourceFileLocation, outputFileLocation.");

            //args = new string[4];
            //var start = args[0] = "abbe";
            //var end = args[1] = "zoom";
            //var sourceLocation = args[2] = "words-english.txt";
            //var outPutLocation = args[3] = "words-english.result.txt";
            var start = args[0];
            var end = args[1];
            var sourceLocation = args[2];
            var outPutLocation = args[3];

            var services = new ServiceCollection()
                .AddMediatR(Assembly.GetExecutingAssembly());

            services
                .AddSingleton(new WordFilesStoredSettings(sourceLocation, outPutLocation))
                .RegisterAppServices()
                .AddLogging(x => x.AddConsole());

            var provider = services.BuildServiceProvider();
            var mediator = provider
                .GetRequiredService<IMediator>();

            var logger = provider
                .GetRequiredService<ILogger<Program>>();

            var watch = new Stopwatch();
            watch.Start();

            logger.LogInformation("Start Application");

            await mediator.Send(new WriteShortestPathToFileCommand(start, end));

            logger.LogInformation($"End Application. Total time {watch.Elapsed.TotalSeconds} seconds");
        }
    }

}
