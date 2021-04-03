using Spark.Core.Shared.Entities;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public interface IRatingRepository<T>
    {
        Task Vote(Rating<T> rating);
    }
}
