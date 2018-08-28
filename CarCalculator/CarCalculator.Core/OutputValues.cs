using System;
using System.Collections.Generic;
using System.Text;

namespace CarCalculator.Core
{
    public class OutputValues
    {
        #region fields
        public double ExciseDuty { get; set; }
        public double ImportDuty { get; set; }
        public double VAT { get; set; }
        public double FullPrice { get; set; }
        #endregion

        #region ctors
        public OutputValues()
        {
            ExciseDuty = 0.0;
            ImportDuty = 0.0;
            VAT = 0.0;
            FullPrice = 0.0;
        }
        public OutputValues(double exciseDuty, double importDuty, double vat, double fullPrice)
        {
            ExciseDuty = exciseDuty;
            ImportDuty = importDuty;
            VAT = vat;
            FullPrice = fullPrice;
        }
        #endregion

        #region methods
        public bool IsExciseDuty()
        {
            return IsCorrect(ExciseDuty);
        }
        public bool IsImportDuty()
        {
            return IsCorrect(ImportDuty);
        }
        public bool IsVAT()
        {
            return IsCorrect(VAT);
        }
        public bool IsFullPrice()
        {
            return IsCorrect(FullPrice);
        }
        public static bool IsCorrect(double v)
        {
            if (v >= 0)
                return true;
            return false;
        }
        #endregion

    }
}
