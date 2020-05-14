using ES.Gateway.Interfaces.Requests;

namespace ES.Gateway.Interfaces.UseCases
{
    public interface IUseCase<TRequest, TResponse, TView>
        where TRequest : IExchangeRequest
        where TView : class
    {
    }
}
