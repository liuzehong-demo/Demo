using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Demo.Helper
{
    public class HttpHelper
    {
        public static string Post(string url, Dictionary<string, string> arguments, int timeout = 0)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(Encode(arguments));
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream2 = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream2, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string Post(string url, string postData, int timeout = 0)
        {
            return Post(url, postData, null, timeout);
        }


        public static string Post(string url, string postData, string contenttype, int timeout = 0)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            if (string.IsNullOrEmpty(contenttype))
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                request.ContentType = contenttype;
            }
            request.ContentLength = bytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream2 = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream2, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string Post(string url, string postData, string contenttype, out WebHeaderCollection headers, int timeout = 0)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            if (string.IsNullOrEmpty(contenttype))
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                request.ContentType = contenttype;
            }
            request.ContentLength = bytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream2 = response.GetResponseStream())
                    {
                        headers = response.Headers;
                        using (StreamReader reader = new StreamReader(stream2, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string Post(string url, string postData, string contenttype, Dictionary<string, string> headers, string method = "", int timeout = 0)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            if (!string.IsNullOrEmpty(method))
            {
                request.Method = method;
            }
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            if (string.IsNullOrEmpty(contenttype))
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                request.ContentType = contenttype;
            }
            if (headers != null && headers.Count > 0)
            {
                foreach (var item in headers.Keys)
                {
                    request.Headers.Add(item, headers[item]);
                }

            }
            request.ContentLength = bytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream2 = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream2, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string Get(string url, Dictionary<string, string> headers, int timeout = 0)
        {

            if (headers != null && headers.Count > 0)
            {
                url = Encode(headers);
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static string Post(HttpWebRequest request, byte[] bytes)
        {
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream2 = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream2, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string Get(HttpWebRequest request, int timeout = 0)
        {

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }


        public static string GetHasCatch(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var stream = ex.Response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string Encode(Dictionary<string, string> data)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in data)
            {
                builder.AppendFormat("{0}={1}&", pair.Key, HttpUtility.UrlEncode(pair.Value));
            }
            char[] trimChars = new char[] { '&' };
            return builder.CString("").TrimEnd(trimChars);
        }
    }
}
