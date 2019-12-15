using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentB
{
    public static class ServiceCollectionExtensions
    {
        public static IComponentBBuilder AddComponentB(this IServiceCollection sc)
        {
            if (sc == null)
            {
                throw new ArgumentNullException(nameof(sc));
            }

            var builder = new ComponentBBuilder(sc);

            return builder;
        }
    }
}
