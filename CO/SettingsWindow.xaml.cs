using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CO
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.Loaded += SettingsWindow_Loaded;
            txtFull.KeyDown += TxtFull_KeyDown;
            txtOrc.KeyDown += TxtOrc_KeyDown;
            txtRect.KeyDown += TxtRect_KeyDown;
        }

        private void TxtRect_KeyDown(object sender, KeyEventArgs e)
        {
            List<string> hotKey = new List<string>();
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
            {
                hotKey.Add("Ctrl");
            }

            if ((Keyboard.Modifiers & ModifierKeys.Alt) > 0)
            {
                hotKey.Add("Alt");
            }

            if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
            {
                hotKey.Add("Shift");
            }
            int keyCode = (int)e.Key;
            if (hotKey.Count > 0)
            {
                keyCode = (int)e.SystemKey;
            }
            if (keyCode > 33 && keyCode < 70)
            {
                if (keyCode > 33 && keyCode < 44)
                {
                    hotKey.Add((keyCode - 34).ToString());
                }
                else
                {
                    if (hotKey.Count > 0)
                    {
                        hotKey.Add(e.SystemKey.ToString());
                    }
                    else
                    {
                        hotKey.Add(e.Key.ToString());
                    }
                }

            }
            txtRect.Text = string.Join("+", hotKey.ToArray());
        }

        private void TxtOrc_KeyDown(object sender, KeyEventArgs e)
        {
            List<string> hotKey = new List<string>();
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
            {
                hotKey.Add("Ctrl");
            }

            if ((Keyboard.Modifiers & ModifierKeys.Alt) > 0)
            {
                hotKey.Add("Alt");
            }
            if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
            {
                hotKey.Add("Shift");
            }

            int keyCode = (int)e.Key;
            if (hotKey.Count > 0)
            {
                keyCode = (int)e.SystemKey;
            }
            if (keyCode > 33 && keyCode < 70)
            {
                if (keyCode > 33 && keyCode < 44)
                {
                    hotKey.Add((keyCode - 34).ToString());
                }
                else
                {
                    if (hotKey.Count > 0)
                    {
                        hotKey.Add(e.SystemKey.ToString());
                    }
                    else
                    {
                        hotKey.Add(e.Key.ToString());
                    }
                }

            }
            txtOrc.Text = string.Join("+", hotKey.ToArray());
        }

        private void TxtFull_KeyDown(object sender, KeyEventArgs e)
        {
            List<string> hotKey = new List<string>();
            if ((Keyboard.Modifiers & ModifierKeys.Control)>0)
            {
                hotKey.Add("Ctrl");
            }

            if ((Keyboard.Modifiers & ModifierKeys.Alt) > 0)
            {
                hotKey.Add("Alt");
            }

            if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
            {
                hotKey.Add("Shift");
            }
            int keyCode = (int)e.Key;
            if (hotKey.Count > 0)
            {
                keyCode = (int)e.SystemKey;
            }
            if (keyCode > 33 && keyCode < 70)
            {
                if (keyCode > 33 && keyCode < 44)
                {
                    hotKey.Add((keyCode - 34).ToString());
                }
                else
                {
                    if (hotKey.Count > 0)
                    {
                        hotKey.Add(e.SystemKey.ToString());
                    }
                    else
                    {
                        hotKey.Add(e.Key.ToString());
                    }
                }
              
            }          
            txtFull.Text = string.Join("+", hotKey.ToArray());
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            chkStart.IsChecked = Properties.Settings.Default.IsStartWithWindowStart;
            chkClose.IsChecked = Properties.Settings.Default.IsMinimized;
            txtFull.Text = Properties.Settings.Default.FullHotKey;
            txtOrc.Text = Properties.Settings.Default.OrcHotKey;
            txtRect.Text = Properties.Settings.Default.RectHotKey;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsStartWithWindowStart = chkStart.IsChecked ?? false;
            Properties.Settings.Default.IsMinimized = chkClose.IsChecked ?? false;
            Properties.Settings.Default.FullHotKey = txtFull.Text;
            Properties.Settings.Default.OrcHotKey = txtOrc.Text;
            Properties.Settings.Default.RectHotKey = txtRect.Text;
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
