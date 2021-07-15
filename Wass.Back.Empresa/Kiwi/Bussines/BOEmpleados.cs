using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Kiwi.Interface;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOEmpleados : IBOCrud<Empleados>
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCEmpleados _dalc;

        public BOEmpleados(EmpresaContext context)
        {
            _dalc = new DALCEmpleados(context);
        }

        public async Task<ResponseBase<Empleados>> GetAsync(long id)
        {
            try
            {
                var obj = await _dalc.GetAsync(id);

                if (obj != null)
                {
                    return new ResponseBase<Empleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<Empleados>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "El empleado consultado no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Empleados>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empleados>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay empleados disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empleados no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empleados>>> GetPorSedeAsync(long idSede)
        {
            try
            {
                var obj = await _dalc.GetPorSedeAsync(idSede);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay empleados disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empleados no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Empleados>> GetPorNumDocumentoAsync(int idTipoDocumento, string numDocumento)
        {
            try
            {
                var obj = await _dalc.GetPorNumDocumentoAsync(idTipoDocumento, numDocumento);

                if (obj != null)
                {
                    return new ResponseBase<Empleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<Empleados>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "El empleado consultado no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Empleados>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empleados>>> GetPorCargoAsync(int idCargo)
        {
            try
            {
                var obj = await _dalc.GetPorCargoAsync(idCargo);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay empleados disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empleados no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empleados>>> GetPorEstadoAsync(int idEstadoEmpleado)
        {
            try
            {
                var obj = await _dalc.GetPorEstadoAsync(idEstadoEmpleado);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay empleados disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empleados no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empleados>>> GetPorEmpresaAsync(int idEmpresa)
        {
            try
            {
                var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay empleados disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empleados no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Empleados>>> GetPorSedeCargoAsync(long idSede, int idCargo)
        {
            try
            {
                var obj = await _dalc.GetPorSedeCargoAsync(idSede, idCargo);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Empleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay empleados disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Empleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de empleados no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Empleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Empleados>> SetAsync(Empleados objeto, Transaction transaccion)
        {


            if (transaccion == Transaction.Delete)
            {
                return new ResponseBase<Empleados>()
                {
                    codigo = (int)HttpStatusCode.NotFound,
                    estado = false,
                    mensaje = $"La operación de eliminar menús no ha sido implementada.",
                    datos = null
                };
            }
            else
            {
                var data = await _dalc.SetAsync(objeto, transaccion);
                if (data != null)
                {
                    return new ResponseBase<Empleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación realizada con exito",
                        datos = data
                    };
                }
                else
                    return new ResponseBase<Empleados>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación solicitada no se pudo realizar.",
                        datos = data
                    };
            }
        }
    }
}
