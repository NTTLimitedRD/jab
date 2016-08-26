using System;
using System.Net.Http;
using jab.Interfaces;

namespace jab.Http
{
    /// <summary>
    /// Extension methods for <see cref="JabTestConfiguration"/>.
    /// </summary>
    public static class JabHttpClientFactory
    {

        /// <summary>
        /// Construct an <see cref="HttpClient"/> for the current <see cref="IJabTestConfiguration.BaseUrl"/>, if any.
        /// </summary>
        /// <param name="configuration">
        /// The <see cref="IJabApiOperation"/> to extract the configuration from. This cannot be null.
        /// </param>
        /// <returns>
        /// An <see cref="HttpClient"/> initialized for the current <see cref="IJabTestConfiguration.BaseUrl"/> or null,
        /// if <see cref="IJabTestConfiguration.BaseUrl"/> is null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> cannot be null.
        /// </exception>
        public static HttpClient GetClient(this IJabTestConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return configuration.BaseUrl != null ? new HttpClient() { BaseAddress = configuration.BaseUrl } : null;
        }
    }
}
