using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Correo;
using Wass.Back.Empresa.Models.Peticiones.v1.Empresa;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
using Wass.Back.Empresa.Rabbit.Utility;

namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOEmpresa
    {

        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCEmpresa _dalc;
        private readonly UtilityCorreoElectronico _email;
        public BOEmpresa(EmpresaContext context, IConfiguration config)
        {
            _dalc = new DALCEmpresa(context);
            _email = new UtilityCorreoElectronico(config);
        }

        private async Task<ResponseBase<Empresas>> ValidarReglasNegocio(Empresas datos)
        {
            var result = await GetPorNitAsync(datos);
            if (!result.codigo.Equals((int)HttpStatusCode.OK))
                return new ResponseBase<Empresas>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = string.Empty,
                    datos = null
                };
            else
                return new ResponseBase<Empresas>()
                {
                    codigo = (int)HttpStatusCode.Conflict,
                    estado = true,
                    mensaje = "El NIT con el digito e verificación que esta intentando registrar ya existe.",
                    datos = null
                };

        }
        public async Task<ResponseBase<Empresas>> GetPorNitAsync(Empresas datos)
        {
            try
            {
                var empresa = await _dalc.GetPorNitAsync(datos.nit, datos.digVerficacion);
                if (empresa != null)
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = empresa
                    };
                }
                else
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "La empresa consultada no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Empresas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Empresas>> GetEmpresaAsync(long idEmpresa)
        {
            try
            {
                var empresa = await _dalc.Get(idEmpresa);

                if (empresa != null)
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = empresa
                    };
                }
                else
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "La empresa consultada no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Empresas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empresas>>> GetEmpresasAsync()
        {
            try
            {
                var empresas = await _dalc.GetTodas();

                if (empresas != null)
                {
                    if (empresas.Count > 0)
                        return new ResponseBase<List<Empresas>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = empresas
                        };
                    else
                        return new ResponseBase<List<Empresas>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay empresas disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empresas>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empresas no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empresas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empresas>>> GetPorTipoAfiliacionAsync(TipoAfiliacion tipoAfiliacion)
        {
            try
            {
                var empresas = await _dalc.GetPorTipoAfiliacionAsync(tipoAfiliacion);

                if (empresas != null)
                {
                    if (empresas.Count > 0)
                        return new ResponseBase<List<Empresas>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = empresas
                        };
                    else
                        return new ResponseBase<List<Empresas>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay empresas disponibles para el tipo de afiliación {tipoAfiliacion.ToString("G")}.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empresas>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empresas no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empresas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Empresas>> SaveAsync(RequestEmpresa _data, Transaction trans)
        {
            try
            {
                var ob = JsonConvert.DeserializeObject<Empresas>(JsonConvert.SerializeObject(_data));
                ob.tipoAfiliacion = _data.tipoAfiliacion.ToString("G");

                var reglasNegocio = await ValidarReglasNegocio(ob);
                if (reglasNegocio.codigo.Equals((int)HttpStatusCode.OK))
                {
                    ob.eliminado = false;
                    var data = await _dalc.Set(ob, trans);
                    if (data != null)
                    {
                        return new ResponseBase<Empresas>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = $"Operación realizada con exito",
                            datos = data
                        };
                    }
                    else
                        return new ResponseBase<Empresas>()
                        {
                            codigo = (int)HttpStatusCode.InternalServerError,
                            estado = false,
                            mensaje = $"La operación solicitada no se pudo realizar.",
                            datos = data
                        };

                }
                else
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.Conflict,
                        estado = false,
                        mensaje = reglasNegocio.mensaje,
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Empresas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<Empresas>> EditarEmpresa(RequestEmpresa _data, Transaction trans)
        {
            try
            {
                var ob = JsonConvert.DeserializeObject<Empresas>(JsonConvert.SerializeObject(_data));
                ob.tipoAfiliacion = _data.tipoAfiliacion.ToString("G");

                var data = await _dalc.Set(ob, trans);
                if (data != null)
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación realizada con exito",
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación solicitada no se pudo realizar.",
                        datos = data
                    };

                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Empresas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Empresas>> EliminarEmpresa(RequestEmpresa _data)
        {
            try
            {
                var ob = JsonConvert.DeserializeObject<Empresas>(JsonConvert.SerializeObject(_data));
                ob.tipoAfiliacion = _data.tipoAfiliacion.ToString("G");


                ob.eliminado = true;
                var result = await _dalc.Eliminar(_data.idEmpresa);
                if (result.estado)
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación realizada con exito",
                        datos = null
                    };
                }
                else
                {
                    return new ResponseBase<Empresas>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación solicitada no se pudo realizar.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Empresas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<(bool, string)>> CorreoSolicitarInfoAsync(RequestSolicitarInfo contenido)
        {
            try
            {
                var empresa = await _dalc.Get(contenido.idEmpresa);
                if (empresa != null)
                {
                    var resultEmail = await _email.EnviarCorreo(new RequestCorreo()
                    {
                        destinatario = contenido.email,
                        asunto = "Solicitud de Información Empresa - WASS",
                        conCopia = null,
                        contenido = $"Señores {empresa.razonSocial.ToUpper()}<br/><br/><br/>"
                                 + $"Se solicita la siguiente información para continuar el proceso de afiliación:<br/><br/>"
                                 + $"{contenido.Contenido}"
                                 + "<br/><br/><br/><br/>"
                                 + "Atentamente WASS,"
                    });
                    if (resultEmail.Item1)
                    {
                        return new ResponseBase<(bool, string)>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "",
                            datos = resultEmail
                        };
                    }
                    else
                    {
                        return new ResponseBase<(bool, string)>()
                        {
                            codigo = (int)HttpStatusCode.BadRequest,
                            estado = false,
                            mensaje = resultEmail.Item2,
                            datos = (resultEmail.Item1, resultEmail.Item2)
                        };
                    }
                }
                else
                {
                    return new ResponseBase<(bool, string)>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "El objeto no existe",
                        datos = (false, "el objeto no existe")
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<(bool, string)>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = (false, ex.Message)
                };
            }
        }
    }
}
