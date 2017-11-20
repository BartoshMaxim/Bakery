using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.DB.Interfaces
{
    public interface ICakeImageRepository
    {
        List<Image> GetImages(int cakeid);

        int GetCakeImageId(int cakeid, int imageid);

        bool InsertCakeImageReference(int cakeid, int imageid, LoginModel loginModel);

        bool DeleteCakeImageReference(int cakeid, int imageid, LoginModel loginModel);
    }
}
