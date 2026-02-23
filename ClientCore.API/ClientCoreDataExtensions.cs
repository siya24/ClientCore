using Microsoft.EntityFrameworkCore;

namespace ClientCore.API
{
    public static class ClientCoreDataExtensions
    {
        public static async Task MigrateDbAsyncy(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ClientCoreDBContext>();
            await context.Database.MigrateAsync();
        }
    }
}
