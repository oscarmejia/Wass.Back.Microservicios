using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Certificacion;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOCertificacion
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCCertificacion _dalc;

        public BOCertificacion(EmpresaContext context)
        {
            _dalc = new DALCCertificacion(context);
        }

        public async Task<ResponseBase<Certificacion>> Get(int idCertificado)
        {

            try
            {
                var certificado = await _dalc.Get(idCertificado);

                if (certificado != null)
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = certificado
                    };
                }
                else
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "Este certificado no se encuentra",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Certificacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }

        }

        public async Task<ResponseBase<List<Certificacion>>> GetTodas()
        {
            try
            {
                var certificado = await _dalc.GetTodas();

                if (certificado != null)
                {
                    return new ResponseBase<List<Certificacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = certificado
                    };
                }
                else
                {
                    return new ResponseBase<List<Certificacion>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "No hay certificaciones disponibles",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Certificacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Certificacion>>> GetTodasPorEmpresa(long idEmpresa)
        {
            try
            {
                var certificacion = await _dalc.GetTodasPorEmpresa(idEmpresa);

                if (certificacion != null)
                {
                    return new ResponseBase<List<Certificacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = certificacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Certificacion>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "Esta empresa no tiene ceritficados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Certificacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Certificacion>>> GetTodasPorEmpleado(long idEmpleado)
        {
            try
            {
                var certificacion = await _dalc.GetTodasPorEmpleado(idEmpleado);

                if (certificacion != null)
                {
                    if (certificacion.Count > 0)
                    {
                        return new ResponseBase<List<Certificacion>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = String.Empty,
                            datos = certificacion
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<Certificacion>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "El empleado no tiene ceritficados",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<Certificacion>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "La consulta de Certificados no arrojo resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Certificacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ResponseCertificacion>>> GetTodasGeneralEmpresa(long idEmpresa)
        {
            try
            {
                var certificaciones = await _dalc.GetTodasEmpresaGeneral(idEmpresa);
                var certificacionesEmpleados = await _dalc.GetTodasEmpleadosEmpresaGeneral(idEmpresa);
                var obj = new List<ResponseCertificacion>();
                var certificado = new List<Certificacion>();
                var certificadosEmpleados = new List<Certificacion>();

                foreach (var item in certificaciones)
                {
                    certificado.Add(item);

                }
                foreach (var empleado in certificacionesEmpleados)
                {
                    var cE = await GetTodasPorEmpleado(empleado.idEmpleado);

                    if (cE.codigo.Equals((int)HttpStatusCode.OK))
                    {
                        foreach (var item in cE.datos)
                        {
                            certificadosEmpleados.Add(item);
                        }
                        //certificadosEmpleados.Add(cE.datos);
                    }
                    else
                    {

                    }

                }
                obj.Add(new ResponseCertificacion()
                {
                    idEmpresa = idEmpresa,
                    certificadosEmpresa = certificado,
                    certificadosEmpleados = certificadosEmpleados

                });
                if (certificaciones != null)
                {
                    if (certificaciones.Count > 0)
                    {
                        return new ResponseBase<List<ResponseCertificacion>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = String.Empty,
                            datos = obj
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<ResponseCertificacion>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "no hay certificados para esta Empresa",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<ResponseCertificacion>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "la consulta de certificados no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ResponseCertificacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Certificacion>> SaveCertificacion(Certificacion certificacion, Transaction transaction)
        {
            var ob = JsonConvert.DeserializeObject<Certificacion>(JsonConvert.SerializeObject(certificacion));

            if (transaction == Transaction.Insert)
            {
                var certificado = await _dalc.Set(ob, transaction);

                if (certificado != null)
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Certificado creado con exito",
                        datos = certificado
                    };
                }
                else
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = "Certificado no creado",
                        datos = certificado
                    };
                }
            }
            else
            {
                var certificado = await _dalc.Set(ob, transaction);

                if (certificado != null)
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Certificado actualizado con exito",
                        datos = certificado
                    };
                }
                else
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = "Certificado no actualizado",
                        datos = certificado
                    };
                }
            }


        }


        public async Task<ResponseBase<Certificacion>> EliminarCertificado(Certificacion certificacion)
        {
            try
            {
                var certificado = await _dalc.Eliminar(certificacion);

                if (certificado.estado)
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = null
                    };
                }
                else
                {
                    return new ResponseBase<Certificacion>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = "Operacion no realizada",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Certificacion>()
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
