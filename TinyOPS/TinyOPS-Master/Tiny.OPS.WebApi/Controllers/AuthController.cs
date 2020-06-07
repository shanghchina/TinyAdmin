using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tiny.OPS.Contract;
using Tiny.OPS.WebApi.Filter;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// login权限验证
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="authUser"></param>
        // POST: Auth/Login
        [HttpPost]
        [NoCheck]
        public Rsp_Auth_User Login([FromBody]VmAuthUser authUser)
        {
            // 密码解密

            // 查询验证码

            // 清除验证码

            // 生成令牌

            // 保存在线信息

            // 返回 token 与 用户信息
            var result = new Rsp_Auth_User();
            result.token = Guid.NewGuid().ToString();

            return result;
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        // POST: Auth/Info
        [HttpGet]
        [NoCheck]
        public VmVerifyCode GetCodeImg()
        {
            var result = new VmVerifyCode();
            result.img = Guid.NewGuid().ToString();
            result.uuid = Guid.NewGuid().ToString();
            return result;
        }


        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [NoCheck]
        private IActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (request.Username == "AngelaDaddy" && request.Password == "123456")
            {
                // push the user’s name into a claim, so we can identify the user later on.
                var claims = new[]
                {
                   new Claim(ClaimTypes.Name, request.Username)
               };

                string securityKey = "test秘钥";
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
                    issuer: "onlyEudTmc",
                    audience: "onlyEudTmc",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    data = new { token = new JwtSecurityTokenHandler().WriteToken(token) },
                    Code = 200,
                    Message = "获取Token成功!"
                });
            }

            return Ok(new
            {
                Data = new { },
                Code = 401,
                Message = "用户名密码错误!"
            });
        }
    }
}
