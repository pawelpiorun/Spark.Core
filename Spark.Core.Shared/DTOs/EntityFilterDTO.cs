using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.Shared.DTOs
{
    public abstract class EntityFilterDTO<TFilter>
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public PaginationDTO Pagination => new PaginationDTO() { Page = Page, ItemsPerPage = ItemsPerPage };
        
        public TFilter Filter { get; set; }
    }
}
