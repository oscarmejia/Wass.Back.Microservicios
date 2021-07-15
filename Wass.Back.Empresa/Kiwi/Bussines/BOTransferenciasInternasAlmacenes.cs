using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.TransferenciasInternasAlmacenes;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOTransferenciasInternasAlmacenes
    {
        private readonly DALCTransferenciasInternasAlmacenes _dalc;
        private readonly DALCAlmacen _dalcAlmacen;
        private readonly DALCRepuestosAlmacen _dalcRepuestosAlmacen;
        private readonly DALCRepuestos _dalcRepuestos;

        public BOTransferenciasInternasAlmacenes(EmpresaContext context)
        {
            _dalc = new DALCTransferenciasInternasAlmacenes(context);
            _dalcAlmacen = new DALCAlmacen(context);
            _dalcRepuestosAlmacen = new DALCRepuestosAlmacen(context);
            _dalcRepuestos = new DALCRepuestos(context);
        }

        public async Task<ResponseBase<List<TransferenciasInternasAlmacenesRequest>>> GetTodas()
        {
            try
            {
                var transferenciasInternasAlmacenesRequest = await _dalc.GetTodas();
                var datos = new List<TransferenciasInternasAlmacenesRequest>();

                if (transferenciasInternasAlmacenesRequest != null && transferenciasInternasAlmacenesRequest.Count > 0)
                {
                    foreach (var item in transferenciasInternasAlmacenesRequest)
                    {
                        var data = new TransferenciasInternasAlmacenesRequest()
                        {
                            idTransferenciasInternasAlmacenes = item.idTransferenciasInternasAlmacenes,
                            idAlmacenEmisor = item.idAlmacenEmisor,
                            idAlmacenReceptor = item.idAlmacenReceptor,
                            repuestos = !String.IsNullOrEmpty(item.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRequest>>(item.repuestos) : new List<RepuestosRequest>(),
                            fechaHora = item.fechaHora,
                            motivoTransferencia = item.motivoTransferencia,
                            ordenTrabajo = !String.IsNullOrEmpty(item.ordenTrabajo) ? JsonConvert.DeserializeObject<OrdenTrabajoRequest>(item.ordenTrabajo) : new OrdenTrabajoRequest(),
                            idEmpresa = item.idEmpresa,
                            estado = item.estado

                        };

                        datos.Add(data);
                    }
                    return new ResponseBase<List<TransferenciasInternasAlmacenesRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<TransferenciasInternasAlmacenesRequest>>()
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
                return new ResponseBase<List<TransferenciasInternasAlmacenesRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<TransferenciasInternasAlmacenesRequest>>> GetTodasPorEmpresa(long idEmpresa)
        {
            try
            {
                var transferenciasInternasAlmacenesRequest = await _dalc.GetTodasPorEmpresa(idEmpresa);
                var datos = new List<TransferenciasInternasAlmacenesRequest>();

                if (transferenciasInternasAlmacenesRequest != null && transferenciasInternasAlmacenesRequest.Count > 0)
                {
                    foreach (var item in transferenciasInternasAlmacenesRequest)
                    {
                        var data = new TransferenciasInternasAlmacenesRequest()
                        {
                            idTransferenciasInternasAlmacenes = item.idTransferenciasInternasAlmacenes,
                            idAlmacenEmisor = item.idAlmacenEmisor,
                            idAlmacenReceptor = item.idAlmacenReceptor,
                            repuestos = !String.IsNullOrEmpty(item.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRequest>>(item.repuestos) : new List<RepuestosRequest>(),
                            fechaHora = item.fechaHora,
                            motivoTransferencia = item.motivoTransferencia,
                            ordenTrabajo = !String.IsNullOrEmpty(item.ordenTrabajo) ? JsonConvert.DeserializeObject<OrdenTrabajoRequest>(item.ordenTrabajo) : new OrdenTrabajoRequest(),
                            idEmpresa = item.idEmpresa,
                            estado = item.estado

                        };

                        datos.Add(data);
                    }
                    return new ResponseBase<List<TransferenciasInternasAlmacenesRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<TransferenciasInternasAlmacenesRequest>>()
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
                return new ResponseBase<List<TransferenciasInternasAlmacenesRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TransferenciasInternasAlmacenesRequest>> GetPorId(long idTransferenciasInternasAlmacenesRequest)
        {
            try
            {
                var transferenciasInternasAlmacenesRequest = await _dalc.Get(idTransferenciasInternasAlmacenesRequest);
                if (transferenciasInternasAlmacenesRequest != null)
                {
                    var data = new TransferenciasInternasAlmacenesRequest()
                    {
                        idTransferenciasInternasAlmacenes = transferenciasInternasAlmacenesRequest.idTransferenciasInternasAlmacenes,
                        idAlmacenEmisor = transferenciasInternasAlmacenesRequest.idAlmacenEmisor,
                        idAlmacenReceptor = transferenciasInternasAlmacenesRequest.idAlmacenReceptor,
                        repuestos = !String.IsNullOrEmpty(transferenciasInternasAlmacenesRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRequest>>(transferenciasInternasAlmacenesRequest.repuestos) : new List<RepuestosRequest>(),
                        fechaHora = transferenciasInternasAlmacenesRequest.fechaHora,
                        motivoTransferencia = transferenciasInternasAlmacenesRequest.motivoTransferencia,
                        ordenTrabajo = !String.IsNullOrEmpty(transferenciasInternasAlmacenesRequest.ordenTrabajo) ? JsonConvert.DeserializeObject<OrdenTrabajoRequest>(transferenciasInternasAlmacenesRequest.ordenTrabajo) : new OrdenTrabajoRequest(),
                        idEmpresa = transferenciasInternasAlmacenesRequest.idEmpresa,
                        estado = transferenciasInternasAlmacenesRequest.estado
                    };
                    return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
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
                return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TransferenciasInternasAlmacenesRequest>> actualizarTransferenciasInternasAlmacenes(long idTransferenciaInterna, long estado)
        {
            try
            {
                var getTransferencia = await _dalc.Get(idTransferenciaInterna);

                if (getTransferencia != null)
                {
                    //estado = 167 => Recibida 180
                    //estado = 168 => Rechazada 181
                    if (getTransferencia.estado == 180 && (estado == 181 || estado == 180))
                    {
                        return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                        {
                            codigo = (int)HttpStatusCode.BadRequest,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar. No puede pasar de estado Recibida a estado Cancelada o la transferencia ya se realizó ",
                            datos = null
                        };
                    }
                    var data = await _dalc.actualizarEstado(getTransferencia.idTransferenciasInternasAlmacenes, estado);

                    var dataTransformada = new TransferenciasInternasAlmacenesRequest()
                    {
                        idTransferenciasInternasAlmacenes = data.idTransferenciasInternasAlmacenes,
                        idAlmacenEmisor = data.idAlmacenEmisor,
                        idAlmacenReceptor = data.idAlmacenReceptor,
                        repuestos = !String.IsNullOrEmpty(data.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRequest>>(data.repuestos) : new List<RepuestosRequest>(),
                        fechaHora = data.fechaHora,
                        motivoTransferencia = data.motivoTransferencia,
                        ordenTrabajo = !String.IsNullOrEmpty(data.ordenTrabajo) ? JsonConvert.DeserializeObject<OrdenTrabajoRequest>(data.ordenTrabajo) : new OrdenTrabajoRequest(),
                        idEmpresa = data.idEmpresa,
                        estado = data.estado
                    };
                    if (estado == 180)
                    {

                        var almacenEmisor = await _dalcAlmacen.Get(dataTransformada.idAlmacenEmisor);
                        var almacenReceptor = await _dalcAlmacen.Get(dataTransformada.idAlmacenReceptor);

                        //long almacenE = 1 almacenEmisor, almacenR = 2 almacenReceptor
                        long almacenE = 1;
                        long almacenR = 2;
                        // almacen.tipo == 1 Fisico
                        // almacen.tipo == 2 Virtual

                        //Valida si la cantidad  en el almacen emisor es igual o mayor a la cantidad solicitada en la transferencia
                        bool cantidad = false;

                        List<RepuestosRequest> ob = new List<RepuestosRequest>();

                        if (almacenEmisor != null && almacenReceptor != null && ((almacenEmisor.tipo == 1 && almacenReceptor.tipo == 1) || (almacenEmisor.tipo == 1 && almacenReceptor.tipo == 2) || (almacenEmisor.tipo == 2 && almacenReceptor.tipo == 1)))
                        {
                            foreach (var item in dataTransformada.repuestos)
                            {
                                var repuestoEmisor = await _dalcRepuestosAlmacen.GetPorIdRepuestoAlmacen(Convert.ToInt64(item.idRepuesto), almacenEmisor.idAlmacen);
                                var repuestoReceptor = await _dalcRepuestosAlmacen.GetPorIdRepuestoAlmacen(Convert.ToInt64(item.idRepuesto), almacenReceptor.idAlmacen);

                                if (repuestoEmisor != null && repuestoReceptor != null)
                                {
                                    var repuestosAlmacenEmisor = await _dalcRepuestosAlmacen.Get(repuestoEmisor.idRepuestosAlmacen);
                                    //estado = 1 Creada, estado = 2 Recibida, estado = 3 Cancelada
                                    if (repuestosAlmacenEmisor.cantidadActual >= Convert.ToInt64(item.cantidad))
                                    {
                                        var cantidadRepuestoE = await _dalcRepuestosAlmacen.TransferirRepuestosAlmacen(almacenEmisor.idAlmacen, Convert.ToInt64(item.idRepuesto), Convert.ToInt64(item.cantidad), almacenE);
                                        var cantidadRepuestoR = await _dalcRepuestosAlmacen.TransferirRepuestosAlmacen(almacenReceptor.idAlmacen, Convert.ToInt64(item.idRepuesto), Convert.ToInt64(item.cantidad), almacenR);
                                        ob.Add(item);
                                        cantidad = true;
                                    }
                                }
                                if (repuestoEmisor != null)
                                {
                                    item.existenciaActual = repuestoEmisor.cantidadActual;
                                }
                                if (repuestoReceptor == null && repuestoEmisor != null)
                                {
                                    var repuestosAlmacenEmisor = await _dalcRepuestosAlmacen.Get(repuestoEmisor.idRepuestosAlmacen);
                                    var nuevoRepuestoAlmacen = new RepuestosAlmacen()
                                    {
                                        cantidadActual = Convert.ToInt64(item.cantidad),
                                        idAlmacen = almacenReceptor.idAlmacen,
                                        idRepuestos = repuestoEmisor.idRepuestos,
                                    };

                                    if (repuestosAlmacenEmisor.cantidadActual >= Convert.ToInt64(item.cantidad))
                                    {
                                        var dataNuevoRepuestoAlmacen = await _dalcRepuestosAlmacen.Set(nuevoRepuestoAlmacen, Transaction.Insert);
                                        var cantidadRepuestoE = await _dalcRepuestosAlmacen.TransferirRepuestosAlmacen(almacenEmisor.idAlmacen, Convert.ToInt64(item.idRepuesto), Convert.ToInt64(item.cantidad), almacenE);
                                        ob.Add(item);
                                        cantidad = true;
                                    }
                                }
                            }
                            if (ob.Count > 0 && cantidad == true)
                            {

                                dataTransformada.repuestos = ob;

                                getTransferencia.repuestos = JsonConvert.SerializeObject(dataTransformada.repuestos);
                                getTransferencia.estado = dataTransformada.estado;

                                var dataTransferenciasInternasAlmacenesRequest = await _dalc.Set(getTransferencia, Transaction.Update);

                                var dataResp = new TransferenciasInternasAlmacenesRequest()
                                {
                                    idTransferenciasInternasAlmacenes = dataTransferenciasInternasAlmacenesRequest.idTransferenciasInternasAlmacenes,
                                    idAlmacenEmisor = dataTransferenciasInternasAlmacenesRequest.idAlmacenEmisor,
                                    idAlmacenReceptor = dataTransferenciasInternasAlmacenesRequest.idAlmacenReceptor,
                                    repuestos = !String.IsNullOrEmpty(dataTransferenciasInternasAlmacenesRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRequest>>(dataTransferenciasInternasAlmacenesRequest.repuestos) : new List<RepuestosRequest>(),
                                    fechaHora = dataTransferenciasInternasAlmacenesRequest.fechaHora,
                                    motivoTransferencia = dataTransferenciasInternasAlmacenesRequest.motivoTransferencia,
                                    ordenTrabajo = !String.IsNullOrEmpty(dataTransferenciasInternasAlmacenesRequest.ordenTrabajo) ? JsonConvert.DeserializeObject<OrdenTrabajoRequest>(dataTransferenciasInternasAlmacenesRequest.ordenTrabajo) : new OrdenTrabajoRequest(),
                                    idEmpresa = dataTransferenciasInternasAlmacenesRequest.idEmpresa,
                                    estado = dataTransferenciasInternasAlmacenesRequest.estado
                                };
                                return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                                {
                                    codigo = (int)HttpStatusCode.OK,
                                    estado = true,
                                    mensaje = "Operacion realizada con exito",
                                    datos = dataResp
                                };
                            }
                            else
                            {
                                return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                                {
                                    codigo = (int)HttpStatusCode.BadRequest,
                                    estado = false,
                                    mensaje = "La operacion no se ha podido completar. ",
                                    datos = null
                                };
                            }
                        }
                    }
                    else
                    {
                        return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar. No se pueden hacer transferencias entre almacenes virtuales o los almacenes no existen. El estado debe ser Recibido",
                            datos = null
                        };
                    }

                    var dataRespuesta = new TransferenciasInternasAlmacenesRequest()
                    {
                        idTransferenciasInternasAlmacenes = data.idTransferenciasInternasAlmacenes,
                        idAlmacenEmisor = data.idAlmacenEmisor,
                        idAlmacenReceptor = data.idAlmacenReceptor,
                        repuestos = !String.IsNullOrEmpty(data.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRequest>>(data.repuestos) : new List<RepuestosRequest>(),
                        fechaHora = data.fechaHora,
                        motivoTransferencia = data.motivoTransferencia,
                        ordenTrabajo = !String.IsNullOrEmpty(data.ordenTrabajo) ? JsonConvert.DeserializeObject<OrdenTrabajoRequest>(data.ordenTrabajo) : new OrdenTrabajoRequest(),
                        idEmpresa = data.idEmpresa,
                        estado = data.estado
                    };
                    return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar. El idTransferencia no existe ",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TransferenciasInternasAlmacenesRequest>> guardarTransferenciasInternasAlmacenes(TransferenciasInternasAlmacenesRequest transferenciasInternasAlmacenesRequest, Transaction transaction)
        {
            try
            {
                var almacenEmisor = await _dalcAlmacen.Get(transferenciasInternasAlmacenesRequest.idAlmacenEmisor);
                var almacenReceptor = await _dalcAlmacen.Get(transferenciasInternasAlmacenesRequest.idAlmacenReceptor);


                //Motivo Transferencia Orden de trabajo = 1, Balancear Repuestos = 2, Devolucion = 3
                if (transferenciasInternasAlmacenesRequest.motivoTransferencia == 1 && transferenciasInternasAlmacenesRequest.ordenTrabajo == null)
                {
                    return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar. Debe ingresar el id de orden de trabajo y el id de usuario ",
                        datos = null
                    };
                }

                List<RepuestosRequest> ob = new List<RepuestosRequest>();

                if (almacenEmisor != null && almacenReceptor != null && ((almacenEmisor.tipo == 1 && almacenReceptor.tipo == 1) || (almacenEmisor.tipo == 1 && almacenReceptor.tipo == 2) || (almacenEmisor.tipo == 2 && almacenReceptor.tipo == 1)))
                {
                    //estado = 166 => Creada
                    var dataTransformada = new TransferenciasInternasAlmacenes()
                    {
                        idTransferenciasInternasAlmacenes = transferenciasInternasAlmacenesRequest.idTransferenciasInternasAlmacenes,
                        idAlmacenEmisor = transferenciasInternasAlmacenesRequest.idAlmacenEmisor,
                        idAlmacenReceptor = transferenciasInternasAlmacenesRequest.idAlmacenReceptor,
                        repuestos = JsonConvert.SerializeObject(transferenciasInternasAlmacenesRequest.repuestos),
                        fechaHora = transferenciasInternasAlmacenesRequest.fechaHora,
                        motivoTransferencia = transferenciasInternasAlmacenesRequest.motivoTransferencia,
                        ordenTrabajo = JsonConvert.SerializeObject(transferenciasInternasAlmacenesRequest.ordenTrabajo),
                        idEmpresa = transferenciasInternasAlmacenesRequest.idEmpresa,
                        estado = 179
                    };
                    if (dataTransformada != null)
                    {
                        var dataTransferenciasInternasAlmacenesRequest = await _dalc.Set(dataTransformada, transaction);
                        var dataRespuesta = new TransferenciasInternasAlmacenesRequest()
                        {
                            idTransferenciasInternasAlmacenes = dataTransferenciasInternasAlmacenesRequest.idTransferenciasInternasAlmacenes,
                            idAlmacenEmisor = dataTransferenciasInternasAlmacenesRequest.idAlmacenEmisor,
                            idAlmacenReceptor = dataTransferenciasInternasAlmacenesRequest.idAlmacenReceptor,
                            repuestos = !String.IsNullOrEmpty(dataTransferenciasInternasAlmacenesRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRequest>>(dataTransferenciasInternasAlmacenesRequest.repuestos) : new List<RepuestosRequest>(),
                            fechaHora = dataTransferenciasInternasAlmacenesRequest.fechaHora,
                            motivoTransferencia = dataTransferenciasInternasAlmacenesRequest.motivoTransferencia,
                            ordenTrabajo = !String.IsNullOrEmpty(dataTransferenciasInternasAlmacenesRequest.ordenTrabajo) ? JsonConvert.DeserializeObject<OrdenTrabajoRequest>(dataTransferenciasInternasAlmacenesRequest.ordenTrabajo) : new OrdenTrabajoRequest(),
                            idEmpresa = dataTransferenciasInternasAlmacenesRequest.idEmpresa,
                            estado = dataTransferenciasInternasAlmacenesRequest.estado
                        };
                        return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataRespuesta
                        };
                    }
                    else
                    {
                        return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar. ",
                            datos = null
                        };
                    }

                }
                else
                {
                    return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar. No se pueden hacer transferencias entre almacenes virtuales o los almacenes no existen",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<TransferenciasInternasAlmacenesRequest>()
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
