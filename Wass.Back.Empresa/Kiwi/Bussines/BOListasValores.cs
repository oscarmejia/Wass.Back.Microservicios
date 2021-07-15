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
    public class BOListasValores : IBOLectura<ListasValores>
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCListasValores _dalc;

        public BOListasValores(EmpresaContext context)
        {
            _dalc = new DALCListasValores(context);
        }

        public async Task<ResponseBase<ListasValores>> GetAsync(long id)
        {
            try
            {
                var obj = await _dalc.GetAsync(id);

                if (obj != null)
                {
                    return new ResponseBase<ListasValores>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<ListasValores>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "El valor consultado no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ListasValores>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ListasValores>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ListasValores>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ListasValores>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay valores de la lista disponibles disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ListasValores>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de valore de listas disponibles no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ListasValores>>()
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
