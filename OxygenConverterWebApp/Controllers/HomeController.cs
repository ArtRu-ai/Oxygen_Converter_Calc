using OxyConverterLib;
using OxygenConverterWebApp.Domain;
using OxygenConverterWebApp.Infrastructure;
using OxygenConverterWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace OxygenConverterWebApp.Controllers
{
    public class HomeController : Controller
    {
        IUserProfileRepository _users;
        IVariantsRepository _variants;
        IInputDataVariantsRepository _inputDataVariants;

        public HomeController() : this(new DALContext()) { }

        public HomeController(IDALContext context)
        {
            _users = context.Users;
            _variants = context.Variants;
            _inputDataVariants = context.InputDataVariants;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Demo()
        {
            OxyConverterLib.Calculate ocl = new OxyConverterLib.Calculate();

            #region --- Задать исходные данные по умолчанию

            ocl.Q = 80;
            ocl.C = 3;
            ocl.T = 298;
            ocl.P = 140000;

            #endregion --- Задать исходные данные по умолчанию

            ViewBag.InputData = ocl;
            ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");

            return View();
        }

        [HttpPost]
        public ActionResult Demo(string _ID_Variant)
        {
            int ID_Variant = int.Parse(_ID_Variant);
            OxyConverterLib.Calculate ocl = new OxyConverterLib.Calculate();

            #region --- Задать исходные данные для выбранного варианта

            ocl.Q = _inputDataVariants.All.First(p => p.Variants.ID_Variant == ID_Variant && p.Owner.ID_User == _users.CurrentUser.ID_User).Q;
            ocl.C = _inputDataVariants.All.First(p => p.Variants.ID_Variant == ID_Variant && p.Owner.ID_User == _users.CurrentUser.ID_User).C;
            ocl.T = _inputDataVariants.All.First(p => p.Variants.ID_Variant == ID_Variant && p.Owner.ID_User == _users.CurrentUser.ID_User).T;
            ocl.P = _inputDataVariants.All.First(p => p.Variants.ID_Variant == ID_Variant && p.Owner.ID_User == _users.CurrentUser.ID_User).P;

            #endregion --- Задать исходные данные для выбранного варианта

            ViewBag.InputData = ocl;
            ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");

            return View();
        }

        [HttpPost]
        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult RezultDemo(InputDataModel InputData)
        {
            DemoModel result = new DemoModel(InputData);

            ViewBag.Vud = result.Vud;
            ViewBag.Wud = result.Wud;
            ViewBag.Ph = result.Ph;
            ViewBag.Ro_h = result.Ro_h;
            ViewBag.Lambda = result.Lambda;
            ViewBag.W_g = result.W_g;
            ViewBag.Ro_g = result.Ro_g;
            ViewBag.H0 = result.H0;
            ViewBag.D = result.D;
            ViewBag.Vm = result.Vm;
            ViewBag.d_dn = result.d_dn;
            ViewBag.d_g = result.d_g;
            ViewBag.V = result.V;
            ViewBag.H_k = result.H_k;
            ViewBag.H_c = result.H_c;
            ViewBag.H_v = result.H_v;
            ViewBag.t_c = result.t_c;
            ViewBag.t_dn = result.t_dn;
            ViewBag.t_k = result.t_k;
            ViewBag.delta = result.delta;
            ViewBag.D_n = result.D_n;
            ViewBag.H = result.H;
            ViewBag.d_otv = result.d_otv;

            // ! Save input data to Session
            Session["InputData"] = InputData;

            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult Excel()
        {
            ViewBag.Result = "Файл успешно сохранен!";

            // ! Get input data from Session
            InputDataModel inputData = (InputDataModel)Session["InputData"];

            DemoModel result = new DemoModel(inputData);

            try
            {
                string dataTimeNow = DateTime.Now.ToString("dd MMMM yyyy HH-mm-ss");
                ViewBag.Result = dataTimeNow;

                #region --- Формирование файла Excel с результатами решения задачи
                Excel.Application application = new Excel.Application();
                Excel.Workbook workBook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workBook.ActiveSheet;

                // Cells[СТРОКА, СТОЛБЕЦ]
                worksheet.Cells[1, 1] = "Рассчётные данные";
                worksheet.Cells[2, 1] = "Дата расчета: " + ViewBag.Result;

                worksheet.Cells[4, 1] = "Исходные данные";
                worksheet.Cells[5, 1] = "Номинальная емкость конвертера";
                worksheet.Cells[5, 2] = inputData.Q.ToString();

                worksheet.Cells[6, 1] = "Удельная интенсивность продувки";
                worksheet.Cells[6, 2] = inputData.C.ToString();

                worksheet.Cells[7, 1] = "Температура кислорода перед соплами кислородной фурмы";
                worksheet.Cells[7, 2] = inputData.T.ToString();

                worksheet.Cells[8, 1] = "Давление кислорода на срезе сопел кислородной фурмы";
                worksheet.Cells[8, 2] = inputData.P.ToString();

                worksheet.Cells[3, 4] = "Расчетные показатели";

                worksheet.Cells[5, 4] = "Удельный объём кислородного конвертера";
                worksheet.Cells[5, 5] = result.Vud.ToString();

                worksheet.Cells[6, 4] = "Критическая скорость истеения кислорода";
                worksheet.Cells[6, 5] = result.Wud.ToString();

                worksheet.Cells[7, 4] = "Давление кислорода перед соплами кислородной фурмы";
                worksheet.Cells[7, 5] = result.Ph.ToString();

                worksheet.Cells[8, 4] = "Начальная плотность кислорода";
                worksheet.Cells[8, 5] = result.Ro_h.ToString();

                worksheet.Cells[9, 4] = "Критерий скорости истечения кислорода";
                worksheet.Cells[9, 5] = result.Lambda.ToString();

                worksheet.Cells[10, 4] = "Скорость истечения кислорода на срезе сопла кислородной фурмы";
                worksheet.Cells[10, 5] = result.W_g.ToString();

                worksheet.Cells[11, 4] = "Плотность кислорода на срезе сопла кислородной фурмы";
                worksheet.Cells[11, 5] = result.Ro_g.ToString();

                worksheet.Cells[12, 4] = "Глубина спокойной ванны";
                worksheet.Cells[12, 5] = result.H0.ToString();

                worksheet.Cells[13, 4] = "Внутренний диаметр конвертера";
                worksheet.Cells[13, 5] = result.D.ToString();

                worksheet.Cells[14, 4] = "Объём металлической ванны";
                worksheet.Cells[14, 5] = result.Vm.ToString();

                worksheet.Cells[15, 4] = "Внутренний диаметр днища";
                worksheet.Cells[15, 5] = result.d_dn.ToString();

                worksheet.Cells[16, 4] = "Диаметр горловины конвертера";
                worksheet.Cells[16, 5] = result.d_g.ToString();

                worksheet.Cells[17, 4] = "Рабочий объём конвертера";
                worksheet.Cells[17, 5] = result.V.ToString();

                worksheet.Cells[17, 4] = "Высота конической части конвертера";
                worksheet.Cells[17, 5] = result.H_k.ToString();

                worksheet.Cells[18, 4] = "Высота цилиндрической части конвертера";
                worksheet.Cells[18, 5] = result.H_c.ToString();

                worksheet.Cells[19, 4] = "Внутренняя высота конвертера";
                worksheet.Cells[19, 5] = result.H_v.ToString();

                worksheet.Cells[20, 4] = "Толщина футеровки конвертера в цилиндрической части";
                worksheet.Cells[20, 5] = result.t_c.ToString();

                worksheet.Cells[20, 4] = "Толщина днища конвертера";
                worksheet.Cells[20, 5] = result.t_dn.ToString();

                worksheet.Cells[21, 4] = "Толщина футеровки конвертера в коничской части";
                worksheet.Cells[21, 5] = result.t_k.ToString();

                worksheet.Cells[22, 4] = "Толщина металлического кожуха конвертера";
                worksheet.Cells[22, 5] = result.delta.ToString();

                worksheet.Cells[23, 4] = "Наружный диаметр конвертера";
                worksheet.Cells[23, 5] = result.D_n.ToString();

                worksheet.Cells[24, 4] = "Полная высота конвертера";
                worksheet.Cells[24, 5] = result.H.ToString();

                worksheet.Cells[25, 4] = "Диаметр сталевыпускного отверстия";
                worksheet.Cells[25, 5] = result.d_otv.ToString();

                String excelFileName = Server.MapPath("~/Content") + "\\Demo.xlsx";

                if (System.IO.File.Exists(excelFileName))
                {
                    System.IO.File.Delete(excelFileName);
                }

                // ! Save path & filename
                workBook.SaveAs(excelFileName);

                workBook.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.FinalReleaseComObject(application);

                // ! Redirect to download file
                Response.RedirectPermanent("/Content/Demo.xlsx");

                #endregion --- Формирование файла Excel с результатами решения задачи
            }
            catch (Exception e)
            {
                ViewBag.Result = "Невозможно сохранить файл (" + e.Message + ").";
            }
            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult Cabinet()
        {
            ViewBag.Message = "Личная страница";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Расчётная программа";
            ViewBag.Assembly = "Версия " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ившин Артём Андреевич";

            return View();
        }
    }
}