using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace Business.BusinessAcpects
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            //var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            //foreach (var role in _roles)
            //{
            //    if (roleClaims.Contains(role))
            //    {
            //        return;
            //    }
            //}

            var token = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            if (token != "")
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var decodeToken = jwtSecurityToken.Claims;
                //yetkileri göndermemişsin token ile
                foreach (var claim in decodeToken)
                {
                    foreach (var role in _roles)
                    {
                        if (claim.ToString().Contains(role))
                        {
                            //tmmdır sorun çözdül ü:) var mı başka bir şey yok hocam çok teşekkü ederim
                            //lafı olmaz takılırsan yaz tekrardan tmmdır hocam çok teşeşkürler
                            return;
                        }
                    }
                }
            }
            throw new Exception("İşlem yapmaya yetkiniz yok");
        }
    }
}
