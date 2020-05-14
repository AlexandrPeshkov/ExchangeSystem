using System.Collections.Generic;

namespace ES.Domain.ApiResults
{
    public class CommandResult<TContent> : BaseApiResult
    {
        public TContent Content { get; set; }
        public CommandResult(TContent content = default, List<string> messages = null, bool isSuccess = true)
        {
            Messages = messages ?? new List<string>();
            Content = content;
            IsSuccess = isSuccess;        
        }
    }
}
