#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
echo Script is from $DIR
if [ -z "$1" ]
 then
    $ProjectDir = $DIR
esle
	$ProjectDir = $(dirname "$1")
	echo "Running for $ProjectDir
fi

$APKFilename = "$(wget "http://pso2es.10nub.es/game.php?filename=true" --no-http-keep-alive --dns-timeout=15 -read-timeout=30 -UserAgent "APK Ripper (*NIX)" --quiet -O -)"

$Pathtolib = "$ProjectDir/lib"
$PathtoAPK = "$Pathtolib/$APKFilename"

echo "Downloading $APKFileName from pso2es.10.nub.es to $ProjectDir"

cd $Pathtolib
wget "http://pso2es.10nub.es/game.php" --no-http-keep-alive --dns-timeout=15 -read-timeout=30 -UserAgent "APK Ripper (*NIX)" --continue
unzip -xfj "$PathtoAPK" "assets/bin/Data/Managed/*.dll"
