using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tiny.OPS.Contract;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateToken([FromBody] TokenRequest request)
        {
            if (request.Username == "AngelaDaddy" && request.Password == "123456")
            {
                // push the user’s name into a claim, so we can identify the user later on.
                var claims = new[]
                {
                   new Claim(ClaimTypes.Name, request.Username)
               };
                var securityKey = "123456";
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                /**
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:
                    iss: The issuer of the token，token 是给谁的
                    sub: The subject of the token，token 主题
                    exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                    iat: Issued At。 token 创建时间， Unix 时间戳格式
                    jti: JWT ID。针对当前 token 的唯一标识
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                 * */
                var token = new JwtSecurityToken(
                    issuer: "issuerCompany",
                    audience: "issuerCompany",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    data = new { token = jwtToken },
                    Code = 200,
                    Message = "获取Token成功!"
                });
            }

            return Ok(new
            {
                Data = new { },
                Code = 400,
                Message = "用户名密码错误!"
            });
        }


        /// <summary>
        /// 校验token
        /// </summary>
        /// <param name="token"></param>
        [HttpPost]
        public void CheckToken(string token)
        {
            string key = "f47b558d-7654-458c-99f2-13b190ef0199";
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            //校验token
            var validateParameter = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "issuerCompany",
                ValidAudience = "issuerCompany",
                IssuerSigningKey = securityKey,
            };

            //不校验，直接解析token
            //jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token1);
            try
            {
                //校验并解析token
                var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, validateParameter, out SecurityToken validatedToken);//validatedToken:解密后的对象
                var jwtPayload = ((JwtSecurityToken)validatedToken).Payload.SerializeToJson(); //获取payload中的数据 

            }
            catch (SecurityTokenExpiredException)
            {
                //表示过期
            }
            catch (SecurityTokenException)
            {
                //表示token错误
            }
        }

    }
}
