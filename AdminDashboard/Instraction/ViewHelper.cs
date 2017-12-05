using Bakery.DB.Interfaces;
using Bakery.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminDashboard.Instraction
{
    public static class ViewHelper
    {
        public static MvcHtmlString ImageHelper(this HtmlHelper helper, IImage image, UrlHelper urlHelper)
        {
            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString ImageHelper(this HtmlHelper helper, IImage image, UrlHelper urlHelper, string className)
        {
            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            tagBuilder.Attributes["class"] = className;

            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString ImageHelper(this HtmlHelper helper, int imageId, UrlHelper urlHelper)
        {
            var image = BakeryRepository.GetImageRepository().GetImage(imageId);

            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            return new MvcHtmlString(tagBuilder.ToString());
        }


        public static MvcHtmlString ImageHelper(this HtmlHelper helper, int imageId, UrlHelper urlHelper, string className)
        {
            var image = BakeryRepository.GetImageRepository().GetImage(imageId);

            var tagBuilder = new TagBuilder("img");

            tagBuilder.Attributes["src"] = urlHelper.Content(image.ImagePath);

            tagBuilder.Attributes["alt"] = image.ImageName;

            tagBuilder.Attributes["title"] = image.ImageName;

            tagBuilder.Attributes["class"] = className;

            return new MvcHtmlString(tagBuilder.ToString());
        }


    }
}