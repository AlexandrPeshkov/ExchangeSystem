using System.Collections.Generic;
using ES.Domain.Interfaces;

namespace ES.Infrastructure.Interfaces
{
    public interface IRuleExecutor<TInput, TIndexed, TOutput> where TIndexed : IIndexedObject<TInput>
    {
        IReadOnlyList<TOutput> Execute(int? startIndex = null, int? endIndex = null);
    }
}
