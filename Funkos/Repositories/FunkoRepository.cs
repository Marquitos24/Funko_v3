using Funkos.Models;

namespace Funkos.Repositories;

public class FunkoRepository : IFunkoRepository
{

    private readonly List<Funko> funkos = new();
    
    public List<Funko> GetAll()
    {
        return funkos;
    }

    public Funko? GetById(long id)
    {
        return funkos.FirstOrDefault(f => f.Id == id);
    }

    public Funko Create(Funko newFunko)
    {
        var funko = new Funko
        {
            Id = newFunko.Id,
            Name = newFunko.Name,
            Precio = newFunko.Precio,
            Categoria = newFunko.Categoria,
            CreateAt = newFunko.CreateAt
        };
        funkos.Add(funko);
        return funko;
    }

    public Funko? Update(long id, Funko updatedFunko)
    {
        var funko = GetById(id);

        if (funko is null) return null;
        
        funko.Name = updatedFunko.Name;
        funko.Precio = updatedFunko.Precio;
        funko.Categoria = updatedFunko.Categoria;
        funko.UpdateAt = DateTime.UtcNow;
        
        return funko;
    }
    
    public bool Delete(long id)
    {
        var funko = GetById(id);
        if (funko is null) return false;

        funkos.Remove(funko);
        return true;
    }
    
}