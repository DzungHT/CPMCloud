using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMCloud.Models.ViewModels
{
    public class MenuDTO
    {
        public int MenuID { get; set; }

        public int? MenuPID { get; set; }

        public string MenuPName { get; set; }

        public int? ApplicationID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FontIcon { get; set; }

        public string Url { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public int? Sort_Order { get; set; }

        public string Path { get; set; }

        public int? Status { get; set; }

        public string ApplicationName { get; set; }
        public int? RoleID { get; set; }
        public List<int> Selection { get; set; }

        /// <summary>
        /// Được hiểu đơn giản là lấy dữ liệu ở trang số draw
        /// </summary>
        public int draw { get; set; }

        public int recordPerPage { get; set; }
    }
}