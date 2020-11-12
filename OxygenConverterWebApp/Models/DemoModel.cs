using OxyConverterLib;
using System;

namespace OxygenConverterWebApp.Models
{
    public class DemoModel
    {
        private OxyConverterLib.Calculate ocl = new OxyConverterLib.Calculate();
        private InputDataModel _inputData = new InputDataModel();

        public DemoModel() { }

        public DemoModel(InputDataModel InputData)
        {
            _inputData = InputData;

            #region --- Передать исходные данные в экземпляр библиотеки
            ocl.Q = _inputData.Q;
            ocl.q = _inputData.q;
            ocl.T = _inputData.T;
            ocl.P = _inputData.P;
            #endregion --- Передать исходные данные в экземпляр библиотеки
        }
        #region --- Получить расчетные показатели                
        public double Vud
        {
            get { return ocl.Vud; }
        }

        public double Wud
        {
            get { return ocl.Wud; }
        }

        public double Ph
        {
            get { return ocl.Ph; }
        }

        public double Ro_h
        {
            get { return ocl.Ro_h; }
        }

        public double Lambda
        {
            get { return ocl.Lambda; }
        }

        public double W_g
        {
            get { return ocl.W_g; }
        }

        public double Ro_g
        {
            get { return ocl.Ro_g; }
        }

        public double H0
        {
            get { return ocl.H0; }
        }

        public double D
        {
            get { return ocl.D; }
        }

        public double Vm
        {
            get { return ocl.Vm; }
        }

        public double d_dn
        {
            get { return ocl.d_dn; }
        }

        public double d_g
        {
            get { return ocl.d_g; }
        }

        public double V
        {
            get { return ocl.V; }
        }

        public double H_k
        {
            get { return ocl.H_k; }
        }

        public double H_c
        {
            get { return ocl.H_c; }
        }

        public double H_v
        {
            get { return ocl.H_v; }
        }

        public double t_c
        {
            get { return ocl.t_c; }
        }

        public double t_dn
        {
            get { return ocl.t_dn; }
        }

        public double t_k
        {
            get { return ocl.t_k; }
        }

        public double delta
        {
            get { return ocl.delta; }
        }

        public double D_n
        {
            get { return ocl.D_n; }
        }

        public double H
        {
            get { return ocl.H; }
        }

        public double d_otv
        {
            get { return ocl.d_otv; }
        }
        #endregion --- Получить расчетные показатели
    }
}
