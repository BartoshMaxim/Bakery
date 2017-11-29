using AdminDashboard.Models.Entities.Image;
using Bakery.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Bakery.DB;
using System.Web.Mvc;
using System.IO;

namespace AdminDashboard.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageRepository _imageRepository;

        public ImageController(ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // GET: Image
        public ActionResult Index()
        {
            var images = _imageRepository.GetImages();

            if (!images.Any())
            {
                ModelState.AddModelError("", "Can't found any images!");
            }

            return View(images);
        }

        // GET: Image/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var imageid = _imageRepository.GetCountRows();

                    var ext = Path.GetExtension(imageModel.ImageFile.FileName);

                    var imagepath = Path.Combine(Server.MapPath("~/Images/Uploaded"), imageid + ext);

                    var image = new Image
                    {
                        ImageId = imageid,
                        ImageName = imageModel.ImageName,
                        ImagePath = imagepath
                    };

                    imageModel.ImageFile.SaveAs(imagepath);

                    var result =  _imageRepository.InsertImage(image);

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
            return View();
        }

        // POST: Image/Edit/5
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

        // GET: Image/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Image/Delete/5
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
