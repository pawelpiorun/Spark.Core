using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Spark.Core.Server.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddSparkCore(this IMvcBuilder builder,
            IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.Services.AddSparkCoreAuthentication(configuration);
            builder.AddApplicationPart(assembly);

            return builder;
        }
    }
}
