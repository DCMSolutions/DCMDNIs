using AutoMapper;
using DCMDNIs.Server.Models;
using DCMDNIs.Server.Repositorio.Contrato;
using DCMDNIs.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;



namespace DCMDNIs.Server.Repositorio.Implementacion
{
    public class DniRepositorio : IDniRepositorio
    {
        private readonly IMapper _mapper;
        private readonly IConsultaRepositorio _consulta;
        public DniRepositorio(IMapper mapper, IConsultaRepositorio consulta)
        {
            _mapper = mapper;
            _consulta = consulta;
        }

        private string fileName = Path.Combine(Directory.GetCurrentDirectory(), "dnis.ans");


        //funciones
        public Dni GetHabilitadoByNumero(int numero)
        {
            try
            {
                var result = GetDniByNumero(numero);
                _consulta.AddConsulta(new Consulta("Se consulto por el DNI " + numero + (result.Habilitado == true ? " y " : " y no ") + "estaba habilitado.", false));
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message == "No existe dni con ese numero en la base de datos")
                {
                    _consulta.AddConsulta(new Consulta("Se consulto por el DNI " + numero + " y no estaba en la base de datos.",false));
                    return new Dni
                    {
                        Mensaje = "El DNI no se encuentra en la base de datos.",
                        Habilitado = false
                    };
                }
                _consulta.AddConsulta(new Consulta("Se consulto por el DNI " + numero + " y hubo un error", true));
                throw new Exception("No se pudo obtener el dni");
            }
        }

        //CRUD
        public List<Dni> GetDnis()
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    List<Dni> dnis = new();
                    return dnis;
                }
                else
                {
                    string content = System.IO.File.ReadAllText(fileName);
                    List<Dni> dnis = JsonSerializer.Deserialize<List<Dni>>(content);
                    return dnis;
                }
            }
            catch
            {
                throw new Exception("Hubo un error al buscar los dnis");
            }
        }

        public int GetNextId()
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    return 1;
                }
                else
                {
                    string content = System.IO.File.ReadAllText(fileName);
                    List<Dni> dnis = JsonSerializer.Deserialize<List<Dni>>(content);
                    if (dnis == null || dnis.Count == 0)
                    {
                        return 1;
                    }
                    int maxId = dnis.Max(d => d.Id);
                    return maxId + 1;
                }
            }
            catch
            {
                throw new Exception("Hubo un error al buscar el máximo");
            }
        }

        public Dni GetDniById(int id)
        {
            try
            {
                var dnis = GetDnis();
                var result = dnis.Where(x => x.Id == id).FirstOrDefault();
                if (result != null) return result;
                else throw new Exception("No existe dni con ese id");
            }
            catch
            {
                throw new Exception("No se pudo obtener el dni");
            }
        }

        public Dni GetDniByNumero(int numero)
        {
            try
            {
                var dnis = GetDnis();
                var result = dnis.Where(x => x.Numero == numero).FirstOrDefault();
                if (result != null) return result;
                else throw new Exception("No existe dni con ese numero en la base de datos");
            }
            catch
            {
                throw new Exception("No se pudo obtener el dni");
            }
        }

        public bool AddEditDni(Dni dni)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    CrearNueva(dni);
                    return true;
                }
                else
                {
                    var dnis = GetDnis();

                    var existingDni = dnis.Where(d => d.Id == dni.Id).FirstOrDefault();

                    if (existingDni == null)
                    {
                        dnis.Add(dni);
                    }
                    else
                    {
                        existingDni.Numero = dni.Numero;
                        existingDni.Habilitado = dni.Habilitado;
                        existingDni.Mensaje = dni.Mensaje;
                        existingDni.Nombre = dni.Nombre;
                        existingDni.Apellido = dni.Apellido;
                    }

                    string json = JsonSerializer.Serialize(dnis, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(fileName, json);
                    return true;
                }
            }
            catch
            {
                throw new Exception("No se pudo agregar o editar el dni");
            }
        }


        public bool DeleteDni(int idDni)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    var dnis = GetDnis();

                    var existingDni = dnis.Where(d => d.Id == idDni).FirstOrDefault();

                    if (existingDni == null)
                    {
                        return true;
                    }
                    else
                    {
                        dnis = dnis.Where(d => d.Id != idDni).ToList();
                    }

                    string json = JsonSerializer.Serialize(dnis, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(fileName, json);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw new Exception("No se pudo eliminar el dni");
            }
        }

        List<Dni> CrearNueva(Dni dni)
        {
            List<Dni> nuevaDni = new() { dni };

            string json = JsonSerializer.Serialize(nuevaDni, new JsonSerializerOptions { WriteIndented = true });
            using StreamWriter sw = System.IO.File.CreateText(fileName);
            sw.Write(json);

            return nuevaDni;
        }
    }
}
