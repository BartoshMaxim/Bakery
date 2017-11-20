using Bakery.DB.Interfaces;

namespace Bakery.DB
{
    public class Image : IImage
    {
        public int ImageId { get; set; }

        public string ImageName { get; set; }

        public string ImagePath { get; set; }
    }
}
