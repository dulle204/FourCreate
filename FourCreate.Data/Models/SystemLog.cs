namespace FourCreate.Data.Models;
public class SystemLog : BaseEntity
{
    public string ResourceType { get; set; }
    public string ResourceIdentifier { get; set; }
    public string Event { get; set; }
    public string ResourceChangeset { get; set; }
    public string Comment { get; set; }
}
