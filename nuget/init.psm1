# get current running script path
$scriptPath = $MyInvocation.MyCommand.Path
$scriptPath = Split-Path $scriptPath
# set up logging with NLog
# requires NLog.dll and NLog.config to be in the same directory as this script
# adapted from https://github.com/NLog/NLog/issues/233
# needs absolute path to NLog.dll and NLog.config (use variable "scriptPath")
# ignore version and location output from LoadFile
[Reflection.Assembly]::LoadFile("$scriptPath\NLog.dll") | Out-Null

# load NLog config file
# note all logging targets in NLog config file need to be absolute (not relative) paths
$ne = New-Object NLog.Config.XmlLoggingConfiguration("$scriptPath\NLog.config")
# assign config file
([NLog.LogManager]::Configuration) = $ne

function Get-Logger {
    param (
        [string] $name
    )
    return [NLog.LogManager]::GetLogger($name)
}
# get the default logger
# $logger = [NLog.LogManager]::GetLogger("")
# Export-ModuleMember -Variable $scriptPath
Export-ModuleMember -Function Get-Logger