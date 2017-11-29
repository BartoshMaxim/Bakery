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
            var image = _imageRepository.GetImage(id);

            if (image == null)
            {
                ModelState.AddModelError("", $"Image with {id} ID was not found");
                return Redirect("Index");
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

                    var imageid = _imageRepository.GetCountRows();

                    if(imageid == 0)
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
