using MessageLogger.Web.Helpers;
using MessageLogger.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MessageLogger.Web.Controllers
{
    public class RegisterController : BaseController
    {
        public RegisterController()
        {

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
                    var application = await base.PostWebServiceObject<RegisterResultModel>("register", null, model);
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