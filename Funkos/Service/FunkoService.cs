using Funkos.Models;
using Funkos.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Funkos.Service;

public class FunkoService(IFunkoRepository repository) : IFunkoService
{
    public List<Funko> GetAll()
    {
        List<Funko> funkos = repository.GetAll();
        return funkos;
    }
    
    public Funko? GetById(long id)
    {
        Funko? funko = repository.GetById(id);
        return funko;
    }
    
    public Funko Add(Funko  newFunko)
    {
        Funko funko = repository.Create(newFunko);
        return funko;
    }
    
    public Funko? Update(long id, Funko updatedFunko)
    {
        Funko? funko = repository.Update(id, updatedFunko);
        if (funko == null) throw new ArgumentException($"No existe el Funko");;
        return funko;
    }
    
    public bool Delete(long id)
    {
        var eliminado =  repository.Delete(id);
        return eliminado;
    }
    
}