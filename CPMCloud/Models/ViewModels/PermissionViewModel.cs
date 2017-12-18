using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMCloud.Models.ViewModels
{
    public class PermissionViewModel
    {
        public int PermissionID { get; set; }

        public int? ResourceID { get; set; }

        public int? OperationID { get; set; }

        public int? RoleID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DataTableRequest DataTable { get; set; }

        public string ApplicationName { get; set; }

        public int? ApplicationID { get; set; }

        public List<int> Selection { get; set; }
    }
}