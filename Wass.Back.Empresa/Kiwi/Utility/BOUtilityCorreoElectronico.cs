using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wass.Back.Empresa.Kiwi.Interface;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Correo;
using Wass.Back.Empresa.Rabbit.Interface;
using Wass.Back.Empresa.Rabbit.Utility;

namespace Wass.Back.Empresa.Kiwi.Utility
{
    public class BOUtilityCorreoElectronico : IBOUtilityConrreoElectronico
    {
		private readonly IUtilityCorreoElectronico _correo;

		public BOUtilityCorreoElectronico(IConfiguration config)
		{
			_correo = new UtilityCorreoElectronico(config);
		}

		public ResponseBase<RequestCorreo> DeserializeSolicitud(string json)
		{
			var response = new ResponseBase<RequestCorreo>();

			try
			{
				response.codigo = 200;
				response.datos = JsonConvert.DeserializeObject<RequestCorreo>(json);
			}
			catch (Exception ex)
			{
				response.codigo = 500;
				response.mensaje = ex.Message;
				response.datos = null;
			}

			return response;
		}


		public async Task<ResponseBase<bool>> EnviarCorreo(RequestCorreo correo, List<IFormFile> adjuntos)
		{
			var response = new ResponseBase<bool>();

			try
			{
				var archivos = await ConvertirAdjuntos(adjuntos);
				var result = await _correo.EnviarCorreo(correo, archivos);
				if (result.Item1)
				{
					response.codigo = 200;
					response.datos = true;
				}
				else
				{
					response.codigo = 500;
					response.mensaje = result.Item2;
					response.datos = false;
				}
			}
			catch (Exception ex)
			{
				response.codigo = 500;
				response.mensaje = ex.Message;
			}

			return response;
		}

		public async Task<List<(Stream file, string type, string name)>> ConvertirAdjuntos(List<IFormFile> files)
		{
			var streams = new List<(Stream file, string type, string name)>();
			foreach (var file in files)
			{
				if (file.Length > 0)
				{
					using (var reader = new StreamReader(file.OpenReadStream()))
					{
						var contentAsString = await reader.ReadToEndAsync();
						var bfile = new byte[contentAsString.Length * sizeof(char)];
						var sfile = new MemoryStream(bfile);

						streams.Add((sfile, file.ContentType, file.FileName));
					}
				}
			}
			return streams;
		}
	}
}
