using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi
{
	/// <summary>
	///		HTTP action result that adds an HTTP authentication challenge if the inner result returns an <see cref="HttpStatusCode.Unauthorized"/> status code.
	/// </summary>
	class AddChallengeOnUnauthenticationResult
		: IHttpActionResult
	{
		/// <summary>
		///		Create a new HTTP challenge-on-unauthorised action result.
		/// </summary>
		/// <param name="challenge">
		///		An <see cref="AuthenticationHeaderValue"/> representing the authentication challenge.
		/// </param>
		/// <param name="innerResult">
		///		The inner action result to execute.
		/// </param>
		public AddChallengeOnUnauthenticationResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
		{
			if (challenge == null)
				throw new ArgumentNullException("challenge");

			if (innerResult == null)
				throw new ArgumentNullException("innerResult");

			Challenge = challenge;
			InnerResult = innerResult;
		}

		/// <summary>
		///		An <see cref="AuthenticationHeaderValue"/> representing the authentication challenge.
		/// </summary>
		public AuthenticationHeaderValue Challenge
		{
			get; 
			private set;
		}

		/// <summary>
		///		The inner action result to execute.
		/// </summary>
		public IHttpActionResult InnerResult
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
		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				// Only one challenge per authentication scheme.
				if (!response.Headers.WwwAuthenticate.Any(header => header.Scheme == Challenge.Scheme))
					response.Headers.WwwAuthenticate.Add(Challenge);
			}

			return response;
		}
	}
}