using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark.Core.Client.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static Dictionary<string, string> GetQueryStrings(
            this NavigationManager manager, string url)
        {
            if (string.IsNullOrWhiteSpace(url)
                || !url.Contains("?")
                || url.Substring(url.Length - 1) == "?")
                return null;

            var queryStrings = url.Split(new string[] { "?" }, System.StringSplitOptions.None)[1];
            Dictionary<string, string> dictQueryString =
                queryStrings.Split('&').ToDictionary(c => c.Split('=')[0],
                    c => Uri.UnescapeDataString(c.Split('=')[1]));

            return dictQueryString;
        }
    }
}
