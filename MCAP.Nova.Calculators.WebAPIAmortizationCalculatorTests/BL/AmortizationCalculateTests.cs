using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Models;

namespace MCAP.Nova.Calculators.WebAPIAmortizationCalculator.BL.Tests
{
    [TestClass()]
    public class AmortizationCalculateTests
    {
        [TestCategory("CoreApplication")]
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateTest_NullInfo_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = null;

            AmortizationCalculate.Calculate(amortizationInformation);
        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTest_InvalidInteresRate_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 0,
                dblMonthlyPaymentAmount = 1500,
                dblOutstandingBalance = 250000,
                dblRemainingAmortization = 0
            };

            AmortizationCalculate.Calculate(amortizationInformation);

        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTest_InvalidMonthlyPaymentAmount_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 5,
                dblMonthlyPaymentAmount = 1000,
                dblOutstandingBalance = 250000,
                dblRemainingAmortization = 0
            };

            AmortizationCalculate.Calculate(amortizationInformation);

        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        public void CalculateTest_ValidRemainAmortization_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 5,
                dblMonthlyPaymentAmount = 1500,
                dblOutstandingBalance = 250000,
                dblRemainingAmortization = 0
            };

            var result = AmortizationCalculate.Calculate(amortizationInformation);

            Assert.AreEqual(result.dblRemainingAmortization, 283);

        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        public void CalculateTest_ValidRemainAmortizationExample2_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 2.85,
                dblMonthlyPaymentAmount = 500.25,
                dblOutstandingBalance = 99998.75,
                dblRemainingAmortization = 0
            };

            var result = AmortizationCalculate.Calculate(amortizationInformation);

            Assert.AreEqual(result.dblRemainingAmortization, 271);

        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        public void CalculateTest_NotValidRemainAmortization_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 5,
                dblMonthlyPaymentAmount = 1500,
                dblOutstandingBalance = 250000,
                dblRemainingAmortization = 0
            };

            var result = AmortizationCalculate.Calculate(amortizationInformation);

            Assert.AreNotEqual(result.dblRemainingAmortization, 0);

        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTest_MontlyPaymentAmountLessThan0_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 1,
                dblMonthlyPaymentAmount = -5,
                dblOutstandingBalance = -16,
                dblRemainingAmortization = 0
            };

            var result = AmortizationCalculate.Calculate(amortizationInformation);

        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTest_OustandingBalanceLessThan0_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 1,
                dblMonthlyPaymentAmount = 5,
                dblOutstandingBalance = -16,
                dblRemainingAmortization = 0
            };

            var result = AmortizationCalculate.Calculate(amortizationInformation);

        }

        [TestCategory("CoreApplication")]
        [TestMethod()]
        public void CalculateTest_BadInformation_ExceptionThrown()
        {
            AmortizationInformation amortizationInformation = new AmortizationInformation()
            {
                dblInterestRate = 999,
                dblMonthlyPaymentAmount = 999999999999,
                dblOutstandingBalance = 999999999999,
                dblRemainingAmortization = 0
            };

            var result = AmortizationCalculate.Calculate(amortizationInformation);

            Assert.AreEqual(result.dblRemainingAmortization, 2);

        }
    }
}