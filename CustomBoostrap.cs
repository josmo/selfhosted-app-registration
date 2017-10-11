using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;
using NancyService.helpers;
using Jose;



namespace NancyService
{
    public class CustomBoostrap : DefaultNancyBootstrapper
    {

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: true);
            base.Configure(environment);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var configuration =
                new StatelessAuthenticationConfiguration(nancyContext =>
                {
    
                    var jwtToken = nancyContext.Request.Headers.Authorization;
                    

                    try
                    {
                        var requestToken = jwtToken.Substring("Bearer: ".Length).Trim();
                        var payload = JWT.Payload<JwtToken>(requestToken);
                        var epoch = new DateTime(1970, 1,1,0,0,0, DateTimeKind.Utc);
                        var tokenExpires = epoch.AddSeconds(payload.exp);


                        if (tokenExpires < DateTime.Now) return null;

                        var principal = new ClaimsPrincipal(new HttpListenerBasicIdentity(payload.sub, null));
                        IEnumerable<Claim> identityClaims = new List<Claim>();
                        if (payload.permissions != null)
                        {
                            identityClaims = payload.permissions.Select(claim => new Claim("permissions", claim));
                        }
                        var identities = new ClaimsIdentity(identityClaims);
                        principal.AddIdentity(identities);

                        return principal;
           
                    }
                    catch (Exception)
                    {
                       
                        return null;
                    }

                });
            
            StatelessAuthentication.Enable(pipelines, configuration);
            base.ApplicationStartup(container, pipelines);
            pipelines.AfterRequest += ctx =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "DELETE, POST");
            };
        }
      

    }
}

