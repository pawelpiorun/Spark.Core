using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.Shared.DTOs
{
    public class FilterOperationDTO
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public PaginationDTO Pagination => new PaginationDTO() { Page = Page, ItemsPerPage = ItemsPerPage };
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public bool Expense { get; set; } = true;
        public bool Income { get; set; } = true;
    }
}
