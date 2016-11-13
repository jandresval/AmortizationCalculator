using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Models;

namespace MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Controllers.Tests
{
    [TestClass()]
    public class AmortizationCalculationControllerTests
    {
        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_NullInfo_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(null);

            //Assert
            Assert.AreEqual(actionResult.Content.ReadAsStringAsync().Result.Replace("\"",""), @"Value cannot be null.\r\nParameter name: Amortization Information is null.");
        }

        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_InterestRate0_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(
                                new Models.AmortizationInformation
                                {
                                    dblInterestRate =0,
                                    dblMonthlyPaymentAmount = 1500,
                                    dblOutstandingBalance = 250000
                                });

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.IsNotNull(actionResult.Content);
            Assert.AreEqual(actionResult.Content.ReadAsStringAsync().Result.Replace("\"", ""), @"Interest Rate can not be 0 or less.");
        }

        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_MonthlyPaymentTooLow_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(
                                new Models.AmortizationInformation
                                {
                                    dblInterestRate = 5,
                                    dblMonthlyPaymentAmount = 1000,
                                    dblOutstandingBalance = 250000
                                });

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.IsNotNull(actionResult.Content);
            Assert.AreEqual(actionResult.Content.ReadAsStringAsync().Result.Replace("\"", ""), @"Montly Payment Amount is too low.");
        }

        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_ValidInformation_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(
                                new Models.AmortizationInformation
                                {
                                    dblInterestRate = 5,
                                    dblMonthlyPaymentAmount = 1500,
                                    dblOutstandingBalance = 250000
                                });

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.OK, actionResult.StatusCode);
            Assert.IsNotNull(actionResult.Content);
            var data = actionResult.Content.ReadAsStringAsync().Result;
            var amortizationInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<AmortizationInformation>(data);
            Assert.AreEqual(amortizationInformation.dblRemainingAmortization, 283);
        }

        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_ValidInformation2_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(
                                new Models.AmortizationInformation
                                {
                                    dblInterestRate = 2.85,
                                    dblMonthlyPaymentAmount = 500.25,
                                    dblOutstandingBalance = 99998.75
                                });

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.OK, actionResult.StatusCode);
            Assert.IsNotNull(actionResult.Content);
            var data = actionResult.Content.ReadAsStringAsync().Result;
            var amortizationInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<AmortizationInformation>(data);
            Assert.AreEqual(amortizationInformation.dblRemainingAmortization, 271);
        }

        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_MontlyPaymentAmountLessThan0_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(
                                new Models.AmortizationInformation
                                {
                                    dblInterestRate = 1,
                                    dblMonthlyPaymentAmount = -5,
                                    dblOutstandingBalance = -16
                                });

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.IsNotNull(actionResult.Content);
            Assert.AreEqual(actionResult.Content.ReadAsStringAsync().Result.Replace("\"", ""), @"Montly Payment Amount can not be 0 or less.");
        }

        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_OustandingBalanceLessThan0_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(
                                new Models.AmortizationInformation
                                {
                                    dblInterestRate = 1,
                                    dblMonthlyPaymentAmount = 5,
                                    dblOutstandingBalance = -16
                                });

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.IsNotNull(actionResult.Content);
            Assert.AreEqual(actionResult.Content.ReadAsStringAsync().Result.Replace("\"", ""), @"Oustanding Balance can not be 0 or less.");
        }

        [TestCategory("WebAPI")]
        [TestMethod()]
        public void PutTest_BadInformation_MessageReturned()
        {
            //Arrange
            var controller = new AmortizationCalculationController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            //Act
            HttpResponseMessage actionResult = controller.Put(
                                new Models.AmortizationInformation
                                {
                                    dblInterestRate = 999,
                                    dblMonthlyPaymentAmount = 999999999999,
                                    dblOutstandingBalance = 999999999999
                                });

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.OK, actionResult.StatusCode);
            Assert.IsNotNull(actionResult.Content);
            var data = actionResult.Content.ReadAsStringAsync().Result;
            var amortizationInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<AmortizationInformation>(data);
            Assert.AreEqual(amortizationInformation.dblRemainingAmortization, 2);
        }


    }
}