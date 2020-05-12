using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.DataImport.StockExchangeGateways;
using ES.Domain;
using ES.Domain.Entities;

namespace ES.Infrastructure.Services
{
    public class ImportMetaDataService
    {
        private readonly CryptoCompareGateway _cryptoCompareGateway;

        private readonly CoreDBContext _coreDBContext;

        public ImportMetaDataService(CryptoCompareGateway cryptoCompareGateway, CoreDBContext coreDBContext)
        {
            _cryptoCompareGateway = cryptoCompareGateway;
            _coreDBContext = coreDBContext;
        }

        public async Task ImportAllCurrencies()
        {
            List<Currency> currencies = await _cryptoCompareGateway.ImportAllCurrencies();
            if (currencies?.Any() == true)
            {
                await _coreDBContext.AddRangeAsync(currencies);
                await _coreDBContext.SaveChangesAsync();
            }
        }
    }
}
