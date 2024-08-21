using DCMDNIs.Server.Models;
using DCMDNIs.Server.Repositorio.Contrato;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;

namespace DCMDNIs.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepositorio _consulta;

        public ConsultaController(IConsultaRepositorio consulta)
        {
            _consulta = consulta;
        }
        //El CRUD
        [HttpGet]
        public async Task<IActionResult> GetConsultas()
        {
            try
            {
                var response = await _consulta.GetConsultas();
                Console.WriteLine("resp" + response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
