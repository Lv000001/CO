using CO.Helper;
using CO.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CO
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon _notifyIcon = null;
        private bool isQuit = false;

        private MainViewModel mainViewModel;
        private IntPtr handle;

        private bool isSetting =false;

        public MainWindow()
        {
            InitializeComponent();
            this.Left = SystemParameters.WorkArea.Width - 200;
            this.Top = SystemParameters.WorkArea.Height - 100;
            this.Closing += MainWindow_Closing;
            this.Activated += MainWindow_Activated;
            mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;
            InitialTray();

        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            handle = new WindowInteropHelper(this).Handle;
            SetHotKey();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isQuit && Properties.Settings.Default.IsMinimized)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 系统托盘
        /// </summary>
        private void InitialTray()
        {
            //设置托盘的各个属性
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Text = "OC";
            _notifyIcon.Visible = true;//托盘按钮是否可见
            _notifyIcon.Icon = global::CO.Properties.Resources.logo;
            _notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
            menu.MenuItems.Add(new System.Windows.Forms.MenuItem("退出", (s, e) =>
            {
                isQuit = true;
                this.Close();
            }));
            _notifyIcon.ContextMenu = menu;
        }

        /// <summary>
        /// 托盘图标鼠标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        const int WM_HOTKEY = 0x0312;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    if (isSetting)
                    {
                        break;
                    }
                    switch (wParam.ToInt32())
                    {
                        case 1780:
                            mainViewModel.CaptureRectangleCommand.Execute(this);
                            break;
                        case 1781:
                            mainViewModel.FullScreenCommand.Execute(this);
                            break;
                        case 1782:
                            mainViewModel.OrcCommand.Execute(this);
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 设置系统热键
        /// </summary>
        private void SetHotKey()
        {
            string rect = Properties.Settings.Default.RectHotKey;
            string full = Properties.Settings.Default.FullHotKey;
            string orc = Properties.Settings.Default.OrcHotKey;
            RegiseterHotKey(1780, rect);
            RegiseterHotKey(1781, full);
            RegiseterHotKey(1782, orc);
        }

        private void RegiseterHotKey(int hotKeyId, string hotkey)
        {
            string[] keys = hotkey.Split('+');
            List<HotKeyHelper.KeyModifiers> keyModifiers = new List<HotKeyHelper.KeyModifiers>();
            List<string> normalKey = new List<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() == "ctrl")
                {
                    keyModifiers.Add(HotKeyHelper.KeyModifiers.Ctrl);
                }
                else if (keys[i].ToLower() == "alt")
                {
                    keyModifiers.Add(HotKeyHelper.KeyModifiers.Alt);
                }
                else if (keys[i].ToLower() == "shift")
                {
                    keyModifiers.Add(HotKeyHelper.KeyModifiers.Shift);
                }
                else
                {
                    normalKey.Add(keys[i]);
                }
            }
            if (normalKey.Count == 1) //
            {
                Keys norKey = Keys.D1;
                int key = 0;
                try
                {
                    if (int.TryParse(normalKey[0], out key))
                    {
                        norKey = (Keys)System.Enum.Parse(typeof(Keys), "D" + normalKey[0]);
                    }
                    else
                    {
                        norKey = (Keys)System.Enum.Parse(typeof(Keys), normalKey[0]);
                    }
                }
                catch (Exception)
                {
                    return;
                }

                HotKeyHelper.KeyModifiers sysKeyModifiers = HotKeyHelper.KeyModifiers.None;
                for (int i = 0; i < keyModifiers.Count; i++)
                {
                    sysKeyModifiers |= keyModifiers[i];
                }
                HotKeyHelper.UnregisterHotKey(handle, hotKeyId);
                HotKeyHelper.RegisterHotKey(handle, hotKeyId, sysKeyModifiers, norKey);
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            isSetting = true;
            SettingsWindow settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog() ?? false)
            {
                SetHotKey();
            }
            isSetting = false;
        }
    }
}
