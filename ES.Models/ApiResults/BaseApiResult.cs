using System.Collections.Generic;

namespace ES.Domain.ApiResults
{
    public class BaseApiResult
    {
        public List<string> Messages { get; set; }

        public bool IsSuccess { get; set; }
    }
}
