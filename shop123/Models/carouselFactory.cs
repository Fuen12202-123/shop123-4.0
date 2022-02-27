using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class carouselFactory
    {
        shop123Entities2 db = new shop123Entities2();
         public List<carousel> queryAll()
        {
            var carousel = db.carousel.ToList();
            return carousel;
        }
    }
}