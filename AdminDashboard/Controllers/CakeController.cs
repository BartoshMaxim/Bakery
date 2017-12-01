using AdminDashboard.Core.Helpers;
using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.DB;
using AdminDashboard.Core.ControllersLogic;

namespace AdminDashboard.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;

        private readonly IImageRepository _imageRepository;

        private readonly ICakeImageRepository _cakeImageRepository;

        public CakeController(ICakeRepository cakeRepository, IImageRepository imageRepository, ICakeImageRepository cakeImageRepository)
        {
            _cakeRepository = cakeRepository;
            _imageRepository = imageRepository;
            _cakeImageRepository = cakeImageRepository;
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

            var images = _cakeImageRepository.GetImages(cake.CakeId);

            var image = _imageRepository.GetImage(cake.ImageId);

            ViewBag.PreviewImage = image;

            ViewBag.Images = images;

            if (cake == null)
            {
                return RedirectToAction("Index", $"Cake with {id} ID not found!");
            }

            return View(cake);
        }

        // GET: Cake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cake/Create
        [HttpPost]
        public ActionResult Create(CreateCakeModel cakeModel)
        {
            if (!CakeHepler.ImageIsExistsInCreateCakeModel(cakeModel))
            {
                ModelState.AddModelError("Image", "Choose image or upload image");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (cakeModel.ImageId == 0)
                    {
                        var result = ImageHelper.UploadImage(cakeModel, _imageRepository, Server);

                        if (result != 0)
                        {
                            cakeModel.ImageId = result;
                        }
                        else
                        {
                            ModelState.AddModelError("Image", "Can not upload this image");
                        }
                    }
                    else
                    {
                        var result = _imageRepository.IsExists(cakeModel.ImageId);

                        if (result)
                        {
                            ModelState.AddModelError("Image", $"Image with {cakeModel.ImageId} ID not found!");
                        }
                    }


                    var insertResult = _cakeRepository.InsertCake(cakeModel);

                    if (insertResult)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cake was not created!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error!");
                }
            }

            return View();
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
