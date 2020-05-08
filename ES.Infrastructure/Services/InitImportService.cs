using System.Threading.Tasks;
using ES.DataImport.StockExchangeGateways;

namespace ES.Infrastructure.Services
{
    public class InitImportService
    {
        private readonly CryptoCompareGateway _cryptoCompareGateway;
        public InitImportService(CryptoCompareGateway cryptoCompareGateway)
        {
            _cryptoCompareGateway = cryptoCompareGateway;
            InitDBExchanges().Start();
        }

        private async Task InitDBExchanges()
        {
            var all = await _cryptoCompareGateway.ImportAllCurrencies();
        }
    }
}
