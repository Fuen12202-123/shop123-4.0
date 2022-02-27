using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class catalogBFactory
    {
        shop123Entities2 db = new shop123Entities2();

        public List<catalogB> queryBycatA(int catalogAId)
        {
            var catalogB = db.catalogB.Where(m => m.catalogAId == catalogAId).ToList();
            return catalogB;
        }
    }
}