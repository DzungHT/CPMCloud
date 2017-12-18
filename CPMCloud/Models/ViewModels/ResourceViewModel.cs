using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMCloud.Models.ViewModels
{
    public class ResourceViewModel
    {
        public int? ResourceID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ApplicationID { get; set; }
        public DataTableRequest DataTable { get; set; }
    }
}