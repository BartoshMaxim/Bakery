using BakeryApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApi.Respositories
{
    public class ImageRepository : IImageRepository
    {
        public bool DeleteImage(int imageid, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Image GetImage(int imageid)
        {
            throw new NotImplementedException();
        }

        public List<Image> GetImages()
        {
            throw new NotImplementedException();
        }

        public bool InsertImage(Image image, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateImage(Image updateImage, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}