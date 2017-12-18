using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMCloud.Models.ViewModels
{
    public class RoleViewModel
    {
        public int? RoleID { get; set; }
        public int? UserID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DataTableRequest DataTable { get; set; }
        public List<int> Selection { get; set; }
    }
}