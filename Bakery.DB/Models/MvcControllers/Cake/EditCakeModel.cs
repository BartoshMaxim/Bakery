using Bakery.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bakery.DB
{
    public class EditCakeModel : ICake
    {
        [Required]
        public int CakeId { get; set; }


        [Required(ErrorMessage = "Enter Cake Name")]
        [MaxLength(50, ErrorMessage = "Cake Name can not contain more than 50 characters")]
        public string CakeName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter Cake Description")]
        public string CakeDescription { get; set; }

        [Required(ErrorMessage = "Enter Cake Price")]
        public float CakePrice { get; set; }

        public int ImageId { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public static explicit operator EditCakeModel(Cake cake)
        {
            return new EditCakeModel
            {
                AddedDate = cake.AddedDate,
                CakeDescription = cake.CakeDescription,
                CakeId = cake.CakeId,
                CakeName = cake.CakeName,
                CakePrice = cake.CakePrice,
                ImageId = cake.ImageId
            };
        }

    }
}
