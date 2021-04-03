using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.Shared.DTOs
{
    public class RatedEntityDTO<TRated>
    {
        public int Rating { get; set; }
        public double AverageRating { get; set; }
        public TRated RatedEntity { get; set; }
    }
}
