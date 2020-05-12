using ES.Domain.Interfaces.Requests;

namespace ES.Domain.Interfaces.UseCases
{
    public interface IUseCase<TRequest, TResponse, TView>
        where TResponse : class
        where TRequest : IExchangeRequest
    {
    }
}
