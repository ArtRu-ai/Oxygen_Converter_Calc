using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excel = Microsoft.Office.Interop.Excel;
using OxyConverterLib;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {

        private string fileName = "Mat_model";
        Excel.Application objExcel = null;
        Excel.Workbook WorkBook = null;

        private object oMissing = System.Reflection.Missing.Value;
        [TestMethod]
        public void TestMethod1()
        {
            OxyConverterLib.Calculate cc = new OxyConverterLib.Calculate();

            #region 1. Присвоить исходные данные 
            cc.Q = 80;
            cc.q = 3;
            cc.T = 298;
            cc.P = 140000;
            #endregion 1. Присвоить исходные данные 

            #region 2. Передать исходные данные в Excel-файл, записать в соответствующие ячейки (формат адреса ячейки [номер строки, номер столбца])

            objExcel = new Excel.Application();
            WorkBook = objExcel.Workbooks.Open(
                        Directory.GetCurrentDirectory() + "\\" + fileName);
            Excel.Worksheet WorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)WorkBook.Sheets["Лист1"];

            ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[3, 3]).Value2 = cc.Q;
            ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[4, 3]).Value2 = cc.q;
            ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[5, 3]).Value2 = cc.T;
            ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[6, 3]).Value2 = cc.P;

            #endregion 2. Передать исходные данные в Excel-файл, записать в соответствующие ячейки (формат адреса ячейки [номер строки, номер столбца])

            #region 3. Прочитать из ячейки Excel-файла расчетное значение 

            double Get_Vud = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[11, 3]).Value.ToString());
            double Get_Wud = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[12, 3]).Value.ToString());
            double Get_Ph = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[13, 3]).Value.ToString());
            double Get_Ro_h = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[14, 3]).Value.ToString());
            double Get_Lambda = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[15, 3]).Value.ToString());
            double Get_W_g = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[16, 3]).Value.ToString());
            double Get_Ro_g = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[17, 3]).Value.ToString());
            double Get_H0 = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[18, 3]).Value.ToString());
            double Get_D = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[19, 3]).Value.ToString());
            double Get_Vm = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[20, 3]).Value.ToString());

            double Get_d_dn = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[21, 3]).Value.ToString());
            double Get_d_g = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[22, 3]).Value.ToString());
            double Get_V = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[23, 3]).Value.ToString());
            double Get_H_k = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[24, 3]).Value.ToString());
            double Get_H_c = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[25, 3]).Value.ToString());
            double Get_H_v = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[26, 3]).Value.ToString());
            double Get_t_c = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[27, 3]).Value.ToString());
            double Get_t_dn = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[28, 3]).Value.ToString());
            double Get_t_k = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[29, 3]).Value.ToString());
            double Get_delta = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[30, 3]).Value.ToString());

            double Get_D_n = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[31, 3]).Value.ToString());
            double Get_H = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[32, 3]).Value.ToString());
            double Get_d_otv = double.Parse(((Excel.Range)WorkBook.Sheets["Лист1"].Cells[33, 3]).Value.ToString());


            if (WorkBook != null) WorkBook.Close(false, null, null);
            if (objExcel != null) objExcel.Quit();


            #endregion 3. Прочитать из ячейки Excel-файла расчетное значение 

            #region  4. Сравнить значения из Excel и из библиотеки с нужной точностью, знаков после запятой    

            Console.WriteLine("--- Результаты расчетов");
            Console.WriteLine("");

            Assert.AreEqual(Get_Vud, Math.Round((double)cc.Vud, 2), 0.01);
            Console.WriteLine("Удельный объем кислородного конвертера : ожидается Vud={0}; фактически cc.Vud()= {1}",
                    Get_Vud, Math.Round((double)cc.Vud, 2));

            Assert.AreEqual(Get_Wud, Math.Round((double)cc.Wud, 2), 0.01);
            Console.WriteLine("Критическая скорость истечения кислорода : ожидается Wud={0}; фактически cc.Wud()= {1}",
                    Get_Wud, Math.Round((double)cc.Wud, 2));

            Assert.AreEqual(Get_Ph, Math.Round((double)cc.Ph, 2), 0.01);
            Console.WriteLine("Давление кислорода перед соплами кислородной фурмы : ожидается Ph={0}; фактически cc.Ph()= {1}",
                    Get_Ph, Math.Round((double)cc.Ph, 2));

            Assert.AreEqual(Get_Ro_h, Math.Round((double)cc.Ro_h, 2), 0.01);
            Console.WriteLine("Начальная плотность кислорода : ожидается Ro_h={0}; фактически cc.Ro_h()= {1}",
                    Get_Ro_h, Math.Round((double)cc.Ro_h, 2));

            Assert.AreEqual(Get_Lambda, Math.Round((double)cc.Lambda, 2), 0.01);
            Console.WriteLine("Критерий скорости истечения кислорода : ожидается Lambda={0}; фактически cc.Lambda()= {1}",
                    Get_Lambda, Math.Round((double)cc.Lambda, 2));

            Assert.AreEqual(Get_W_g, Math.Round((double)cc.W_g, 2), 0.01);
            Console.WriteLine("Скорость истечения  кислорода на срезе сопла кислородной фурмы : ожидается W_g={0}; фактически cc.W_g()= {1}",
                    Get_W_g, Math.Round((double)cc.W_g, 2));

            Assert.AreEqual(Get_Ro_g, Math.Round((double)cc.Ro_g, 2), 0.01);
            Console.WriteLine("Плотность кислорода на срезе сопла кислородной фурмы : ожидается Ro_g={0}; фактически cc.Ro_g()= {1}",
                    Get_Ro_g, Math.Round((double)cc.Ro_g, 2));

            Assert.AreEqual(Get_H0, Math.Round((double)cc.H0, 2), 0.01);
            Console.WriteLine("Глубина спокойной ванны : ожидается H0={0}; фактически cc.H0()= {1}",
                    Get_H0, Math.Round((double)cc.H0, 2));

            Assert.AreEqual(Get_D, Math.Round((double)cc.D, 2), 0.01);
            Console.WriteLine("Внутренний диаметр конвертора : ожидается D={0}; фактически cc.D()= {1}",
                    Get_D, Math.Round((double)cc.D, 2));

            Assert.AreEqual(Get_Vm, Math.Round((double)cc.Vm, 2), 0.01);
            Console.WriteLine("Объём металлической ванны : ожидается Vm={0}; фактически cc.Vm()= {1}",
                    Get_Vm, Math.Round((double)cc.Vm, 2));

            Assert.AreEqual(Get_d_dn, Math.Round((double)cc.d_dn, 2), 0.01);
            Console.WriteLine("Внутренний диаметр днища : ожидается d_dn={0}; фактически cc.d_dn()= {1}",
                    Get_d_dn, Math.Round((double)cc.d_dn, 2));

            Assert.AreEqual(Get_d_g, Math.Round((double)cc.d_g, 2), 0.01);
            Console.WriteLine("Диаметр горловины конвертора : ожидается d_g={0}; фактически cc.d_g()= {1}",
                    Get_d_g, Math.Round((double)cc.d_g, 2));

            Assert.AreEqual(Get_V, Math.Round((double)cc.V, 2), 0.01);
            Console.WriteLine("Рабочий объём конвертора : ожидается V={0}; фактически cc.V()= {1}",
                    Get_V, Math.Round((double)cc.V, 2));

            Assert.AreEqual(Get_H_k, Math.Round((double)cc.H_k, 2), 0.01);
            Console.WriteLine("Высота конической части конвертера : ожидается H_k={0}; фактически cc.H_k()= {1}",
                    Get_H_k, Math.Round((double)cc.H_k, 2));

            Assert.AreEqual(Get_H_c, Math.Round((double)cc.H_c, 2), 0.01);
            Console.WriteLine("Высота цилидрической части конвертера : ожидается H_c={0}; фактически cc.H_c()= {1}",
                    Get_H_c, Math.Round((double)cc.H_c, 2));

            Assert.AreEqual(Get_H_v, Math.Round((double)cc.H_v, 2), 0.01);
            Console.WriteLine("Внутренняя высота конвертера : ожидается H_v={0}; фактически cc.H_v()= {1}",
                    Get_H_v, Math.Round((double)cc.H_v, 2));

            Assert.AreEqual(Get_t_c, Math.Round((double)cc.t_c, 2), 0.01);
            Console.WriteLine("Толщина футеровки конвертера в цилиндрической части : ожидается t_c={0}; фактически cc.t_c()= {1}",
                    Get_t_c, Math.Round((double)cc.t_c, 2));

            Assert.AreEqual(Get_t_dn, Math.Round((double)cc.t_dn, 2), 0.01);
            Console.WriteLine("Толщина днища конвертера : ожидается t_dn={0}; фактически cc.t_dn()= {1}",
                    Get_t_dn, Math.Round((double)cc.t_dn, 2));

            Assert.AreEqual(Get_t_k, Math.Round((double)cc.t_k, 2), 0.01);
            Console.WriteLine("Толщина футеровки конвертера в конической части : ожидается t_k={0}; фактически cc.t_k()= {1}",
                    Get_t_k, Math.Round((double)cc.t_k, 2));

            Assert.AreEqual(Get_delta, Math.Round((double)cc.delta, 2), 0.01);
            Console.WriteLine("Толщина металлического кожуха конвертера : ожидается delta={0}; фактически cc.delta()= {1}",
                    Get_delta, Math.Round((double)cc.delta, 2));

            Assert.AreEqual(Get_D_n, Math.Round((double)cc.D_n, 2), 0.01);
            Console.WriteLine("Наружный диаметр конвертера : ожидается D_n={0}; фактически cc.D_n()= {1}",
                    Get_D_n, Math.Round((double)cc.D_n, 2));

            Assert.AreEqual(Get_H, Math.Round((double)cc.H, 2), 0.01);
            Console.WriteLine("Полная высота конвертера : ожидается H={0}; фактически cc.H()= {1}",
                    Get_H, Math.Round((double)cc.H, 2));

            Assert.AreEqual(Get_d_otv, Math.Round((double)cc.d_otv, 2), 0.01);
            Console.WriteLine("Диаметр сталевыпускного отверстия : ожидается d_otv={0}; фактически cc.d_otv() = {1}",
                    Get_d_otv, Math.Round((double)cc.d_otv, 2));





            #endregion  4. Сравнить значения из Excel и из библиотеки с нужной точностью, знаков после запятой    

        }
    }
}
