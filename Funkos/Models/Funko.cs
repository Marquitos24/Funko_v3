namespace Funkos.Models;

public record Funko
{
    public long Id { get; init; }
    public string Name { get; set; }
    public string Precio  { get; set; }
    public Categoria Categoria { get; set; }
    public DateTime CreateAt {get; init; }
    public DateTime? UpdateAt {get; set; }
    public DateTime? DeleteAt {get; init; }
}