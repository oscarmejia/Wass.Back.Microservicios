using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.Interface;
namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCCertificacion
    {
        private readonly EmpresaContext _context;

        public DALCCertificacion(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Certificacion> Get(int idCertificado)
        {
            return await _context.Certificaciones.Where(x => x.idCertificado == idCertificado).Include(x => x.empresas).FirstOrDefaultAsync();
        }

        public async Task<List<Certificacion>> GetTodas()
        {
            return await _context.Certificaciones.Where(x => !x.eliminado).ToListAsync();
        }


        public async Task<List<Certificacion>> GetTodasPorEmpleado(long idEmpleado)
        {
            return await _context.Certificaciones.Where(x => x.idEmpleado == idEmpleado).ToListAsync();
        }

        public async Task<List<Certificacion>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _context.Certificaciones.Where(x => x.idEmpresa == idEmpresa).ToListAsync();
        }

        public async Task<List<Certificacion>> GetTodasEmpresaGeneral(long idEmpresa)
        {
            var sql = (from certificacion in _context.Certificaciones
                       join empresa in _context.Empresas on certificacion.idEmpresa equals empresa.idEmpresa
                       where empresa.idEmpresa == idEmpresa
                       select certificacion).AsQueryable();


            return await sql.ToListAsync();
        }

        public async Task<List<Empleados>> GetTodasEmpleadosEmpresaGeneral(long idEmpresa)
        {
            var sqlEmpleado = (from empleado in _context.Empleados
                               join sede in _context.Sedes on empleado.idSede equals sede.idSede
                               where sede.idEmpresa == idEmpresa
                               select empleado
                      ).AsQueryable();

            return await sqlEmpleado.ToListAsync();
        }


        public async Task<Certificacion> Set(Certificacion certificacion, Transaction trasaction)
        {
            switch (trasaction)
            {
                case Transaction.Insert:
                    return await Crear(certificacion);
                case Transaction.Update:
                    return await Actualizar(certificacion);
                default:
                    return certificacion;
            }
        }

        public async Task<Certificacion> Crear(Certificacion certificacion)
        {
            _ = _context.Add(certificacion);
            _ = await _context.SaveChangesAsync();

            return certificacion;
        }

        public async Task<Certificacion> Actualizar(Certificacion certificacion)
        {
            _ = _context.Update(certificacion);
            _ = await _context.SaveChangesAsync();

            return certificacion;
        }

        public async Task<ResponseTransaction> Eliminar(Certificacion certificacion)
        {
            try
            {
                var cerificado = _context.Certificaciones.Where(x => x.idCertificado == certificacion.idCertificado).FirstOrDefault();
                cerificado.eliminado = true;
                _ = _context.Update(cerificado);
                _ = await _context.SaveChangesAsync();

                return new ResponseTransaction()
                {
                    estado = true,
                    mensaje = "Certificado eliminado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new ResponseTransaction()
                {
                    estado = false,
                    mensaje = $"Error: {ex.Message}"
                };
            }
        }
    }
}
