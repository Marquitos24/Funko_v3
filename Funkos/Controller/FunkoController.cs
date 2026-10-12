using Funkos.Models;
using Funkos.Repositories;
using Funkos.Dto;
using Funkos.Mappers;
using Funkos.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Funkos.Controller;

[ApiController]
[Route("api/[controller]")]
public class FunkoController(IFunkoService service) : ControllerBase
{

    [HttpGet]
    public ActionResult<List<FunkosDtoResponse>> GetAll()
    {
       var resultado = service.GetAll();

        try
        {
            List<FunkosDtoResponse> dtos = resultado.Select(f => FunkoMapper.ToDtoResponse(f)).ToList();

            return Ok(dtos);
        }
        catch (Exception)
        {
            return StatusCode(500, "Se ha producido un error al obtener los Funkos.");
        }
    }
    
    [HttpGet("{id:long}")]
    public ActionResult<FunkosDtoResponse?> GetById([FromRoute] long id)
    {
        if(id <= 0) return BadRequest("No existe ese id");
        
        try
        {
            var funko = service.GetById(id);
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
        
        try
        {
            var creado = service.Add(request);
        
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
        
        try
        {
            var actualizado = service.Update(id, funko);

            if (actualizado is null) return NotFound("No es posible actualizar el funko");

            var response = FunkoMapper.ToDtoResponse(actualizado);

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(500, $"Se ha producido un error al actualizar el Funko con id {id}.");
        }
    }
    
    [HttpDelete("{id:long}")]
    public ActionResult Delete([FromRoute] long id)
    {
        try
        {
            bool deleted = service.Delete(id);

            return deleted
                ? Ok($"Funko con ID {id} eliminado")
                : NotFound("NO se puede eliminar el funko");
        }
        catch (Exception)
        {
            return StatusCode(500, $"Se ha producido un error al eliminar el Funko {id}.");

        }
    }
}