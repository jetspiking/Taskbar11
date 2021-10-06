# Taskbar11
<img src="https://github.com/jetspiking/Taskbar11/blob/main/Images/Taskbar11_LargeLeft.png">
Change the position and size of the Windows taskbar in Windows 11

# Description
Taskbar11 customizes the look of the Windows 11 taskbar, which doesn't have any options by default. The taskbar can be set to the top or bottom (default) of the screen, it also allows for setting various icon sizes.

# Usage
Download and launch the executable "Taskbar11.exe". The program is portable.

[Releases](https://github.com/jetspiking/Taskbar11/releases)

# Requirements
- Windows 11

Possibly works on other version of Windows. However, since Windows 11 is the first version with locked settings there really is no purpose to install it on earlier versions of Windows.

# Information
Taskbar11 edits registry values in the following paths for the taskbar position and behaviour, icon sizes and visible buttons:
- Computer\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3
- Computer\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced
- Computer\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\PenWorkspace
- Computer\HKEY_CURRENT_USER\Software\Microsoft\TabletTip\1.7
- Computer\HKEY_CURRENT_USER\Software\Microsoft\Touchpad

- In the "StuckRects3" path the key "TaskbarSi" is modified. Values 0, 1, 2 represent the taskbar icon size (respectively small, medium and large).
- - In the "Advanced" path the key "Settings" is modified. Index 8 is modified (second row, first column). Possible values are 122 and 123, which define whether the taskbar automatically hides (respectively).
- In the "Advanced" path the key "Settings" is modified. Index 12 is modified (second row, fifth column). Possible values are 0, 1, 2 and 3. However, only the values 1 and 3 are usable without a broken view in Windows 11. Because of this, the values for left and right are not included in the Combobox in the application. The value that can be edited is highlighted in the image below.
 
<img src="https://raw.githubusercontent.com/jetspiking/Taskbar11/main/Images/Taskbar11_RegistryPositionValue.png" width="500">

After editing the registry values, the application kills the explorer.exe process. This forces a reload of the UI which displays the new changes afterwards.

# Overview
Small
<img src="https://github.com/jetspiking/Taskbar11/blob/main/Images/Taskbar11_SmallCentered.png">

Medium
<img src="https://github.com/jetspiking/Taskbar11/blob/main/Images/Taskbar11_MediumCentered.png">

Large
<img src="https://github.com/jetspiking/Taskbar11/blob/main/Images/Taskbar11_LargeLeft.png">

