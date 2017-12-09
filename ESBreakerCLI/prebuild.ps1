param(
[string]$ProjectDir = "ESBreakerCLI\"
)

$ErrorActionPreference = "Stop"

$shell = New-Object -Com Shell.Application
$FOF_MULTIDESTFILES = 0x1
$FOF_CONFIRMMOUSE = 0x2
$FOF_SILENT = 0x4
$FOF_RENAMEONCOLLISION = 0x8
$FOF_NOCONFIRMATION = 0x10
$FOF_WANTMAPPINGHANDLE = 0x20
$FOF_ALLOWUNDO = 0x40
$FOF_FILESONLY = 0x80
$FOF_SIMPLEPROGRESS = 0x100
$FOF_NOCONFIRMMKDIR = 0x200
$FOF_NOERRORUI = 0x400
$FOF_NOCOPYSECURITYATTRIBS = 0x800
$FOF_NORECURSION = 0x1000
$FOF_NO_CONNECTED_ELEMENTS = 0x2000
$FOF_WANTNUKEWARNING = 0x4000
$FOF_NORECURSEREPARSE = 0x8000
$FOF_NO_UI = $FOF_SILENT + $FOF_NOCONFIRMATION + $FOF_NOERRORUI + $FOF_NOCONFIRMMKDIR

$UA = "APK Ripper (Windows)"

function ExtractZip($fldr, $dst)
{
	foreach($item in $fldr.items())
	{
		If ($item.GetFolder -ne $Null)
		{
			#Write-Host $item.Path
			ExtractZip $item.GetFolder $dst
		}
		ElseIf ($item.Path -like '*.dll')
		{
			#Write-Host $item.Path
			$dst.CopyHere($item, $FOF_NO_UI)
		}
	}
}

$APKFilename = Invoke-WebRequest -Uri "http://pso2es.10nub.es/game.php?filename=true" -DisableKeepAlive -TimeoutSec 30 -UserAgent $UA

Write-Output "Downloading $APKFileName from pso2es.10.nub.es to $ProjectDir"
[Console]::Out.Flush()

$Pathtolib = $(Resolve-Path -Path ($ProjectDir + "lib\")).Path
$PathtoAPK = "$Pathtolib" + "$APKFilename"
$PathtoZIP = "$Pathtolib" + "PSO2es.zip"

if (Test-Path -Path $PathtoAPK)
{
	Write-Output "Already got a cached copy"
	[Console]::Out.Flush()
}
else
{
	Write-Output "Donwloading APK. this WILL take a while, Saving APK as $PathtoAPK"
	[Console]::Out.Flush()
	Invoke-WebRequest -Uri "http://pso2es.10nub.es/game.php" -DisableKeepAlive -TimeoutSec 30 -UserAgent $UA -OutFile $PathtoAPK
}

Write-Output "TODO: Delete old APKs"

if (Test-Path -Path $PathtoZIP)
{
	Write-Output "Already extracted DLLs, if you still can not build,"
	Write-Output "delete $PathtoZIP"
	[Console]::Out.Flush()
}
else
{
	Copy-Item -Path $PathtoAPK -Destination $PathtoZIP -Force -Confirm:$false

	Write-Output "Extracting DLLs from APK"
	[Console]::Out.Flush()
	ExtractZip $shell.NameSpace($PathtoZIP) $shell.NameSpace($Pathtolib)
	Write-Output "Done with prebuild script"
}
