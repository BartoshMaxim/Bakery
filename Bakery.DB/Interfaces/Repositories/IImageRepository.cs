using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface IImageRepository
    {
        IList<Image> GetImages();

        IList<Image> GetImages(int from, int to, IImage image = null);

        IImage GetImage(int imageid);

        bool InsertImage(IImage image);

        bool DeleteImage(int imageid);

        bool UpdateImage(IImage updateImage);

        bool IsExists(int imageid);

        int GetCountRows();

        int GetCountRows(IImage image);

        int GetIdForNextImage();
    }
}
