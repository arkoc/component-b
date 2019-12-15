using Microsoft.Extensions.DependencyInjection;
using System;

namespace ComponentB
{
    public interface IComponentBBuilder
    {
        IServiceCollection ServiceCollection { get; }
    }
}
