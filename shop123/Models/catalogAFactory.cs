using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class catalogAFactory
    {
        shop123Entities2 db = new shop123Entities2();

        public List<catalogA> queryAll()
        {
            var catalogA = db.catalogA.ToList();
            return catalogA;
        }

    }
}