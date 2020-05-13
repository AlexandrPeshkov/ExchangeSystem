namespace ES.DataImport.Requests.CryptoCompare
{
    public class MinuteCandleRequest : BaseCryptoCompareRequest
    {
        /// <summary>
        /// Преобразование 
        /// </summary>
        public bool TryConversion { get; set; } = false;

        /// <summary>
        /// Лимит (max 2000)
        /// </summary>
        public int Limit { get; set; } = 2000;

        /// <summary>
        /// From символ
        /// </summary>
        public string fsym { get; set; }

        /// <summary>
        /// To символ
        /// </summary>
        public string tsym { get; set; }

        /// <summary>
        /// Биржа
        /// </summary>
        public string E { get; set; }

        /// <summary>
        /// Timestamp до
        /// </summary>
        public long ToTs { get; set; }

        public MinuteCandleRequest(BaseCryptoCompareRequest baseRequest = null)
        {
            Api_Key = baseRequest?.Api_Key;
            ExtraParams = baseRequest?.ExtraParams;
        }
    }
}
