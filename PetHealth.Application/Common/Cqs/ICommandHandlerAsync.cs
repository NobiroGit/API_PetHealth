using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Common.Cqs;

public interface ICommandHandlerAsync<TCommand> 
    where TCommand: ICommandDefinitionAsync
{
    Task<Result> Execute(TCommand command);
}

public interface ICommandHandlerAsync<TCommand, TResult>
    where TCommand : ICommandDefinitionAsync<TResult>
{
    Task<TResult> Execute(TCommand command);
}