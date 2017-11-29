using Bakery.DB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Bakery.DB
{
    public class Image : IImage
    {
        public int ImageId { get; set; }

        [Required]
        [Display(Name ="Image Name")]
        public string ImageName { get; set; }

        public string ImagePath { get; set; }
    }
}
