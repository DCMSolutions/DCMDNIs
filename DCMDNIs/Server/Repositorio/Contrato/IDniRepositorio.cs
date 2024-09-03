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
        Dni GetHabilitadoByNumero(int numero);
        
        //CRUD Dnis
        List<Dni> GetDnis();
        int GetNextId();
        Dni GetDniById(int id);
        Dni GetDniByNumero(int numero);
        bool AddEditDni(Dni dni);
        bool DeleteDni(int idDni);
    }
}
