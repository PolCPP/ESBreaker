#!/bin/sh
cd "`dirname \"$0\"`"
wget -N -c https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
mono nuget.exe restore
msbuild /p:BuildWithMono="true" /p:Configuration=Debug
msbuild /p:BuildWithMono="true" /p:Configuration=Release
