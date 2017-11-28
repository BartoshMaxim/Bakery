using AdminDashboard.Models.Entities.Customer;
using Bakery.DB;
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
        // GET: Customer
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

        [HttpPost]
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

        #region Details
        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            if (_customerRepository.IsExists(id))
            {
                var customer = _customerRepository.GetCustomer(id);

                return View(customer);
            }
            else
            {
                ModelState.AddModelError("", $"Can not find customer with {id} ID");
                return View("Index");
            }
        }
        #endregion

        #region Create
        // GET: Customer/Create
        public ActionResult Create()
        {
            var customer = new Customer();

            ViewBag.Roles = GetSelectListItem(customer);

            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _customerRepository.InsertCustomer(customer);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", $"Customer with {customer.Email} Email already exists");
                    }
                }
                catch
                {
                    ModelState.AddModelError("Error", $"Server Error");
                }
            }

            ViewBag.Roles = GetSelectListItem(customer);

            return View(customer);
        }
        #endregion

        #region Edit
        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (_customerRepository.IsExists(id))
            {
                var customer = _customerRepository.GetCustomer(id);

                ViewBag.Roles = GetSelectListItem(customer);

                return View(customer);
            }
            ModelState.AddModelError("", $"Can not find customer with {id} ID");
            return View("Index");
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerEditModel customer)
        {
            if (ModelState.IsValid)
            {
                if (_customerRepository.IsExists(id))
                {
                    customer.CustomerId = id;
                    try
                    {
                        var result = _customerRepository.UpdateCustomer(customer);

                        if (result)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", $"Error update customer with {id} ID");
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Can not find customer with {id} ID");
                }
            }

            ViewBag.Roles = GetSelectListItem(customer);
            return View((Customer)customer);
        }
        #endregion

        private List<SelectListItem> GetSelectListItem(ICustomer customer)
        {
            var roleIndex = 0;
            var roles = new List<SelectListItem>();
            foreach (var roleName in _roleTypeRepository.GetRolesDescriptions())
            {
                var selected = false;
                if (customer.CustomerRole == (RoleType)roleIndex)
                {
                    selected = true;
                }
                roles.Add(new SelectListItem
                {
                    Text = roleName,
                    Value = roleIndex.ToString(),
                    Selected = selected
                });
                roleIndex++;
            }
            return roles;
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = _customerRepository.GetCustomer(id);
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDelete(int id)
        {
            if (_customerRepository.IsExists(id))
            {
                try
                {
                    var result = _customerRepository.DeleteCustomer(id);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Can not delete customer with {id}");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server error");
                }
            }
            else
            {
                ModelState.AddModelError("", $"Can not find customer with {id} ID");
            }
            return View("Delete", id);
        }
    }
}
