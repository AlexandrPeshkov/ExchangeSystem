using System.Collections.Generic;

namespace ES.Domain.Interfaces
{
    public interface ISmaAnalyzable<TOutput>
    {
        IReadOnlyList<TOutput> ComputeSma(int periodCount, int? startIndex = null, int? endIndex = null);

        IReadOnlyList<TOutput> ComputeSma(int periodCount, IEnumerable<int> indexes);

        (TOutput Prev, TOutput Current, TOutput Next) ComputeNeighbourSma(int periodCount, int index);

        TOutput Sma(int periodCount, int index);
    }
}
