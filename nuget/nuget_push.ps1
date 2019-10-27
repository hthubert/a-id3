 param (
    [string]$path = ""
 )
$scriptPath = $MyInvocation.MyCommand.Path
$scriptPath = Split-Path $scriptPath
Import-Module -Name $scriptPath/init.psm1 -Verbose
$logger = Get-Logger('nuget')
$logger.Debug("$path")
$pkg = Get-ChildItem -Path $path "*.nupkg" | Sort-Object LastWriteTime -Descending | Select-Object -First 1 | %{$_.FullName}
$logger.Debug("find package file: $pkg")
if (!$pkg)
{
    return -1
}
$server = "http://106.39.36.80:10002/nuget"
$api_key = "B67A09AC-1D50-428C-BD21-67B81AFAA8AE"
& $scriptPath\nuget.exe push $pkg $api_key -src $server