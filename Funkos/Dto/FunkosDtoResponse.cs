using Funkos.Models;

namespace Funkos.Dto;

public class FunkosDtoResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Precio { get; set; }
    public Categoria Categoria { get; set; }
    public DateTime CreateAt {get; init; }
    public DateTime? UpdateAt {get; init; }
    public DateTime? DeleteAt {get; init; }
}