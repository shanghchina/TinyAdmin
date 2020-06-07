using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;
using Tiny.OPS.WebApi.Filter;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// login登录
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : BaseController
    {

        private IT_Sys_UserDomainService domainService => IoC.Resolve<IT_Sys_UserDomainService>();


        //[HttpPost]
        //[NoCheck]
        //public Rsp_Auth_User LoginIn([FromBody]VmAuthUser authUser)
        //{
        //    // 密码解密
        //    var vmEntity = domainService.GetSysUserByName(authUser.username);

        //    // 返回 token 与 用户信息

        //    var result = new Rsp_Auth_User();
        //    result.user = domainService.GetSysUserByName(authUser.username);
        //    result.token = GetJwtToken(authUser.username);
        //    return result;
        //}

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="authUser"></param>
        // POST: Login/Login
        [HttpPost]
        [NoCheck]
        public BaseResponse LoginIn([FromBody]VmAuthUser authUser)
        {
            try
            {
                //var headToken = Request.Headers;

                //var token = headToken["token"];
                //CheckJwtToken(token);

                if (authUser.password == "123456")
                {
                    var result = new Rsp_Auth_User();
                    result.user = domainService.GetSysUserByName(authUser.username);
                    result.token = GetJwtToken(authUser.username);
                    return ApiSuccessResult(result);
                }
                else
                {
                    return ApiError401("账号密码错误，请重新输入");
                }
            }
            catch (Exception ex)
            {
                _Log4Net.Error("LoginIn--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }


        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="userName"></param>
        // POST: Login/GetSysUser
        [HttpPost]
        [NoCheck]
        public BaseResponse GetSysUser([FromBody]string userName)
        {
            try
            {
                var headToken = Request.Headers;

                var token = headToken["token"];
                CheckJwtToken(token);
                return ApiSuccessResult(token);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetSysUser--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private string GetJwtToken(string userName)
        {
            var securityKey = "1234567890123456";
            //if (request.Username == "AngelaDaddy" && request.Password == "123456")
            {
                // push the user’s name into a claim, so we can identify the user later on.
                var claims = new[]
                {
                   new Claim(ClaimTypes.Name, userName)
               };

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
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.WriteToken(token);

                return jwtToken;
            }


        }

        /// <summary>
        /// 校验token
        /// </summary>
        /// <param name="token"></param>
        private void CheckJwtToken(string token)
        {
            string key = "1234567890123456";
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


        /// <summary>
        /// LoginOut
        /// </summary>
        /// <returns></returns>
        [NoCheck]
        [HttpPost]
        public BaseResponse LoginOut()
        {
            try
            {
                var headToken = Request.Headers;

                var result = string.Empty;
                return ApiSuccessResult(result);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("LoginOut--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }


    }
}