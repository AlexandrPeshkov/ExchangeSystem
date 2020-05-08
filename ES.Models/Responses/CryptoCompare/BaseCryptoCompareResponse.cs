namespace ES.Domain.Responses.CryptoCompare
{
    public class BaseCryptoCompareResponse<TData>
    {
        public string Response { get; set; }

        public string Message { get; set; }

        public TData Data { get; set; }

        public bool IsSuccess => Response?.ToLower() == "success";
    }
}
