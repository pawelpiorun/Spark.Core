using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Core.Client.Components
{

    public partial class MultipleSelector
    {

    }

    public struct MultipleSelectorModel
    {
        public MultipleSelectorModel(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }

}
