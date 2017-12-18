using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMCloud.Models
{
    public class ApplicationsViewModel
    {
        public int? AppicationID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DataTableRequest DataTable { get; set; }
    }
}