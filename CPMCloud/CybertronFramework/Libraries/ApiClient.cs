using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CybertronFramework.Libraries
{
    /// <summary>
    /// Lớp cung cấp các phương thức để gọi WebApi, RestFul Service
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// Đường dẫn URI cơ bản. Ví dụ: http://www.abc.com
        /// </summary>
        public static string BaseURI { get; set; }

        public static string Token { get; set; }

        /// <summary>
        /// Lấy ra đối tượng sử dụng BaseAddress. Yêu cầu BaseAddress phải được gán trước khi sử dụng thuộc tính này.
        /// </summary>
        public static ApiClient Instance
        {
            get
            {
                return new ApiClient(BaseURI);
            }
        }

        /// <summary>
        /// Đối tượng HttpClient nguyên thủy, cơ bản của .NET
        /// </summary>
        private HttpClient client;

        /// <summary>
        /// Tạo một đối tượng ApiClient sử dụng tham số truyền vào làm đường dẫn cơ bản cho Api
        /// </summary>
        /// <param name="baseUri">Đường dẫn cơ bản đến Api. Lưu ý đường dẫn dùng để lấy URI, không phải đường dẫn chung để cộng chuỗi URL sau này. Có thể hiểu đơn giản là tên domain. VD: http://www.abc.com, https://www.abc.com </param>
        public ApiClient(string baseUri)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (HttpContext.Current.Session["TOKEN"] != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)HttpContext.Current.Session["TOKEN"]);
            }
        }

        /// <summary>
        /// Gọi bất đồng bộ API theo phương thức DELETE.
        /// </summary>
        /// <typeparam name="TData">Kiểu dữ liệu của data gửi lên Api</typeparam>
        /// <typeparam name="TResult">Kiểu dữ liệu trả về của Api</typeparam>
        /// <param name="url">Đường dẫn đến Api. Đường dẫn được tính từ sau tên domain</param>
        /// <returns>Trả về một đối tượng có kiểu TResult</returns>
        public async Task<TResult> DeleteApiAsync<TResult>(string url)
        {
            TResult result = default(TResult);
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<TResult>();
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gọi bất đồng bộ API theo phương thức GET.
        /// </summary>
        /// <typeparam name="TData">Kiểu dữ liệu của data gửi lên Api</typeparam>
        /// <typeparam name="TResult">Kiểu dữ liệu trả về của Api</typeparam>
        /// <param name="url">Đường dẫn đến Api. Đường dẫn được tính từ sau tên domain</param>
        /// <returns>Trả về một đối tượng có kiểu TResult</returns>
        public async Task<TResult> GetApiAsync<TResult>(string url)
        {
            TResult result = default(TResult);
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<TResult>();
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gọi bất đồng bộ API theo phương thức POST.
        /// </summary>
        /// <typeparam name="TData">Kiểu dữ liệu của data gửi lên Api</typeparam>
        /// <typeparam name="TResult">Kiểu dữ liệu trả về của Api</typeparam>
        /// <param name="url">Đường dẫn đến Api. Đường dẫn được tính từ sau tên domain</param>
        /// <param name="data">Dữ liệu gửi lên Api</param>
        /// <returns>Trả về một đối tượng có kiểu TResult</returns>
        public async Task<TResult> PostApiAsync<TResult, TData>(string url, TData data)
        {
            TResult result = default(TResult);
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<TResult>();
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gọi bất đồng bộ API theo phương thức PUT.
        /// </summary>
        /// <typeparam name="TData">Kiểu dữ liệu của data gửi lên Api</typeparam>
        /// <typeparam name="TResult">Kiểu dữ liệu trả về của Api</typeparam>
        /// <param name="url">Đường dẫn đến Api. Đường dẫn được tính từ sau tên domain</param>
        /// <param name="data">Dữ liệu gửi lên Api</param>
        /// <returns>Trả về một đối tượng có kiểu TResult</returns>
        public async Task<TResult> PutApiAsync<TResult, TData>(string url, TData data)
        {
            TResult result = default(TResult);
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<TResult>();
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region xác thực
        public async Task<string> GetAccessToken(AuthenticationLogin input)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Resource.ServiceUriBase);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", input.grant_type),
                    new KeyValuePair<string, string>("username", input.username),
                    new KeyValuePair<string, string>("password", input.password),
                    new KeyValuePair<string, string>("client_id", input.client_id),
                    new KeyValuePair<string, string>("client_secret", input.client_secret)
                });
                var response = client.PostAsync("Token", content).Result;
                if (response.IsSuccessStatusCode != true && response.StatusCode != HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                }
                var result = await response.Content.ReadAsAsync<AuthenticationToken>();
                HttpContext.Current.Session["TOKEN"] = result.access_token;
                return result.access_token;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}