using AutoMapper;
using ES.Domain.Configurations;

namespace ES.DataImport.StockExchangeGateways
{
    public class CryptoApiGateway : BaseExchangeGateway
    {
        public CryptoApiGateway(IMapper mapper) : base(mapper)
        {
        }

        protected override string HostName => "api.cryptoapis.io/v1/";

        protected override RequestLimitConfiguration Limits => new RequestLimitConfiguration
        {
            SecondLimit = 3,
            DayLimit = 500,
        };
    }
}
