using System;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using Taskbar11.Controllers;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.WindowsAPICodePack.Shell;
using System.Diagnostics;

namespace Taskbar11.Windows
{
    /// <summary>
    /// Proof of concept implementation for a toolbar.
    /// </summary>
    public class ToolbarProgramWindow
    {
        public ToolbarWindow Toolbar;
        private Image pinImage = new Image();

        private bool dragging = false;
        private double oldMouseX;
        private double oldMouseY;

        private DateTime currentDateTime;

        private Label timeLabel;
        private StackPanel topPanel;
        private TextBox searchBox;

        private const String DockTitle = "Taskbar11 Dock";
        private const String InterfaceJournal = "pack://application:,,,/Resources/Journal.png";
        private const String InterfacePushPin = "pack://application:,,,/Resources/PushPin.png";
        private const String InterfaceSearch = "Search";
        private const String ExplorerProcess = "explorer.exe";
        private const String ShellPath = @"shell:appsFolder\";
        private const String AppsGuid = "{1e87508d-89c2-42f0-8a7e-645a0f50ca58}";
        private const int DefaultHeight = 165;

        private bool openedApps = false;

        private Image[] images;
        private Label[] appLabels;
        private ShellObject[] apps;
        private StackPanel[] appInfoPanels;

        /// <summary>
        /// Creates a new window and initializes interface elements.
        /// </summary>
        public ToolbarProgramWindow()
        {
            Toolbar = new ToolbarWindow();

            Toolbar.Title = DockTitle;
            Toolbar.Icon = new BitmapImage(new Uri(InterfaceJournal));
            Toolbar.ResizeMode = ResizeMode.NoResize;
            Toolbar.WindowStyle = WindowStyle.None;
            Toolbar.Topmost = true;
            Toolbar.Background = new SolidColorBrush(Color.FromArgb(220, 0, 0, 0));
            Toolbar.AllowsTransparency = true;

            Toolbar.Width = ApplicationSettings.ToolbarWidth;
            Toolbar.Height = DefaultHeight;
            Toolbar.Left = SystemParameters.WorkArea.Left;
            Toolbar.Top = SystemParameters.WorkArea.Top;
            Toolbar.Show();

            DockPanel toolbarDock = new DockPanel();
            Toolbar.Content = toolbarDock;

            topPanel = new StackPanel();

            toolbarDock.Children.Add(topPanel);
            DockPanel.SetDock(topPanel, Dock.Top);

            Image appsImage = new Image();
            appsImage.Source = new BitmapImage(new Uri(InterfaceJournal));
            appsImage.Width = ApplicationSettings.ToolbarWidth / 1.8;
            appsImage.Margin = new Thickness(5, 15, 5, 5);
            topPanel.Children.Add(appsImage);
            appsImage.MouseDown += new MouseButtonEventHandler(appsImage_MouseDown);

            currentDateTime = DateTime.Now;
            timeLabel = new Label();
            timeLabel.Content = currentDateTime.ToShortTimeString();
            timeLabel.Foreground = new SolidColorBrush(Colors.White);
            timeLabel.FontSize = 18;
            timeLabel.HorizontalAlignment = HorizontalAlignment.Center;
            timeLabel.VerticalAlignment = VerticalAlignment.Center;
            timeLabel.LayoutTransform = new RotateTransform(90);
            topPanel.Children.Add(timeLabel);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            pinImage.Source = new BitmapImage(new Uri(InterfacePushPin));
            pinImage.Width = ApplicationSettings.ToolbarWidth / 1.5;
            pinImage.Margin = new Thickness(5, 5, 5, 0);
            topPanel.Children.Add(pinImage);

            pinImage.MouseUp += new MouseButtonEventHandler(pinImage_MouseUp);
            pinImage.MouseDown += new MouseButtonEventHandler(pinImage_MouseDown);
            pinImage.MouseMove += new MouseEventHandler(pinImage_MouseMove);

            searchBox = new TextBox();
            searchBox.Background = new SolidColorBrush(Colors.Transparent);
            searchBox.Foreground = new SolidColorBrush(Colors.Gray);
            searchBox.Text = InterfaceSearch;
            topPanel.Children.Add(searchBox);
        }

        /// <summary>
        /// Display all installed apps found on the system and bind events for searching through the application list and launching an application after a left mouse button click.
        /// </summary>
        void appsImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!openedApps)
            {
                Guid appsFolderGuid = new Guid(AppsGuid);
                ShellObject appsFolder = (ShellObject)KnownFolderHelper.FromKnownFolderId(appsFolderGuid);

                ScrollViewer appScroller = new ScrollViewer();
                topPanel.Children.Add(appScroller);

                StackPanel appPanel = new StackPanel();
                appScroller.Content = appPanel;
                appScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                Toolbar.Height = SystemParameters.WorkArea.Height;
                appScroller.Height = SystemParameters.WorkArea.Height;
                Toolbar.Top = SystemParameters.WorkArea.Top;

                apps = new ShellObject[((IKnownFolder)appsFolder).Count()];
                appLabels = new Label[apps.Count()];
                images = new Image[apps.Count()];
                appInfoPanels = new StackPanel[apps.Count()];

                int index = 0;

                foreach (ShellObject app in (IKnownFolder)appsFolder)
                {
                    apps[index] = app;

                    string appName = app.Name;
                    string appUserModelID = app.ParsingName;

                    StackPanel appInfoPanel = new StackPanel();
                    appInfoPanel.Orientation = Orientation.Vertical;
                    appInfoPanels[index] = appInfoPanel;
                    appPanel.Children.Add(appInfoPanel);

                    ImageSource icon = app.Thumbnail.SmallBitmapSource;
                    Image image = new Image();
                    image.Source = icon;
                    image.Margin = new Thickness(1, 1, 1, 1);
                    image.Width = 32;
                    image.Height = 32;
                    images[index] = image;
                    appInfoPanel.Children.Add(image);

                    Label appNameLabel = new Label();
                    appNameLabel.Content = appName;
                    appNameLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    appNameLabel.Foreground = new SolidColorBrush(Colors.White);
                    appLabels[index] = appNameLabel;
                    appInfoPanel.Children.Add(appNameLabel);

                    int localIndex = index++;
                    appInfoPanel.MouseEnter += (eventSender, eventE) => { ((StackPanel)eventSender).Background = new SolidColorBrush(Colors.Gray); };
                    appInfoPanel.MouseLeave += (eventSender, eventE) => { ((StackPanel)eventSender).Background = new SolidColorBrush(Colors.Transparent); };
                    appInfoPanel.MouseDown += (eventSender, eventE) => { Process.Start(ExplorerProcess, ShellPath + apps[localIndex].ParsingName); };
                }

                searchBox.GotKeyboardFocus += (searchBoxObject, keyboardFocusChangedEvent) =>
                {
                    ((TextBox)searchBoxObject).Text = String.Empty;
                    ((TextBox)searchBoxObject).Foreground = new SolidColorBrush(Colors.White);
                };

                searchBox.LostKeyboardFocus += (searchBoxObject, keyboardFocusChangedEvent) =>
                {

                    ((TextBox)searchBoxObject).Text = InterfaceSearch;
                    ((TextBox)searchBoxObject).Foreground = new SolidColorBrush(Colors.Gray);
                };

                searchBox.MouseLeave+=(searchBoxObject, keyboardFocusChangedEvent) =>
                {
                    FocusManager.SetFocusedElement(topPanel, appScroller);
                };

                searchBox.TextChanged += (searchBoxObject, keyboardFocusChangedEvent) =>
                {
                    if (apps.Count() > 0 && ((TextBox)searchBoxObject).Text!=InterfaceSearch)
                        for (int appIndex = 0; appIndex < apps.Count(); appIndex++)
                        {
                            images[appIndex].Visibility = Visibility.Visible;
                            appLabels[appIndex].Visibility = Visibility.Visible;
                            if (!apps[appIndex].Name.Trim().ToLower().Contains(((TextBox)searchBoxObject).Text.Trim().ToLower()))
                            {
                                images[appIndex].Visibility = Visibility.Collapsed;
                                appLabels[appIndex].Visibility = Visibility.Collapsed;
                            }
                        }
                };
            }
            else
            {
                topPanel.Children.RemoveAt(topPanel.Children.Count - 1);
                Toolbar.Height = DefaultHeight;
            }
            openedApps = !openedApps;
        }

        /// <summary>
        /// When PinImage is released the dragging function should be disabled.
        /// </summary>
        void pinImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
        }

        /// <summary>
        /// When PinImage is selected the dragging function should be enabled.
        /// The mouse position is updated as well.
        /// </summary>
        void pinImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragging = true;
            oldMouseX = e.GetPosition(null).X;
            oldMouseY = e.GetPosition(null).Y;
        }

        /// <summary>
        /// Enable moving the window from PinImage.
        /// </summary>
        void pinImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Toolbar.Left += e.GetPosition(null).X - oldMouseX;
                if (!openedApps)
                    Toolbar.Top += e.GetPosition(null).Y - oldMouseY;
            }
        }

        /// <summary>
        /// Timer to update the displayed time in the toolbar.
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            currentDateTime = DateTime.Now;
            timeLabel.Content = currentDateTime.ToShortTimeString();
        }
    }
}
