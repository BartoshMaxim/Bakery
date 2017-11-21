using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IImageRepository
    {
        List<Image> GetImages();

        Image GetImage(int imageid);

        bool InsertImage(Image image);

        bool DeleteImage(int imageid);

        bool UpdateImage(Image updateImage);
    }
}
