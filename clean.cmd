echo off

rem DEL [/P] [/F] [/S] [/Q] [/A[[:]attributes]] names
rem   names         Specifies a list of one or more files or directories.
rem                 Wildcards may be used to delete multiple files. If a
rem                 directory is specified, all files within the directory
rem                 will be deleted.
rem 
rem   /P            Prompts for confirmation before deleting each file.
rem   /F            Force deleting of read-only files.
rem   /S            Delete specified files from all subdirectories.
rem   /Q            Quiet mode, do not ask if ok to delete on global wildcard
rem   /A            Selects files to delete based on attributes
rem   attributes    R  Read-only files            S  System files
rem                 H  Hidden files               A  Files ready for archiving
rem                 -  Prefix meaning not
REM RMDIR [/S] [/Q] [drive:]path
REM RD [/S] [/Q] [drive:]path

REM     /S      Removes all directories and files in the specified directory
REM             in addition to the directory itself.  Used to remove a directory
REM             tree.

REM     /Q      Quiet mode, do not ask if ok to remove a directory tree with /S


rem cd ..\

RD /S /Q ".\bin" 
RD /S /Q ".\ACVerify\bin" 
RD /S /Q ".\ACVerify\obj" 
RD /S /Q ".\AInfo\bin" 
RD /S /Q ".\AInfo\obj" 
RD /S /Q ".\CountLines\bin" 
RD /S /Q ".\CountLines\obj" 
RD /S /Q ".\Mpts\bin" 
RD /S /Q ".\Mpts\obj" 
RD /S /Q ".\Mpts.Contracts\bin"  
RD /S /Q ".\Mpts.Contracts\obj" 
RD /S /Q ".\Mpts.Library\bin"  
RD /S /Q ".\Mpts.Library\obj"
RD /S /Q ".\Mpts-Shutdown\bin"  
RD /S /Q ".\Mpts-Shutdown\obj"  
RD /S /Q ".\MptsSetup\Debug" 
RD /S /Q ".\MptsSetup\Release" 

rem deleting project user files
del /F /S /Q /A:H .\*.suo
del /F /S /Q /A:H .\*.user
del /F /S /Q  .\*.cache
rem deleting objects
del /F /S /Q  .\*.obj
rem deleting intellisence
del /F /S /Q  .\*.ncb
rem deleting debuger informations
del /F /S /Q  .\*.pdb
rem deletind desktop.ini
del /F /S /Q /A:H .\*.ini

rem returning to base directory
rem cd .\Scripts