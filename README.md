# MultiMonitorCalendar
Show the Windows Calendar &amp; Notification Center when clicking the taskbar clock on primary and secondary screens

# Purpose
As part of Windows 11, the Taskbar, Start Menu, Explorer and various other components were either overhauled or completely rewritten. The taskbar has received numerous improvements since it's debut, however it still isn't quite there yet when compared to it's predecessor. One of the most obvious (or at least most commonly mentioned to me) is the fact that the calendar & notifications will only show on the primary monitor. Although ***now*** the clock does show up on secorndary monitors' taskbars, clicking the clock will do nothing.

So yeah, this project allows the calendar and related UI to display on any screen connected to your machine. Click on the clock (any clock) and it will display directly above where you clicked.

# Limitations
Currently the only supported configuration
- Taskbar must be at the bottom of the screen
- Taskbar must be horizontal
- Auto hide must be disabled

# How It Works
The idea is simple. When the app starts up, it creates an invisible button for each connected screen and places the button over the clock on each taskbar. When the button is hovered over, the app queries Windows for the X, Y, Height & Width of the calendar dialog. If necessary, the app will shift the dialog onto the current monitor. When one of the invisible buttons is clicked, the dialog will be displayed in it's current location. Hovering over an invisible button will move the dialog to your current screen if it is already open on another screen.

## *Notes*
- Resolutions other than 3840x2160 have not yet been tested and may have rendering issues due to the width of the invisible button. This is yet to be confirmed and can be easily fixed if necessary
- Once running, the app can only be closed via task manager or CLI. It will not appear on the taskbar or task switcher

# Dependencies
- .NET 8
- Windows 11
- Taskbars with clocks that do nothing when you click them

---

<p align="center">
  Written by Paul Walker - <a href="https://github.com/pwalkerdev">github.com/pwalkerdev</a>
</p>
