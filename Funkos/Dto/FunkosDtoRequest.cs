using Funkos.Models;

namespace Funkos.Dto;

public class FunkosDtoRequest
{
    public string Name { get; set; } = string.Empty;
    public string Precio { get; set; } = string.Empty;
    public Categoria Categoria { get; set; }
}