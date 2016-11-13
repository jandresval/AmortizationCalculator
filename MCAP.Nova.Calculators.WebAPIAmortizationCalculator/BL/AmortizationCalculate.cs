using MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Models;
using MCAP.Nova.Calculators.WebAPIAmortizationCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCAP.Nova.Calculators.WebAPIAmortizationCalculator.BL
{
    /// <summary>
    /// Amortization Calculate Class contains all the function to perform the 
    /// Amortization Calculation.
    /// </summary>
    public class AmortizationCalculate
    {
        /// <summary>
        /// This function calculate the Amortization Remaining following this pattern:
        /// 1. Check if the information provided is ok.
        /// 2. Check if it can perform the operation with the information provided.
        /// 3. Make the calculation and returned the amortizationInformation updated.
        /// 
        /// Considerations:
        /// the function is static since it is only going to perform the calculation also that makes this function 
        /// to be load on memory and that helps with the performance also I let this function to throw all exceptions
        /// controlled and uncontrolled since all the logger and controlling errors should be on the controller which use
        /// this function.
        /// </summary>
        /// <param name="amortizationInformation">Get the information necesary to perform the amortization calculation.</param>
        /// <returns>Returns the amortization information updated.</returns>
        public static AmortizationInformation Calculate(AmortizationInformation amortizationInformation)
        {
            decimal dmlFactor = 0;
            double dblExponencialFactor = 0;
            decimal dmlMontlyPaymentCalculation = 0;
            
            //Block to check if the information is Ok.
            if (amortizationInformation == null)
            {
                throw new ArgumentNullException(Resources.InformationNull);
            }

            if (amortizationInformation.dblMonthlyPaymentAmount < 0)
            {
                throw new ArgumentException(Resources.MontlyPaymentAmountLessThan0);
            }

            if (amortizationInformation.dblOutstandingBalance < 0)
            {
                throw new ArgumentException(Resources.OustandingBalanceLessThan0);
            }

            if (amortizationInformation.dblInterestRate <= 0)
            {
                throw new ArgumentException(Resources.InterestRateLessThan0);
            }

            //Block to perform initial calculation to define if Montly payment is too low.
            dblExponencialFactor = (1d / 6d);
            dmlFactor = Pow(Convert.ToDecimal((1d + amortizationInformation.dblInterestRate / 200)), Convert.ToDecimal(dblExponencialFactor)) - 1;
            dmlMontlyPaymentCalculation = Convert.ToDecimal(1 - 
                                        (amortizationInformation.dblOutstandingBalance * Decimal.ToDouble(dmlFactor) / amortizationInformation.dblMonthlyPaymentAmount));
            if (dmlMontlyPaymentCalculation <= 0)
            {
                throw new ArgumentException(Resources.MontlyPaymentIsTooLow);
            }

            //Block to perform ramining calculation and updating the amortizationInformation.
            amortizationInformation.dblRemainingAmortization = Math.Log(Convert.ToDouble(dmlMontlyPaymentCalculation)) / 
                                                               Math.Log(1 / (1 + Convert.ToDouble(dmlFactor)));
            amortizationInformation.dblRemainingAmortization = Math.Ceiling(amortizationInformation.dblRemainingAmortization);
            return amortizationInformation;

        }

        /// <summary>
        /// Implementation of Pow with Decimals.
        /// 
        /// Considerations:
        /// I decided to create a Pow function even though I am using the same Math.Pow since this function
        /// only uses Double so on the future we can increase the accuracy implementing our own Pow function.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Decimal Pow(Decimal x, Decimal y)
        {
            return Convert.ToDecimal(Math.Pow(Decimal.ToDouble(x), Decimal.ToDouble(y)));
        }

    }
}