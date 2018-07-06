using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FertilityModelNamespace;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Repository repository = new Repository();
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.SearchParameters = Session["SearchParameters"] as SearchParameters ?? viewModel.SearchParameters;
            viewModel.fertilityModels = repository.GetFertilityModels().Where(fm => fm.Fertility2016.HasValue).Where(fm => fm.Iq.HasValue).ToList();            
            Logic logic = new Logic();
            logic.Search(ref viewModel);
            logic.Statistics(ref viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel viewModel)
        {
            Repository repository = new Repository();
            Session["SearchParameters"] = viewModel.SearchParameters;            
            viewModel.fertilityModels = repository.GetFertilityModels().Where(fm => fm.Fertility2016.HasValue).Where(fm => fm.Iq.HasValue).ToList();
            Logic logic = new Logic();
            logic.Search(ref viewModel);
            logic.Statistics(ref viewModel);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            FertilityModel fertilityModel = new FertilityModel();
            return View(fertilityModel);
        }
        [HttpPost]
        public ActionResult Create(FertilityModel fertilityModel)
        {
            Repository repository = new Repository();
            try
            {
                repository.AddNew(fertilityModel);
            }
            catch
            {
                ViewBag.ValidationMessage = "Something went wrong with the validation of this object. Please correct and try again.";
                return View(fertilityModel);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            Repository repository = new Repository();
            FertilityModel fertilityModel = repository.GetFertilityModel(Id);
            return View(fertilityModel);
        }
        [HttpPost]
        public ActionResult Edit(FertilityModel fertilityModel)
        {
            Repository repository = new Repository();
            try
            {
                repository.Edit(fertilityModel);
            }
            catch
            {
                ViewBag.ValidationMessage = "Something went wrong with the validation of this object. Please correct and try again.";
                return View(fertilityModel.Id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            Repository repository = new Repository();
            FertilityModel fertilityModel = repository.GetFertilityModel(Id);
            return View(fertilityModel);
        }
        [HttpPost]
        public ActionResult Delete(FertilityModel fertilityModel)
        {
            Repository repository = new Repository();
            try
            {
                repository.Delete(fertilityModel);
            }
            catch
            {
                ViewBag.ValidationMessage = "Something went wrong with the validation of this object. Please correct and try again.";
                return View(fertilityModel.Id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(string Id)
        {
            Repository repository = new Repository();
            FertilityModel fertilityModel = repository.GetFertilityModel(Id);
            return View(fertilityModel);
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}