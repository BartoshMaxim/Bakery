using AdminDashboard.Models.Entities.Cake;
using Bakery.Core.Helpers;
using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;

        public CakeController(ICakeRepository cakeRepository)
        {
            _cakeRepository = cakeRepository;
        }

        // GET: Cake
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PagesData(SearchCakeModel searchCake)
        {
            var customersCount = 0;
            if (searchCake.IsCakeNotNull())
            {
                customersCount = _cakeRepository.GetCountRows(searchCake);
            }
            else
            {
                customersCount = _cakeRepository.GetCountRows();
            }

            if (customersCount == 0)
            {
                return PartialView("CakesData", null);
            }

            var valideteRowsPage = new ValidateRowsPage(searchCake, customersCount);

            ViewBag.PagesCount = valideteRowsPage.ValidateGetPageCount();

            var from = (searchCake.Page - 1) * searchCake.Rows;

            var to = searchCake.Page * searchCake.Rows;

            ViewBag.SearchCakeModel = searchCake;

            //Get limit customers from database
            var customers = _cakeRepository.GetCakes(from, to, searchCake);

            return PartialView("CakesData", customers);
        }

        // GET: Cake/Details/5
        public ActionResult Details(int id)
        {
            var cake = _cakeRepository.GetCake(id);

            if(cake == null)
            {
                return RedirectToAction("Index", $"Cake with {id} ID not found!");
            }

            return View();
        }

        // GET: Cake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cake/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cake/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cake/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cake/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cake/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
