using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace CO.Helper
{
    class Base64ImageHelper
    {
        /// <summary>
        /// 图片转为base64编码的字符串
        /// </summary>
        /// <param name="imagefilename">图片文件名称</param>
        /// <returns></returns>
        public static string ImgToBase64String(string imagefilename)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(imagefilename))
                {
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    ms.Close();
                    return Convert.ToBase64String(arr);
                }                              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// base64编码的字符串转为图片
        /// </summary>
        /// <param name="strbase64"></param>
        /// <returns></returns>
        public static Bitmap Base64StringToImage(string strbase64)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(strbase64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
