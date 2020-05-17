using System.Collections.Generic;

namespace ES.Domain.ApiResults
{
    public class BaseCommandResult
    {
        public List<string> Messages { get; set; }

        public bool IsSuccess { get; set; }
    }
}
