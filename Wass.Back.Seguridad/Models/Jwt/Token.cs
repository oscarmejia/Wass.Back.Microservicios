using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;

namespace Wass.Back.Seguridad.Models.Jwt
{
    public class Token
    {
        public string _token { get; set; }
        public Token(string token)
        {
            _token = token;
        }
        public string BuildToken(ResponseUsuario user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var oClaims = new List<Claim>
            {
                new Claim("AppID","Wass_App"),
                new Claim("EmailCorporativo", user.email),
                new Claim("idUsuario", user.idUsuario.ToString()),
                new Claim("idEmpresa", user.idEmpresa.ToString())
            };

            var token = new JwtSecurityToken("http://localhost:63939/",
              _token,
              expires: DateTime.Now.AddHours(10),
              signingCredentials: creds,
                claims: oClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
