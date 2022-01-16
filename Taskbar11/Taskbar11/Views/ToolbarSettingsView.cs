using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taskbar11.Interfaces;
using System.Windows.Controls;
using System.Windows;
using Taskbar11.Windows;

namespace Taskbar11.Views
{
    /// <summary>
    /// This is the settings panel which is used for configurating the toolbar.
    /// </summary>
    public class ToolbarSettingsView : IUpdatableAppView 
    {
        private StackPanel rootStackPanel = new StackPanel();

        private ToolbarProgramWindow toolbarWindow;

        private const String InterfaceToolbarSettings = "Toolbar Settings";
        private const String InterfaceEnableToolbar = "Enable Toolbar";
        private const String InterfaceMoveHint = "The Toolbar can be moved by clicking on the pushpin icon and holding left mouse button, followed by dragging the mouse to the desired position.";

        /// <summary>
        /// Initialize interface elements.
        /// </summary>
        public ToolbarSettingsView()
        {
            Label applicationSettingsLabel = new Label();
            applicationSettingsLabel.Content = InterfaceToolbarSettings;
            applicationSettingsLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(applicationSettingsLabel);

            CheckBox toolbarSettingsBox = new CheckBox();
            toolbarSettingsBox.Margin = new Thickness(5, 5, 5, 5);
            toolbarSettingsBox.Content = InterfaceEnableToolbar;
            rootStackPanel.Children.Add(toolbarSettingsBox);

            TextBlock moveHint = new TextBlock();
            moveHint.Margin = new Thickness(5, 5, 5, 5);
            moveHint.TextWrapping = TextWrapping.Wrap;
            moveHint.FontWeight = FontWeights.Light;
            moveHint.Text = InterfaceMoveHint;
            rootStackPanel.Children.Add(moveHint);

            toolbarSettingsBox.Checked += new RoutedEventHandler(toolbarSettingsBox_Checked);
            toolbarSettingsBox.Unchecked += new RoutedEventHandler(toolbarSettingsBox_Unchecked);
        }

        /// <summary>
        /// Called when the toolbar is disabled by unchecking the checkbox.
        /// </summary>
        void toolbarSettingsBox_Unchecked(object sender, RoutedEventArgs e)
        {
            toolbarWindow.Toolbar.Close();
        }

        /// <summary>
        /// Called when the toolbar is enabled by checking the checkbox.
        /// </summary>
        void toolbarSettingsBox_Checked(object sender, RoutedEventArgs e)
        {
            toolbarWindow = new ToolbarProgramWindow();
        }

        /// <summary>
        /// Function that can update the user interface elements on demand.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the root view for displaying purposes.
        /// </summary>
        public FrameworkElement GetView()
        {
            return rootStackPanel;
        }
    }
}
