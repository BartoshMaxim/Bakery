using AdminDashboard.Core.Helpers;
using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.DB;
using AdminDashboard.Core.ControllersLogic;
using AdminDashboard.Models.Controllers;

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
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCakeModel cakeModel)
        {
            if (!CakeHepler.ImageIsExistsInCreateCakeModel(cakeModel))
            {
                ModelState.AddModelError("Image", "Upload any images");
            }

            if (cakeModel.Files.Count() > 8)
            {
                ModelState.AddModelError("Image", "Count images can not be more 8.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var imageidList = new List<int>();

                    // Upload Cake Photos
                    foreach (var file in cakeModel.Files)
                    {
                        if (file != null)
                        {
                            var result = ImageHelper.UploadImage(new UploadImageModel { ImageFile = file, ImageName = file.FileName }, _imageRepository, Server);

                            if (result > 0)
                            {
                                imageidList.Add(result);
                            }
                        }
                    }

                    cakeModel.ImageId = imageidList.FirstOrDefault();

                    //Insert Cake to DB
                    var cakeid = _cakeRepository.InsertCake(cakeModel);

                    //Create CakeImage References
                    foreach (var imageid in imageidList)
                    {
                        _cakeImageRepository.InsertCakeImageReference(new CakeImage
                        {
                            CakeId = cakeid,
                            ImageId = imageid
                        });
                    }

                    if (cakeid != 0)
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


        public ActionResult Edit(int id)
        {

            var cake = default(Cake);

            cake = (Cake)_cakeRepository.GetCake(id);

            if (cake == null)
            {
                return HttpNotFound($"Can not find cake with {id} ID");
            }

            var images = _cakeImageRepository.GetImages(id);

            if (images != null)
            {
                ViewBag.Images = images;
            }

            return View((EditCakeModel)cake);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCakeModel editCake)
        {
            if (ModelState.IsValid && _cakeRepository.IsExists(editCake.CakeId))
            {
                try
                {
                    var imageidList = new List<int>();

                    // Upload Cake Photos
                    foreach (var file in editCake.Files)
                    {
                        if (file != null)
                        {
                            var result = ImageHelper.UploadImage(new UploadImageModel { ImageFile = file, ImageName = file.FileName }, _imageRepository, Server);

                            if (result > 0)
                            {
                                imageidList.Add(result);
                            }
                        }
                    }

                    if (_cakeImageRepository.GetImages(editCake.CakeId).Count == 0)
                    {
                        editCake.ImageId = imageidList.FirstOrDefault();
                    }

                    //Create CakeImage References
                    foreach (var imageid in imageidList)
                    {
                        _cakeImageRepository.InsertCakeImageReference(new CakeImage
                        {
                            CakeId = editCake.CakeId,
                            ImageId = imageid
                        });
                    }

                    //Update Cake
                    var updateResult = _cakeRepository.UpdateCake(editCake);

                    if (updateResult)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Cake with {editCake.CakeId} ID was not updated!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error!");
                }
            }

            return View(editCake);
        }

        // GET: Cake/Delete/5
        public ActionResult Delete(int id)
        {
            var cake = _cakeRepository.GetCake(id);

            var images = _cakeImageRepository.GetImages(cake.CakeId);

            ViewBag.Images = images;

            if (cake == null)
            {
                return HttpNotFound($"Can not find cake with {id} ID");
            }
            return View(cake);
        }

        // POST: Cake/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCake(int id)
        {
            if (_cakeRepository.IsExists(id))
            {
                try
                {
                    var result = _cakeRepository.DeleteCake(id);

                    if (result)
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Can not delete cake with {id}");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server error");
                }
            }
            else
            {
                return HttpNotFound($"Can not find cake with {id} ID");
            }

            return View("Delete", id);
        }

        [HttpPost]
        public void DeleteImage(CakeImage cakeImage)
        {
            if (_cakeImageRepository.IsExists(cakeImage))
            {
                _cakeImageRepository.DeleteCakeImageReference(cakeImage);

                var cake = _cakeRepository.GetCake(cakeImage.CakeId);

                if (cake.ImageId == cakeImage.ImageId)
                {
                    var resultCakeImage = _cakeImageRepository.GetImages(cakeImage.CakeId).FirstOrDefault();
                    cake.ImageId = resultCakeImage != null ? resultCakeImage.ImageId : 0;

                    _cakeRepository.UpdateCake(cake);
                }

                _imageRepository.DeleteImage(cakeImage.ImageId);
            }
        }
    }
}
