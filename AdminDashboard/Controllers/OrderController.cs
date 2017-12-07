using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PagesData(SearchOrderModel searchCustomer)
        {
            var customersCount = 0;
            if (searchCustomer.IsCustomerNotNull())
            {
                customersCount = _customerRepository.GetCountRows(searchCustomer);
            }
            else
            {
                customersCount = _customerRepository.GetCountRows();
            }

            if (customersCount == 0)
            {
                return PartialView("CustomersData", null);
            }

            var valideteRowsPage = new ValidateRowsPage(searchCustomer, customersCount);

            ViewBag.PagesCount = valideteRowsPage.ValidateGetPageCount();

            var from = (searchCustomer.Page - 1) * searchCustomer.Rows;

            var to = searchCustomer.Page * searchCustomer.Rows;

            ViewBag.SearchCustomerModel = searchCustomer;

            //Get limit customers from database
            var customers = _customerRepository.GetCustomers(from, to, searchCustomer);

            return PartialView("CustomersData", customers);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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
