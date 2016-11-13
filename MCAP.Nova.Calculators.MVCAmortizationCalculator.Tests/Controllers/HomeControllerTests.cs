using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCAP.Nova.Calculators.MVCAmortizationCalculator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MCAP.Nova.Calculators.MVCAmortizationCalculator.Models;

namespace MCAP.Nova.Calculators.MVCAmortizationCalculator.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestCategory("MVCController")]
        [TestMethod()]
        public void IndexTest_NullModel_FirstCall()
        {
            HomeController controllerAmortizationCalculator = new HomeController();

            var response = controllerAmortizationCalculator.Index() as ViewResult;

            Assert.AreNotEqual(null, response);
            Assert.AreEqual(null, response.Model);
        }

        [TestCategory("MVCController")]
        [TestMethod()]
        public void IndexTest_NullAmortizationInformation_PutCall()
        {
            HomeController controllerAmortizationCalculator = new HomeController();

            var response = controllerAmortizationCalculator.Index(null) as Task<ActionResult>;

            Assert.AreNotEqual(null, response);

            var viewResponse = response.Result as ViewResult;
            Assert.AreNotEqual(null, viewResponse);
            Assert.AreEqual(@"Value cannot be null.\r\nParameter name: Amortization Information is null.", viewResponse.ViewBag.Error);
        }

        [TestCategory("MVCController")]
        [TestMethod()]
        public void IndexTest_InterestRate0_PutCall()
        {
            HomeController controllerAmortizationCalculator = new HomeController();

            var response = controllerAmortizationCalculator.Index(new Models.AmortizationInformation
            {
                dblInterestRate=0,
                dblMonthlyPaymentAmount=1500,
                dblOutstandingBalance=250000
            }) as Task<ActionResult>;

            Assert.AreNotEqual(null, response);

            var viewResponse = response.Result as ViewResult;
            Assert.AreNotEqual(null, viewResponse);
            Assert.AreEqual(@"Interest Rate can not be 0 or less.", viewResponse.ViewBag.Error);
        }

        [TestCategory("MVCController")]
        [TestMethod()]
        public void IndexTest_MontlyPaymentTooLow_PutCall()
        {
            HomeController controllerAmortizationCalculator = new HomeController();

            var response = controllerAmortizationCalculator.Index(new Models.AmortizationInformation
            {
                dblInterestRate = 5,
                dblMonthlyPaymentAmount = 1000,
                dblOutstandingBalance = 250000
            }) as Task<ActionResult>;

            Assert.AreNotEqual(null, response);

            var viewResponse = response.Result as ViewResult;
            Assert.AreNotEqual(null, viewResponse);
            Assert.AreEqual(@"Montly Payment Amount is too low.", viewResponse.ViewBag.Error);
        }

        [TestCategory("MVCController")]
        [TestMethod()]
        public void IndexTest_ValidInformation_PutCall()
        {
            HomeController controllerAmortizationCalculator = new HomeController();

            var response = controllerAmortizationCalculator.Index(new Models.AmortizationInformation
            {
                dblInterestRate = 5,
                dblMonthlyPaymentAmount = 1500,
                dblOutstandingBalance = 250000
            }) as Task<ActionResult>;

            Assert.AreNotEqual(null, response);

            var viewResponse = response.Result as ViewResult;
            Assert.AreNotEqual(null, viewResponse);
            Assert.AreNotEqual(null, viewResponse.Model);

            var amortizationInformation = viewResponse.Model as AmortizationInformation;
            Assert.AreEqual(283, amortizationInformation.dblRemainingAmortization);
        }

        [TestCategory("MVCController")]
        [TestMethod()]
        public void IndexTest_ValidInformation1_PutCall()
        {
            HomeController controllerAmortizationCalculator = new HomeController();

            var response = controllerAmortizationCalculator.Index(new Models.AmortizationInformation
            {
                dblInterestRate = 2.85,
                dblMonthlyPaymentAmount = 500.25,
                dblOutstandingBalance = 99998.75
            }) as Task<ActionResult>;

            Assert.AreNotEqual(null, response);

            var viewResponse = response.Result as ViewResult;
            Assert.AreNotEqual(null, viewResponse);
            Assert.AreNotEqual(null, viewResponse.Model);

            var amortizationInformation = viewResponse.Model as AmortizationInformation;
            Assert.AreEqual(271, amortizationInformation.dblRemainingAmortization);
        }
    }
}