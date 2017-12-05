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
            foreach(var image in cakeModel.Files)
            {
                if (image != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
