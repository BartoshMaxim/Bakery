using AdminDashboard.Models.Entities.Image;
using Bakery.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Bakery.DB;
using System.Web.Mvc;
using System.IO;
using Bakery.DB.Interfaces;
using Bakery.Core.Helpers;

namespace AdminDashboard.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PagesData(SearchImageModel searchImage)
        {
            var customersCount = 0;
            if (searchImage.IsImageNotNull())
            {
                customersCount = _imageRepository.GetCountRows(searchImage);
            }
            else
            {
                customersCount = _imageRepository.GetCountRows();
            }

            if (customersCount == 0)
            {
                return PartialView("ImagesData", null);
            }

            var valideteRowsPage = new ValidateRowsPage(searchImage, customersCount);

            ViewBag.PagesCount = valideteRowsPage.ValidateGetPageCount();

            var from = (searchImage.Page - 1) * searchImage.Rows;

            var to = searchImage.Page * searchImage.Rows;

            ViewBag.SearchImageModel = searchImage;

            //Get limit customers from database
            var customers = _imageRepository.GetImages(from, to, searchImage);

            if (Request.UrlReferrer.AbsolutePath.Equals("/Cake/Create"))
            {
                return PartialView("ExistsImagesData", customers);
            }

            return PartialView("ImagesData", customers);
        }

        // GET: Image/Details/5
        public ActionResult Details(int id)
        {
            var image = _imageRepository.GetImage(id);

            if (image == null)
            {
                return HttpNotFound($"Image with { id} ID was not fount!");
            }

            return View(image);
        }

        // GET: Image/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Image/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var uploadpath = "~/Images/Upload";

                    var imageid = _imageRepository.GetIdForNextImage();

                    if (imageid == 0)
                    {
                        imageid++;
                    }

                    var ext = Path.GetExtension(imageModel.ImageFile.FileName);

                    var physicalImagePath = Path.Combine(Server.MapPath(uploadpath), imageid + ext);

                    var imagepath = uploadpath + "/" + imageid + ext;

                    var image = new Image
                    {
                        ImageId = imageid,
                        ImageName = imageModel.ImageName,
                        ImagePath = imagepath
                    };

                    imageModel.ImageFile.SaveAs(physicalImagePath);

                    var result = _imageRepository.InsertImage(image);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can't add this image!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error");
                }
            }
            return View(imageModel);
        }

        // GET: Image/Edit/5
        public ActionResult Edit(int id)
        {
            var image = _imageRepository.GetImage(id);

            if (image == null)
            {
                return HttpNotFound($"Image with { id} ID was not fount!");
            }

            return View(image);
        }

        // POST: Image/Edit/5
        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _imageRepository.UpdateImage(image);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Image with {image.ImageId} ID was not update!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error!");
                }
            }
            return View(image);
        }

        // GET: Image/Delete/5
        public ActionResult Delete(int id)
        {
            var image = _imageRepository.GetImage(id);

            if (image == null)
            {
                return HttpNotFound($"Image with { id} ID was not fount!");
            }
            return View(image);
        }

        // POST: Image/Delete/5
        [HttpPost]
        public ActionResult ImageDelete(int id)
        {
            if (_imageRepository.IsExists(id))
            {
                try
                {
                    var image = _imageRepository.GetImage(id);

                    var pathToDeleteImage = Server.MapPath(image.ImagePath);

                    System.IO.File.Delete(pathToDeleteImage);

                    var result = _imageRepository.DeleteImage(id);
                    
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return HttpNotFound($"Image with { id} ID was not fount!");
                    }
                }
                catch
                {
                    Response.StatusCode = 500;
                    return RedirectToAction("Index", $"Server Error!");
                }
            }
            else
            {
                return HttpNotFound($"Image with { id} ID was not fount!");
            }
        }
    }
}
