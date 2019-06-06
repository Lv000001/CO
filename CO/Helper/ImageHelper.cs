using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CO.Helper
{
    class ImageHelper
    {
        /// <summary> 
        /// 截取图片方法 
        /// </summary> 
        /// <param name="url">图片地址</param> 
        /// <param name="beginX">开始位置-X</param> 
        /// <param name="beginY">开始位置-Y</param> 
        /// <param name="getX">截取宽度</param> 
        /// <param name="getY">截取长度</param> 
        /// <param name="fileName">文件名称</param> 
        public static string CutImage(string url, int beginX, int beginY, int getX, int getY, string fileName)
        {
            using (Bitmap bitmap = new Bitmap(url))
            {//原图 
                if (((beginX + getX) <= bitmap.Width) && ((beginY + getY) <= bitmap.Height))
                {
                    using (Bitmap destBitmap = new Bitmap(getX, getY))
                    {//目标图 
                        Rectangle destRect = new Rectangle(0, 0, getX, getY);//矩形容器 
                        Rectangle srcRect = new Rectangle(beginX, beginY, getX, getY);

                        Graphics g = Graphics.FromImage(destBitmap);
                        g.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);

                        ImageFormat format = ImageFormat.Jpeg;
                        switch (fileName.Substring(fileName.Length - 3, 3).ToLower())
                        {
                            case "jpg":
                                format = ImageFormat.Jpeg;
                                break;
                            case "png":
                                format = ImageFormat.Png;
                                break;
                            case "bmp":
                                format = ImageFormat.Bmp;
                                break;
                            case "gif":
                                format = ImageFormat.Gif;
                                break;
                        }
                        destBitmap.Save(fileName, format);
                        destBitmap.Dispose();
                        return fileName;
                    }
                }
                else
                {
                    return "截取范围超出图片范围";
                }
            }
        }

        // <summary>
        /// 转换Bitmap到BitmapSource
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static System.Windows.Media.Imaging.BitmapSource GetBitmapSource(System.Drawing.Bitmap bmp)
        {
            System.Windows.Media.Imaging.BitmapFrame bf = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bf = System.Windows.Media.Imaging.BitmapFrame.Create(ms, System.Windows.Media.Imaging.BitmapCreateOptions.None, System.Windows.Media.Imaging.BitmapCacheOption.OnLoad);

            }
            return bf;
        }
    }
}
