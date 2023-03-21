using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using Taskbar11.Interfaces;

namespace Taskbar11.Views
{
    /// <summary>
    /// This is the settings panel which is used for configurating the taskbar.
    /// </summary>
    public class TaskbarSettingsView : IUpdatableAppView
    {
        private StackPanel rootStackPanel = new StackPanel();
        public ComboBox TaskbarPositionBox = new ComboBox();
        public ComboBoxItem TaskbarPositionBoxItemTop = new ComboBoxItem();
        public ComboBoxItem TaskbarPositionBoxItemBottom = new ComboBoxItem();
        public ComboBox TaskbarSizeBox = new ComboBox();
        public ComboBoxItem TaskbarSizeBoxItemSmall = new ComboBoxItem();
        public ComboBoxItem TaskbarSizeBoxItemMedium = new ComboBoxItem();
        public ComboBoxItem TaskbarSizeBoxItemLarge = new ComboBoxItem();
        public ComboBox TaskbarIndentationBox = new ComboBox();
        public ComboBoxItem TaskbarIndentationBoxItemLeft = new ComboBoxItem();
        public ComboBoxItem TaskbarIndentationBoxItemCenter = new ComboBoxItem();
        public CheckBox TaskbarSearchBox = new CheckBox();
        public CheckBox TaskbarTaskBox = new CheckBox();
        public CheckBox TaskbarWidgetsBox = new CheckBox();
        public CheckBox TaskbarChatBox = new CheckBox();
        public CheckBox TaskbarPenCheckBox = new CheckBox();
        public CheckBox TaskbarTouchCheckBox = new CheckBox();
        public CheckBox TaskbarVirtualTouchpadBox = new CheckBox();
        public CheckBox TaskbarBehaviourAutoHideBox = new CheckBox();
        public CheckBox TaskbarBehaviourMultiMonitorOnBox = new CheckBox();
        public CheckBox TaskbarBehaviourMultiMonitorPositionBox = new CheckBox();
        public CheckBox OldContextMenuBox = new CheckBox();
        public CheckBox TaskbarOrientationDependantPositionBox = new CheckBox();
        public StackPanel TaskbarOrientationDependantStackPanel = new StackPanel();
        public ComboBox TaskbarOrientationDependantLandscapePositionBox = new ComboBox();
        public ComboBoxItem TaskbarOrientationDependantLandscapePositionBoxItemBottom = new ComboBoxItem();
        public ComboBoxItem TaskbarOrientationDependantLandscapePositionBoxItemTop = new ComboBoxItem();
        public ComboBox TaskbarOrientationDependantPortraitPositionBox = new ComboBox();
        public ComboBoxItem TaskbarOrientationDependantPortraitPositionBoxItemBottom = new ComboBoxItem();
        public ComboBoxItem TaskbarOrientationDependantPositionBoxItemTop = new ComboBoxItem();
        public Button SaveButton = new Button();

        public readonly String InterfaceTextSaveHint = Properties.Resources.DonTForgetToApplyAndSaveTheSettingsByPressingTheBottomSaveButton;
        public readonly String InterfaceTextTaskbarPosition = Properties.Resources.TaskbarPosition;
        public readonly String InterfaceTextTaskbarTop = Properties.Resources.Top;
        public readonly String InterfaceTextTaskbarBottom = Properties.Resources.Bottom;
        public readonly String InterfaceTaskbarSize = Properties.Resources.TaskbarSize;
        public readonly String InterfaceTaskbarSmall = Properties.Resources.Small;
        public readonly String InterfaceTaskbarMedium = Properties.Resources.Medium;
        public readonly String InterfaceTaskbarLarge = Properties.Resources.Large;
        public readonly String InterfaceTaskbarIndentation = Properties.Resources.TaskbarIndentation;
        public readonly String InterfaceTaskbarIndentationLeft = Properties.Resources.Left;
        public readonly String InterfaceTaskbarIndentationCenter = Properties.Resources.Center;
        public readonly String InterfaceTaskbarItems = Properties.Resources.TaskbarItems;
        public readonly String InterfaceTaskbarSearch = Properties.Resources.Search;
        public readonly String InterfaceTaskbarTaskView = Properties.Resources.TaskView;
        public readonly String InterfaceTaskbarWidgets = Properties.Resources.Widgets;
        public readonly String InterfaceTaskbarChat = Properties.Resources.Chat;
        public readonly String InterfaceTaskbarCornerIcons = Properties.Resources.TaskbarCornerIcons;
        public readonly String InterfaceTaskbarPen = Properties.Resources.Pen;
        public readonly String InterfaceTaskbarTouchKeyboard = Properties.Resources.TouchKeyboard;
        public readonly String InterfaceTaskbarVirtualTouchpad = Properties.Resources.VirtualTouchpad;
        public readonly String InterfaceTaskbarBehaviour = Properties.Resources.TaskbarBehaviour;
        public readonly String InterfaceTaskbarAutomaticallyHideTaskbar = Properties.Resources.AutomaticallyHideTaskbar;
        public readonly String InterfaceMultiMonitorTaskbar = Properties.Resources.MultiMonitorTaskbar;
        public readonly String InterfaceSecondaryTaskbar = Properties.Resources.ShowSecondaryTaskbars;
        public readonly String InterfaceEqualTaskbarPositions = Properties.Resources.EqualTaskbarPositions;
        public readonly String InterfaceContextMenu = Properties.Resources.ContextMenu;
        public readonly String InterfaceUseOldContextMenu = Properties.Resources.UseOldContextMenuForRightMouseButtonClick;
        public readonly String InterfaceTabletOptions = Properties.Resources.TabletOptions;
        public readonly String InterfaceOrientationDependantTaskbar = Properties.Resources.UseOrientationDependantTaskbarPosition;
        public readonly String InterfaceLandscape = Properties.Resources.Landscape;
        public readonly String InterfacePortrait = Properties.Resources.Portrait;
        public readonly String InterfaceSave = Properties.Resources.Save;
        public readonly String InterfaceFloppy = "pack://application:,,,/Resources/FloppyDisk.png";

        /// <summary>
        /// Initialize interface elements.
        /// </summary>
        public TaskbarSettingsView()
        {
            rootStackPanel = new StackPanel();

            Label saveHintLabel = new Label();
            saveHintLabel.Content = InterfaceTextSaveHint;
            saveHintLabel.FontWeight = FontWeights.ExtraLight;
            rootStackPanel.Children.Add(saveHintLabel);

            Label taskbarPositionLabel = new Label();
            taskbarPositionLabel.Content = InterfaceTextTaskbarPosition;
            taskbarPositionLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(taskbarPositionLabel);

            TaskbarPositionBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarPositionBox);

            TaskbarPositionBoxItemTop.Content = InterfaceTextTaskbarTop;
            TaskbarPositionBox.Items.Add(TaskbarPositionBoxItemTop);

            TaskbarPositionBoxItemBottom.Content = InterfaceTextTaskbarBottom;
            TaskbarPositionBox.Items.Add(TaskbarPositionBoxItemBottom);

            rootStackPanel.Children.Add(new Separator());

            Label taskbarSizeLabel = new Label();
            taskbarSizeLabel.Content = InterfaceTaskbarSize;
            taskbarSizeLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(taskbarSizeLabel);

            TaskbarSizeBox.Margin = new Thickness(5,5,5,5);
            rootStackPanel.Children.Add(TaskbarSizeBox);

            TaskbarSizeBoxItemSmall.Content = InterfaceTaskbarSmall;
            TaskbarSizeBox.Items.Add(TaskbarSizeBoxItemSmall);

            TaskbarSizeBoxItemMedium.Content = InterfaceTaskbarMedium;
            TaskbarSizeBox.Items.Add(TaskbarSizeBoxItemMedium);

            TaskbarSizeBoxItemLarge.Content = InterfaceTaskbarLarge;
            TaskbarSizeBox.Items.Add(TaskbarSizeBoxItemLarge);

            rootStackPanel.Children.Add(new Separator());

            Label taskbarIndentationLabel = new Label();
            taskbarIndentationLabel.Content = InterfaceTaskbarIndentation;
            taskbarIndentationLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(taskbarIndentationLabel);

            TaskbarIndentationBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarIndentationBox);

            TaskbarIndentationBoxItemLeft.Content = InterfaceTaskbarIndentationLeft;
            TaskbarIndentationBox.Items.Add(TaskbarIndentationBoxItemLeft);

            TaskbarIndentationBoxItemCenter.Content = InterfaceTaskbarIndentationCenter;
            TaskbarIndentationBox.Items.Add(TaskbarIndentationBoxItemCenter);

            rootStackPanel.Children.Add(new Separator());

            Label taskbarItemsLabel = new Label();
            taskbarItemsLabel.Content = InterfaceTaskbarItems;
            taskbarItemsLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(taskbarItemsLabel);

            TaskbarSearchBox.Content = InterfaceTaskbarSearch;
            TaskbarSearchBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarSearchBox);

            TaskbarTaskBox.Content = InterfaceTaskbarTaskView;
            TaskbarTaskBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarTaskBox);

            TaskbarWidgetsBox.Content = InterfaceTaskbarWidgets;
            TaskbarWidgetsBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarWidgetsBox);

            TaskbarChatBox.Content = InterfaceTaskbarChat;
            TaskbarChatBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarChatBox);

            rootStackPanel.Children.Add(new Separator());

            Label taskbarCornerIconsLabel = new Label();
            taskbarCornerIconsLabel.Content = InterfaceTaskbarCornerIcons;
            taskbarCornerIconsLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(taskbarCornerIconsLabel);

            TaskbarPenCheckBox.Content = InterfaceTaskbarPen;
            TaskbarPenCheckBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarPenCheckBox);

            TaskbarTouchCheckBox.Content = InterfaceTaskbarTouchKeyboard;
            TaskbarTouchCheckBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarTouchCheckBox);

            TaskbarVirtualTouchpadBox.Content = InterfaceTaskbarVirtualTouchpad;
            TaskbarVirtualTouchpadBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarVirtualTouchpadBox);

            rootStackPanel.Children.Add(new Separator());

            Label taskbarBehaviourLabel = new Label();
            taskbarBehaviourLabel.Content = InterfaceTaskbarBehaviour;
            taskbarBehaviourLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(taskbarBehaviourLabel);

            TaskbarBehaviourAutoHideBox.Content = InterfaceTaskbarAutomaticallyHideTaskbar;
            TaskbarBehaviourAutoHideBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarBehaviourAutoHideBox);

            rootStackPanel.Children.Add(new Separator());

            Label multiMonitorTaskbarLabel = new Label();
            multiMonitorTaskbarLabel.Content = InterfaceMultiMonitorTaskbar;
            multiMonitorTaskbarLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(multiMonitorTaskbarLabel);

            TaskbarBehaviourMultiMonitorOnBox.Content = InterfaceSecondaryTaskbar;
            TaskbarBehaviourMultiMonitorOnBox.Margin = new Thickness(5,5,5,5);
            rootStackPanel.Children.Add(TaskbarBehaviourMultiMonitorOnBox);

            TaskbarBehaviourMultiMonitorPositionBox.Content = InterfaceEqualTaskbarPositions;
            TaskbarBehaviourMultiMonitorPositionBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(TaskbarBehaviourMultiMonitorPositionBox);

            rootStackPanel.Children.Add(new Separator());

            Label contextMenuLabel = new Label();
            contextMenuLabel.Content = InterfaceContextMenu;
            contextMenuLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(contextMenuLabel);

            OldContextMenuBox.Content = InterfaceUseOldContextMenu;
            OldContextMenuBox.Margin = new Thickness(5, 5, 5, 5);
            rootStackPanel.Children.Add(OldContextMenuBox);

            rootStackPanel.Children.Add(new Separator());

            Label taskbarTabletOptionsLabel = new Label();
            taskbarTabletOptionsLabel.Content = InterfaceTabletOptions;
            taskbarTabletOptionsLabel.FontWeight = FontWeights.Bold;
            rootStackPanel.Children.Add(taskbarTabletOptionsLabel);

            TaskbarOrientationDependantPositionBox.Content = InterfaceOrientationDependantTaskbar;
            TaskbarOrientationDependantPositionBox.Margin = new Thickness(5,5,5,5);
            rootStackPanel.Children.Add(TaskbarOrientationDependantPositionBox);

            TaskbarOrientationDependantStackPanel.Visibility = Visibility.Collapsed;
            TaskbarOrientationDependantStackPanel.Orientation = Orientation.Vertical;
            rootStackPanel.Children.Add(TaskbarOrientationDependantStackPanel);

            Label landscapeLabel = new Label();
            landscapeLabel.Content = InterfaceLandscape;
            landscapeLabel.FontWeight = FontWeights.SemiBold;
            landscapeLabel.Margin = new Thickness(15, 0, 5, 0);
            TaskbarOrientationDependantStackPanel.Children.Add(landscapeLabel);

            TaskbarOrientationDependantLandscapePositionBox.FontWeight = FontWeights.SemiBold;
            TaskbarOrientationDependantLandscapePositionBox.Margin = new Thickness(20, 0, 5, 0);
            TaskbarOrientationDependantStackPanel.Children.Add(TaskbarOrientationDependantLandscapePositionBox);

            TaskbarOrientationDependantLandscapePositionBoxItemTop.Content = InterfaceTextTaskbarTop;
            TaskbarOrientationDependantLandscapePositionBox.Items.Add(TaskbarOrientationDependantLandscapePositionBoxItemTop);

            TaskbarOrientationDependantLandscapePositionBoxItemBottom.Content = InterfaceTextTaskbarBottom;
            TaskbarOrientationDependantLandscapePositionBox.Items.Add(TaskbarOrientationDependantLandscapePositionBoxItemBottom);

            Label portraitLabel = new Label();
            portraitLabel.Content = InterfacePortrait;
            portraitLabel.FontWeight = FontWeights.SemiBold;
            portraitLabel.Margin = new Thickness(15, 0, 5, 0);
            TaskbarOrientationDependantStackPanel.Children.Add(portraitLabel);

            TaskbarOrientationDependantPortraitPositionBox.FontWeight = FontWeights.SemiBold;
            TaskbarOrientationDependantPortraitPositionBox.Margin = new Thickness(20, 0, 5, 0);
            TaskbarOrientationDependantStackPanel.Children.Add(TaskbarOrientationDependantPortraitPositionBox);

            TaskbarOrientationDependantPositionBoxItemTop.Content = InterfaceTextTaskbarTop;
            TaskbarOrientationDependantPortraitPositionBox.Items.Add(TaskbarOrientationDependantPositionBoxItemTop);

            TaskbarOrientationDependantPortraitPositionBoxItemBottom.Content = InterfaceTextTaskbarBottom;
            TaskbarOrientationDependantPortraitPositionBox.Items.Add(TaskbarOrientationDependantPortraitPositionBoxItemBottom);

            rootStackPanel.Children.Add(new Separator());

            SaveButton.Margin = new Thickness(5, 5, 5, 5);
            SaveButton.Width = 100;
            rootStackPanel.Children.Add(SaveButton);

            StackPanel sourcePanel = new StackPanel();
            sourcePanel.Orientation = Orientation.Horizontal;
            SaveButton.Content = sourcePanel;

            Image saveImage = new Image();
            saveImage.Source = new BitmapImage(new Uri(InterfaceFloppy));
            saveImage.Width = 30;
            sourcePanel.Children.Add(saveImage);

            TextBlock saveText = new TextBlock();
            saveText.Text = InterfaceSave;
            saveText.FontWeight = FontWeights.Bold;
            saveText.VerticalAlignment = VerticalAlignment.Center;
            sourcePanel.Children.Add(saveText);
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
