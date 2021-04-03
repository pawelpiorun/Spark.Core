using Spark.Core.Client.Services;
using Spark.Core.Shared.Entities;
using System;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public class RatingRepository<T> : IRatingRepository<T>
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/rating";

        public RatingRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task Vote(Rating<T> rating)
        {
            var response = await httpService.Post(url, rating);
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }
    }
}
