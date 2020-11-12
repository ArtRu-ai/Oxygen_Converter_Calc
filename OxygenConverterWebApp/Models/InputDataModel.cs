using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxyConverterLib;

namespace OxygenConverterWebApp.Models
{
    [Serializable]
    public class InputDataModel
    {
        private OxyConverterLib.Calculate ocl = new OxyConverterLib.Calculate();

        public InputDataModel() { }

        public double Q
        {
            get { return ocl.Q; }
            set { ocl.Q = value; }
        }

        public double q
        {
            get { return ocl.q; }
            set { ocl.q = value; }
        }

        public double T
        {
            get { return ocl.T; }
            set { ocl.T = value; }
        }

        public double P
        {
            get { return ocl.P; }
            set { ocl.P = value; }
        }
    }
}