using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DCMDNIs.Shared;
using System.Linq.Expressions;
using DCMDNIs.Shared.Models;
using DCMDNIs.Server.Models;

namespace DCMDNIs.Server.Repositorio.Contrato
{
    public interface IConsultaRepositorio
    {
        //CRUD Consultas
        Task<List<Consulta>> GetConsultas();
        Task<bool> AddConsulta(Consulta consulta);
        Task<bool> DeleteAllConsultas();
    }
}
