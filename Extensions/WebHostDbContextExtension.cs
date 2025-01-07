using Microsoft.EntityFrameworkCore;

namespace BiteBox.Backend.Extensions;

public static class WebHostDbContextExtension
{
    public static async Task PrepareDbContext(this IHost webHost)
    {
        using (var scope = webHost.Services.CreateScope())
        {
            using (var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                await applicationDbContext.Database.MigrateAsync();
            }
        }
    }
}