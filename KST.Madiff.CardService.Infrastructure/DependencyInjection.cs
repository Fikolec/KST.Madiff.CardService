using KST.Madiff.CardService.Domain.Interfaces.Repositories;
using KST.Madiff.CardService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace KST.Madiff.CardService.Infrastructure;
public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<Services.CardService>();
        services.AddScoped<ICardRepository, CardRepository>();
    }
}
