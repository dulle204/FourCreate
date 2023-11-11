namespace FourCreate.Data.Models;
public abstract class BaseEntity
{
    public int Id { get; set; }
    public long CreatedAt { get; set; }
}
