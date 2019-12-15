using Microsoft.Extensions.DependencyInjection;
using System;

namespace ComponentB
{
    public class ComponentBBuilder : IComponentBBuilder
    {
        public IServiceCollection ServiceCollection { get; }

        public ComponentBBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
        }
    }
}
