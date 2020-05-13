namespace ES.Domain.Requests.CryptoCompare
{
    public class MinutePairOHLCVRequest : BaseCryptoCompareRequest
    {
        /// <summary>
        /// Преобразование 
        /// </summary>
        public bool TryConversion { get; set; }

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

        ///// <summary>
        ///// Мин - 1
        ///// </summary>
        //public int Aggregate { get; set; }

        /// <summary>
        /// Лимит (max 2000)
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Timestamp до
        /// </summary>
        public long ToTs { get; set; }
    }
}
