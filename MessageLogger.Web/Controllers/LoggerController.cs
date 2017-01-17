﻿using MessageLogger.Web.Helpers;
using MessageLogger.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MessageLogger.Web.Controllers
{
    public class LoggerController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LogRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = new LogViewModel();
                try
                {
                    //check if session exists
                    var access_token = HttpContext.Session[model.application_id];
                    if (access_token == null)
                        throw new Exception("Please authenticate before using the logging.");

                    var encorded_token = Base64Utility.Encode(access_token.ToString());
                    var token = await base.PostWebServiceObject<LogResultModel>("log", encorded_token, model);
                    result.Result = token;

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

        public ActionResult Result(LogViewModel model)
        {
            model = (LogViewModel)TempData["model"];
            return View(model);
        }


        #region Auth

        [HttpGet]
        public ActionResult Authenticate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Authenticate(AuthRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = new AuthViewModel();
                try
                {
                    var auth_credentials = model.GetAuthorizationHeaderValue();
                    var encorded_auth_credentials = Base64Utility.Encode(auth_credentials);
                    var token = await base.PostWebServiceObject<AuthResultModel>("auth", encorded_auth_credentials, null);
                    result.Result = token;

                    //create session
                    HttpContext.Session[model.application_id] = token.access_token;

                    TempData["model"] = result;
                    return RedirectToAction("AuthenticateResult");
                }
                catch (Exception e)
                {
                    result.Error = e.Message;
                    TempData["model"] = result;
                    return RedirectToAction("AuthenticateResult");
                }
            }

            return View(model);
        }

        public ActionResult AuthenticateResult(AuthViewModel model)
        {
            model = (AuthViewModel)TempData["model"];
            return View(model);
        }

        #endregion
    }
}