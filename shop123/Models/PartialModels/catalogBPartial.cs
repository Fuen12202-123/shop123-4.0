using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    [MetadataType(typeof(catalogBMetadata))]
    public partial class catalogB
    {
        public class catalogBMetadata
        {
            [DisplayName("分類編號")]
            public int id { get; set; }
            [DisplayName("分類細項")]
            public string catalogBName { get; set; }
            [DisplayName("分類大項")]
            public int catalogAId { get; set; }
        }

    }
    
}