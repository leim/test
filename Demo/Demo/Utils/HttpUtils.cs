using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Utils
{
    public class HttpUtils
    {
        public HttpUtils(string url)
        {
            this.url = new Uri(url);
        }

        public async Task<string> GetRequest()
        {
            this.request = (HttpWebRequest)WebRequest.Create(this.url);
            request.Method = "GET";
            request.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            try
            {
                var response = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse,
                                                                        request.EndGetResponse, null);
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        this.responseString = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException wex)
            {
                this.responseString = "网络异常！";
            }
            catch (Exception ex)
            {
                this.responseString = "其他异常！";
            }
            return this.responseString;
        }

        public async Task<string> PostRequest(string data)
        {
            this.request = (HttpWebRequest)WebRequest.Create(this.url);
            request.Method = "POST";
            request.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            try
            {
                var postStream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream,
                                                                        request.EndGetRequestStream, null);
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();
                var response = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse,
                                                                        request.EndGetResponse, null);
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        this.responseString = reader.ReadToEnd();
                    }
                }
                //this.responseString = HttpUtility.UrlDecode(this.responseString);
            }
            catch (WebException wex)
            {
                this.responseString = "网络异常！";
            }
            catch (Exception ex)
            {
                this.responseString = "其他异常！";
            }
            return this.responseString;
        }

        private Uri url;
        private HttpWebRequest request;
        private string responseString;
    }
}
