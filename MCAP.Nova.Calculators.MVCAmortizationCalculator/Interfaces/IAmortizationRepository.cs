using MCAP.Nova.Calculators.MVCAmortizationCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCAP.Nova.Calculators.MVCAmortizationCalculator.Interfaces
{
    /// <summary>
    /// Define the repository interface to comunicate with our web api or any other source.
    /// </summary>
    public interface IAmortizationRepository
    {
        Task<AmortizationInformation> CalculateAmortization(AmortizationInformation amortizationInformation);
    }
}
