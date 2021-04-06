using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Spark.Core.Server
{
    public class SparkDbContext : IdentityDbContext
    {
        public SparkDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
