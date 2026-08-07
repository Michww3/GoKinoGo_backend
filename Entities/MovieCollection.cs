namespace GoKinoGo.Entities;

public class MovieCollection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CollectionType Type { get; set; }
    public bool IsActive { get; set; }
    public ICollection<CollectionItem> Items { get; set; } = [];
}
