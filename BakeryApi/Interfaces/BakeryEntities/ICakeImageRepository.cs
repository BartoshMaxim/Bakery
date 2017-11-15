using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryApi.Interfaces
{
    public interface ICakeImageRepository
    {
        List<Image> GetImages(int cakeid);

        bool InsertCakeImageReference(int cakeid, int imageid, LoginModel loginModel);

        bool DeleteCakeImageReference(int cakeid, int imageid, LoginModel loginModel);
    }
}
