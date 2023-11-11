namespace FourCreate.Domain;
public class DomainResult<T> where T : class
{
    public T Entity { get; set; }
    public bool IsSuccess { get; set; } = false;
    public string ErrorMessage { get; set; } = null;

    public DomainResult(T entity)
    {
        Entity = entity;
    }
}
