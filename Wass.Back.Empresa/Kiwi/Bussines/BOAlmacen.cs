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
    public class BOAlmacen
    {
        private readonly DALCAlmacen _dalc;

        public BOAlmacen(EmpresaContext context)
        {
            _dalc = new DALCAlmacen(context);
        }

        public async Task<ResponseBase<List<Almacen>>> GetTodas()
        {
            try
            {
                var almacen = await _dalc.GetTodas();

                if (almacen != null)
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = almacen
                    };
                }
                else
                {
                    return new ResponseBase<List<Almacen>>()
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
                return new ResponseBase<List<Almacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Almacen>>> GetTodasPorCuadrilla(long idCuadrilla)
        {
            try
            {
                var almacen = await _dalc.GetTodasPorCuadrilla(idCuadrilla);

                if (almacen != null)
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = almacen
                    };
                }
                else
                {
                    return new ResponseBase<List<Almacen>>()
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
                return new ResponseBase<List<Almacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Almacen>>> GetTodasPorSede(long idSede)
        {
            try
            {
                var almacen = await _dalc.GetTodasPorSede(idSede);

                if (almacen != null)
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = almacen
                    };
                }
                else
                {
                    return new ResponseBase<List<Almacen>>()
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
                return new ResponseBase<List<Almacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Almacen>>> GetTodasPorTipo(long tipo)
        {
            try
            {
                var almacen = await _dalc.GetTodasPorTipo(tipo);

                if (almacen != null)
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = almacen
                    };
                }
                else
                {
                    return new ResponseBase<List<Almacen>>()
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
                return new ResponseBase<List<Almacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Almacen>> GetPorId(long idAlmacen)
        {
            try
            {
                var almacen = await _dalc.Get(idAlmacen);
                if (almacen != null)
                {
                    return new ResponseBase<Almacen>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = almacen
                    };
                }
                else
                {
                    return new ResponseBase<Almacen>()
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
                return new ResponseBase<Almacen>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Almacen>>> GetPorEmpresaAsync(long idEmpresa)
        {
            try
            {
                var objSede = await _dalc.getTodasPorTipoEmpresaSede(idEmpresa, 1);
                var objCuadrilla = await _dalc.getTodasPorTipoEmpresaCuadrilla(idEmpresa, 2);
                var obj = new List<Almacen>();

                if (objSede != null)
                {
                    foreach (var item in objSede)
                    {
                        obj.Add(item);
                    }
                }
                if (objCuadrilla != null)
                {
                    foreach (var item in objCuadrilla)
                    {
                        obj.Add(item);
                    }
                }
                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Almacen>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Almacen>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay almaccenes disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de almacenes no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Almacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Almacen>>> getTodasPorTipoEmpresa(long idEmpresa, long tipo)
        {
            try
            {
                var objSede = await _dalc.getTodasPorTipoEmpresaSede(idEmpresa, tipo);
                var objCuadrilla = await _dalc.getTodasPorTipoEmpresaCuadrilla(idEmpresa, tipo);


                if (objSede.Count > 0)
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = objSede
                    };
                }
                else if (objCuadrilla.Count > 0)
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = objCuadrilla
                    };
                }
                else
                {
                    return new ResponseBase<List<Almacen>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "No hay almaccenes disponibles.",
                        datos = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Almacen>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<Almacen>> guardarAlmacen(Almacen almacen, Transaction transaction)
        {
            try
            {
                //Tipo = 1 Fisico, Tipo = 2 Virtual
                //Estado = true Activo, Estado = false Desactivo
                if ((almacen.tipo == 1 && almacen.idSede > 0) || (almacen.tipo == 2 && almacen.idCuadrilla > 0))
                {
                    var dataAlmacen = await _dalc.Set(almacen, transaction);
                    return new ResponseBase<Almacen>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = almacen
                    };
                }
                else
                {
                    return new ResponseBase<Almacen>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "Si es un almacen fisico se le debe asignar una sede. Si el almacen es virtual se le debe asignar una cuadrilla.",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<Almacen>()
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
