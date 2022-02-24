using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shop123.Models;


namespace shop123.ViewModel
{
    //public class AdminCatalogViewModel
    //{
    //    public catalogA CatalogA { get; set; }
    //    public catalogB CatalogB { get; set; }
    //    public IEnumerable<catalogB> CatalogBes { get; set; }
    //    public IEnumerable<CatalogViewModel> Catalog { get; set; }

    //}
    public class AdminCatalogViewModel
    {
        public int catbId { get; set; }
        public string catbName { get; set; }
        public int cataId { get; set; }
        public string cataName { get; set; }
    }
}