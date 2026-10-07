using Funkos.Models;
using Funkos.Repositories;
using Funkos.Dto;
using Funkos.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Funkos.Controller;

[ApiController]
[Route("api/[controller]")]
public class FunkoController(IFunkoRepository repo) : ControllerBase
{
    
    [HttpGet]
    public ActionResult<List<FunkosDtoResponse>> GetAll()
    {
        List<Funko> funkos = repo.GetAll();

        try
        {
            List<FunkosDtoResponse> dtos = funkos.Select(f => FunkoMapper.ToDtoResponse(f)).ToList();

            return Ok(dtos);
        }
        catch (Exception)
        {
            return StatusCode(500, "Se ha producido un error al obtener los Funkos.");
        }
    }
    
    [HttpGet("{id:long}")]
    public ActionResult<FunkosDtoResponse> GetById([FromRoute] long id)
    {
        if(id <= 0) return BadRequest("No existe ese id");
        
        try
        {
            Funko? funko = repo.GetById(id);
            if (funko is null) return NotFound($"No existe el id {id} de funko");
            FunkosDtoResponse dto = FunkoMapper.ToDtoResponse(funko);

            return Ok(dto);
        }
        catch (Exception)
        {
            return StatusCode(500, $"Se ha producido un error al obtener el Funko {id}.");
        }
    }
    
    [HttpPost]
    public ActionResult<FunkosDtoResponse> Create([FromBody] FunkosDtoRequest dto)
    {
        var request = FunkoMapper.ToEntity(dto);
        if (request is null)  return NotFound("Los datos o la estructura no es correcta");
        
        try
        {
            var creado = repo.Create(request);
        
            var response = FunkoMapper.ToDtoResponse(creado);
        
            return CreatedAtAction(nameof(GetById), new { id = response.Id}, response  ) ;
        }
        catch (Exception)
        {
            return StatusCode(500, "Se ha producido un error al crear el Funko.");

        }
    }
    
    [HttpPut("{id:long}")]
    public ActionResult<FunkosDtoResponse> Update([FromRoute] long id, [FromBody] FunkosDtoRequest dto)
    {
        var funko = FunkoMapper.ToEntity(dto);
        if (funko is null) return BadRequest("Los datos o la estructura no es correcta");
        
        try
        {
            var actualizado = repo.Update(id, funko);

            if (actualizado is null) return NotFound("No es posible actualizar el funko");

            var response = FunkoMapper.ToDtoResponse(actualizado);

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(500, $"Se ha producido un error al actualizar el Funko {id}.");

        }
    }
    
    [HttpDelete("{id:long}")]
    public ActionResult Delete([FromRoute] long id)
    {
        try
        {
            bool deleted = repo.Delete(id);

            return deleted
                ? Ok($"Funko con {id} eliminado")
                : NotFound("NO se puede eliminar el funko");
        }
        catch (Exception)
        {
            return StatusCode(500, $"Se ha producido un error al eliminar el Funko {id}.");

        }
    }
}