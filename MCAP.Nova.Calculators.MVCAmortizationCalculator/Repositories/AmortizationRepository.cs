using MCAP.Nova.Calculators.MVCAmortizationCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCAP.Nova.Calculators.MVCAmortizationCalculator.Models;
using System.Threading.Tasks;
using System.Net.Http;
using MCAP.Nova.Calculators.MVCAmortizationCalculator.Properties;

namespace MCAP.Nova.Calculators.MVCAmortizationCalculator.Repositories
{
    /// <summary>
    /// Implementation of our Repository connecting to our web api
    /// </summary>
    public class AmortizationRepository : IAmortizationRepository
    {
        /// <summary>
        /// Function to invoke put web api verb from our webapi and update the amortization information
        /// or throw the exception to be managed for own controller.
        /// 
        /// </summary>
        /// <param name="amortizationInformation">Get the amortizationInformation from the client</param>
        /// <returns>Returns the amortization information updated.</returns>
        public async Task<AmortizationInformation> CalculateAmortization(AmortizationInformation amortizationInformation)
        {
            using(var client = helpers.HttpClientFactory.GetClient())
            {
                var response = await client.PutAsJsonAsync(Resources.AmortizationCalculationPutFunction, amortizationInformation);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result.Replace("\"",""));
                }

                var data = response.Content.ReadAsStringAsync().Result;
                amortizationInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<AmortizationInformation>(data);
                return amortizationInformation;
            }
        }
    }
}