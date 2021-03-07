using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BluePrism.TechTest.Application.Behaviours
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _validatorFactory;
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

        public ValidatorBehavior(IValidatorFactory validatorFactory, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validatorFactory = validatorFactory;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetType().Name;

            _logger.LogInformation("----- Validating command {CommandType}", typeName);

            IValidator<TRequest> validator = _validatorFactory.GetValidator<TRequest>();
            if (validator != null)
            {
                var result = validator.Validate(request);
                if (!result.IsValid)
                {
                    var failures = result.Errors;
                    var sb = new StringBuilder();
                    if (failures.Any())
                    {
                        _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);

                        failures.ToList()
                            .ForEach(x => sb.AppendLine(x.ErrorMessage));

                        throw new Exception(sb.ToString());
                    }
                }
            }

            return await next();
        }
    }
}
