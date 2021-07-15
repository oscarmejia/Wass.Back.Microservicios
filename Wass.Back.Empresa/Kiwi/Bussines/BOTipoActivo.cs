using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOTipoActivo
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCTipoActivo _dalc;

        public BOTipoActivo(EmpresaContext context)
        {
            _dalc = new DALCTipoActivo(context);
        }

        public async Task<ResponseBase<List<TipoActivo>>> GetTodas()
        {
            try
            {
                var tipoActivo = await _dalc.GetTodas();

                if (tipoActivo != null)
                {
                    return new ResponseBase<List<TipoActivo>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = tipoActivo
                    };
                }
                else
                {
                    return new ResponseBase<List<TipoActivo>>()
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
                return new ResponseBase<List<TipoActivo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<TipoActivo>> GetPorId(long idTipoActivo)
        {
            try
            {
                var tipoActivo = await _dalc.Get(idTipoActivo);
                if (tipoActivo != null)
                {
                    return new ResponseBase<TipoActivo>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = tipoActivo
                    };
                }
                else
                {
                    return new ResponseBase<TipoActivo>()
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
                return new ResponseBase<TipoActivo>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<TipoActivo>> guardar(TipoActivo tipoActivo, Transaction transaction)
        {
            try
            {
                var tipoActivos = await _dalc.GetTodas();
                var listatipoActivosSinRepetir = new List<string>();
                foreach (var item in tipoActivos)
                {
                    listatipoActivosSinRepetir.Add(item.tipoActivo);
                }


                if (listatipoActivosSinRepetir.IndexOf(tipoActivo.tipoActivo) < 0 && tipoActivo.tipoActivo.Trim() != "")
                {
                    string palabras = tipoActivo.tipoActivo.Trim();
                    palabras = Regex.Replace(palabras, @"\s+", " ");
                    var tipoActivoInsertar = new TipoActivo()
                    {
                        tipoActivo = palabras
                    };
                    var dataTipoActivo = await _dalc.Set(tipoActivoInsertar, transaction);
                    if (dataTipoActivo != null)
                    {
                        return new ResponseBase<TipoActivo>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataTipoActivo
                        };
                    }
                    else
                    {
                        return new ResponseBase<TipoActivo>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<TipoActivo>()
                    {
                        codigo = (int)HttpStatusCode.BadRequest,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<TipoActivo>()
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
