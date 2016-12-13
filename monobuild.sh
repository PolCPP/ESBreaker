#!/bin/sh
cd "`dirname \"$0\"`"
wget -N -c http://nuget.org/nuget.exe
mono nuget.exe restore
xbuild /p:BuildWithMono="true"
