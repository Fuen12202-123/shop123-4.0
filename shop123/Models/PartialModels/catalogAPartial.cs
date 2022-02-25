using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    [MetadataType(typeof(catalogAMetadata))]
    public partial class catalogA
    {
        public class catalogAMetadata
        {
            [DisplayName("分類編號")]
            public int id { get; set; }
            [DisplayName("分類大項項目")]
            public string catalogAName { get; set; }

        }

    }
    
}