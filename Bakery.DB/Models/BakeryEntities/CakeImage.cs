using Bakery.DB.Interfaces;

namespace Bakery.DB
{
    public class CakeImage: ICakeImage
    {
        public int CakeImageId { get; set; }

        public int CakeId { get; set; }

        public int ImageId { get; set; }
    }
}
