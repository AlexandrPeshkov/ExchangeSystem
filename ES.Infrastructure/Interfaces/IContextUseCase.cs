using System.Threading.Tasks;
using ES.Domain.ApiResults;
using ES.Domain.Interfaces;

namespace ES.Infrastructure.Interfaces
{
    public interface IContextUseCase<TCommand, TView>
        where TCommand : IApiCommand
        where TView : class
    {
        Task<CommandResult<TView>> Execute(TCommand command);
    }
}
