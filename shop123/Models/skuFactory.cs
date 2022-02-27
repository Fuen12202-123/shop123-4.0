using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class skuFactory
    {
        shop123Entities2 db = new shop123Entities2();

        public List<sku> queryAll()
        {
            var sku = db.sku.ToList();
            return sku;
        } 
        public List<sku> queryByspuid(int spuid)
        {
            var sku = db.sku.Where(m => m.spuId == spuid).ToList();
            return sku;
        }

        public int queryBycolorsize(string color, string size)
        {
            var sku = db.sku.Where(s => s.skuColor == color && s.skuSize == size).Select(s => s.id).FirstOrDefault();
            return sku;
        }

        
    }
}