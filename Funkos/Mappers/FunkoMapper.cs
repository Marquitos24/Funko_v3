using Funkos.Dto;
using Funkos.Models;

namespace Funkos.Mappers;

public class FunkoMapper
{
    public static Funko? ToEntity(FunkosDtoRequest request)
    {
        return new Funko
        {
            Name = request.Name,
            Precio = request.Precio,
            Categoria = request.Categoria
        };
    }
   
    
    public static FunkosDtoResponse ToDtoResponse(Funko funko)
    {
        return new FunkosDtoResponse
        {
            Id = funko.Id,
            Name = funko.Name,
            Precio = funko.Precio,
            Categoria = funko.Categoria,
            CreateAt = funko.CreateAt,
            UpdateAt = funko.UpdateAt,
            DeleteAt =  funko.DeleteAt
        };
    }
}