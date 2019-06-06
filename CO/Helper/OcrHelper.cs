using CO.Entity;
using CO.Enum;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CO.Helper
{
    class OcrHelper
    {
        /// <summary>
        /// 授权码
        /// </summary>
        public static String TOKEN = "";

        /// <summary>
        /// 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        /// </summary>
        private static String clientId = "LnDgawVHlEbLXNunMX2gC8cY";

        /// <summary>
        /// 百度云中开通对应服务应用的 Secret Key
        /// </summary>
        private static String clientSecret = "WMpu9tyssYR9Ozl94wNlK020Ub51xeiG";

        /// <summary>
        /// 获得授权码
        /// </summary>
        /// <returns></returns>
        public static String GetAccessToken()
        {
            // 授权地址
            string authHost = "https://aip.baidubce.com/oauth/2.0/token";
            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("grant_type", "client_credentials");
            nameValueCollection.Add("client_id", clientId);
            nameValueCollection.Add("client_secret", clientSecret);

            string type;
            string result = HttpHelper.DoPost(authHost, nameValueCollection, null, out type);
            if (!string.IsNullOrWhiteSpace(result))
            {                
                AccessToken accessToken = JsonConvert.DeserializeObject<AccessToken>(result);
                if (accessToken != null)
                {
                    return accessToken.Token;
                }
            }
            return result;
        }

        /// <summary>
        /// 识别图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="languageType"></param>
        /// <returns></returns>
        public static List<OcrWord> OcrImage(string image, LanguageType languageType = LanguageType.CHN_ENG)
        {
            // 文字识别地址  
            string ocrHost = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic";

            if (string.IsNullOrWhiteSpace(TOKEN))
            {
                TOKEN = GetAccessToken();
            }
            ocrHost = string.Format("{0}?access_token={1}", ocrHost, TOKEN);
            // head  Content-Type	application/x-www-form-urlencoded
            NameValueCollection paramList = new NameValueCollection();
            NameValueCollection heads = new NameValueCollection();
            heads.Add("ContentType", "application/x-www-form-urlencoded");
            paramList.Add("image", image);
           // paramList.Add("language_type", languageType.ToString());
            string type;
            string result = HttpHelper.DoPost(ocrHost, paramList, heads, out type);
            if (!string.IsNullOrWhiteSpace(result))
            {
                OcrResponse ocrResponse = JsonConvert.DeserializeObject<OcrResponse>(result);
                if (ocrResponse != null)
                {
                    return ocrResponse.WordsResult;
                }
            }

            return null;
        }


        /// <summary>
        /// 识别url对应的图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="languageType"></param>
        /// <returns></returns>
        public static List<OcrWord> OcrUrl(string url, LanguageType languageType = LanguageType.CHN_ENG)
        {
            // 文字识别地址  
            string ocrHost = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic";
            if (string.IsNullOrWhiteSpace(TOKEN))
            {
                TOKEN = GetAccessToken();
            }
            ocrHost = string.Format("{0}?access_token={1}", ocrHost, TOKEN);
            // head  Content-Type	application/x-www-form-urlencoded
            NameValueCollection paramList = new NameValueCollection();
            NameValueCollection heads = new NameValueCollection();
            heads.Add("ContentType", "application/x-www-form-urlencoded");
            paramList.Add("url", url);
            paramList.Add("language_type", System.Enum.GetName(typeof(LanguageType), languageType));
            string type;
            string result = HttpHelper.DoPost(ocrHost, paramList, heads, out type);
            if (!string.IsNullOrWhiteSpace(result))
            {
                OcrResponse ocrResponse = JsonConvert.DeserializeObject<OcrResponse>(result);
                if (ocrResponse != null)
                {
                    return ocrResponse.WordsResult;
                }
            }

            return null;
        }
    }
}

