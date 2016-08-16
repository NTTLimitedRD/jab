using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace WebApi
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
		/// <summary>
		///		Create a new HTTP authentication failure result.
		/// </summary>
		/// <param name="request">
		///		An <see cref="HttpRequestMessage"/> representing the request that failed authentication.
		/// </param>
		/// <param name="reason">
		///		A textual description of the reason that authentication failed.
		/// </param>
		public AuthenticationFailureResult(HttpRequestMessage request, string reason)
		{
			if (request == null)
				throw new ArgumentNullException("request");

			if (String.IsNullOrWhiteSpace(reason))
				throw new System.ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'reason'.", "reason");

			Request = request;
			Reason = reason;
		}

		/// <summary>
		///		An <see cref="HttpRequestMessage"/> representing the request that failed authentication.
		/// </summary>
		public HttpRequestMessage Request
		{
			get; 
			private set;
		}

		/// <summary>
		///		A textual description of the reason that authentication failed.
		/// </summary>
		public string Reason
		{
			get;
			private set;
		}

		/// <summary>
		///		Asynchronously execute the result.
		/// </summary>
		/// <param name="cancellationToken">
		///		A cancellation token that can be used to cancel execution.
		/// </param>
		/// <returns>
		///		The resulting <see cref="HttpResponseMessage"/>.
		/// </returns>
		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(
				Request.CreateErrorResponse(
					statusCode: HttpStatusCode.Unauthorized,
					message: Reason
				)
			);
		}
    }
}