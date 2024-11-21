using Application.Specifications.Facturas;
using Application.Specifications.Kanban.Sistemas;
using Ardalis.Specification;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    /*
     * IRepositoryBase(Ardalis), nos permite manejar de manera sencilla la gestion de la 
     * base de datos generico.
     * Esta interfaz generica nos permite implementar dicho repositorio.
     */
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
        
    }

    /*
     * IReadRepositoryBase(Ardalis), nos permite leer una base de datos de forma generica.
     * Esta interfaz generica nos permite implementar dicho repositorio.
     * 
     */
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class
    {

    }
}
