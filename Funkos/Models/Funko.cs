namespace Funkos.Models;

public record Funko
{
    public long Id { get; init; }
    public required  string Name { get; set; }
    public required  string Precio  { get; set; }
    public required  Categoria Categoria { get; set; }
    public DateTime CreateAt {get; init; }
    public DateTime? UpdateAt {get; set; }
    public DateTime? DeleteAt {get; init; }
}