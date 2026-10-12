using Funkos.Models;

namespace Funkos.Service;

public interface IFunkoService
{
    List<Funko> GetAll();
    Funko? GetById(long id);
    Funko Add(Funko newFunko);
    Funko? Update(long id, Funko updatedFunko);
    bool Delete(long id);
}