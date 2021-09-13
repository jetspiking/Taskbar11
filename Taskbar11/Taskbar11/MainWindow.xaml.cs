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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Diagnostics;

namespace Taskbar11
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Taskbar11";

            int taskbarSize = GetTaskbarSize();

            if (taskbarSize > -1 && taskbarSize < 4)
                XAML_TaskbarSizeBox.SelectedIndex = taskbarSize;
            else 
                XAML_TaskbarSizeBox.SelectedItem = taskbarSize.ToString();

            int taskbarPosition = GetTaskbarPosition();

            XAML_TaskbarPositionBox.SelectedIndex = taskbarPosition == 1 ? 1 : 0;
        }

        /// <summary>
        /// Read the taskbar position from the registry.
        /// </summary>
        /// <returns>Integer that specifies the taskbar position on index 12 of the "Settings" key.</returns>
        public int GetTaskbarPosition()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3", true);
            if (key != null)
            {
                Object value = key.GetValue("Settings");
                if (value != null)
                {
                    Byte[] data = (Byte[])value;
                    return data[7 + 5];
                }
            }
            return -1;
        }

        /// <summary>
        /// Write the taskbar position to the registry.
        /// </summary>
        /// <param name="taskbarPosition">Byte that specifies the taskbar position on index 12 of the "Settings" key.</param>
        public void SetTaskbarPosition(Byte taskbarPosition)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3", true);
            if (key != null)
            {
                Object value = key.GetValue("Settings");
                if (value != null)
                {
                    Byte[] data = (Byte[])value;
                    data[7 + 5] = taskbarPosition;
                    key.SetValue("Settings", data, RegistryValueKind.Binary);
                }
            }
        }

        /// <summary>
        /// Read the taskbar size from the registry.
        /// </summary>
        /// <returns>Integer that specifies the taskbar size.</returns>
        public int GetTaskbarSize()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            if (key != null)
            {
                Object value = key.GetValue("TaskbarSi");
                if (value != null)
                    return (int)value;
            }
            return -1;
        }

        /// <summary>
        /// Write the taskbar size to the registry.
        /// </summary>
        /// <param name="value">Integer that specifies the taskbar size.</param>
        public void SetTaskbarSize(int value)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            if (key != null)
                key.SetValue("TaskbarSi", value, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Represents the "Save" button. Writes the data to the registry and forces explorer.exe process to restart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SetTaskbarSize(XAML_TaskbarSizeBox.SelectedIndex);
            SetTaskbarPosition(XAML_TaskbarPositionBox.SelectedIndex == 0 ? (Byte)3 : (Byte)1);
            RestartExplorer();
        }

        /// <summary>
        /// Forces the explorer.exe process to restart by killing it (the UI needs to reload to display the changes). 
        /// </summary>
        private static void RestartExplorer()
        {
            Process p = new Process();
            foreach (Process process in Process.GetProcesses())
                if (process.ProcessName == "explorer")
                    process.Kill();
        }
    }
}

