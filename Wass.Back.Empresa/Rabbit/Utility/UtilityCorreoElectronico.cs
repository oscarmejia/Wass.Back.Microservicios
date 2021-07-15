using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Wass.Back.Empresa.Models.Peticiones.v1.Correo;
using Wass.Back.Empresa.Rabbit.Interface;

namespace Wass.Back.Empresa.Rabbit.Utility
{
    public class UtilityCorreoElectronico : IUtilityCorreoElectronico
    {
		private readonly IConfiguration _config;

		public UtilityCorreoElectronico(IConfiguration config)
        {
        }

        public async Task<(bool, string)> EnviarCorreo(RequestCorreo correo, List<(Stream archivo, string tipo, string nombre)> adjuntos = null)
        {
			try
			{
				var servidor = _config["Correo:Servidor"];
				var puerto = int.Parse(_config["Correo:Puerto"]);
				var usuario = _config["Correo:Usuario"];
				var contraseña = _config["Correo:Contrasena"];

				using (var client = new SmtpClient(servidor, puerto))
				{
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential(usuario, contraseña);
					client.EnableSsl = true;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;

					var mailMessage = new MailMessage()
					{
						From = new MailAddress(usuario),
						Body = correo.contenido,
						Subject = correo.asunto,
						IsBodyHtml = true
					};

					mailMessage.To.Add(correo.destinatario);
					if (correo.conCopia != null && correo.conCopia.Length > 0)
					{
						foreach (var item in correo.conCopia) mailMessage.CC.Add(item);
					}
					if (adjuntos != null && adjuntos.Count > 0)
					{
						foreach (var item in adjuntos) mailMessage.Attachments.Add(new Attachment(item.archivo, item.nombre));
					}

					await client.SendMailAsync(mailMessage);
					return (true, "");
				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}
    }
}
