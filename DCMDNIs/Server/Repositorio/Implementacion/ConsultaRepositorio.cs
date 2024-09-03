using AutoMapper;
using DCMDNIs.Server.Models;
using DCMDNIs.Server.Repositorio.Contrato;
using DCMDNIs.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Xml;

namespace DCMDNIs.Server.Repositorio.Implementacion
{
    public class ConsultaRepositorio : IConsultaRepositorio
    {
        private string fileName = Path.Combine(Directory.GetCurrentDirectory(), "consultas.ans");

        //CRUD
        public async Task<List<Consulta>> GetConsultas()
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    CrearVacia();
                    return new List<Consulta>();
                }
                else
                {
                    string content = System.IO.File.ReadAllText(fileName);
                    List<Consulta>? consultas = JsonSerializer.Deserialize<List<Consulta>>(content);
                    return consultas;
                }
            }
            catch
            {
                throw new Exception("Hubo un error al buscar las consultas");
            }
        }

        public async Task<bool> AddConsulta(Consulta consulta)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    CrearConConsulta(consulta);
                    return true;
                }
                else
                {
                    string content = System.IO.File.ReadAllText(fileName);
                    List<Consulta>? consultas = JsonSerializer.Deserialize<List<Consulta>>(content);
                    consultas.Add(consulta);
                    Guardar(consultas);
                    return true;
                }
            }
            catch
            {
                throw new Exception("No se pudo agregar la consulta");
            }
        }


        public async Task<bool> DeleteAllConsultas()
        {
            try
            {
                File.WriteAllText(fileName, string.Empty);
                return true;
            }
            catch
            {
                throw new Exception("No se pudieron eliminar las consultas");
            }
        }

        //funciones auxiliares
        void CrearVacia()
        {
            List<Consulta> nuevaLista = new();
            string json = JsonSerializer.Serialize(nuevaLista, new JsonSerializerOptions { WriteIndented = true });
            using StreamWriter sw = System.IO.File.CreateText(fileName);
            sw.Write(json);
        }
        void CrearConConsulta(Consulta consulta)
        {
            List<Consulta> nuevaLista = new() { consulta };
            string json = JsonSerializer.Serialize(nuevaLista, new JsonSerializerOptions { WriteIndented = true });
            using StreamWriter sw = System.IO.File.CreateText(fileName);
            sw.Write(json);
        }
        void Guardar(List<Consulta> consultas)
        {
            string json = JsonSerializer.Serialize(consultas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

    }
}
