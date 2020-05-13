namespace ES.Domain.Tests.UseCases.CryptoCompare
{
    public abstract class BaseCryptoCompareUseCaseTest : BaseUseCaseTest
    {
        protected override string HostName => "min-api.cryptocompare.com";

        public BaseCryptoCompareUseCaseTest()
        {

        }
    }
}
