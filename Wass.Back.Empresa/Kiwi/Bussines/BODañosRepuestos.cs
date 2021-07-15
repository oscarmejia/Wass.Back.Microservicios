using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BODañosRepuestos
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCDañosRepuestos _dalc;

        private readonly DALCRepuestos _dalcRepuestos;
        private readonly DALCAlmacen _dalcAlmacen;
        private readonly DALCRepuestosAlmacen _dalcRepuestosAlmacen;

        public BODañosRepuestos(EmpresaContext context)
        {
            _dalc = new DALCDañosRepuestos(context);
            _dalcRepuestos = new DALCRepuestos(context);
            _dalcAlmacen = new DALCAlmacen(context);
            _dalcRepuestosAlmacen = new DALCRepuestosAlmacen(context);
        }

        public async Task<ResponseBase<List<DañosRepuestos>>> GetTodas()
        {
            try
            {
                var dañosRepuestos = await _dalc.GetTodas();

                if (dañosRepuestos != null)
                {
                    return new ResponseBase<List<DañosRepuestos>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dañosRepuestos
                    };
                }
                else
                {
                    return new ResponseBase<List<DañosRepuestos>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo ningun resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<DañosRepuestos>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<DañosRepuestos>> GetPorId(long idDañosRepuestos)
        {
            try
            {
                var dañosRepuestos = await _dalc.Get(idDañosRepuestos);
                if (dañosRepuestos != null)
                {
                    return new ResponseBase<DañosRepuestos>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dañosRepuestos
                    };
                }
                else
                {
                    return new ResponseBase<DañosRepuestos>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<DañosRepuestos>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<DañosRepuestos>> guardarDañosRepuestos(DañosRepuestos dañosRepuestos, Transaction transaction)
        {
            try
            {
                var repuesto = await _dalcRepuestos.Get(dañosRepuestos.idRepuesto);
                var almacen = await _dalcAlmacen.Get(dañosRepuestos.idAlmacen);
                bool registro = false;
                //Estado = true Activo, Estado = false Desactivo
                if (repuesto != null && almacen != null && almacen.estado == true && !registro)
                {
                    var repuestoEnAlmacen = await _dalcRepuestosAlmacen.GetPorIdRepuestoAlmacen(repuesto.idRepuestos, almacen.idAlmacen);
                    if (repuestoEnAlmacen != null)
                    {
                        var repuestosAlmacenEmisor = await _dalcRepuestosAlmacen.Get(repuestoEnAlmacen.idRepuestosAlmacen);
                        if (repuestosAlmacenEmisor.cantidadActual >= dañosRepuestos.cantidad)
                        {
                            var cantidadRepuesto = await _dalcRepuestosAlmacen.DañosRepuestosAlmacen(almacen.idAlmacen, dañosRepuestos.idRepuesto, dañosRepuestos.cantidad);
                            registro = true;
                        }
                        dañosRepuestos.existenciaActual = repuestoEnAlmacen.cantidadActual;
                    }

                    if (registro)
                    {
                        var dataDañosRepuestos = await _dalc.Set(dañosRepuestos, transaction);
                        return new ResponseBase<DañosRepuestos>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataDañosRepuestos
                        };
                    }
                    else
                    {
                        return new ResponseBase<DañosRepuestos>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<DañosRepuestos>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<DañosRepuestos>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
    }
}
