namespace KST.Madiff.CardService.Application.Interfaces;
public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}
