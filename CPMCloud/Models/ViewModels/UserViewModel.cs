using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMCloud.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public bool? NeedChangePassword { get; set; }

        public DateTime? ChangePasswordTime { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int? Status { get; set; }

        public string ActiveCode { get; set; }

        public DateTime? RequestDate { get; set; }

        public string[] Roles { get; set; }

        public DataTableRequest DataTable { get; set; }
    }
}