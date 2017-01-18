using MessageLogger.Web.Helpers;
using MessageLogger.Web.Models;
using MessageLogger.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MessageLogger.Web.Controllers
{
    public class RegisterController : Controller
    {

        private IRegisterWebService service;

        public RegisterController(IRegisterWebService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = new RegisterViewModel();
                try
                {
                    var application = await service.Register(model);
                    result.Result = application;
                    TempData["model"] = result;
                    return RedirectToAction("Result");
                }
                catch (Exception e)
                {
                    result.Error = e.Message;
                    TempData["model"] = result;
                    return RedirectToAction("Result");
                }
            }
            
            return View(model);
        }
        
        public ActionResult Result(RegisterViewModel model)
        {
            model = (RegisterViewModel)TempData["model"];
            return View(model);
        }
    }
}