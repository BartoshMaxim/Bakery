using System.Web;

namespace Bakery.DB.Interfaces
{
    public interface IUploadImageModel
    {
        string ImageName { get; set; }

        HttpPostedFileBase ImageFile { get; set; }
    }
}
