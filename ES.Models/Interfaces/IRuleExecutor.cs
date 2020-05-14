using System.Collections.Generic;

namespace ES.Domain.Interfaces
{
    public interface IRuleExecutor<TInput, TIndexed, TOutput> where TIndexed : IIndexedObject<TInput>
    {
        IReadOnlyList<TOutput> Execute(int? startIndex = null, int? endIndex = null);
    }
}
