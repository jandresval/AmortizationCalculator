using MCAP.Nova.Calculators.MVCAmortizationCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MCAP.Nova.Calculators.MVCAmortizationCalculator.helpers
{
    /// <summary>
    /// Class to create our factory http client
    /// </summary>
    public static class HttpClientFactory
    {
        /// <summary>
        /// Function to return a web api client.
        /// </summary>
        /// <returns>Returns httpclient to perform calls json type.</returns>
        public static HttpClient GetClient()
        {
            string apiUrl = Resources.WebAPIServer;

            var client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);

            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}