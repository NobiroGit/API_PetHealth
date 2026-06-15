namespace PetHealth.Application.Common.Cqs;

public interface IQueryHandlerAsync<TQuery, TResult> 
    where TQuery: IQueryDefinitionAsync<TResult>
{
    Task<TResult> Execute(TQuery query);
}