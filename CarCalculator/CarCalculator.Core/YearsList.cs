using System;
using System.Collections.Generic;
using System.Text;

namespace CarCalculator.Core
{
    public class YearsList
    {
        #region fields
        public List<int> Years { get; }
        #endregion

        #region ctors
        public YearsList()
        {
            Years = ReversedYearsList(1900);
        }
        public YearsList(bool isReversed)
        {
            switch(isReversed)
            {
                case true:
                    Years = ReversedYearsList(1900);
                    break;
                case false:
                    Years = UnreversedYearsList();
                    break;
            }
        }
        public YearsList(int startFrom)
        {
            Years = ReversedYearsList(startFrom);
        }
        #endregion

        #region methods
        private List<int> ReversedYearsList(int startFrom)
        {
            List<int> years = new List<int>();
            int thisYear = DateTime.Now.Year;
            for (int i = thisYear; i >= startFrom; i--)
            {
                years.Add(i);
            }
            return years;
        }
        private List<int> UnreversedYearsList()
        {
            List<int> years = new List<int>();
            int thisYear = DateTime.Now.Year;
            for (int i = 1900; i <= thisYear; i++)
            {
                years.Add(i);
            }
            return years;
        }
        #endregion
    }
}
