using Funkos.Dto;
using Funkos.Mappers;
using Funkos.Models;
using Funkos.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Funkos.Routes;

public static class FunkosRoutes
{
    public static void MapFunkosRoutes(this WebApplication app)
    {
        FunkoRepository repository = new FunkoRepository();
     
        var group = app.MapGroup("/api/funkos");
        var nextId = 1;

        group.MapGet("/", Results<Ok<List<FunkosDtoResponse>>, NotFound<string>> () =>
        {
            List<Funko> funkos = repository.GetAll();
            
            if (funkos is null)
            {
                return TypedResults.NotFound("No hay funkos");
            }

            List<FunkosDtoResponse> dtos = funkos.Select(f => FunkoMapper.ToDtoResponse(f)).ToList();

            return dtos.Count != 0
                ? TypedResults.Ok(dtos)
                : TypedResults.NotFound("No hay funkos");
        });

        group.MapGet("/{id:long}", Results<Ok<FunkosDtoResponse>, NotFound<string>> (long id) =>
        {
            Funko? funko = repository.GetById(id);
            
            if (funko is null)
            {
                return TypedResults.NotFound("No existe este id de funko");
            }

            var dto = FunkoMapper.ToDtoResponse(funko);

            return dto != null
                ? TypedResults.Ok(dto)
                : TypedResults.NotFound("No hay funkos");
        });

        group.MapPost("/", Results<Created<FunkosDtoResponse>, BadRequest<string>> (FunkosDtoRequest dto) =>
        {
            var funko = new Funko
            {
                Id = nextId++,
                Name = dto.Name,
                Precio = dto.Precio,
                Categoria = dto.Categoria,
                CreateAt = DateTime.UtcNow
            };
            var creado = repository.Create(funko);

            if (creado is null)
                return TypedResults.BadRequest("No es posible crear el funko");

            var response = FunkoMapper.ToDtoResponse(creado);
            
            return TypedResults.Created($"/{funko.Id}", response);
        });

        group.MapPut("/{id:long}", Results<Ok<FunkosDtoResponse>, NotFound<string>> (long id, FunkosDtoRequest dto) =>
        {
            Funko? funko = FunkoMapper.ToEntity(dto);
            var actualizado = repository.Update(id, funko);

            if (actualizado is null)
                return TypedResults.NotFound("No es posible actualizar el funko");

            var response = FunkoMapper.ToDtoResponse(actualizado);

            return TypedResults.Ok(response);
        });

        group.MapDelete("/{id:long}", Results<Ok, NotFound<string>> (long id) =>
        {
            bool deleted = repository.Delete(id);
    
            return deleted 
                ? TypedResults.Ok()
                : TypedResults.NotFound("NO se puede eliminar el funko");
        });
    }

}