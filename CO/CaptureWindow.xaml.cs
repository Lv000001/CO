using CO.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CO
{
    /// <summary>
    /// CaptureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CaptureWindow : Window
    {
        private string tempImageFileName = "";
        private System.Windows.Point mouseDownPoint;

        /// <summary>
        /// 截获的图片保存文件名
        /// </summary>
        public string CaptureImageFileName { get; private set; }

        public CaptureWindow()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Loaded += CaptureWindow_Loaded;
            this.MouseLeftButtonDown += CaptureWindow_MouseLeftButtonDown;
            this.MouseMove += CaptureWindow_MouseMove;
            this.MouseLeftButtonUp += CaptureWindow_MouseLeftButtonUp;
            this.Unloaded += CaptureWindow_Unloaded;
        }

        private void CaptureWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tempImageFileName) && File.Exists(tempImageFileName))
            {
                File.Delete(tempImageFileName);
            }
        }

        private void CaptureWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mouseDownPoint = new System.Windows.Point();
            if (selectBorder.ActualWidth < 15 && selectBorder.ActualHeight < 15)
            {
                // 图片太小不识别
            }
            else
            {
                int left = (int)Canvas.GetLeft(selectBorder);
                int top = (int)Canvas.GetTop(selectBorder);
                CaptureImageFileName = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid().ToString().Replace("-", "") + ".png");
                ImageHelper.CutImage(tempImageFileName, left, top, (int)selectBorder.ActualWidth, (int)selectBorder.ActualHeight, CaptureImageFileName);
                using (Bitmap bitmap = new Bitmap(CaptureImageFileName))
                {
                    Clipboard.SetImage(ImageHelper.GetBitmapSource(bitmap));
                }
                bkImg.Source = null;
                this.Close();
            }
        }

        private void CaptureWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Windows.Point curPoint = Mouse.GetPosition(this as FrameworkElement);
                selectBorder.Width = Math.Abs(mouseDownPoint.X - curPoint.X);
                selectBorder.Height = Math.Abs(mouseDownPoint.Y - curPoint.Y);
                Canvas.SetLeft(selectBorder, mouseDownPoint.X < curPoint.X ? mouseDownPoint.X : curPoint.X);
                Canvas.SetTop(selectBorder, mouseDownPoint.Y < curPoint.Y ? mouseDownPoint.Y : curPoint.Y);
            }
        }

        private void CaptureWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectBorder.Visibility = Visibility.Visible;
            selectBorder.Width = 0;
            selectBorder.Height = 0;
            mouseDownPoint = Mouse.GetPosition(this as FrameworkElement);
            Console.WriteLine(mouseDownPoint);
        }

        private void CaptureWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tempImageFileName = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid().ToString().Replace("-", "") + ".png");
            using (Bitmap bitmap = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, new System.Drawing.Size((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight), CopyPixelOperation.SourceCopy);
                }
                bitmap.Save(tempImageFileName, ImageFormat.Png);
            }
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = new Uri(tempImageFileName);
            bitmapImage.EndInit();

            bkImg.Source = bitmapImage.Clone();
        }
    }
}
