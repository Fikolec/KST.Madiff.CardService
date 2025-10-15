using KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
using KST.Madiff.CardService.Application.Validators;
using KST.Madiff.CardService.Domain.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace KST.Madiff.CardService.Application;
public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        //Validators
        services.AddSingleton<GetAllowedCardActionsValidator>();

        //UseCases
        services.AddScoped<IGetAllowedCardActionsUseCase, GetAllowedCardActionsUseCase>();

        //Policies
        services.AddScoped<AllowedCardActionsPolicy>();
    }
}
