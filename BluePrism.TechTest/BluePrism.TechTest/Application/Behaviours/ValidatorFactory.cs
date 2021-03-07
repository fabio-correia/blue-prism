using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrism.TechTest.Application.Behaviours
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider _provider;

        public ValidatorFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _provider.GetService(validatorType) as IValidator;
        }
    }
}
