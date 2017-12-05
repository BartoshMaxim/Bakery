using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICakeImageRepository
    {
        IList<Image> GetImages(int cakeid);

        int GetCakeImageId(ICakeImage cakeImage);

        bool InsertCakeImageReference(ICakeImage cakeImage);

        bool DeleteCakeImageReference(ICakeImage cakeImage);

        bool DeleteCakeImageReference(int cakeImageId);

        bool IsExists(int cakeimageid);

        int GetCountRows();

        bool IsExists(ICakeImage cakeImage);
    }
}
