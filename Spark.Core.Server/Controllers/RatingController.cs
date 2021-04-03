using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spark.Core.Shared.Entities;
using System;
using System.Threading.Tasks;

namespace Spark.Core.Server.Controllers
{
    [ApiController]
    [Route("api/rating")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class RatingController<T> : ControllerBase
    {
        private readonly DbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public RatingController(DbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Rate(Rating<T> rating)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            var currentRating = await context.Set<Rating<T>>()
                .FirstOrDefaultAsync(r => r.RatedEntityID == rating.RatedEntityID
                && r.UserID == rating.UserID);

            if (currentRating is null)
            {
                rating.UserID = userId;
                rating.RatingDate = DateTime.Today;
                context.Add(rating);
                await context.SaveChangesAsync();
            }
            else
            {
                currentRating.Rate = rating.Rate;
                await context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
