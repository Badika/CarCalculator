using System;
using System.Collections.Generic;
using System.Text;

namespace CarCalculator.Core
{
    public class InputValues
    {
        #region fields
        public int Price { get; set; }
        public int Year { get; set; }
        public string EngineType { get; set; }
        public double EngineVolume { get; set; }
        #endregion

        #region ctors
        public InputValues()
        {
            Price = 0;
            Year = 1990;
            EngineType = "Petrol";
            EngineVolume = 0.0;
        }
        public InputValues(int price, int year, string engineType, double engineVolume)
        {
            Price = price;
            Year = year;
            EngineType = engineType;
            EngineVolume = engineVolume;
        }
        #endregion

        #region methods
        public bool IsYear()
        {
            return IsYear(Year);
        }
        public static bool IsYear(int year)
        {
            if (year >= 0 && year <= DateTime.Now.Year)
                return true;
            return false;
        }

        public bool IsEngineVolume()
        {
            return IsEngineVolume(EngineVolume);
        }
        public static bool IsEngineVolume(double engineVolume)
        {
            if (engineVolume >= 0)
                return true;
            return false;
        }
        public static bool IsEngineVolume(string engineVolume)
        {
            double ev;
            try
            {
                ev = Convert.ToDouble(engineVolume);
            }
            catch (Exception) { return false; }
            return true;
        }

        public bool IsPrice()
        {
            return IsPrice(Price);
        }
        public static bool IsPrice(int price)
        {
            if (price >= 0)
                return true;
            return false;
        }
        public static bool IsPrice(string price)
        {
            int p;
            try
            {
                p = Convert.ToInt32(price);
            }
            catch (Exception) { return false; }
            return true;
        }

        public bool IsEngineType()
        {
            return IsEngineType(EngineType);
        }
        public static bool IsEngineType(string et)
        {
            if (!string.IsNullOrWhiteSpace(et))
                return true;
            return false;
        }

        public void ClearValues()
        {
            Price = 0;
            Year = 1990;
            EngineType = "Petrol";
            EngineVolume = 0.0;
        }
        #endregion
    }
}
