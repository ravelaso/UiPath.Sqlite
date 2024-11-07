# Sqlite for UiPath

This set of activities allows you to work with a Sqlite files in UiPath

- Compatibility: net6.0 / UiPath Windows C#/VB Project.

# Issues:

If you want to use in production, the machine should have the SQLite.Interop.dll within:
``C:\Program Files\UiPath\Studio`` Alternatively, if you wish to just develop, it is sufficient to place the dll
within the root project folder you are working. Bear in mind, this will only work for the project on that machine 
when developing. If you want to be accessible system-wide it must be placed in the directory mentioned before.