using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface IImageRepository
    {
        List<Image> GetImages();

        Image GetImage(int imageid);

        bool InsertImage(Image image, LoginModel loginModel);

        bool DeleteImage(int imageid, LoginModel loginModel);

        bool UpdateImage(Image updateImage, LoginModel loginModel);
    }
}
