#!/bin/sh
cd "`dirname \"$0\"`"
wget -N -c https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
mono nuget.exe restore
xbuild /p:BuildWithMono="true"
