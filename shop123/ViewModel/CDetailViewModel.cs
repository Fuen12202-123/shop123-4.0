using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shop123.Models;

namespace shop123.ViewModel
{
    public class CDetailViewModel
    {
        public spu Spu
        {
            get;
            set;
        }
        //public sku Sku
        //{
        //    get;
        //    set;
        //}
        public List<comment> Comments
        {
            get;
            set;
        }

        public IEnumerable<sku> Sku { get; set; }
    }
}