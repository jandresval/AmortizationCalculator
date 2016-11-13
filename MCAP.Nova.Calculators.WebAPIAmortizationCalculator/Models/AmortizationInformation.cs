using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Models
{
    /// <summary>
    /// Model that contains the Amortization Information necesary to perform
    /// the Amortization Calculation.
    /// </summary>
    public class AmortizationInformation
    {
        public double dblMonthlyPaymentAmount { get; set; }
        public double dblOutstandingBalance { get; set; }
        public double dblInterestRate { get; set; }
        public double dblRemainingAmortization { get; set; }

    }
}