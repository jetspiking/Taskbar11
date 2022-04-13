using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Taskbar11.Controllers;
using Taskbar11.Views;
using Taskbar11.Interfaces;

namespace Taskbar11
{
    public partial class MainWindow : Window, IMenuUpdateNotifier
    {
        private const String AppTitle = "Taskbar11";
        private TaskbarSettingsView taskbarSettingsView = new TaskbarSettingsView();
        private Taskbar11MenuView menuView;
        private ToolbarSettingsView toolbarSettingsView = new ToolbarSettingsView();

        /// <summary>
        /// Initialize the application.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            this.Title = AppTitle;
            this.ResizeMode = ResizeMode.NoResize;
            this.Height = ApplicationSettings.ApplicationHeight;
            this.Width = ApplicationSettings.ApplicationWidth;
            AppDockPanel.Width = ApplicationSettings.ApplicationWidth;
            AppScrollViewer.Width = ApplicationSettings.ApplicationScrollerWidth;

            SetContent();

            VerifyTaskbarSettings();
            taskbarSettingsView.SaveButton.Click += new RoutedEventHandler(Save_Click);
        }

        private void SetContent()
        {
            menuView = new Taskbar11MenuView(this);
            DockPanel.SetDock(menuView.GetView(), Dock.Left);
            AppDockPanel.Children.Add(menuView.GetView());

            AppScrollViewer.Content = taskbarSettingsView.GetView();
        }

        /// <summary>
        /// Verifies which settings are currently enabled or disabled in the register.
        /// </summary>
        private void VerifyTaskbarSettings()
        {
            int taskbarSize = TaskbarSettingsController.GetTaskbarSize();

            if (taskbarSize > -1 && taskbarSize < 3)
                taskbarSettingsView.TaskbarSizeBox.SelectedIndex = taskbarSize;
            else
                taskbarSettingsView.TaskbarSizeBox.SelectedItem = taskbarSize.ToString();
            int taskbarPosition = TaskbarSettingsController.GetTaskbarPosition();
            taskbarSettingsView.TaskbarPositionBox.SelectedIndex = taskbarPosition == 1 ? 0 : 1;
            int taskbarAlignment = TaskbarSettingsController.GetTaskbarAlignment();

            taskbarSettingsView.TaskbarIndentationBox.SelectedIndex = taskbarAlignment < taskbarSettingsView.TaskbarIndentationBox.Items.Count ? taskbarAlignment : 0;
            taskbarSettingsView.TaskbarSearchBox.IsChecked = TaskbarSettingsController.IsTaskbarSearchVisible();
            taskbarSettingsView.TaskbarTaskBox.IsChecked = TaskbarSettingsController.IsTaskbarTaskViewVisible();
            taskbarSettingsView.TaskbarWidgetsBox.IsChecked = TaskbarSettingsController.IsTaskbarWidgetsVisible();
            taskbarSettingsView.TaskbarChatBox.IsChecked = TaskbarSettingsController.IsTaskbarChatVisible();
            taskbarSettingsView.TaskbarPenCheckBox.IsChecked = TaskbarSettingsController.IsTaskbarPenVisible();
            taskbarSettingsView.TaskbarTouchCheckBox.IsChecked = TaskbarSettingsController.IsTaskbarTouchKeyboardVisible();
            taskbarSettingsView.TaskbarVirtualTouchpadBox.IsChecked = TaskbarSettingsController.IsTaskbarTouchpadVisible();
            taskbarSettingsView.TaskbarBehaviourAutoHideBox.IsChecked = TaskbarSettingsController.IsTaskbarHidden();
            taskbarSettingsView.TaskbarBehaviourMultiMonitorOnBox.IsChecked = TaskbarSettingsController.IsTaskbarOnMultipleMonitors();
            taskbarSettingsView.TaskbarBehaviourMultiMonitorPositionBox.IsChecked = TaskbarSettingsController.IsTaskbarMultiMonitorPositionTaskbar();
            taskbarSettingsView.OldContextMenuBox.IsChecked = TaskbarSettingsController.IsUseOldContextMenu();

            taskbarSettingsView.TaskbarOrientationDependantPositionBox.Checked += new RoutedEventHandler(XAML_TaskbarOrientationDependantPositionCheckBox_Checked);
            taskbarSettingsView.TaskbarOrientationDependantPositionBox.Unchecked += new RoutedEventHandler(XAML_TaskbarOrientationDependantPositionCheckBox_Unchecked);
            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
        }

        /// <summary>
        /// Called to notify UI of selection change to show comboboxes for taskbar position on orientation change.
        /// </summary>
        private void XAML_TaskbarOrientationDependantPositionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            taskbarSettingsView.TaskbarOrientationDependantStackPanel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Called to notify UI of selection change to hide comboboxes for taskbar position on orientation change.
        /// </summary>
        private void XAML_TaskbarOrientationDependantPositionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            taskbarSettingsView.TaskbarOrientationDependantStackPanel.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Called to notify UI of landscape or portrait mode switch, respectively.
        /// </summary>
        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            CheckOrientation();
            ApplicationUtilities.RestartExplorer();
        }

        /// <summary>
        /// Set the taskbar position, based on the orientation of the device.
        /// </summary>
        private void CheckOrientation()
        {
            if (!(Boolean)taskbarSettingsView.TaskbarOrientationDependantPositionBox.IsChecked) return;

            if (SystemParameters.PrimaryScreenWidth > SystemParameters.PrimaryScreenHeight)
            {
                if (taskbarSettingsView.TaskbarOrientationDependantLandscapePositionBox.SelectedIndex == -1) return;
                TaskbarSettingsController.SetTaskbarPosition(taskbarSettingsView.TaskbarOrientationDependantLandscapePositionBox.SelectedIndex == 0 ? (Byte)1 : (Byte)3);
            }
            else
            {
                if (taskbarSettingsView.TaskbarOrientationDependantPortraitPositionBox.SelectedIndex == -1) return;
                TaskbarSettingsController.SetTaskbarPosition(taskbarSettingsView.TaskbarOrientationDependantPortraitPositionBox.SelectedIndex == 0 ? (Byte)1 : (Byte)3);
            }
        }
        
        /// <summary>
        /// Represents the "Save" button. Writes the data to the registry and forces explorer.exe process to restart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TaskbarSettingsController.SetTaskbarSize((Byte)taskbarSettingsView.TaskbarSizeBox.SelectedIndex);
            TaskbarSettingsController.SetTaskbarPosition(taskbarSettingsView.TaskbarPositionBox.SelectedIndex == 0 ? (Byte)1 : (Byte)3);
            TaskbarSettingsController.SetTaskbarAlignment((Byte)taskbarSettingsView.TaskbarIndentationBox.SelectedIndex);
            TaskbarSettingsController.SetTaskbarSearchVisible(taskbarSettingsView.TaskbarSearchBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarTaskViewVisible(taskbarSettingsView.TaskbarTaskBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarWidgetsVisible(taskbarSettingsView.TaskbarWidgetsBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarChatVisible(taskbarSettingsView.TaskbarChatBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarPenVisible(taskbarSettingsView.TaskbarPenCheckBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarTouchKeyboardVisible(taskbarSettingsView.TaskbarTouchCheckBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarTouchpadVisible(taskbarSettingsView.TaskbarVirtualTouchpadBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarHides(taskbarSettingsView.TaskbarBehaviourAutoHideBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarOnMultipleMonitors(taskbarSettingsView.TaskbarBehaviourMultiMonitorOnBox.IsChecked.Value);
            TaskbarSettingsController.SetTaskbarMultiMonitorPosition(taskbarSettingsView.TaskbarBehaviourMultiMonitorPositionBox.IsChecked.Value);
            TaskbarSettingsController.SetUseOldContextMenu(taskbarSettingsView.OldContextMenuBox.IsChecked.Value);
            CheckOrientation();
            ApplicationUtilities.RestartExplorer();
        }

        /// <summary>
        /// Called when the menu should be updated.
        /// </summary>
        /// <param name="menuWindow">Enum which describes which menu should be shown.</param>
        public void UpdateMenu(Models.MenuWindows menuWindow)
        {
            switch (menuWindow)
            {
                case Models.MenuWindows.Taskbar11Settings:
                    AppScrollViewer.Content = taskbarSettingsView.GetView();
                    break;
                case Models.MenuWindows.ToolbarSettings:
                    AppScrollViewer.Content = toolbarSettingsView.GetView();
                    break;
            }
        }

    }
}

