﻿using DCMDNIs.Server.Models;
using DCMDNIs.Server.Repositorio.Contrato;
using DCMDNIs.Shared.Models;
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
        private readonly IConsultaRepositorio _consulta;

        public DniController(IDniRepositorio dni, IConsultaRepositorio consulta)
        {
            _dni = dni;
            _consulta = consulta;
        }

        //funciones
        [HttpGet("habilitado/{numero:int}")]
        public async Task<IActionResult> GetHabilitadoByNumero(int numero)
        {
            try
            {
                var response = _dni.GetHabilitadoByNumero(numero);
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
                var response = _dni.GetDnis();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nextId}")]
        public async Task<IActionResult> GetNextId()
        {
            try
            {
                var response = _dni.GetNextId();
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
                var response = _dni.GetDniById(id);
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
                var response = _dni.GetDniByNumero(numero);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEditDni([FromBody] Dni dni)
        {
            try
            {
                var response = _dni.AddEditDni(dni);
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
                var response = _dni.DeleteDni(idDni);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
