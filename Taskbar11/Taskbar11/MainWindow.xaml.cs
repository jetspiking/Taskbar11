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
using System.Security.AccessControl;

namespace Taskbar11
{
    public partial class MainWindow : Window
    {
        private const String AppTitle = "Taskbar11";
        private const String PathExplorerStuckRects3 = @"Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3";
        private const String PathExplorerAdvanced = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        private const String PathSearch = @"Software\Microsoft\Windows\CurrentVersion\Search";
        private const String PathPenWorkspace = @"Software\Microsoft\Windows\CurrentVersion\PenWorkspace";
        private const String PathTabletTip = @"Software\Microsoft\TabletTip\1.7";
        private const String PathTouchpad = @"Software\Microsoft\Touchpad";
        private const String PathExplorerMMStuckRects3 = @"Software\Microsoft\Windows\CurrentVersion\Explorer\MMStuckRects3";
        private const String PathInprocServer32 = @"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32";
        private const String PathCLSID = @"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}";

        public MainWindow()
        {
            InitializeComponent();
            this.Title = AppTitle;

            int taskbarSize = GetTaskbarSize();

            if (taskbarSize > -1 && taskbarSize < 3)
                XAML_TaskbarSizeBox.SelectedIndex = taskbarSize;
            else
                XAML_TaskbarSizeBox.SelectedItem = taskbarSize.ToString();

            int taskbarPosition = GetTaskbarPosition();

            XAML_TaskbarPositionBox.SelectedIndex = taskbarPosition == 1 ? 1 : 0;

            int taskbarAlignment = GetTaskbarAlignment();

            XAML_TaskbarIndentationBox.SelectedIndex = taskbarAlignment < XAML_TaskbarIndentationBox.Items.Count ? taskbarAlignment : 0;
            XAML_TaskbarSearchCheckBox.IsChecked = IsTaskbarSearchVisible();
            XAML_TaskbarTaskViewBox.IsChecked = IsTaskbarTaskViewVisible();
            XAML_TaskbarWidgetsBox.IsChecked = IsTaskbarWidgetsVisible();
            XAML_TaskbarChatBox.IsChecked = IsTaskbarChatVisible();
            XAML_TaskbarPenCheckBox.IsChecked = IsTaskbarPenVisible();
            XAML_TaskbarTouchCheckBox.IsChecked = IsTaskbarTouchKeyboardVisible();
            XAML_TaskbarVirtualTouchpadCheckBox.IsChecked = IsTaskbarTouchpadVisible();
            XAML_TaskbarBehaviourAutoHideCheckBox.IsChecked = IsTaskbarHidden();
            XAML_TaskbarBehaviourMultiMonitorOnCheckBox.IsChecked = IsTaskbarOnMultipleMonitors();
            XAML_TaskbarBehaviourMultiMonitorPositionCheckBox.IsChecked = IsTaskbarMultiMonitorPositionTaskbar();
            XAML_UseOldContextMenuCheckBox.IsChecked = IsUseOldContextMenu();

            XAML_TaskbarOrientationDependantPositionCheckBox.Checked += new RoutedEventHandler(XAML_TaskbarOrientationDependantPositionCheckBox_Checked);
            XAML_TaskbarOrientationDependantPositionCheckBox.Unchecked += new RoutedEventHandler(XAML_TaskbarOrientationDependantPositionCheckBox_Unchecked);
            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
        }



        /// <summary>
        /// Read the taskbar position from the registry.
        /// </summary>
        /// <returns>Integer that specifies the taskbar position on index 12 of the "Settings" key.</returns>
        public int GetTaskbarPosition()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
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
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
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
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
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
        /// <param name="taskbarSize">Byte that specifies the taskbar size.</param>
        public void SetTaskbarSize(Byte taskbarSize)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue("TaskbarSi", taskbarSize, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the taskbar aligment from the registry.
        /// </summary>
        /// <returns>Integer that specifies the taskbar aligment.</returns>
        public int GetTaskbarAlignment()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue("TaskbarAl");
                if (value != null)
                    return (int)value;
            }
            return -1;
        }

        /// <summary>
        /// Write the taskbar alignment to the registry.
        /// </summary>
        /// <param name="taskbarAlignment">Byte that specifies the taskbar alignment.</param>
        public void SetTaskbarAlignment(Byte taskbarAlignment)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue("TaskbarAl", taskbarAlignment, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the SearchboxTaskbarMode property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Search button is visible on the taskbar.</returns>
        public Boolean IsTaskbarSearchVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathSearch, true);
            if (key != null)
            {
                Object value = key.GetValue("SearchboxTaskbarMode");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar SearchboxTaskbarMode property to the registry. 
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Search button is visible on the taskbar.</param>
        public void SetTaskbarSearchVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathSearch, true);
            if (key != null)
                key.SetValue("SearchboxTaskbarMode", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the ShowTaskViewButton property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the TaskView button is visible on the taskbar.</returns>
        public Boolean IsTaskbarTaskViewVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue("ShowTaskViewButton");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar ShowTaskViewButton property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the TaskView button is visible on the taskbar.</param>
        public void SetTaskbarTaskViewVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue("ShowTaskViewButton", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TaskbarDa property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Widgets button is visible on the taskbar.</returns>
        public Boolean IsTaskbarWidgetsVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue("TaskbarDa");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar TaskbarDa property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Widgets button is visible on the taskbar.</param>
        public void SetTaskbarWidgetsVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue("TaskbarDa", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TaskbarMn property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Chat button is visible on the taskbar.</returns>
        public Boolean IsTaskbarChatVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue("TaskbarMn");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar TaskbarMn property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Chat button is visible on the taskbar.</param>
        public void SetTaskbarChatVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue("TaskbarMn", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the PenWorkspaceButtonDesiredVisibility property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Pen button is visible on the taskbar.</returns>
        public Boolean IsTaskbarPenVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathPenWorkspace, true);
            if (key != null)
            {
                Object value = key.GetValue("PenWorkspaceButtonDesiredVisibility");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar PenWorkspaceButtonDesiredVisibility property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Pen button is visible on the taskbar.</param>
        public void SetTaskbarPenVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathPenWorkspace, true);
            if (key != null)
                key.SetValue("PenWorkspaceButtonDesiredVisibility", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TipbandDesiredVisibility property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Touch Keyboard button is visible on the taskbar.</returns>
        public Boolean IsTaskbarTouchKeyboardVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTabletTip, true);
            if (key != null)
            {
                Object value = key.GetValue("TipbandDesiredVisibility");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar TipbandDesiredVisibility property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Touch Keyboard button is visible on the taskbar.</param>
        public void SetTaskbarTouchKeyboardVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTabletTip, true);
            if (key != null)
                key.SetValue("TipbandDesiredVisibility", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TouchpadDesiredVisibility property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Touchpad button is visible on the taskbar.</returns>
        public Boolean IsTaskbarTouchpadVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTouchpad, true);
            if (key != null)
            {
                Object value = key.GetValue("TouchpadDesiredVisibility");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the TouchpadDesiredVisibility property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Touchpad button is visible on the taskbar.</param>

        public void SetTaskbarTouchpadVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTouchpad, true);
            if (key != null)
                key.SetValue("TouchpadDesiredVisibility", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the Settings property from the registry.
        /// </summary>
        /// <returns>Boolean that specifies whether the taskbar automatically hides based on index 8 of the "Settings" key.</returns>
        public Boolean IsTaskbarHidden()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
            if (key != null)
            {
                Object value = key.GetValue("Settings");
                if (value != null)
                {
                    Byte[] data = (Byte[])value;
                    return data[7 + 1] == 123;
                }
            }
            return false;
        }

        /// <summary>
        /// Write the Settings property to the registry.
        /// </summary>
        /// <param name="taskbarPosition">Boolean that specifies whether the taskbar automatically hides based on index 8 of the "Settings" key.</param>
        public void SetTaskbarHides(Boolean isTaskbarHidden)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
            if (key != null)
            {
                Object value = key.GetValue("Settings");
                if (value != null)
                {
                    Byte[] data = (Byte[])value;
                    data[7 + 1] = (Byte)(isTaskbarHidden ? 123 : 122);
                    key.SetValue("Settings", data, RegistryValueKind.Binary);
                }
            }
        }

        /// <summary>
        /// Read the MMTaskbarEnabled property from the registry
        /// </summary>
        /// <returns>Boolean that specifies whether the taskbar is shown on multiple monitors.</returns>
        public Boolean IsTaskbarOnMultipleMonitors()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue("MMTaskbarEnabled");
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the MMTaskbarEnabled property to the register.
        /// </summary>
        /// <param name="isVisible">Boolean that specifies whether the taskbar is shown on multiple monitors.</param>
        public void SetTaskbarOnMultipleMonitors(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue("MMTaskbarEnabled", isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the monitor visibility settings from the registry.
        /// </summary>
        /// <returns>Boolean that specifies the Multi Monitor Taskbar Behaviour, based on index 12 of each display key.</returns>
        public Boolean IsTaskbarMultiMonitorPositionTaskbar()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerMMStuckRects3, true);

            if (key != null)
            {
                Byte displayCount = 0;
                foreach (String keyName in key.GetValueNames())
                {
                    if (key.GetValueKind(keyName) == RegistryValueKind.Binary)
                    {
                        displayCount++;
                        Object value = key.GetValue(keyName);
                        if (value != null)
                        {
                            Byte[] data = (Byte[])value;
                            if (data[7 + 5] != GetTaskbarPosition()) return false;
                        }
                    }
                }
                if (displayCount < 2) return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Write the monitor visibility settings to the registry.
        /// </summary>
        /// <param name="multiMonitorTaskbarPosition">Boolean that specifies the Multi Monitor Taskbar Behaviour, based on index 12 of each display key.</param>
        public void SetTaskbarMultiMonitorPosition(Boolean multiMonitorTaskbarPosition)
        {
            if (!multiMonitorTaskbarPosition) return;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerMMStuckRects3, true);
            if (key != null)
            {
                foreach (String keyName in key.GetValueNames())
                {
                    if (key.GetValueKind(keyName) == RegistryValueKind.Binary)
                    {
                        Object value = key.GetValue(keyName);
                        if (value != null)
                        {
                            Byte[] data = (Byte[])value;
                            data[7 + 5] = (Byte)GetTaskbarPosition();
                            key.SetValue(keyName, data, RegistryValueKind.Binary);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check whether a specific key is present on the system, this determines whether the old context menu is being used.
        /// </summary>
        /// <returns>Boolean that defines whether the old context menu is used.</returns>
        private Boolean IsUseOldContextMenu()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathInprocServer32, true);
            if (key == null) return false;
            else return (key.GetValue(String.Empty) == String.Empty);
        }

        /// <summary>
        /// Write settings for the context menu to the registry.
        /// </summary>
        /// <param name="useOldContextMenu">Boolean that defines whether the old context menu is used.</param>
        public void SetUseOldContextMenu(Boolean useOldContextMenu)
        {
            RegistrySecurity registrySecurity = new RegistrySecurity();
            registrySecurity.SetAccessRule(new RegistryAccessRule(Environment.UserDomainName + "\\" + Environment.UserName, RegistryRights.FullControl, AccessControlType.Allow));
            
            RegistryKey clsidKey = Registry.CurrentUser.OpenSubKey(PathCLSID, true);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathInprocServer32, true);

            if (clsidKey == null)
            {
                Registry.CurrentUser.CreateSubKey(PathCLSID, RegistryKeyPermissionCheck.Default);
                clsidKey = Registry.CurrentUser.OpenSubKey(PathCLSID, true);
            }
            if (key == null)
            {
                Registry.CurrentUser.CreateSubKey(PathInprocServer32, RegistryKeyPermissionCheck.Default);
                key = Registry.CurrentUser.OpenSubKey(PathInprocServer32, true);
            }
            
            clsidKey.SetAccessControl(registrySecurity);

            if (useOldContextMenu)
                key.SetValue(String.Empty, String.Empty);
            else if (clsidKey != null)
                Registry.CurrentUser.DeleteSubKeyTree(PathInprocServer32);
        }

        /// <summary>
        /// Called to notify UI of selection change to show comboboxes for taskbar position on orientation change.
        /// </summary>
        void XAML_TaskbarOrientationDependantPositionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            XAML_TaskbarOrientationDependantStackPanel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Called to notify UI of selection change to hide comboboxes for taskbar position on orientation change.
        /// </summary>
        void XAML_TaskbarOrientationDependantPositionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            XAML_TaskbarOrientationDependantStackPanel.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Called to notify UI of landscape or portrait mode switch, respectively.
        /// </summary>
        void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            CheckOrientation();
            RestartExplorer();
        }

        /// <summary>
        /// Set the taskbar position, based on the orientation of the device.
        /// </summary>
        void CheckOrientation()
        {
            if (!(Boolean)XAML_TaskbarOrientationDependantPositionCheckBox.IsChecked) return;

            if (SystemParameters.PrimaryScreenWidth > SystemParameters.PrimaryScreenHeight)
            {
                if (XAML_TaskbarOrientationDependantLandscapePositionBox.SelectedIndex == -1) return;
                SetTaskbarPosition(XAML_TaskbarOrientationDependantLandscapePositionBox.SelectedIndex == 0 ? (Byte)3 : (Byte)1);
            }
            else
            {
                if (XAML_TaskbarOrientationDependantPortraitPositionBox.SelectedIndex == -1) return;
                SetTaskbarPosition(XAML_TaskbarOrientationDependantPortraitPositionBox.SelectedIndex == 0 ? (Byte)3 : (Byte)1);
            }
        }


        /// <summary>
        /// Represents the "Save" button. Writes the data to the registry and forces explorer.exe process to restart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SetTaskbarSize((Byte)XAML_TaskbarSizeBox.SelectedIndex);
            SetTaskbarPosition(XAML_TaskbarPositionBox.SelectedIndex == 0 ? (Byte)3 : (Byte)1);
            SetTaskbarAlignment((Byte)XAML_TaskbarIndentationBox.SelectedIndex);
            SetTaskbarSearchVisible(XAML_TaskbarSearchCheckBox.IsChecked.Value);
            SetTaskbarTaskViewVisible(XAML_TaskbarTaskViewBox.IsChecked.Value);
            SetTaskbarWidgetsVisible(XAML_TaskbarWidgetsBox.IsChecked.Value);
            SetTaskbarChatVisible(XAML_TaskbarChatBox.IsChecked.Value);
            SetTaskbarPenVisible(XAML_TaskbarPenCheckBox.IsChecked.Value);
            SetTaskbarTouchKeyboardVisible(XAML_TaskbarTouchCheckBox.IsChecked.Value);
            SetTaskbarTouchpadVisible(XAML_TaskbarVirtualTouchpadCheckBox.IsChecked.Value);
            SetTaskbarHides(XAML_TaskbarBehaviourAutoHideCheckBox.IsChecked.Value);
            SetTaskbarOnMultipleMonitors(XAML_TaskbarBehaviourMultiMonitorOnCheckBox.IsChecked.Value);
            SetTaskbarMultiMonitorPosition(XAML_TaskbarBehaviourMultiMonitorPositionCheckBox.IsChecked.Value);
            SetUseOldContextMenu(XAML_UseOldContextMenuCheckBox.IsChecked.Value);
            CheckOrientation();
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

