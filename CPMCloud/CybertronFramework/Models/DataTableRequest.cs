using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CybertronFramework.Models
{
    /// <summary>
    /// Mô tả đối tượng mà dataTable submit lên server xử lý
    /// </summary>
    public class DataTableRequest
    {
        /// <summary>
        /// Được hiểu đơn giản là lấy dữ liệu ở trang số draw
        /// </summary>
        public int draw { get; set; }
        /// <summary>
        /// Bắt đầu lấy dữ liệu từ bản ghi số start.
        /// Hiểu đơn giản có thể hiểu: bản ghi thứ start là bản ghi đầu tiên của trang draw
        /// </summary>
        public int start { get; set; }
        /// <summary>
        /// Số bản ghi trên một trang
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// Dữ liệu search toàn cục của cả bảng. Áp dụng cho tất cả các cột có thuộc tính "searchable" = true
        /// </summary>
        public Search search { get; set; }
        /// <summary>
        /// Dữ liệu sắp xếp. Bao gồm cột sắp xếp, chiều sắp xếp (tăng/giảm)
        /// </summary>
        public Order order { get; set; }

        /// <summary>
        /// Mô tả đối tượng lưu trữ thông tin tìm kiếm
        /// </summary>
        public class Search
        {
            /// <summary>
            /// Giá trị tìm kiếm
            /// </summary>
            public string value { get; set; }
            /// <summary>
            /// Có xử dụng regex không?
            /// </summary>
            public bool regex { get; set; }
        }

        /// <summary>
        /// Mô tả đối tượng lưu trữ thông tin sắp xếp
        /// </summary>
        public class Order
        {
            /// <summary>
            /// Chỉ số cột được sắp xếp
            /// </summary>
            public int column { get; set; }
            /// <summary>
            /// Viết tắt của Direction. Hướng sắp xếp (tăng/giảm):
            /// "asc" - tăng. "desc" - giảm.
            /// </summary>
            public string dir { get; set; }
        }

        /// <summary>
        /// Mô tả một cột dữ liệu
        /// </summary>
        public class Column
        {
            /// <summary>
            /// Tên trường dữ liệu mapping với cột
            /// </summary>
            public string data { get; set; }
            /// <summary>
            /// Tên của cột
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// Có thể tìm kiếm trên cột?
            /// </summary>
            public bool searchable { get; set; }
            /// <summary>
            /// Cột có thể được sắp xếp?
            /// </summary>
            public bool orderable { get; set; }
            /// <summary>
            /// Thông tin tìm kiếm của cột.
            /// </summary>
            public Search search { get; set; }
        }
    }
}