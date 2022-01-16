using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security.AccessControl;

namespace Taskbar11.Controllers
{
    /// <summary>
    /// Bind actions to the UI-views for TaskbarSettingsView.
    /// </summary>
    public static class TaskbarSettingsController
    {
        private const String PathExplorerStuckRects3 = @"Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3";
        private const String PathExplorerAdvanced = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        private const String PathSearch = @"Software\Microsoft\Windows\CurrentVersion\Search";
        private const String PathPenWorkspace = @"Software\Microsoft\Windows\CurrentVersion\PenWorkspace";
        private const String PathTabletTip = @"Software\Microsoft\TabletTip\1.7";
        private const String PathTouchpad = @"Software\Microsoft\Touchpad";
        private const String PathExplorerMMStuckRects3 = @"Software\Microsoft\Windows\CurrentVersion\Explorer\MMStuckRects3";
        private const String PathInprocServer32 = @"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32";
        private const String PathCLSID = @"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}";

        private const String ValueKeySettings = "Settings";
        private const String ValueKeyTaskbarSize = "TaskbarSi";
        private const String ValueKeyTaskbarAlignment = "TaskbarAl";
        private const String ValueKeySearchboxTaskbarMode = "SearchboxTaskbarMode";
        private const String ValueKeyShowTaskViewButton = "ShowTaskViewButton";
        private const String ValueKeyTaskbarWidgets = "TaskbarDa";
        private const String ValueKeyTaskbarChat = "TaskbarMn";
        private const String ValueKeyPenWorkspace = "PenWorkspaceButtonDesiredVisibility";
        private const String ValueKeyTouchKeyboard = "TipbandDesiredVisibility";
        private const String ValueKeyTouchpad = "TouchpadDesiredVisibility";
        private const String ValueKeyMultiMonitorTaskbar = "MMTaskbarEnabled";

        /// <summary>
        /// Read the taskbar position from the registry.
        /// </summary>
        /// <returns>Integer that specifies the taskbar position on index 12 of the "Settings" key.</returns>
        public static int GetTaskbarPosition()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeySettings);
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
        public static void SetTaskbarPosition(Byte taskbarPosition)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeySettings);
                if (value != null)
                {
                    Byte[] data = (Byte[])value;
                    data[7 + 5] = taskbarPosition;
                    key.SetValue(ValueKeySettings, data, RegistryValueKind.Binary);
                }
            }
        }

        /// <summary>
        /// Read the taskbar size from the registry.
        /// </summary>
        /// <returns>Integer that specifies the taskbar size.</returns>
        public static int GetTaskbarSize()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyTaskbarSize);
                if (value != null)
                    return (int)value;
            }
            return -1;
        }

        /// <summary>
        /// Write the taskbar size to the registry.
        /// </summary>
        /// <param name="taskbarSize">Byte that specifies the taskbar size.</param>
        public static void SetTaskbarSize(Byte taskbarSize)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue(ValueKeyTaskbarSize, taskbarSize, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the taskbar aligment from the registry.
        /// </summary>
        /// <returns>Integer that specifies the taskbar aligment.</returns>
        public static int GetTaskbarAlignment()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyTaskbarAlignment);
                if (value != null)
                    return (int)value;
            }
            return -1;
        }

        /// <summary>
        /// Write the taskbar alignment to the registry.
        /// </summary>
        /// <param name="taskbarAlignment">Byte that specifies the taskbar alignment.</param>
        public static void SetTaskbarAlignment(Byte taskbarAlignment)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue(ValueKeyTaskbarAlignment, taskbarAlignment, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the SearchboxTaskbarMode property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Search button is visible on the taskbar.</returns>
        public static Boolean IsTaskbarSearchVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathSearch, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeySearchboxTaskbarMode);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar SearchboxTaskbarMode property to the registry. 
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Search button is visible on the taskbar.</param>
        public static void SetTaskbarSearchVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathSearch, true);
            if (key != null)
                key.SetValue(ValueKeySearchboxTaskbarMode, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the ShowTaskViewButton property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the TaskView button is visible on the taskbar.</returns>
        public static Boolean IsTaskbarTaskViewVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyShowTaskViewButton);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar ShowTaskViewButton property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the TaskView button is visible on the taskbar.</param>
        public static void SetTaskbarTaskViewVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue(ValueKeyShowTaskViewButton, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TaskbarDa property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Widgets button is visible on the taskbar.</returns>
        public static Boolean IsTaskbarWidgetsVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyTaskbarWidgets);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar TaskbarDa property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Widgets button is visible on the taskbar.</param>
        public static void SetTaskbarWidgetsVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue(ValueKeyTaskbarWidgets, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TaskbarMn property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Chat button is visible on the taskbar.</returns>
        public static Boolean IsTaskbarChatVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyTaskbarChat);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar TaskbarMn property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Chat button is visible on the taskbar.</param>
        public static void SetTaskbarChatVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue(ValueKeyTaskbarChat, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the PenWorkspaceButtonDesiredVisibility property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Pen button is visible on the taskbar.</returns>
        public static Boolean IsTaskbarPenVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathPenWorkspace, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyPenWorkspace);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar PenWorkspaceButtonDesiredVisibility property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Pen button is visible on the taskbar.</param>
        public static void SetTaskbarPenVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathPenWorkspace, true);
            if (key != null)
                key.SetValue(ValueKeyPenWorkspace, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TipbandDesiredVisibility property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Touch Keyboard button is visible on the taskbar.</returns>
        public static Boolean IsTaskbarTouchKeyboardVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTabletTip, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyTouchKeyboard);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the taskbar TipbandDesiredVisibility property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Touch Keyboard button is visible on the taskbar.</param>
        public static void SetTaskbarTouchKeyboardVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTabletTip, true);
            if (key != null)
                key.SetValue(ValueKeyTouchKeyboard, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the TouchpadDesiredVisibility property from the registry.
        /// </summary>
        /// <returns>Boolean that defines whether the Touchpad button is visible on the taskbar.</returns>
        public static Boolean IsTaskbarTouchpadVisible()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTouchpad, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyTouchpad);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the TouchpadDesiredVisibility property to the registry.
        /// </summary>
        /// <param name="isVisible">Boolean that defines whether the Touchpad button is visible on the taskbar.</param>

        public static void SetTaskbarTouchpadVisible(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathTouchpad, true);
            if (key != null)
                key.SetValue(ValueKeyTouchpad, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the Settings property from the registry.
        /// </summary>
        /// <returns>Boolean that specifies whether the taskbar automatically hides based on index 8 of the "Settings" key.</returns>
        public static Boolean IsTaskbarHidden()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeySettings);
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
        public static void SetTaskbarHides(Boolean isTaskbarHidden)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerStuckRects3, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeySettings);
                if (value != null)
                {
                    Byte[] data = (Byte[])value;
                    data[7 + 1] = (Byte)(isTaskbarHidden ? 123 : 122);
                    key.SetValue(ValueKeySettings, data, RegistryValueKind.Binary);
                }
            }
        }

        /// <summary>
        /// Read the MMTaskbarEnabled property from the registry
        /// </summary>
        /// <returns>Boolean that specifies whether the taskbar is shown on multiple monitors.</returns>
        public static Boolean IsTaskbarOnMultipleMonitors()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
            {
                Object value = key.GetValue(ValueKeyMultiMonitorTaskbar);
                if (value != null)
                    return ((int)value) == 1;
            }
            return false;
        }

        /// <summary>
        /// Write the MMTaskbarEnabled property to the register.
        /// </summary>
        /// <param name="isVisible">Boolean that specifies whether the taskbar is shown on multiple monitors.</param>
        public static void SetTaskbarOnMultipleMonitors(Boolean isVisible)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathExplorerAdvanced, true);
            if (key != null)
                key.SetValue(ValueKeyMultiMonitorTaskbar, isVisible ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Read the monitor visibility settings from the registry.
        /// </summary>
        /// <returns>Boolean that specifies the Multi Monitor Taskbar Behaviour, based on index 12 of each display key.</returns>
        public static Boolean IsTaskbarMultiMonitorPositionTaskbar()
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
        public static void SetTaskbarMultiMonitorPosition(Boolean multiMonitorTaskbarPosition)
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
        public static Boolean IsUseOldContextMenu()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(PathInprocServer32, true);
            if (key == null) return false;
            else return (key.GetValue(String.Empty) == String.Empty);
        }

        /// <summary>
        /// Write settings for the context menu to the registry.
        /// </summary>
        /// <param name="useOldContextMenu">Boolean that defines whether the old context menu is used.</param>
        public static void SetUseOldContextMenu(Boolean useOldContextMenu)
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
    }
}
