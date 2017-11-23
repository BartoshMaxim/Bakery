using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IImageRepository
    {
        List<IImage> GetImages();

        IImage GetImage(int imageid);

        bool InsertImage(IImage image);

        bool DeleteImage(int imageid);

        bool UpdateImage(IImage updateImage);
    }
}
