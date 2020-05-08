using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ES.Domain.Interfaces;
using ES.Domain.Period;

namespace ES.DataImport.Interfaces
{
    public interface IImporter
    {
        // startTime & endTime should be inclusive
        Task<IReadOnlyList<IOhlcv>> ImportAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, PeriodOption period = PeriodOption.Daily, CancellationToken token = default(CancellationToken));
    }
}
