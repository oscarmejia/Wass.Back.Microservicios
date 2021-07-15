using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wass.Back.Seguridad.Models.Entity;
using Wass.Back.Seguridad.Models.Enum;
using Wass.Back.Seguridad.Models.Jwt;
using Wass.Back.Seguridad.Models.Peticiones.Base;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;
using Wass.Back.Seguridad.Rabbit.Context;
using Wass.Back.Seguridad.Rabbit.DALC;
using Wass.Back.Seguridad.Rabbit.Interface;
using Wass.Back.Seguridad.Rabbit.Utility;

namespace Wass.Back.Seguridad.Kiwi.Bussines
{
    public class BOUsuario
    {
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly IConfiguration _configuration;
		private readonly DALCUsuarios _dalc;
		private readonly IUtilityCorreoElectronico _email;

		private readonly string mayus = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private readonly string letras = "abcdefghijklmnopqrstuvwxyz";
		private readonly string numeros = "1234567890";
		string key = "01234567890123456789012345678901";


		public BOUsuario(SeguridadContext context, IConfiguration configuration)
		{
			_dalc = new DALCUsuarios(context);
			_email = new UtilityCorreoElectronico(configuration);
			_configuration = configuration;
		}

		public async Task<ResponseBase<Usuarios>> GetUsuarioId(long idUsuario)
		{
			try
			{
				var usuario = await _dalc.Get(idUsuario);
				if (usuario != null)
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = usuario
					};
				}
				else
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El usuario consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Usuarios>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Usuarios>> GetUsuarioProspectoIdEmpresa(long idEmpresa)
		{
			try
			{
				var usuario = await _dalc.GetUserProspectoPorEmpresa(idEmpresa);
				if (usuario != null)
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = usuario
					};
				}
				else
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El usuario consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Usuarios>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		public async Task<ResponseBase<List<Usuarios>>> GetUsuarioIdEmpresa(long idEmpresa)
		{
			try
			{
				var usuario = await _dalc.GetUserPorEmpresa(idEmpresa);
				if (usuario != null)
				{
					return new ResponseBase<List<Usuarios>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = usuario
					};
				}
				else
				{
					return new ResponseBase<List<Usuarios>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El usuario consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Usuarios>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<ResponseUsuario>> GetAutentica(RequestAutentica datos)
		{
			try
			{
				var usuario = await _dalc.GetAutenticar(datos.email, datos.passw);
				var objUsuario = JsonConvert.DeserializeObject<ResponseUsuario>(JsonConvert.SerializeObject(usuario));

				if (usuario != null)
				{
					objUsuario.token = new Token(_configuration["SecretsKeyApp:Jwt_Key"]).BuildToken(objUsuario);
					objUsuario.roles = usuario.usuarioRoles;
					return new ResponseBase<ResponseUsuario>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = objUsuario
					};
				}
				else
				{
					return new ResponseBase<ResponseUsuario>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "El nombre de usuario o contraseña no es valido, o el usuario no esta activo.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<ResponseUsuario>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<ResponseValidarToken>> ValidarToken(string token)
		{
			try
			{
				var jwt = token;
				var handler = new JwtSecurityTokenHandler();
				var tokenRead = handler.ReadJwtToken(jwt);

				var tokenDecode = tokenRead.Claims.ToList();

				var validTo = tokenRead.ValidTo;

				var userToken = 0;


				tokenDecode.ForEach(item => {
					if (item.Type == "idUsuario")
					{
						userToken = Int32.Parse(item.Value);
					}

				});

				var user = await _dalc.Get(userToken);


				if (user != null && DateTime.Now < validTo)
				{
					return new ResponseBase<ResponseValidarToken>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = "token valido",
						datos = new ResponseValidarToken() { tokenValido = true }
					};
				}
				else
				{
					return new ResponseBase<ResponseValidarToken>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = "token no valido",
						datos = new ResponseValidarToken() { tokenValido = false }
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<ResponseValidarToken>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Usuarios>> CrearUsuario(Usuarios datos, Transaction trans)
		{
			try
			{
				var ob = JsonConvert.DeserializeObject<Usuarios>(JsonConvert.SerializeObject(datos));
				if (trans == Transaction.Delete)
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = $"La operación de eliminar menús no ha sido implementada.",
						datos = null
					};
				}
				else
				{
					ob.passw = GenerarPassword();
					var data = await _dalc.Set(ob, trans);
					if (data != null)
					{
						/*
						 * Llenar datos de Contacto
						 */


						/*
						 * Enviar correo electrónico de creación de usuario
						 */
						var result = await EnviarCorreo(datos, ob.passw);

						if (result.estado)
						{
							return new ResponseBase<Usuarios>()
							{
								codigo = (int)HttpStatusCode.OK,
								estado = true,
								mensaje = $"Operación realizada con exito",
								datos = data
							};
						}
						else
						{
							return new ResponseBase<Usuarios>()
							{
								codigo = (int)HttpStatusCode.InternalServerError,
								estado = false,
								mensaje = $"Ha ocurrido un error: {result.msgError}",
								datos = data
							};
						}
					}
					else
					{
						return new ResponseBase<Usuarios>()
						{
							codigo = (int)HttpStatusCode.InternalServerError,
							estado = false,
							mensaje = $"La operación solicitada no se pudo realizar.",
							datos = data
						};
					}
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Usuarios>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Usuarios>> sendEmailForPassword(string email)
		{
			try
			{
				var dataEmail = await _dalc.GetUsuarioByEmail(email);

				if (dataEmail != null)
				{
					var dataEncripted = new RequestDataToEncrypted()
					{
						idUsuario = dataEmail.idUsuario,
						email = dataEmail.email,
						password = dataEmail.passw
					};

					var onjString = JsonConvert.SerializeObject(dataEncripted);

					var dataSecure = EncryptString(key, onjString);
					var formatDataSecure = dataSecure.Replace("/", "-").Replace("+", "_").Replace("=", "@");
					var dataSend = await sendEmailForgotPassWord(dataEmail, formatDataSecure);
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = "se ha enviado corretamente el correo",
						datos = dataEmail
					};
				}
				else
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.NoContent,
						estado = false,
						mensaje = "No se ha enviado el correo",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Usuarios>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Usuarios>> AsociarUsuario(RequestAsociar asociar)
		{
			try
			{
				var data = await _dalc.Asociar(asociar);
				if (data != null)
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = data
					};
				}
				else
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = data
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Usuarios>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Usuarios>> CambiarContrasena(RequestContrasena cambiar)
		{
			try
			{
				var data = await _dalc.Get(cambiar.idUsuario);
				var val = ValidarSeguridad(cambiar.contrasenaNueva);

				if ((data.passw == cambiar.contrasenaAnterior) && val)
				{
					var result = await _dalc.CambiarContrasena(cambiar);
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = result
					};
				}
				else
				{
					return new ResponseBase<Usuarios>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = !val ?
							"Datos inconsistentes en la contrasena. Debe contener por lo menos una mayuscula y un número" :
							"La contraseña anterior no coincide.",
						datos = data
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Usuarios>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		private bool ValidarSeguridad(string contrasena)
		{
			if (contrasena.Length < 5) return false;

			var rmayus = mayus.ToCharArray().Select(x => x.ToString()).ToList();
			var rnum = numeros.ToCharArray().Select(x => x.ToString()).ToList();
			var xmayus = 0;
			var xnum = 0;

			foreach (var item in rmayus) if (contrasena.Contains(item)) xmayus += 1;
			foreach (var item in rnum) if (contrasena.Contains(item)) xnum += 1;

			if (xmayus == 0 || xnum == 0) return false;
			return true;
		}

		private string GenerarPassword()
		{
			var rand = new Random();
			var psw = "";

			for (var x = 0; x < 3; x++) psw = $"{psw}{mayus[rand.Next(0, 25)]}";
			for (var x = 0; x < 4; x++) psw = $"{psw}{letras[rand.Next(0, 25)]}";
			for (var x = 0; x < 3; x++) psw = $"{psw}{numeros[rand.Next(0, 9)]}";

			return Desordenar(psw);
		}

		private string Desordenar(string cadena)
		{
			var arr = cadena.ToCharArray().Select(x => x.ToString()).ToList();
			var arrDes = new List<string>();
			var res = "";
			var randNum = new Random();

			while (arr.Count > 0)
			{
				int val = randNum.Next(0, arr.Count - 1);
				arrDes.Add(arr[val]);
				arr.RemoveAt(val);
			}
			foreach (var item in arrDes) res = $"{res}{item}";
			return res;
		}

		public string EncryptString(string key, string plainText)
		{
			byte[] iv = new byte[16];
			byte[] array;

			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(key);
				aes.IV = iv;
				aes.Padding = PaddingMode.PKCS7;
				aes.Mode = CipherMode.CBC;

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
						{
							streamWriter.Write(plainText);
						}

						array = memoryStream.ToArray();
					}
				}
			}

			return Convert.ToBase64String(array);
		}

		private async Task<(bool estado, string msgError)> EnviarCorreo(Usuarios datos, string psw)
		{
			return await _email.EnviarCorreo(new RequestCorreo()
			{
				destinatario = datos.email,
				asunto = "Creación de usuario WASS",
				conCopia = null,
				contenido = $"Bienvenido a WASS<br/><br/>"
							+ $"Se ha creado un nuevo usuario con el correo electrónico <b>{datos.email}</b>.<br/>"
							+ $"Tu contraseña de acceso es <b>{psw}</b>.<br/><br/><br/>"
							+ "Sigue los siguientes links para continuar:<br/><br/>"
							+ "1. Para cambiar tu contraseña puedes dar click en el siguiente link<br/>"
							+ $"{datos.urlCambioContrasena}.<br/><br/>"
							+ "2. Para continuar tu proceso y registrar tu Empresa, ingresa al siguiente link<br/>"
							+ $"{datos.urlDatosEmpresa}<br/><br/><br/><br/>"
							+ "Saludos,<br/>Equipo de WASS"
			});
		}

		private async Task<(bool estado, string msgError)> sendEmailForgotPassWord(Usuarios usuario, string dataSecure)
		{
			var url = "http://localhost:4200/restorePassword/" + dataSecure;
			return await _email.EnviarCorreo(new RequestCorreo()
			{
				destinatario = usuario.email,
				asunto = "Recuperacion de contraseña para cuenta Wass",
				conCopia = null,
				contenido = $"Bienvenido a WASS<br/><br/>"
							+ $"senor@ <b>{usuario.idUsuario}</b>.<br/>"
							+ $"hemo resivido una peticion de recuperacion de contraseña<b></b>.<br/>"
							+ $"para tu cuenta de wass <b></b>.<br/><br/><br/>"
							+ "Sigue los siguientes links para continuar:<br/><br/>"
							+ "1. Para reestableser tu contraseña puedes dar click en el siguiente link<br/>"
							+ $"<b>{url}<br/>.<br/>"
							+ "Saludos,<br/>Equipo de WASS"
			});
		}
	}
}
