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
    public class BOCuadrillaEmpleados : IBOCrud<CuadrillaEmpleados>
    {
        public Dictionary<string, string> EndPointsDictinoDictionary { get; set; }
        private readonly DALCCuadrillaEmpleados _dalc;
        private readonly string _msg_base;

        public BOCuadrillaEmpleados(EmpresaContext context)
        {
            _dalc = new DALCCuadrillaEmpleados(context);
            _msg_base = "cuadrilla de empleados";
        }

        public async Task<ResponseBase<CuadrillaEmpleados>> GetAsync(long id)
        {
            try
            {
                var datos = await _dalc.GetAsync(id);

                if (datos != null)
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"La {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaEmpleados>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CuadrillaEmpleados>> GetAsync(Guid id)
        {
            try
            {
                var datos = await _dalc.GetAsync(id);

                if (datos != null)
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"La {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaEmpleados>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        private async Task<ResponseBase<CuadrillaEmpleados>> ValidarReglasNegocio(CuadrillaEmpleados datos)
        {
            var result = await ValidarEmpleadoCuadrilla(datos);
            if (!result.codigo.Equals((int)HttpStatusCode.OK))
                return result;

            if (datos.lider)
            {
                result = await ValidarLiderEquipo(datos);
                if (!result.codigo.Equals((int)HttpStatusCode.OK))
                    return result;
            }

            return await ValidarUnicidadCuadrilla(datos);
        }

        private async Task<ResponseBase<CuadrillaEmpleados>> ValidarLiderEquipo(CuadrillaEmpleados datos)
        {
            try
            {
                var result = await _dalc.GetLiderCuadrilla(datos.idCuadrilla);

                if (result != null)
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.Conflict,
                        estado = true,
                        mensaje = $"La {_msg_base} tiene ya un lider asociado.",
                        datos = result
                    };
                }
                else
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaEmpleados>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        private async Task<ResponseBase<CuadrillaEmpleados>> ValidarEmpleadoCuadrilla(CuadrillaEmpleados datos)
        {
            try
            {
                var result = await _dalc.GetEmpleadoCuadrilla(datos.idEmpleado, datos.idCuadrilla);

                if (result != null)
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.Conflict,
                        estado = true,
                        mensaje = $"La {_msg_base} tiene este empleado asociado.",
                        datos = result
                    };
                }
                else
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaEmpleados>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        private async Task<ResponseBase<CuadrillaEmpleados>> ValidarUnicidadCuadrilla(CuadrillaEmpleados datos)
        {
            try
            {
                var result = await _dalc.GetUnicidadCuadrilla(datos.idEmpleado, datos.idCuadrilla);

                if (result != null)
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.Conflict,
                        estado = true,
                        mensaje = $"El empleado a asociar a la {_msg_base} ya pertenece a otra cuadrilla.",
                        datos = result
                    };
                }
                else
                {
                    return new ResponseBase<CuadrillaEmpleados>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaEmpleados>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuadrillaEmpleados>>> GetPorCuadrillaAsync(long id)
        {
            try
            {
                var obj = await _dalc.GetPorCuadrillaAsync(id);

                if (obj != null)
                {
                    var listaEmpleados = new List<CuadrillaEmpleados>();
                    if (obj.Count > 0)
                    {
                        for (int i = 0; i < obj.Count; i++)
                        {
                            //var listaEmpleados = new List<CuadrillaEmpleados>();
                            var data = new CuadrillaEmpleados()
                            {
                                idEmpleadoCuadrilla = obj[i].idEmpleadoCuadrilla,
                                creador = obj[i].creador,
                                estado = obj[i].estado,
                                empleado = obj[i].empleado,
                                idCuadrilla = obj[i].idCuadrilla,
                                lider = obj[i].lider,
                                editor = obj[i].editor,
                                eliminado = obj[i].eliminado,
                                fechaCreacion = obj[i].fechaCreacion,
                                fechaEdicion = obj[i].fechaEdicion,
                                idEmpleado = obj[0].idEmpleado
                            };

                            listaEmpleados.Add(data);

                        };

                        return new ResponseBase<List<CuadrillaEmpleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = listaEmpleados
                        };
                    }
                    else
                        return new ResponseBase<List<CuadrillaEmpleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<CuadrillaEmpleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CuadrillaEmpleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuadrillaEmpleados>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<CuadrillaEmpleados>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<CuadrillaEmpleados>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<CuadrillaEmpleados>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_msg_base} no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CuadrillaEmpleados>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CuadrillaEmpleados>> SetAsync(CuadrillaEmpleados objeto, Transaction transaccion)
        {
            try
            {

                var reglas = await ValidarReglasNegocio(objeto);
                if (reglas.codigo.Equals((int)HttpStatusCode.OK) && transaccion != Transaction.Delete)
                {
                    var data = await _dalc.SetAsync(objeto, transaccion);
                    if (data != null)
                    {
                        return new ResponseBase<CuadrillaEmpleados>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = $"Operación realizada con exito",
                            datos = data
                        };
                    }
                    else
                        return new ResponseBase<CuadrillaEmpleados>()
                        {
                            codigo = (int)HttpStatusCode.InternalServerError,
                            estado = false,
                            mensaje = $"La operación solicitada no se pudo realizar.",
                            datos = data
                        };
                }
                else
                {
                    return reglas;
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaEmpleados>()
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
