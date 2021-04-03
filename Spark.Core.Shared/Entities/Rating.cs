using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.Shared.Entities
{
    public class Rating<T>
    {
        public int ID { get; set; }
        public int Rate { get; set; }
        public DateTime RatingDate { get; set; }
        public int RatedEntityID { get; set; }
        public T RatedEntity { get; set; }
        public string UserID { get; set; }
    }
}
