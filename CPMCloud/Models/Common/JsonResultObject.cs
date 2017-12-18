using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMCloud.Models
{
    public class JsonResultObject<TData>
    {
        /// <summary>
        /// Trạng thái của kết quả trả về có thành công không?
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        //  Thông điệp lỗi nếu không thành công.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Dữ liệu trả về nếu hành động thành công
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// Mã response
        /// </summary>
        public string Status { get; set; }
    }
}