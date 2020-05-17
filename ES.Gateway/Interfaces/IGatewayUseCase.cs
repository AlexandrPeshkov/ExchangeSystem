using System;
using System.Threading.Tasks;
using ES.Domain.ApiResults;
using ES.Gateway.Interfaces.Requests;

namespace ES.Gateway.Interfaces.UseCases
{
    public interface IGatewayUseCase<TRequest, TResponse, TView>
        where TRequest : IExchangeRequest
        where TView : class
    {
        Task<CommandResult<TView>> Execute(TRequest request, UriBuilder uriBuilder);
    }
}
