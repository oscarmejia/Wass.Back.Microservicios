using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.RepuestosAlmacen;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BORepuestosAlmacen
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCRepuestosAlmacen _dalc;
        private readonly DALCRepuestos _dalcRepuestos;
        private readonly DALCAlmacen _dalcAlmacen;
        private readonly BOOrdenEntregaAlmacen _boOrdenEntrega;
        private readonly BOAjustesAlmacenes _boAjustesAlmacenes;

        public BORepuestosAlmacen(EmpresaContext context)
        {
            _dalc = new DALCRepuestosAlmacen(context);
            _dalcRepuestos = new DALCRepuestos(context);
            _dalcAlmacen = new DALCAlmacen(context);
            _boOrdenEntrega = new BOOrdenEntregaAlmacen(context);
        }

        public async Task<ResponseBase<List<RepuestosAlmacen>>> GetTodas()
        {
            try
            {
                var repuestosAlmacen = await _dalc.GetTodas();

                if (repuestosAlmacen != null)
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosAlmacen
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
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
                return new ResponseBase<List<RepuestosAlmacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RepuestosAlmacen>>> GetTodasDebajoMinima()
        {
            try
            {
                var repuestosAlmacen = await _dalc.GetTodas();
                var respuesta = new List<RepuestosAlmacen>();


                if (repuestosAlmacen != null)
                {
                    foreach (var item in repuestosAlmacen)
                    {
                        if (item.cantidadActual < item.cantidadMinima)
                        {
                            respuesta.Add(item);
                        }
                    }
                    return new ResponseBase<List<RepuestosAlmacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
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
                return new ResponseBase<List<RepuestosAlmacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RepuestosAlmacen>>> GetTodasDebajoOptima()
        {
            try
            {
                var repuestosAlmacen = await _dalc.GetTodas();
                var respuesta = new List<RepuestosAlmacen>();

                if (repuestosAlmacen != null)
                {
                    foreach (var item in repuestosAlmacen)
                    {
                        if (item.cantidadActual < item.cantidadOptima)
                        {
                            respuesta.Add(item);
                        }
                    }

                    return new ResponseBase<List<RepuestosAlmacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
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
                return new ResponseBase<List<RepuestosAlmacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RepuestosAlmacen>>> GetRepuestosPorAlmacen(long idAlmacen)
        {
            try
            {
                var repuestosAlmacen = await _dalc.GetRepuestosPorAlmacen(idAlmacen);

                if (repuestosAlmacen != null)
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosAlmacen
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
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
                return new ResponseBase<List<RepuestosAlmacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RepuestosAlmacen>>> GetAlmacenesPorRepuesto(long idRepuesto)
        {
            try
            {
                var repuestosAlmacen = await _dalc.GetAlmacenesPorRepuesto(idRepuesto);

                if (repuestosAlmacen != null)
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosAlmacen
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosAlmacen>>()
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
                return new ResponseBase<List<RepuestosAlmacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<RepuestosAlmacen>> GetPorId(long idRepuestosAlmacen)
        {
            try
            {
                var repuestosAlmacen = await _dalc.Get(idRepuestosAlmacen);
                if (repuestosAlmacen != null)
                {
                    return new ResponseBase<RepuestosAlmacen>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosAlmacen
                    };
                }
                else
                {
                    return new ResponseBase<RepuestosAlmacen>()
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
                return new ResponseBase<RepuestosAlmacen>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RepuestosAlmacen>> RepuestosEnAlmacen(long idAlmacen, long idRepuesto)
        {
            try
            {
                var repuestosAlmacen = await _dalc.RepuestosEnAlmacen(idAlmacen, idRepuesto);
                if (repuestosAlmacen != null)
                {
                    return new ResponseBase<RepuestosAlmacen>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosAlmacen
                    };
                }
                else
                {
                    return new ResponseBase<RepuestosAlmacen>()
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
                return new ResponseBase<RepuestosAlmacen>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RepuestosAlmacenRequest>> GetCantidadDiasParaEstarPorDebajoCantidadMinima(long idRepuesto, DateTime fechaActual, long idAlmacen)
        {
            try
            {

                var promedioUsoDiarioOrdenEntrega = await _boOrdenEntrega.GetPromedioUsoDiario(idRepuesto, fechaActual, idAlmacen);
                var respuestoAlmacen = await _dalc.GetPorIdRepuestoAlmacen(idRepuesto, idAlmacen);
                long countDays = 0;

                if (promedioUsoDiarioOrdenEntrega.datos != null && respuestoAlmacen != null)
                {
                    double cantidadActual = respuestoAlmacen.cantidadActual;
                    while (cantidadActual >= respuestoAlmacen.cantidadMinima)
                    {
                        cantidadActual -= promedioUsoDiarioOrdenEntrega.datos.promedio;
                        countDays++;
                    }

                    var respuesta = new RepuestosAlmacenRequest()
                    {
                        repuestoAlmacen = respuestoAlmacen,
                        promedioUsoDiario = promedioUsoDiarioOrdenEntrega.datos.promedio,
                        cantidadDiasParaDebajoCantidadMinima = countDays
                    };
                    return new ResponseBase<RepuestosAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<RepuestosAlmacenRequest>()
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
                return new ResponseBase<RepuestosAlmacenRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RepuestosAlmacen>> guardarRepuestosAlmacen(RepuestosAlmacen repuestosAlmacen, Transaction transaction)
        {
            try
            {
                var repuesto = await _dalcRepuestos.Get(repuestosAlmacen.idRepuestos);
                var almacen = await _dalcAlmacen.Get(repuestosAlmacen.idAlmacen);

                //Estado = true Activo, Estado = false Desactivo
                if (repuesto != null && almacen != null && almacen.estado == true)
                {
                    var repuestoEnAlmacen = await _dalc.GetPorIdRepuestoAlmacen(repuesto.idRepuestos, almacen.idAlmacen);
                    if (repuestoEnAlmacen != null)
                    {
                        var dataRepuestosAlmacen = await _dalc.AgregarCantidadRepuestosAlmacen(almacen.idAlmacen, repuesto.idRepuestos, repuestosAlmacen.cantidadActual);
                        return new ResponseBase<RepuestosAlmacen>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataRepuestosAlmacen
                        };

                    }
                    else
                    {
                        var dataRepuestosAlmacen = await _dalc.Set(repuestosAlmacen, transaction);
                        return new ResponseBase<RepuestosAlmacen>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataRepuestosAlmacen
                        };
                    }
                }
                else
                {
                    return new ResponseBase<RepuestosAlmacen>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar. No existe el repuesto o no existe el almacen. El almacen debe estar Activo",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<RepuestosAlmacen>()
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
