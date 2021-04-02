using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Spark.Core.Server.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task SaveChangesWithIdentityInsertAsync<T>(
            this DbContext context)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            await context.EnableIdentityInsert<T>();
            await context.SaveChangesAsync();
            await context .DisableIdentityInsert<T>();
            await transaction.CommitAsync();
        }

        public static async Task EnableIdentityInsert<T>(this DbContext context)
        {
            await SetIdentityInsert<T>(context, enable: true);
        }

        public static async Task DisableIdentityInsert<T>(this DbContext context)
        {
            await SetIdentityInsert<T>(context, enable: false);
        }

        private static async Task SetIdentityInsert<T>(DbContext context, bool enable)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var value = enable ? "ON" : "OFF";
            await context.Database.ExecuteSqlRawAsync(
                $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
        }
    }
}
