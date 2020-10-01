using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyConverterLib
{

    public class Calculate
    {
        #region Константы
        /// <summary>
        /// универсальная газовая постоянная, для удельной - делить на молярную массу
        /// </summary>
        public const double R = 8314;

        /// <summary>
        /// показатель адиабаты, равный для двухатомных газов 1.4
        /// </summary>
        public const double K = 1.4f;
        #endregion

        #region Входные данные
        /// <summary>
        /// номинальная емкость конвертера
        /// </summary>
        public double Q;

        /// <summary>
        /// удельная интенсивность продувки
        /// </summary>
        public double q;

        /// <summary>
        /// Температура кислорода перед соплами кислородной фурмы
        /// </summary>
        public double T;

        /// <summary>
        /// Давление кислорода на срезе сопел кислородной фурмы
        /// </summary>
        public double P;
        #endregion

        #region Рассчитываемые данные
        /// <summary>
        /// Удельный объём кислородного конвертера
        /// </summary>
        public double Vud => (1 / (1 + Q * (Math.Pow(10, -3))));

        /// <summary>
        /// Критическая скорость истеения кислорода
        /// </summary>
        public double Wud => Math.Sqrt((2 * K / (K + 1)) * ((R / 32) * T));

        /// <summary>
        /// Давление кислорода перед соплами кислородной фурмы
        /// </summary>
        public double Ph => (0.588 + (0.00392 * Q)) * Math.Pow(10, 6);

        /// <summary>
        /// Начальная плотность кислорода
        /// </summary>
        public double Ro_h => Ph / ((R / 32) * T);

        /// <summary>
        /// Критерий скорости истечения кислорода
        /// </summary>
        public double Lambda => Math.Sqrt(((K + 1) / (K - 1)) * Math.Pow((1 - (P / Ph)), ((K - 1) / K)));

        /// <summary>
        /// Скорость истечения кислорода на срезе сопла кислородной фурмы
        /// </summary>
        public double W_g => Wud * Lambda;

        /// <summary>
        /// Плотность кислорода на срезе сопла кислородной фурмы
        /// </summary>
        public double Ro_g => Ro_h * Math.Pow(1 - ((K - 1) / (K + 1)) * Math.Pow(Lambda, 2), (1 / (K - 1)));

        /// <summary>
        /// Глубина спокойной ванны
        /// </summary>
        public double H0 => Math.Pow(0.016 * Math.Sqrt(W_g) * Math.Pow(Ro_g, 0.1) * (Math.Pow(Vud, 0.5) / Math.Pow(3.1, 0.05)) * Math.Pow(Q / 0.23, 0.3), 0.57);

        /// <summary>
        /// Внутренний диаметр конвертера
        /// </summary>
        public double D => (0.599 - (0.00032 * Q)) * Math.Sqrt(Q / H0);

        /// <summary>
        /// Объём металлической ванны
        /// </summary>
        public double Vm => (Q / 7);

        /// <summary>
        /// Внутренний диаметр днища
        /// </summary>
        public double d_dn => (-D + Math.Sqrt(Math.Pow(D, 2) - 4 * (Math.Pow(D, 2) - 12 * Vm / Math.PI / H0))) / 2;

        /// <summary>
        /// Диаметр горловины конвертера
        /// </summary>
        public double d_g => 0.33 * Math.Pow(Q, 0.4);

        /// <summary>
        /// Рабочий объём конвертера
        /// </summary>
        public double V => Vud * Q;

        /// <summary>
        /// Высота конической части конвертера
        /// </summary>
        public double H_k => (V - Vm) / (Math.PI * ((Math.Pow(D, 2) + Math.Pow(d_g, 2) + D * d_g) / 12) + Math.Pow(D, 2) / (4 * (0.45 + 0.01 * Q)));

        /// <summary>
        /// Высота цилиндрической части конвертера
        /// </summary>
        public double H_c => H_k / (0.45 + 0.001 * Q);

        /// <summary>
        /// Внутренняя высота конвертера
        /// </summary>
        public double H_v => H0 + H_k + H_c;

        /// <summary>
        /// Толщина футеровки конвертера в цилиндрической части
        /// </summary>
        public double t_c => 0.142 * Math.Pow(Q, 0.33333);

        /// <summary>
        /// Толщина днища конвертера
        /// </summary>
        public double t_dn => t_c + 0.125;

        /// <summary>
        /// Толщина футеровки конвертера в коничской части
        /// </summary>
        public double t_k => t_c - 0.15;

        /// <summary>
        /// Толщина металлического кожуха конвертера
        /// </summary>
        public double delta => 0.015 * Math.Pow(Q, 0.33333);

        /// <summary>
        /// Наружный диаметр конвертера
        /// </summary>
        public double D_n => D + 2 * t_c + 2 * delta;

        /// <summary>
        /// Полная высота конвертера
        /// </summary>
        public double H => H_v + t_dn + delta;

        /// <summary>
        /// Диаметр сталевыпускного отверстия
        /// </summary>
        public double d_otv => (0.00033 * Q) + 0.1;

        #endregion


    }
}
