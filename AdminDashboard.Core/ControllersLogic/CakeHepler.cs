using Bakery.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDashboard.Core.ControllersLogic
{
    public static class CakeHepler
    {
        public static bool ImageIsExistsInCreateCakeModel(CreateCakeModel cakeModel)
        {
            if (cakeModel.ImageId != 0)
            {
                return true;
            }

            if (cakeModel.ImageName != null && cakeModel.ImageFile != null)
            {
                return true;
            }

            return false;
        }
    }
}
