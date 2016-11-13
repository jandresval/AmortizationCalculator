using MCAP.Nova.Calculators.WebAPIAmortizationCalculator.BL;
using MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Controllers
{
    public class AmortizationCalculationController : ApiController
    {
        /// <summary>
        /// This variable contains the logger manager implementation.
        /// </summary>
        private static Logger m_logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        /// <returns>Returns a HttpResponseMessage with a error status code</returns>
        // GET: api/AmortizationCalculation
        public HttpResponseMessage Get()
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.NotImplemented);
            return responseMessage;
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a HttpResponseMessage with a error status code</returns>
        // GET: api/AmortizationCalculation/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.NotImplemented);
            return responseMessage;
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Returns a HttpResponseMessage with a error status code</returns>
        // POST: api/AmortizationCalculation
        public HttpResponseMessage Post([FromBody]AmortizationInformation info)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.NotImplemented);
            return responseMessage;
        }

        /// <summary>
        /// I used the put function because I am updating the amortizationInformation
        /// </summary>
        /// <param name="amortizationInformation">Get the amortization information from the client</param>
        /// <returns>Returns a HttpResponseMessage with the status code and amortization information updated or the error message</returns>
        // PUT: api/AmortizationCalculation/
        public HttpResponseMessage Put([FromBody]AmortizationInformation amortizationInformation)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                var returnAmortizationInformation = AmortizationCalculate.Calculate(amortizationInformation);
                responseMessage = Request.CreateResponse(HttpStatusCode.OK,returnAmortizationInformation);
            }
            catch(ArgumentNullException ex)
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                m_logger.Info(ex);
            }
            catch(ArgumentException ex)
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                m_logger.Info(ex);
            }
            catch(Exception ex)
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError,"Internal Server Error.");
                m_logger.Error(ex);
            }
            
            return responseMessage;
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a HttpResponseMessage with a error status code</returns>
        // DELETE: api/AmortizationCalculation/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.NotImplemented);
            return responseMessage;
        }
    }
}
