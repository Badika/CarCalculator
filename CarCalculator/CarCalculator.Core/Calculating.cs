using System;
using System.Collections.Generic;
using System.Text;

namespace CarCalculator.Core
{
    public static class Calculating
    {
        public static OutputValues PriceCalculating(InputValues inputValues)
        {
            double engineTypeCoef;

            switch (inputValues.EngineType)
            {
                case "Petrol":
                    engineTypeCoef = 50;
                    break;
                case "Diesel":
                    engineTypeCoef = 75;
                    break;
                default:
                    engineTypeCoef = 50;
                    break;
            }

            double v = inputValues.EngineVolume;
            var fullYears = DateTime.Now.Year - inputValues.Year;

            if (fullYears <= 0)
                fullYears = 1;


            var exciseDuty = engineTypeCoef * v * fullYears;
            var importDuty = (inputValues.Price + exciseDuty) * 0.1;
            var vat = importDuty * 2;
            var fullPrice = inputValues.Price + vat + exciseDuty + importDuty;

            OutputValues outputValues = new OutputValues(exciseDuty, importDuty, vat, fullPrice);
            return outputValues;
        }
    }
}
