using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OxyConverterLib;
using OxygenConverterWebApp.Domain;
using OxygenConverterWebApp.Infrastructure;
using OxygenConverterWebApp.Models;

namespace OxygenConverterWebApp.Controllers
{
    [Authorize] // К контроллеру получают доступ только аутентифицированные пользователи.
    public class InputDataVariantsController : Controller
    {
        IInputDataVariantsRepository _inputDataVariants;
        IVariantsRepository _variants;
        IUserProfileRepository _users;

        public InputDataVariantsController()
            : this(new DALContext())
        {
        }

        public InputDataVariantsController(IDALContext context)
        {
            _variants = context.Variants;
            _inputDataVariants = context.InputDataVariants;
            _users = context.Users;
        }

        public ActionResult Index()
        {
            ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
            return View(_users.CurrentUser.InputDataVariants.ToList());
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            OxyConverterDB _database = new OxyConverterDB();
            _database.InputDataVariants.RemoveRange(_database.InputDataVariants.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Filter")]
        public ActionResult Index(Variants variant)
        {
            if (variant.ID_Variant != 0) // если выбран элемент списка "Все", который прописан при формировании выпадающего списка в представлении Index
            {
                ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
                return View(_users.CurrentUser.InputDataVariants.Where(t => t.ID_Variant == variant.ID_Variant).ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(InputDataVariants inputDataVariants)
        {
            OxyConverterDB _database = new OxyConverterDB();

            #region --- Ввод тестовых данных в базу данных       

            int _ID_Variant_1 = _database.Variants.Where(p => p.NameVariant == "Вариант 1" && p.Owner.ID_User == _users.CurrentUser.ID_User).First().ID_Variant;
            InputDataVariants inputDataVariants_1 = new InputDataVariants
            {
                ID_Variant = _ID_Variant_1,
                Q = 80,
                C = 3,
                T = 298,
                P = 140000,
                Owner = _users.CurrentUser
            };
            _inputDataVariants.InsertOrUpdate(inputDataVariants_1);
            _inputDataVariants.Save();

            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
            return View();
        }

        [HttpPost]
        public ActionResult Create(InputDataVariants inputDataVariants)
        {
            if (ModelState.IsValid)
            {
                inputDataVariants.Owner = _users.CurrentUser;
                _inputDataVariants.InsertOrUpdate(inputDataVariants);
                _inputDataVariants.Save();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ID_Variant = new SelectList(_variants.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Variant", "NameVariant");
            return View(_inputDataVariants.All.FirstOrDefault(t => t.ID_InputDataVariant == id));
        }

        [HttpPost]
        public ActionResult Edit(InputDataVariants inputDataVariants)
        {
            if (ModelState.IsValid)
            {
                _inputDataVariants.InsertOrUpdate(inputDataVariants);
                _inputDataVariants.Save();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_inputDataVariants.All.FirstOrDefault(t => t.ID_InputDataVariant == id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _inputDataVariants.Remove(_inputDataVariants.All.FirstOrDefault(t => t.ID_InputDataVariant == id));
            _inputDataVariants.Save();
            return RedirectToAction("Index");
        }
    }
}