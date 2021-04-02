using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.DTOs
{
    public class PaginatedResponse<T>
    {
        public T Response { get; set; }
        public int TotalPages {get; set; }
    }
}
