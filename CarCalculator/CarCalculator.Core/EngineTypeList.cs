using System;
using System.Collections.Generic;
using System.Text;

namespace CarCalculator.Core
{
    public class EngineTypeList
    {
        #region fields
        public List<string> EngineTypes { get; }
        #endregion

        #region ctors
        public EngineTypeList()
        {
            EngineTypes = new List<string>() { "Petrol", "Diesel" };
        }
        public EngineTypeList(List<string> list)
        {
            EngineTypes = new List<string>();
            EngineTypes.AddRange(list);
        }
        #endregion

        #region methods

        #endregion
    }
}
