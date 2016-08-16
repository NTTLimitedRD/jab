using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http;
using System.Net.Http.Headers;

namespace WebApi.Filters
{
    public class SimpleAuthenticationFilter : IAuthenticationFilter
    {
		/// <summary>
		///		Pre-completed task returned by methods that aren't really async.
		/// </summary>
		static readonly Task CompletedTask = Task.FromResult<object>(null);

        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpActionContext actionContext = context.ActionContext;
            if (actionContext == null)
                return CompletedTask;

            if (actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count != 0 ||
                actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count != 0)
                {
                    return CompletedTask;
                }

            AuthenticationHeaderValue authenticationHeader = context.Request.Headers.Authorization;
            if (authenticationHeader == null)
            {
                context.ErrorResult = new AuthenticationFailureResult(context.Request, "No authorization header found.");
                return CompletedTask;
            }

            if (authenticationHeader.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult(context.Request, string.Format("Unsupported authentication type {0}", authenticationHeader.Scheme));
                return CompletedTask;
            }

            if (string.IsNullOrWhiteSpace(authenticationHeader.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult(context.Request, "No authentication token is supplied.");
                return CompletedTask;
            }

            return CompletedTask;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new AddChallengeOnUnauthenticationResult(new AuthenticationHeaderValue("Bearer"), context.Result);
            return CompletedTask;
        }
    }
}