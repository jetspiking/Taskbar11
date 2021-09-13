# Taskbar11
<img src="https://github.com/jetspiking/Taskbar11/blob/main/Images/Taskbar11_Large.png" width="500">
Change the position and size of the Windows taskbar in Windows 11

# Description
Taskbar11 customizes the look of the Windows 11 taskbar, which doesn't have any options by default. The taskbar can be set to the top or bottom (default) of the screen, it also allows for setting various icon sizes.

# Usage
Download and the .exe under releases and launch the executable "Taskbar11.exe". The program is portable.

[Releases](https://github.com/jetspiking/Taskbar11/releases)

# Requirements
- Windows 11

# Information
Taskbar11 edits registry values in the following paths, respectively for taskbar position and icon size:
- Computer\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\StuckRects3
- Computer\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced

- In the "StuckRects3" path the key "TaskbarSi" is modified. Values 0, 1, 2 represent the taskbar icon size (respectively small, medium and large).
- In the "Advances" path the key "Settings" is modified. Index 12 is modified (second row, fifth column). Possible values are 0, 1, 2 and 3. However, only the values 1 and 3 are usable without a broken view.  

After editing the registry values, the application kills the explorer.exe process. This forces a reload of the UI which displays the new changes afterwards.

<img src="https://raw.githubusercontent.com/jetspiking/Taskbar11/main/Images/Taskbar11_RegistryPositionValue.png" width="500">
