using System;
using Taskbar11.Interfaces;
using System.Windows.Controls;
using Taskbar11.Models;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Taskbar11.Views
{
    /// <summary>
    /// This is the menu panel which is used for selecting the options.
    /// </summary>
    public class Taskbar11MenuView : IUpdatableAppView
    {
        private IMenuUpdateNotifier menuUpdateNotifier;
        private DockPanel rootDockPanel = new DockPanel();
        private StackPanel menuStackPanel = new StackPanel();
        private StackPanel infoStackPanel = new StackPanel();
        private Button taskbar11SettingsButton = new Button();
        private Button toolbarSettingsButton = new Button();

        private const String InterfaceLogo = "pack://application:,,,/Resources/Taskbar11Logo.png";
        private const String InterfaceTaskbar = "Taskbar";
        private const String InterfaceToolbar = "Toolbar";
        private const String InterfaceWebsite = "https://github.com/jetspiking/Taskbar11";

        /// <summary>
        /// Initialize interface elements.
        /// </summary>
        public Taskbar11MenuView(IMenuUpdateNotifier menuUpdateNotifier)
        {
            this.menuUpdateNotifier = menuUpdateNotifier;

            rootDockPanel.Children.Add(menuStackPanel);
            rootDockPanel.Children.Add(infoStackPanel);
            DockPanel.SetDock(menuStackPanel, Dock.Top);
            DockPanel.SetDock(infoStackPanel, Dock.Bottom);

            menuStackPanel.Margin = new System.Windows.Thickness(0, 5, 0, 5);
            menuStackPanel.Orientation = Orientation.Vertical;
            menuStackPanel.HorizontalAlignment = HorizontalAlignment.Center;

            taskbar11SettingsButton.Content = InterfaceTaskbar;
            taskbar11SettingsButton.Click += new System.Windows.RoutedEventHandler(taskbar11SettingsButton_Click);
            taskbar11SettingsButton.FontSize = 20;
            taskbar11SettingsButton.Background = new SolidColorBrush(Colors.Transparent);
            taskbar11SettingsButton.Margin = new Thickness(0, 5, 0, 5);
            taskbar11SettingsButton.FontWeight = FontWeights.Bold;
            taskbar11SettingsButton.BorderThickness = new Thickness(0, 0, 0, 2);
            taskbar11SettingsButton.BorderBrush = new SolidColorBrush(Colors.Orange);
            menuStackPanel.Children.Add(taskbar11SettingsButton);

            toolbarSettingsButton.Content = InterfaceToolbar;
            toolbarSettingsButton.Click += new System.Windows.RoutedEventHandler(toolbarSettingsButton_Click);
            toolbarSettingsButton.FontSize = 20;
            toolbarSettingsButton.Background = new SolidColorBrush(Colors.Transparent);
            toolbarSettingsButton.Margin = new Thickness(0, 5, 0, 7);
            toolbarSettingsButton.FontWeight = FontWeights.Bold;
            toolbarSettingsButton.BorderThickness = new Thickness(0, 0, 0, 0);
            toolbarSettingsButton.BorderBrush = new SolidColorBrush(Colors.Orange);
            menuStackPanel.Children.Add(toolbarSettingsButton);

            infoStackPanel.VerticalAlignment = VerticalAlignment.Bottom;
            infoStackPanel.Margin = new Thickness(0, 0, 0, 40);

            Image logoImage = new Image();
            logoImage.Source = new BitmapImage(new Uri(InterfaceLogo));
            logoImage.Width = 90;
            logoImage.Height = 90;
            logoImage.Margin = new Thickness(5, 5, 5, 5);
            logoImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(logoImage_MouseDown);

            infoStackPanel.Children.Add(logoImage);
        }

        /// <summary>
        /// Called when a click on the logo image was detected.
        /// </summary>
        void logoImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(InterfaceWebsite);
        }

        /// <summary>
        /// Called when a click on the taskbar settings button was detected.
        /// </summary>
        void taskbar11SettingsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            taskbar11SettingsButton.Margin = new Thickness(0, 5, 0, 5);
            toolbarSettingsButton.Margin = new Thickness(0, 5, 0, 7);
            taskbar11SettingsButton.BorderThickness = new Thickness(0, 0, 0, 2);
            toolbarSettingsButton.BorderThickness = new Thickness(0, 0, 0, 0);
            menuUpdateNotifier.UpdateMenu(MenuWindows.Taskbar11Settings);
        }

        /// <summary>
        /// Called when a click on the toolbar settings button was detected.
        /// </summary>
        void toolbarSettingsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            toolbarSettingsButton.Margin = new Thickness(0, 5, 0, 5);
            taskbar11SettingsButton.Margin = new Thickness(0, 5, 0, 7);
            toolbarSettingsButton.BorderThickness = new Thickness(0, 0, 0, 2);
            taskbar11SettingsButton.BorderThickness = new Thickness(0, 0, 0, 0);
            menuUpdateNotifier.UpdateMenu(MenuWindows.ToolbarSettings);
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
            return rootDockPanel;
        }
    }
}
