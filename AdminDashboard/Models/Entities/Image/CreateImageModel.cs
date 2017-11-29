using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminDashboard.Models.Entities.Image
{
    public class CreateImageModel
    {
        [Required(ErrorMessage = "Please, write image name, it will use in alt's!")]
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "Please, choose image file!")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}