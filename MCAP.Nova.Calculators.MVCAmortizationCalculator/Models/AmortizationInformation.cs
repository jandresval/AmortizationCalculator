using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MCAP.Nova.Calculators.MVCAmortizationCalculator.Models
{
    /// <summary>
    /// Model that contains the Amortization Information necesary to perform
    /// the Amortization Calculation.
    /// </summary>
    public class AmortizationInformation
    {
        [Display(Name = "Montly Payment Amount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}",ApplyFormatInEditMode = true)]
        public double dblMonthlyPaymentAmount { get; set; }

        [Display(Name = "Oustanding Balance")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Required]
        public double dblOutstandingBalance { get; set; }

        [Display(Name = "Interest Rate")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        [Required]
        public double dblInterestRate { get; set; }

        [Display(Name = "Remaining Amortization")]
        public double dblRemainingAmortization { get; set; }
    }
}