using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DCMDNIs.Shared;
using System.Linq.Expressions;
using DCMDNIs.Shared.Models;
using DCMDNIs.Server.Models;

namespace DCMDNIs.Server.Repositorio.Contrato
{
    public interface IDniRepositorio
    {
        //funciones
        Task<Dni> GetHabilitadoByNumero(int numero);
        
        //CRUD Dnis
        Task<List<Dni>> GetDnis();
        Task<Dni> GetDniById(int id);
        Task<Dni> GetDniByNumero(int numero);
        Task<bool> AddDni(Dni dni);
        Task<bool> EditDni(Dni dni);
        Task<bool> DeleteDni(int idDni);
    }
}
