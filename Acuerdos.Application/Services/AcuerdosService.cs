using Acuerdos.Application.Interfaces;
using Acuerdos.Domain.Models;
using Acuerdos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acuerdos.Application.Services
{
    public class AcuerdosService : IAcuerdosService
    {
        private readonly AppDbContext _context;
        public AcuerdosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<AcuerdoComercial>> GetAllAsync(int page, int pageSize)
        {
            var query = _context.AcuerdosComerciales
                .Include(a => a.Tarifas)
                .Include(a => a.AgenteComercial)
                .OrderBy(a => a.IdAcuerdo);

            var totalRecords = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PagedResult<AcuerdoComercial>
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                Data = data
            };
        }

        public async Task<AcuerdoComercial?> GetByIdAsync(int id)
        {
            return await _context.AcuerdosComerciales
                .Include(a => a.Tarifas)
                .Include(a => a.AgenteComercial)
                .FirstOrDefaultAsync(a => a.IdAcuerdo == id);
        }

        public async Task<AcuerdoComercial> CreateAsync(AcuerdoComercial acuerdo)
        {
            _context.AcuerdosComerciales.Add(acuerdo);
            await _context.SaveChangesAsync();
            return acuerdo;
        }

        public async Task<AcuerdoComercial> UpdateAsync(int id, AcuerdoComercial acuerdo)
        {
            var existing = await _context.AcuerdosComerciales
                .Include(a => a.Tarifas)
                .FirstOrDefaultAsync(a => a.IdAcuerdo == id);

            if (existing == null)
                throw new Exception($"No se encontró el acuerdo {id}");

            existing.IdAgente = acuerdo.IdAgente;
            existing.IdTrabajador = acuerdo.IdTrabajador;
            existing.FechaAlta = acuerdo.FechaAlta;
            existing.FechaBaja = acuerdo.FechaBaja;
            existing.Ambito = acuerdo.Ambito;
            existing.DuracionMeses = acuerdo.DuracionMeses;
            existing.ProrrogaAutomatica = acuerdo.ProrrogaAutomatica;
            existing.DuracionProrrogaMeses = acuerdo.DuracionProrrogaMeses;
            existing.Exclusividad = acuerdo.Exclusividad;
            existing.CodFormaPago = acuerdo.CodFormaPago;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.AcuerdosComerciales.FindAsync(id);
            if (existing == null) return false;

            _context.AcuerdosComerciales.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TarifaConsumo>> GetTarifasAsync()
        {
            return await _context.TarifasConsumo.ToListAsync();
        }

        public async Task<List<TarifaDetalleDto>> GetTarifasByAcuerdoAsync(int idAcuerdo)
        {
            var query = from ta in _context.TarifasAcuerdos
                        join tc in _context.TarifasConsumo on ta.IdTarifa equals tc.IdTarifa
                        where ta.IdAcuerdo == idAcuerdo
                        select new TarifaDetalleDto
                        {
                            IdTarifaAcuerdo = ta.IdTarifaAcuerdo,
                            IdTarifa = ta.IdTarifa,
                            PorcRenovacion = ta.PorcRenovacion,
                            FechaVigor = ta.FechaVigor,
                            Nombre = tc.Nombre,
                            InicioVigencia = tc.InicioVigencia,
                            FinVigencia = tc.FinVigencia,
                            CodTarifaAcceso = tc.CodTarifaAcceso,
                            IdProductoComercial = tc.IdProductoComercial,
                            AñadirComisionAPrecios = tc.AñadirComisionAPrecios,
                            IdComisionComercialTipo = tc.IdComisionComercialTipo,
                            IdComisionComercialAjustada = tc.IdComisionComercialAjustada,
                            CodCanal = tc.CodCanal,
                            IdModeloFactura = tc.IdModeloFactura
                        };

            return await query.ToListAsync();
        }
        public async Task UpdateTarifasAsync(int idAcuerdo, List<TarifaAcuerdo> tarifas)
        {
           
            var existingAcuerdo = await _context.AcuerdosComerciales.FindAsync(idAcuerdo);
            if (existingAcuerdo == null)
                throw new Exception($"No se encontró el acuerdo {idAcuerdo}");

       
            if (tarifas == null || tarifas.Count < 20)
                throw new Exception("Debe asignarse al menos 20 tarifas al acuerdo.");

            try
            {
              
                var tarifasExistentes = _context.TarifasAcuerdos
                    .Where(ta => ta.IdAcuerdo == idAcuerdo);
                _context.TarifasAcuerdos.RemoveRange(tarifasExistentes);
                await _context.SaveChangesAsync();

              
                foreach (var tarifa in tarifas)
                {
                  
                    tarifa.IdAcuerdo = idAcuerdo;
                    tarifa.AcuerdoComercial = null; 

                    _context.TarifasAcuerdos.Add(tarifa);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);

                throw new Exception($"Error al actualizar tarifas del acuerdo {idAcuerdo}: {ex.Message}", ex);
            }
        }



        public async Task<List<AgenteComercial>> GetAllAsyncAgenteComercial()
        {
            return await _context.AgentesComerciales.ToListAsync();
        }
    }
}
