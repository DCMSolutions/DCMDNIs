using AutoMapper;
using DCMDNIs.Server.Context;
using DCMDNIs.Server.Models;
using DCMDNIs.Server.Repositorio.Contrato;
using DCMDNIs.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;



namespace DCMDNIs.Server.Repositorio.Implementacion
{
    public class DniRepositorio : IDniRepositorio
    {
        private readonly DcmdnisContext _dbContext;
        private readonly IMapper _mapper;
        public DniRepositorio(DcmdnisContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //funciones
        public async Task<Dni> GetHabilitadoByNumero(int numero)
        {
            try
            {
                var result = await _dbContext.Dnis
                    .Where(Dni => Dni.Numero == numero)
                    .FirstOrDefaultAsync();
                if (result != null)
                {
                    Console.WriteLine("Se consulto por el DNI " + numero + (result.Habilitado == true ? " y " : " y no ") + "estaba habilitado.");
                    return result;
                }
                else
                {
                    Console.WriteLine("Se consulto por el DNI " + numero + " y no estaba en la base de datos.");
                    return new Dni
                    {
                        Mensaje = "El DNI no se encuentra en la base de datos.",
                        Habilitado = false
                    };
                }
            }
            catch
            {
                Console.WriteLine("Se consulto por el DNI " + numero + " y hubo un error");
                throw new Exception("No se pudo obtener el dni");
            }
        }

        //CRUD
        public async Task<List<Dni>> GetDnis()
        {
            try
            {
                var result = await _dbContext.Dnis
                   .AsNoTracking()
                   .ToListAsync();
                return result;
            }
            catch
            {
                throw new Exception("Hubo un error al buscar los dnis");
            }
        }

        public async Task<Dni> GetDniById(int id)
        {
            try
            {
                var result = await _dbContext.Dnis
                    .Where(Dni => Dni.Id == id)
                    .FirstOrDefaultAsync();
                if (result != null) return result;
                else throw new Exception("No existe dni con ese id");
            }
            catch
            {
                throw new Exception("No se pudo obtener el dni");
            }
        }

        public async Task<Dni> GetDniByNumero(int numero)
        {
            try
            {
                var result = await _dbContext.Dnis
                    .Where(Dni => Dni.Numero == numero)
                    .FirstOrDefaultAsync();
                if (result != null) return result;
                else throw new Exception("No existe dni con ese numero en la base de datos");
            }
            catch
            {
                throw new Exception("No se pudo obtener el dni");
            }
        }

        public async Task<bool> AddDni(Dni dni)
        {
            try
            {
                _dbContext.Add(dni);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("No se pudo agregar el dni");
            }
        }

        public async Task<bool> EditDni(Dni dni)
        {
            try
            {

                var existingDni = await _dbContext.Dnis.FindAsync(dni.Id);

                if (existingDni == null)
                {
                    // Locker with the given ID not found
                    return false;
                }

                _dbContext.Update(existingDni).CurrentValues.SetValues(dni);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("No se pudo editar el dni");
            }
        }

        public async Task<bool> DeleteDni(int idDni)
        {
            try
            {
                var entityToRemove = await _dbContext.Dnis.FindAsync(idDni);
                if (entityToRemove != null)
                {
                    _dbContext.Dnis.Remove(entityToRemove);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                throw new Exception("No se pudo eliminar el dni");
            }
        }

    }
}
