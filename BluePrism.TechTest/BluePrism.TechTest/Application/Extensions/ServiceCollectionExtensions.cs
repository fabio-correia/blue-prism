using BluePrism.TechTest.Application.Behaviours;
using BluePrism.TechTest.Business.Services;
using BluePrism.TechTest.Domain;
using BluePrism.TechTest.Infrastruture;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BluePrism.TechTest.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddScoped<IValidatorFactory, ValidatorFactory>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services
                    .AddScoped<IWordsRepository, WordsFileRepository>()
                    .AddScoped<ISearchShortestPath<Word>, SearchWordsPath>()
                    .AddScoped<IDocumentListWriter<Word>, WordsFileListWriter>()
                    .AddScoped<ITreeNodeBuilder<Word>, WordTreeNodeBuilder>()
                    .AddScoped<IWordDistanceComparer, WordComparer>()
                    .AddScoped<IDocumentWriter<Word>, WordsWriter>();

            return services;
        }
    }
}
