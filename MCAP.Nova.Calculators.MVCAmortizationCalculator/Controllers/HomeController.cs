using MCAP.Nova.Calculators.MVCAmortizationCalculator.Interfaces;
using MCAP.Nova.Calculators.MVCAmortizationCalculator.Models;
using MCAP.Nova.Calculators.MVCAmortizationCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MCAP.Nova.Calculators.MVCAmortizationCalculator.Controllers
{
    public class HomeController : Controller
    {
        public static readonly IAmortizationRepository amortizationRepository = new AmortizationRepository();

        /// <summary>
        /// Function to display the index view with all information empty.
        /// </summary>
        /// <returns></returns>
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Function to get information from the user and perform calls to ours web api using the repository
        /// also define a variable to show errors messages that it could get from the respository.
        /// 
        /// </summary>
        /// <param name="amortizationInformation">Get the amortization information from the view</param>
        /// <returns>Returns a action result to be shown on our view.</returns>
        [HttpPost]
        public async Task<ActionResult> Index(AmortizationInformation amortizationInformation)
        {
            double dblYears = 0;
            if (!ModelState.IsValid)
            {
                return View("Index", amortizationInformation);
            }

            try
            {
                amortizationInformation = await amortizationRepository.CalculateAmortization(amortizationInformation);
                if (amortizationInformation.dblRemainingAmortization > 1)
                {
                    ViewBag.Months = "Months";
                }
                else
                {
                    ViewBag.Months = "Month";
                }
                if (amortizationInformation.dblRemainingAmortization > 12)
                {
                    dblYears = Math.Ceiling(amortizationInformation.dblRemainingAmortization / 12);
                    ViewBag.Years = dblYears.ToString() + " Years";
                }

            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View("Index", amortizationInformation);
        }
    }
}