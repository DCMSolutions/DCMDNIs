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
    public class DniController : ControllerBase
    {

        private readonly IDniRepositorio _dni;

        public DniController(IDniRepositorio dni)
        {
            _dni = dni;
        }

        //funciones
        [HttpGet("habilitado/{numero:int}")]
        public async Task<IActionResult> GetHabilitadoByNumero(int numero)
        {
            try
            {
                var response = await _dni.GetHabilitadoByNumero(numero);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //El CRUD
        [HttpGet]
        public async Task<IActionResult> GetDnis()
        {
            try
            {
                var response = await _dni.GetDnis();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDniById(int id)
        {
            try
            {
                var response = await _dni.GetDniById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("numero/{numero:int}")]
        public async Task<IActionResult> GetDniByNumero(int numero)
        {
            try
            {
                var response = await _dni.GetDniByNumero(numero);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDni([FromBody] Dni dni)
        {
            try
            {
                var response = await _dni.AddDni(dni);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditDni([FromBody] Dni dni)
        {
            try
            {
                var response = await _dni.EditDni(dni);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idDni:int}")]
        public async Task<IActionResult> DeleteDni(int idDni)
        {
            try
            {
                var response = await _dni.DeleteDni(idDni);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
