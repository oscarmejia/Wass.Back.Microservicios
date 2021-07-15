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
    public class BOAjustesAlmacenes
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCAjustesAlmacenes _dalc;
        private readonly DALCAlmacen _dalcAlmacen;
        private readonly DALCRepuestosAlmacen _dalcRepuestosAlmacen;
        private readonly DALCRepuestos _dalcRepuestos;

        public BOAjustesAlmacenes(EmpresaContext context)
        {
            _dalc = new DALCAjustesAlmacenes(context);
            _dalcAlmacen = new DALCAlmacen(context);
            _dalcRepuestosAlmacen = new DALCRepuestosAlmacen(context);
        }

        public async Task<ResponseBase<List<AjustesAlmacenes>>> GetTodas()
        {
            try
            {
                var ajustesAlmacenes = await _dalc.GetTodas();

                if (ajustesAlmacenes != null)
                {
                    return new ResponseBase<List<AjustesAlmacenes>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ajustesAlmacenes
                    };
                }
                else
                {
                    return new ResponseBase<List<AjustesAlmacenes>>()
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
                return new ResponseBase<List<AjustesAlmacenes>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<AjustesAlmacenes>> GetPorId(long idAjustesAlmacenes)
        {
            try
            {
                var ajustesAlmacenes = await _dalc.Get(idAjustesAlmacenes);
                if (ajustesAlmacenes != null)
                {
                    return new ResponseBase<AjustesAlmacenes>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ajustesAlmacenes
                    };
                }
                else
                {
                    return new ResponseBase<AjustesAlmacenes>()
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
                return new ResponseBase<AjustesAlmacenes>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<AjustesAlmacenes>> guardarAjustesAlmacenes(AjustesAlmacenes ajustesAlmacenes, Transaction transaction)
        {
            try
            {
                var almacen = await _dalcAlmacen.Get(ajustesAlmacenes.idAlmacen);

                // almacen.tipo == 1 Fisico


                if (almacen != null && almacen.tipo == 1)
                {
                    var almacenRepuestos = await _dalcRepuestosAlmacen.GetPorIdRepuestoAlmacen(ajustesAlmacenes.idRepuesto, almacen.idAlmacen);

                    if (almacenRepuestos.cantidadActual == ajustesAlmacenes.cantidadAnterior)
                    {

                        var cantidadRepuesto = await _dalcRepuestosAlmacen.AjustarCantidadRepuestosAlmacen(almacen.idAlmacen, ajustesAlmacenes.idRepuesto, ajustesAlmacenes.cantidadNueva);
                        ajustesAlmacenes.existenciaActual = almacenRepuestos.cantidadActual;

                        var dataAjustesAlmacenes = await _dalc.Set(ajustesAlmacenes, transaction);

                        return new ResponseBase<AjustesAlmacenes>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataAjustesAlmacenes
                        };
                    }
                    else
                    {
                        return new ResponseBase<AjustesAlmacenes>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar. La cantidad Anterior debe coincidir con la del Almacen",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<AjustesAlmacenes>()
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
                return new ResponseBase<AjustesAlmacenes>()
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
