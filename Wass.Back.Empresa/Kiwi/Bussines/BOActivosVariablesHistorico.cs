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
    public class BOActivosVariablesHistorico
    {
        public Dictionary<string, string> EndPointsDictinoDictionary { get; set; }
        private readonly DALCActivosVariablesHistorico _dalc;
        private readonly string _msg_base;

        public BOActivosVariablesHistorico(EmpresaContext context)
        {
            _dalc = new DALCActivosVariablesHistorico(context);
            _msg_base = "historico de cambios de variables";
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetHistoricoPorIdVariableAsync(long idActivoClasificacionVariable)
        {
            try
            {
                var obj = await _dalc.GetHistoricoPorIdVariableAsync(idActivoClasificacionVariable);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
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
