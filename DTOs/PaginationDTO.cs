using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
    }
}
