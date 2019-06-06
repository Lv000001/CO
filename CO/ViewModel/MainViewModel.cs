using CO.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CO.ViewModel
{
    class MainViewModel
    {
        private ICommand fullScreenCommand;
        /// <summary>
        /// 截全屏
        /// </summary>
        public ICommand FullScreenCommand
        {
            get
            {
                if (fullScreenCommand == null)
                {
                    fullScreenCommand = new DelegateCommand<object>((obj) =>
                    {
                        ((MainWindow)obj).Hide();
                        using (Bitmap bitmap = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, new System.Drawing.Size((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight), CopyPixelOperation.SourceCopy);
                            }
                            System.Windows.Clipboard.SetImage(ImageHelper.GetBitmapSource(bitmap));
                        }
                        ((MainWindow)obj).Show();
                    }, (obj) => { return true; });
                }
                return fullScreenCommand;
            }
        }

        private ICommand captureRectangleCommand;
        /// <summary>
        /// 截矩形图
        /// </summary>
        public ICommand CaptureRectangleCommand
        {
            get
            {
                if (captureRectangleCommand == null)
                {
                    captureRectangleCommand = new DelegateCommand<object>((obj) =>
                    {
                        ((MainWindow)obj).Hide();
                        CaptureWindow capture = new CaptureWindow();
                        capture.Topmost = true;
                        capture.ShowDialog();
                        if (!string.IsNullOrWhiteSpace(capture.CaptureImageFileName) && File.Exists(capture.CaptureImageFileName))
                        {
                            File.Delete(capture.CaptureImageFileName);
                        }
                         ((MainWindow)obj).Show();
                    }, (obj) => { return true; });
                }
                return captureRectangleCommand;
            }
        }

        private ICommand orcCommand;
        /// <summary>
        /// 文字识别
        /// </summary>
        public ICommand OrcCommand
        {
            get
            {
                if (orcCommand == null)
                {
                    orcCommand = new DelegateCommand<object>((obj) =>
                    {
                        ((MainWindow)obj).Hide();
                        CaptureWindow capture = new CaptureWindow();
                        capture.ShowDialog();
                        ((MainWindow)obj).Show();
                        if (!string.IsNullOrWhiteSpace(capture.CaptureImageFileName) && File.Exists(capture.CaptureImageFileName))
                        {
                            var ss = OcrHelper.OcrImage(Base64ImageHelper.ImgToBase64String(capture.CaptureImageFileName));
                            if (ss != null)
                            {
                                string reslut = "";
                                for (int i = 0; i < ss.Count; i++)
                                {
                                    reslut += ss[i].Words + "\r\n";
                                }

                                 ((MainWindow)obj).txtResult.Text = reslut;
                                ((MainWindow)obj).pop.IsOpen = true;
                            }
                            File.Delete(capture.CaptureImageFileName);
                        }
                    }, (obj) => { return true; });
                }
                return orcCommand;
            }
        }

    }
}
