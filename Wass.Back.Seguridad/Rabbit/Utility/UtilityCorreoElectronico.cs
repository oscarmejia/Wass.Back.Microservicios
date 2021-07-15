using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;
using Wass.Back.Seguridad.Rabbit.Interface;

namespace Wass.Back.Seguridad.Rabbit.Utility
{
    public class UtilityCorreoElectronico : IUtilityCorreoElectronico
    {
        private readonly IConfiguration _config;

        public UtilityCorreoElectronico(IConfiguration config)
        {
			_config = config;
		}

        public async Task<(bool estado, string msgError)> EnviarCorreo(RequestCorreo correo)
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
