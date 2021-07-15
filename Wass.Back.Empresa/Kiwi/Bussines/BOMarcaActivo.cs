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
    public class BOMarcaActivo
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCMarcaActivo _dalc;

        public BOMarcaActivo(EmpresaContext context)
        {
            _dalc = new DALCMarcaActivo(context);
        }

        public async Task<ResponseBase<List<MarcaActivo>>> GetTodas()
        {
            try
            {
                var marcaActivo = await _dalc.GetTodas();

                if (marcaActivo != null)
                {
                    return new ResponseBase<List<MarcaActivo>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = marcaActivo
                    };
                }
                else
                {
                    return new ResponseBase<List<MarcaActivo>>()
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
                return new ResponseBase<List<MarcaActivo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<MarcaActivo>> GetPorId(long idMarcaActivo)
        {
            try
            {
                var marcaActivo = await _dalc.Get(idMarcaActivo);
                if (marcaActivo != null)
                {
                    return new ResponseBase<MarcaActivo>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = marcaActivo
                    };
                }
                else
                {
                    return new ResponseBase<MarcaActivo>()
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
                return new ResponseBase<MarcaActivo>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<MarcaActivo>> guardar(MarcaActivo marcaActivo, Transaction transaction)
        {
            try
            {
                var marcaActivos = await _dalc.GetTodas();
                var listaMarcaActivosSinRepetir = new List<string>();
                foreach (var item in marcaActivos)
                {
                    listaMarcaActivosSinRepetir.Add(item.marcaActivo.Trim());
                }

                if (listaMarcaActivosSinRepetir.IndexOf(marcaActivo.marcaActivo.Trim()) < 0 && marcaActivo.marcaActivo.Trim() != "")
                {
                    string palabras = marcaActivo.marcaActivo.Trim();
                    palabras = Regex.Replace(palabras, @"\s+", " ");
                    var marcaActivoInsertar = new MarcaActivo()
                    {
                        marcaActivo = palabras
                    };
                    var dataMarcaActivo = await _dalc.Set(marcaActivoInsertar, transaction);
                    if (dataMarcaActivo != null)
                    {
                        return new ResponseBase<MarcaActivo>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataMarcaActivo
                        };
                    }
                    else
                    {
                        return new ResponseBase<MarcaActivo>()
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
                    return new ResponseBase<MarcaActivo>()
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
                return new ResponseBase<MarcaActivo>()
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
