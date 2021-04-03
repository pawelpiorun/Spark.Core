using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static Type GetIDType(this object obj)
        {
            if (obj is null) return null;

            var entityType = obj.GetType();
            var interfaces = entityType?.GetInterfaces();
            var genericType = interfaces?.FirstOrDefault(t => t.IsGenericType && t.Name.Contains("IWithID"));
            return genericType?.GenericTypeArguments?.FirstOrDefault();
        }
    }
}
