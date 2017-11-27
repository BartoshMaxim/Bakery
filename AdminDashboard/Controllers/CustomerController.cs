using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
    // [Attributes.Authorize]
    public class CustomerController : Controller
    {
        public readonly ICustomerRepository _customerRepository;

        public readonly IRoleTypeRepository _roleTypeRepository;

        public CustomerController(ICustomerRepository customerRepository, IRoleTypeRepository roleTypeRepository)
        {
            _customerRepository = customerRepository;
            _roleTypeRepository = roleTypeRepository;
        }

        #region Index
        // GET: Customers
        public ActionResult Index(int rows = 10, int page = 1)
        {
            var customersCount = _customerRepository.GetCountRows();

            var pagesCount = ValidateRowsPage(ref rows, ref page, customersCount);

            ViewBag.PagesCount = pagesCount;

            ViewBag.Page = page;

            ViewBag.RowsCount = rows;

            //Returns limit customers
            return View();
        }

        public ActionResult CustomersData(int rows, int page)
        {
            var customersCount = _customerRepository.GetCountRows();

            ValidateRowsPage(ref rows, ref page, customersCount);

            var from = (page - 1) * rows;

            var to = page * rows;

            //Get limit customers from database
            var customers = _customerRepository.GetCustomers(from, to);


            return PartialView(customers);
        }

        /// <summary>
        /// Validate number of rows and number of current page
        /// </summary>
        /// <param name="rows">number of rows</param>
        /// <param name="page">number of current page</param>
        /// <param name="customersCount">customers count</param>
        /// <returns>Pages count</returns>
        private int ValidateRowsPage(ref int rows, ref int page, int customersCount)
        {
            var maxCountOfRows = 100;

            if (rows <= 0)
            {
                rows = 1;
            }
            else if (rows > maxCountOfRows)
            {
                rows = maxCountOfRows;
            }

            var pagesCount = customersCount / rows;

            if (customersCount % rows != 0)
            {
                pagesCount++;
            }

            if (page <= 0)
            {
                page = 1;
            }
            else if (page > pagesCount)
            {
                page = pagesCount;
            }

            return pagesCount;
        }

        #endregion

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.RoleNames = _roleTypeRepository.GetRolesDescriptions();

            return View();
        }

        // POST: Customer/Create
        [ValidateAntiForgeryToken]
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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
