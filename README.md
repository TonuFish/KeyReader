# KeyReader

A small project to read keyboard input from a specified process using OS calls, allowing it to be framework independent.

In theory it'll form the core library of a future cross-desktop ([MAUI](https://github.com/dotnet/maui))  typing app once the framework reaches GA.

If it ends up reaching an acceptable level of polish it'll make its way over to NuGet as well :)

## Current State

Testing console project is hardcoded to launch an instance of notepad and write each keypress while the window is targeted.

## Planned Work

- Finish Win10 implementation
  - Handle current keyboard state (modifiers, caps lock)
  - Handle keyboard layouts
  - Restructure for DI, callbacks, etc.
  - General cleanup
  - Tests
- Linux implementation
- MacOS implementation (Maybe... no Mac on hand)
