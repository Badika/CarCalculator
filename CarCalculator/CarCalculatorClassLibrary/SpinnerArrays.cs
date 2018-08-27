using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CarCalculatorClassLibrary
{
    public static class SpinnerArrays
    {
        public static List<int> Years
        {
            get { return GetYears(); }
        }

        public static List<string> EngineTypes
        {
            get { return GetEngineTRypes(); }
        }

        List<string> eTypes = new List<string>() { "Petro"}

        private static List<int> GetYears()
        {
            List<int>  years = new List<int>();
            int thisYear = DateTime.Now.Year;
            for (int i = thisYear; i >= 1900; i--)
            {
                years.Add(i);
            }
            return years;
        }

        private static List<string> GetEngineTRypes()
        {

        }
    }
}