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
    public class BORepuestosDiagnostico
    {
        private readonly DALCRepuestosDiagnostico _dalc;
        private readonly DALCRepuestos _dalcRepuestos;
        private readonly DALCActivosClasificacionDiagnosticos _dalcDiagnostico;

        public BORepuestosDiagnostico(EmpresaContext context)
        {
            _dalc = new DALCRepuestosDiagnostico(context);
            _dalcRepuestos = new DALCRepuestos(context);
            _dalcDiagnostico = new DALCActivosClasificacionDiagnosticos(context);
        }

        public async Task<ResponseBase<List<RepuestosDiagnostico>>> GetTodas()
        {
            try
            {
                var repuestosDiagnostico = await _dalc.GetTodas();


                if (repuestosDiagnostico != null)
                {
                    return new ResponseBase<List<RepuestosDiagnostico>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosDiagnostico
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosDiagnostico>>()
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
                return new ResponseBase<List<RepuestosDiagnostico>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RepuestosDiagnostico>>> GetPorIdDiagnosticoRepuesto(long idDiagnostico)
        {
            try
            {
                var repuestosDiagnostico = await _dalc.GetPorIdDiagnosticoRepuesto(idDiagnostico);


                if (repuestosDiagnostico != null)
                {
                    return new ResponseBase<List<RepuestosDiagnostico>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosDiagnostico
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosDiagnostico>>()
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
                return new ResponseBase<List<RepuestosDiagnostico>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RepuestosDiagnostico>>> GetPorIdRepuestosDiagnostico(long idRepuesto)
        {
            try
            {
                var repuestosDiagnostico = await _dalc.GetPorIdRepuestosDiagnostico(idRepuesto);

                if (repuestosDiagnostico != null)
                {
                    return new ResponseBase<List<RepuestosDiagnostico>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosDiagnostico
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosDiagnostico>>()
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
                return new ResponseBase<List<RepuestosDiagnostico>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<RepuestosDiagnostico>> GetPorId(long idRepuestosDiagnostico)
        {
            try
            {
                var repuestosDiagnostico = await _dalc.Get(idRepuestosDiagnostico);
                if (repuestosDiagnostico != null)
                {
                    return new ResponseBase<RepuestosDiagnostico>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosDiagnostico
                    };
                }
                else
                {
                    return new ResponseBase<RepuestosDiagnostico>()
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
                return new ResponseBase<RepuestosDiagnostico>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<RepuestosDiagnostico>> guardarRepuestosDiagnostico(RepuestosDiagnostico repuestosDiagnostico, Transaction transaction)
        {
            try
            {

                var repuesto = await _dalcRepuestos.Get(repuestosDiagnostico.idRepuestos);
                var diagnostico = await _dalcDiagnostico.GetAsync(repuestosDiagnostico.idDiagnostico);


                if (repuesto != null && diagnostico != null)
                {
                    var dataRepuestoDiagnostico = await _dalc.Set(repuestosDiagnostico, transaction);

                    return new ResponseBase<RepuestosDiagnostico>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dataRepuestoDiagnostico
                    };
                }
                else
                {
                    return new ResponseBase<RepuestosDiagnostico>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar. ",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<RepuestosDiagnostico>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RepuestosDiagnostico>> EliminarRepuestosDiagnostico(long idRepuestosDiagnostico)
        {
            try
            {
                var data = await _dalc.EliminarRepuestosDiagnostico(idRepuestosDiagnostico);


                return new ResponseBase<RepuestosDiagnostico>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre el Comentario realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<RepuestosDiagnostico>()
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
