using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationMVC.DAL;
using RegistrationMVC.Models;

namespace RegistrationMVC.Controllers
{
    public class RegistrationController : Controller
    {
        Register_DAL _registerDAL = new Register_DAL();

        // GET: Registration
        public ActionResult Index()
        {
            var ApplicationList = _registerDAL.GetAllApplications();
            if (ApplicationList.Count == 0)
            {
                TempData["InfoMessage"] = "Currenty no data available";
            }
            return View(ApplicationList);
        }

        // GET: Registration/Details/5
        public ActionResult Details(int id)
        {
            var applications = _registerDAL.GetUserByID(id).FirstOrDefault();
            if (applications == null)
            {
                TempData["ErrorMessage"] = "Product not avialable with id :" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(applications);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registration/Create
        [HttpPost]
        public ActionResult Create(RegisterModel register)
        {
            try
            {
                bool IsInserted = false;

                if (ModelState.IsValid)
                {
                    IsInserted = _registerDAL.RegisterUser(register);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "User details saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "The User already exist / Unable to save the Details";
                    }
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int id)
        {
            var applications = _registerDAL.GetUserByID(id).FirstOrDefault();
            if (applications == null)   
            {
                TempData["ErrorMessage"] = "User not avialable with id :" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(applications);
        }

        // POST: Registration/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(RegisterModel register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _registerDAL.UpdateUser(register);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "User details updated successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the user details";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var register = _registerDAL.GetUserByID(id).FirstOrDefault();

                if (register == null)
                {
                    TempData["ErrorMessage"] = "Product is not avilable with ID : " + id.ToString();
                    RedirectToAction("Index");
                }
                return View(register);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id, FormCollection collection)
        {
            try
            {
                string result = _registerDAL.DeleteUser(id);
                if (result.Contains("Deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
