using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.CentroCosto;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOCentroCosto
    {
        private readonly DALCCentroCosto _dalc;

        public BOCentroCosto(EmpresaContext context)
        {
            _dalc = new DALCCentroCosto(context);
        }

        public async Task<ResponseBase<List<CentroCostoRequest>>> GetTodas()
        {
            try
            {
                var centroCosto = await _dalc.GetTodas();

                var obj = new List<CentroCostoRequest>();
                if (centroCosto != null)
                {
                    foreach (var item in centroCosto)
                    {
                        if (item.idCentroCostoPadre != null)
                        {
                            var centroCostoPadre = await _dalc.Get(Convert.ToInt64(item.idCentroCostoPadre));

                            var detalleCentroCostoPadre = new CentroCostoPadreRequest()
                            {
                                idCentroCosto = centroCostoPadre.idCentroCosto,
                                idCentroCostoPadre = centroCostoPadre.idCentroCostoPadre,
                                idEmpresa = centroCostoPadre.idEmpresa,
                                nombre = centroCostoPadre.nombre,
                                eliminado = centroCostoPadre.eliminado,
                                Sedes = centroCostoPadre.Sedes,
                            };
                            var respuesta = new CentroCostoRequest()
                            {
                                idCentroCosto = item.idCentroCosto,
                                idCentroCostoPadre = detalleCentroCostoPadre,
                                idEmpresa = item.idEmpresa,
                                nombre = item.nombre,
                                eliminado = item.eliminado,
                                Sedes = item.Sedes,
                            };
                            obj.Add(respuesta);
                        }
                        else
                        {
                            var respuestaItem = new CentroCostoRequest()
                            {
                                idCentroCosto = item.idCentroCosto,
                                idEmpresa = item.idEmpresa,
                                nombre = item.nombre,
                                eliminado = item.eliminado,
                                Sedes = item.Sedes,
                            };
                            obj.Add(respuestaItem);
                        }
                    }

                    return new ResponseBase<List<CentroCostoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<List<CentroCostoRequest>>()
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
                return new ResponseBase<List<CentroCostoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CentroCostoRequest>>> GetTodasPorEmpresa(long idEmpresa)
        {
            try
            {
                var centroCosto = await _dalc.GetTodasPorEmpresa(idEmpresa);
                var obj = new List<CentroCostoRequest>();
                if (centroCosto != null)
                {
                    foreach (var item in centroCosto)
                    {
                        if (item.idCentroCostoPadre != null)
                        {
                            var centroCostoPadre = await _dalc.Get(Convert.ToInt64(item.idCentroCostoPadre));

                            var detalleCentroCostoPadre = new CentroCostoPadreRequest()
                            {
                                idCentroCosto = centroCostoPadre.idCentroCosto,
                                idCentroCostoPadre = centroCostoPadre.idCentroCostoPadre,
                                idEmpresa = centroCostoPadre.idEmpresa,
                                nombre = centroCostoPadre.nombre,
                                eliminado = centroCostoPadre.eliminado,
                                Sedes = centroCostoPadre.Sedes,
                            };
                            var respuesta = new CentroCostoRequest()
                            {
                                idCentroCosto = item.idCentroCosto,
                                idCentroCostoPadre = detalleCentroCostoPadre,
                                idEmpresa = item.idEmpresa,
                                nombre = item.nombre,
                                eliminado = item.eliminado,
                                Sedes = item.Sedes,
                            };
                            obj.Add(respuesta);
                        }
                        else
                        {
                            var respuestaItem = new CentroCostoRequest()
                            {
                                idCentroCosto = item.idCentroCosto,
                                idEmpresa = item.idEmpresa,
                                nombre = item.nombre,
                                eliminado = item.eliminado,
                                Sedes = item.Sedes,
                            };
                            obj.Add(respuestaItem);
                        }
                    }
                    return new ResponseBase<List<CentroCostoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<List<CentroCostoRequest>>()
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
                return new ResponseBase<List<CentroCostoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CentroCostoRequest>>> GetPorCentroCostoPadre(string idCentrocostoPadre)
        {
            try
            {
                var centroCosto = await _dalc.GetPorCentroCostoPadre(idCentrocostoPadre);
                var obj = new List<CentroCostoRequest>();
                if (centroCosto != null)
                {
                    foreach (var item in centroCosto)
                    {
                        if (item.idCentroCostoPadre != null)
                        {
                            var centroCostoPadre = await _dalc.Get(Convert.ToInt64(item.idCentroCostoPadre));

                            var detalleCentroCostoPadre = new CentroCostoPadreRequest()
                            {
                                idCentroCosto = centroCostoPadre.idCentroCosto,
                                idCentroCostoPadre = centroCostoPadre.idCentroCostoPadre,
                                idEmpresa = centroCostoPadre.idEmpresa,
                                nombre = centroCostoPadre.nombre,
                                eliminado = centroCostoPadre.eliminado,
                                Sedes = centroCostoPadre.Sedes,
                            };
                            var respuesta = new CentroCostoRequest()
                            {
                                idCentroCosto = item.idCentroCosto,
                                idCentroCostoPadre = detalleCentroCostoPadre,
                                idEmpresa = item.idEmpresa,
                                nombre = item.nombre,
                                eliminado = item.eliminado,
                                Sedes = item.Sedes,
                            };
                            obj.Add(respuesta);
                        }
                        else
                        {
                            var respuestaItem = new CentroCostoRequest()
                            {
                                idCentroCosto = item.idCentroCosto,
                                idEmpresa = item.idEmpresa,
                                nombre = item.nombre,
                                eliminado = item.eliminado,
                                Sedes = item.Sedes,
                            };
                            obj.Add(respuestaItem);
                        }
                    }
                    return new ResponseBase<List<CentroCostoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<List<CentroCostoRequest>>()
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
                return new ResponseBase<List<CentroCostoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CentroCostoRequest>> GetPorId(long idCentroCosto)
        {
            try
            {
                var centroCosto = await _dalc.Get(idCentroCosto);

                if (centroCosto != null)
                {
                    var respuesta = new CentroCostoRequest();
                    if (centroCosto.idCentroCostoPadre != null)
                    {
                        var centroCostoPadre = await _dalc.Get(Convert.ToInt64(centroCosto.idCentroCostoPadre));

                        var detalleCentroCostoPadre = new CentroCostoPadreRequest()
                        {
                            idCentroCosto = centroCostoPadre.idCentroCosto,
                            idCentroCostoPadre = centroCostoPadre.idCentroCostoPadre,
                            idEmpresa = centroCostoPadre.idEmpresa,
                            nombre = centroCostoPadre.nombre,
                            eliminado = centroCostoPadre.eliminado,
                            Sedes = centroCostoPadre.Sedes,
                        };
                        respuesta = new CentroCostoRequest()
                        {
                            idCentroCosto = centroCosto.idCentroCosto,
                            idCentroCostoPadre = detalleCentroCostoPadre,
                            idEmpresa = centroCosto.idEmpresa,
                            nombre = centroCosto.nombre,
                            eliminado = centroCosto.eliminado,
                            Sedes = centroCosto.Sedes,
                        };
                    }
                    else
                    {
                        respuesta = new CentroCostoRequest()
                        {
                            idCentroCosto = centroCosto.idCentroCosto,
                            idEmpresa = centroCosto.idEmpresa,
                            nombre = centroCosto.nombre,
                            eliminado = centroCosto.eliminado,
                            Sedes = centroCosto.Sedes,
                        };
                    }
                    return new ResponseBase<CentroCostoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<CentroCostoRequest>()
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
                return new ResponseBase<CentroCostoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CentroCosto>> EliminarCentroCosto(long idCentroCosto)
        {
            try
            {
                var data = await _dalc.EliminarCentroCosto(idCentroCosto);

                return new ResponseBase<CentroCosto>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre el Comentario realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<CentroCosto>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<CentroCosto>> guardarCentroCosto(CentroCosto centroCosto, Transaction transaction)
        {
            try
            {


                var dataCentroCosto = await _dalc.Set(centroCosto, transaction);

                if (dataCentroCosto != null)
                {

                    return new ResponseBase<CentroCosto>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataCentroCosto
                    };
                }
                else
                {
                    return new ResponseBase<CentroCosto>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo ningun resultado.",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<CentroCosto>()
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
