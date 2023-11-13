namespace FourCreate.Domain.Models;
public record DomainResult<T> where T : class
{
    public T Entity { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string ErrorMessage { get; set; } = null;

    public DomainResult(T entity)
    {
        Entity = entity;
    }

    public DomainResult(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
