using System;
using System.Threading.Tasks;
using AutoMapper;
using ES.Domain;
using ES.Domain.ApiResults;
using ES.Domain.Interfaces;
using ES.Infrastructure.Interfaces;

namespace ES.Infrastructure.UseCases
{
    public abstract class BaseContextUseCase<TCommand, TView> : IContextUseCase<TCommand, TView>
        where TCommand : IApiCommand
        where TView : class
    {
        protected readonly CoreDBContext _context;

        protected readonly IMapper _mapper;

        protected readonly CommandResult<TView> _commandResult;

        protected virtual string OkMessage { get; } = "Success";

        public BaseContextUseCase(CoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _commandResult = new CommandResult<TView>(isSuccess: false);
        }

        public virtual async Task<CommandResult<TView>> Execute(TCommand command)
        {
            try
            {
                TView view = await CommandExecute(command);
                if(view!=null)
                {
                    OkResult(view, OkMessage);
                }
            }
            catch(Exception ex)
            {
                ErrorResult();
            }
            return _commandResult;
        }

        protected abstract Task<TView> CommandExecute(TCommand command);

        protected CommandResult<TView> OkResult(TView content, string message = null)
        {
            _commandResult.Content = content;
            if (string.IsNullOrEmpty(message) == false)
            {
                _commandResult.Messages.Add(message);
            }
            _commandResult.IsSuccess = true;
            return _commandResult;
        }

        protected CommandResult<TView> ErrorResult(string message = null)
        {
            if (string.IsNullOrEmpty(message) == false)
            {
                _commandResult.Messages.Add(message);
            }
            _commandResult.IsSuccess = false;
            return _commandResult;
        }
    }
}
