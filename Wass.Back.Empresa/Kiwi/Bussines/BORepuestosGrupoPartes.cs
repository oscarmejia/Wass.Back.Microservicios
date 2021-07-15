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
    public class BORepuestosGrupoPartes
    {
        private readonly DALCRepuestosGrupoPartes _dalc;
        private readonly DALCRepuestos _dalcRepuestos;

        public BORepuestosGrupoPartes(EmpresaContext context)
        {
            _dalc = new DALCRepuestosGrupoPartes(context);
            _dalcRepuestos = new DALCRepuestos(context);
        }

        public async Task<ResponseBase<List<RepuestosGrupoPartes>>> GetTodas()
        {
            try
            {
                var repuestosGrupoPartes = await _dalc.GetTodas();

                if (repuestosGrupoPartes != null)
                {
                    return new ResponseBase<List<RepuestosGrupoPartes>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosGrupoPartes
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosGrupoPartes>>()
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
                return new ResponseBase<List<RepuestosGrupoPartes>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }

        }

        public async Task<ResponseBase<RepuestosGrupoPartes>> GetRepuestosGrupoParte(int idRepuestosGrupoPartes)
        {
            try
            {
                var repuestosGrupoPartes = await _dalc.GetRepuestoGrupoParte(idRepuestosGrupoPartes);

                if (repuestosGrupoPartes != null)
                {
                    return new ResponseBase<RepuestosGrupoPartes>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosGrupoPartes
                    };
                }
                else
                {
                    return new ResponseBase<RepuestosGrupoPartes>()
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
                return new ResponseBase<RepuestosGrupoPartes>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }

        }

        public async Task<ResponseBase<List<RepuestosGrupoPartes>>> GetTodasByRepuestos(long idRepuestos)
        {
            try
            {
                var repuestosGrupoPartes = await _dalc.GetTodasByRepuesto(idRepuestos);

                if (repuestosGrupoPartes != null)
                {
                    return new ResponseBase<List<RepuestosGrupoPartes>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosGrupoPartes
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosGrupoPartes>>()
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
                return new ResponseBase<List<RepuestosGrupoPartes>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }

        }

        public async Task<ResponseBase<List<RepuestosGrupoPartes>>> GetTodasByGrupoPartes(long idGrupo)
        {
            try
            {
                var repuestosGrupoPartes = await _dalc.GetTodasByGrupoPartes(idGrupo);

                if (repuestosGrupoPartes != null)
                {
                    return new ResponseBase<List<RepuestosGrupoPartes>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestosGrupoPartes
                    };
                }
                else
                {
                    return new ResponseBase<List<RepuestosGrupoPartes>>()
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
                return new ResponseBase<List<RepuestosGrupoPartes>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }

        }


        public async Task<ResponseBase<RepuestosGrupoPartes>> guardarRepuestosGrupoPartes(RepuestosGrupoPartes repuestosDiagnostico, Transaction transaction)
        {
            try
            {
                var dataRepuestoDiagnostico = await _dalc.Set(repuestosDiagnostico, transaction);
                if (dataRepuestoDiagnostico != null)
                {


                    return new ResponseBase<RepuestosGrupoPartes>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dataRepuestoDiagnostico
                    };
                }
                else
                {
                    return new ResponseBase<RepuestosGrupoPartes>()
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
                return new ResponseBase<RepuestosGrupoPartes>()
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
