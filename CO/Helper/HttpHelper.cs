using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;

namespace CO.Helper
{
    class HttpHelper
    {
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramInfos"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string DoGet(string url, NameValueCollection paramInfos, NameValueCollection header, out string contentType)
        {
            string returnValue = null;
            WebClient webClient = new WebClient();
            contentType = "";
            try
            {
                webClient.Headers = new WebHeaderCollection();
                foreach (string key in header.Keys)
                {
                    webClient.Headers.Set((HttpRequestHeader)System.Enum.Parse(typeof(HttpRequestHeader), key), header[key]);
                }
                foreach (string item in paramInfos.Keys)
                {
                    url += string.Format("&{0}={1}", HttpUtility.UrlEncode(item), HttpUtility.UrlEncode(paramInfos[item]));
                }
                byte[] responseArray = webClient.DownloadData(url);

                contentType = webClient.ResponseHeaders.Get("Content-Type");
                returnValue = Encoding.UTF8.GetString(responseArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
            finally
            {
                webClient.Dispose();
                webClient = null;
            }
            return returnValue;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramInfos"></param>
        /// <returns></returns>
        public static string DoPost(string url, NameValueCollection paramInfos, NameValueCollection header, out string contentType)
        {
            string returnValue = null;
            contentType = "";
            WebClient webClient = new WebClient();
            try
            {
                if (header != null)
                {
                    foreach (string key in header.Keys)
                    {
                        webClient.Headers.Set((HttpRequestHeader)System.Enum.Parse(typeof(HttpRequestHeader), key), header[key]);
                    }
                }
                byte[] responseArray = webClient.UploadValues(url, paramInfos);
                try
                {
                    contentType = webClient.ResponseHeaders.Get("Content-Type");
                }
                catch (Exception e) {
                }
               
                returnValue = Encoding.UTF8.GetString(responseArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
            finally
            {
                webClient.Dispose();
                webClient = null;
            }
            return returnValue;
        }

    }
}
