﻿using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace AuroraGUI
{
    /// <summary>
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            var fileTime = File.GetLastWriteTime(GetType().Assembly.Location);
            VerText.Text += FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).FileVersion;
            VerText.Text += $" ({fileTime.Year - 2000}{fileTime.Month:00}{fileTime.Day:00}";
            VerText.Text += IsDebugBuild(GetType().Assembly) ? ".Nightly)" : ".Releases)";
            if (IsDebugBuild(GetType().Assembly)) IsNightly.Visibility = Visibility.Visible;
        }

        private void ButtonCredits_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(File.Exists(MainWindow.SetupBasePath + "CREDITS.html")
                ? $"file://{MainWindow.SetupBasePath}CREDITS.html"
                : "https://github.com/mili-tan/AuroraDNS.GUI/blob/master/CREDITS.md");
        }

        private void ButtonAbout_OnClick(object sender, RoutedEventArgs e)
            => Process.Start("https://github.com/mili-tan/AuroraDNS.GUI");

        private void HyperlinkSponsor_OnClick(object sender, RoutedEventArgs e) 
            => Process.Start("https://afdian.net/@AuroraDNS");

        private static bool IsDebugBuild(Assembly assembly)
        {
            foreach (object attribute in assembly.GetCustomAttributes(false))
            {
                if (attribute is DebuggableAttribute)
                {
                    DebuggableAttribute _attribute = attribute as DebuggableAttribute;

                    return _attribute.IsJITTrackingEnabled;
                }
            }
            return false;
        }
    }
}
