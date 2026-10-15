[CmdletBinding()]
param(
    # Use the assembly extracted by the actual published single-file executable.
    [Parameter(Mandatory)][string]$AssemblyPath,
    [Parameter(Mandatory)][string]$MappingPath,
    [Parameter(Mandatory)][string]$MetadataPath,
    [string]$NativeDirectory = '',
    [string]$OutputPath = ''
)
$ErrorActionPreference = 'Stop'
$assemblyFile = (Resolve-Path -LiteralPath $AssemblyPath).Path
$mapping = [IO.File]::ReadAllText((Resolve-Path -LiteralPath $MappingPath).Path)
$metadataJson = [IO.File]::ReadAllText((Resolve-Path -LiteralPath $MetadataPath).Path)
if (!$NativeDirectory) { $NativeDirectory = Split-Path -Parent $assemblyFile }
$nativeRoot = (Resolve-Path -LiteralPath $NativeDirectory).Path
Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public static class SteamPublishedMetadataNative {
    [DllImport("kernel32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
    public static extern bool SetDllDirectory(string path);
}
'@
if (![SteamPublishedMetadataNative]::SetDllDirectory($nativeRoot)) {
    throw [ComponentModel.Win32Exception]::new([Runtime.InteropServices.Marshal]::GetLastWin32Error())
}
$env:PATH = $nativeRoot + ';' + $env:PATH
$assembly = [Reflection.Assembly]::LoadFrom($assemblyFile)
$jsonOptions = [System.Text.Json.JsonSerializerOptions]::new()
$jsonOptions.PropertyNameCaseInsensitive = $true

function Resolve-ObfuscatedType([string]$Name) {
    $original = 'BetterEndfieldNext.UI.Services.' + $Name
    $pattern = '(?m)^\[BetterEndfieldNext\]' + [regex]::Escape($original) + ' -> \[BetterEndfieldNext\]([^\r\n]+)'
    $match = [regex]::Match($mapping, $pattern)
    if (!$match.Success) { throw "Missing obfuscation mapping for $original" }
    $type = $assembly.GetType($match.Groups[1].Value, $false)
    if (!$type) { throw "Published assembly does not match its obfuscation mapping: $original" }
    return $type
}
function Decode-Dto([string]$Name, [string]$Json) {
    $type = Resolve-ObfuscatedType $Name
    if (!$type.GetConstructor([Type[]]@())) { throw "$Name requires a parameterless JSON constructor." }
    $value = [System.Text.Json.JsonSerializer]::Deserialize($Json, $type, $jsonOptions)
    $encoded = [System.Text.Json.JsonSerializer]::Serialize($value, $type, $jsonOptions)
    return ($encoded | ConvertFrom-Json)
}
function Assert-Same($Actual, $Expected, [string]$Label) {
    if ($Actual -cne $Expected) { throw "$Label did not survive the published serializer." }
}
function Assert-Metadata($Actual, $Expected) {
    foreach ($field in @('AppId','Name','InstallDirectory','Executable','LaunchArguments','BuildId')) {
        Assert-Same $Actual.$field $Expected.$field "metadata.$field"
    }
    Assert-Same $Actual.Depots.Count $Expected.Depots.Count 'metadata.Depots.Count'
    for ($index = 0; $index -lt $Expected.Depots.Count; $index++) {
        Assert-Same $Actual.Depots[$index].Id $Expected.Depots[$index].Id "depot[$index].Id"
        Assert-Same $Actual.Depots[$index].Manifest $Expected.Depots[$index].Manifest "depot[$index].Manifest"
    }
}

$expected = $metadataJson | ConvertFrom-Json
$metadataType = Resolve-ObfuscatedType 'SteamMetadata'
$parser = @($metadataType.GetMethods([Reflection.BindingFlags]'Public,NonPublic,Static') | Where-Object {
    $_.ReturnType -eq $metadataType -and $_.GetParameters().Count -eq 1 -and $_.GetParameters()[0].ParameterType -eq [string]
})
if ($parser.Count -ne 1) { throw 'Cannot identify the published SteamMetadata.Parse method.' }
try { $parsed = $parser[0].Invoke($null, @($metadataJson)) }
catch { throw $_.Exception.GetBaseException() }
Assert-Metadata $parsed $expected
Assert-Metadata (Decode-Dto 'SteamMetadata' $metadataJson) $expected

$cachedJson = @{ FetchedUtc='2026-10-15T00:00:00+00:00'; Metadata=$expected } | ConvertTo-Json -Depth 10
$cached = Decode-Dto 'SteamMetadataCache' $cachedJson
Assert-Metadata $cached.Metadata $expected
if (!$cached.FetchedUtc) { throw 'Cached timestamp was lost.' }
$installedJson = @{
    Product='BetterEndfieldNext.SteamCN'; LibraryPath='C:\SteamFixture'; Metadata=$expected
    CreatedDirectories=@('C:\SteamFixture\steamapps\common\Arknights Endfiled')
    UpdatedUtc='2026-10-15T00:00:00+00:00'; Pending=$true
} | ConvertTo-Json -Depth 10
$installed = Decode-Dto 'SteamInstallReceipt' $installedJson
Assert-Metadata $installed.Metadata $expected
Assert-Same $installed.Product 'BetterEndfieldNext.SteamCN' 'receipt.Product'
Assert-Same $installed.LibraryPath 'C:\SteamFixture' 'receipt.LibraryPath'
Assert-Same $installed.CreatedDirectories.Count 1 'receipt.CreatedDirectories.Count'
Assert-Same $installed.Pending $true 'receipt.Pending'
$compatibility = Decode-Dto 'SteamCompatibilityReceipt' '{"Executable":"C:\\SteamFixture\\steam.exe","OriginalAdministrator":true,"WrittenAdministrator":false}'
Assert-Same $compatibility.Executable 'C:\SteamFixture\steam.exe' 'compatibility.Executable'
Assert-Same $compatibility.OriginalAdministrator $true 'compatibility.OriginalAdministrator'
Assert-Same $compatibility.WrittenAdministrator $false 'compatibility.WrittenAdministrator'

$launchOptionsEntries = @(
    [ordered]@{
        SteamRoot='C:\SteamFixture'; AccountId=[uint32]12345; Original=$null
        Written='"C:\Better Endfield\loaders\BetterEndfieldNext.Launcher.exe" --game "C:\Endfield Game\Endfield.exe" -- --steam-command %command%'
        PreviousWritten=$null; Pending=$false
    },
    [ordered]@{
        SteamRoot='D:\Steam 备用'; AccountId=[uint32]4000000000
        Original='-force-d3d11 --label "保留原选项"'
        Written='"D:\BE 新版\loaders\BetterEndfieldNext.Launcher.exe" --game "D:\终末地\Endfield.exe" -- --steam-command %command%'
        PreviousWritten='"D:\BE 旧版\loaders\BetterEndfieldNext.Launcher.exe" --game "D:\终末地\Endfield.exe" -- --steam-command %command%'
        Pending=$true
    }
)
$launchOptionsJson = [ordered]@{
    Product='BetterEndfieldNext.SteamLaunchOptions'; Entries=$launchOptionsEntries
} | ConvertTo-Json -Depth 10
$launchOptions = Decode-Dto 'SteamLaunchOptionsReceipt' $launchOptionsJson
Assert-Same $launchOptions.Product 'BetterEndfieldNext.SteamLaunchOptions' 'launch-options.Product'
Assert-Same $launchOptions.Entries.Count $launchOptionsEntries.Count 'launch-options.Entries.Count'
for ($index = 0; $index -lt $launchOptionsEntries.Count; $index++) {
    $expectedEntry = $launchOptionsEntries[$index]
    $entry = Decode-Dto 'SteamLaunchOptionsEntry' ($expectedEntry | ConvertTo-Json)
    foreach ($field in @('SteamRoot','AccountId','Original','Written','PreviousWritten','Pending')) {
        Assert-Same $launchOptions.Entries[$index].$field $expectedEntry[$field] "launch-options.Entries[$index].$field"
        Assert-Same $entry.$field $expectedEntry[$field] "launch-options-entry[$index].$field"
    }
}
$replacementJson = [ordered]@{
    Product='BetterEndfieldNext.SteamReplacement'; LibraryPath='C:\SteamFixture'; Metadata=$expected
    Pending=$true; UpdatedUtc='2026-10-15T00:00:00+00:00'
} | ConvertTo-Json -Depth 10
$replacement = Decode-Dto 'SteamReplacementReceipt' $replacementJson
Assert-Same $replacement.Product 'BetterEndfieldNext.SteamReplacement' 'replacement.Product'
Assert-Same $replacement.LibraryPath 'C:\SteamFixture' 'replacement.LibraryPath'
Assert-Metadata $replacement.Metadata $expected
Assert-Same $replacement.Pending $true 'replacement.Pending'
if (!$replacement.UpdatedUtc) { throw 'Replacement timestamp was lost.' }

$result = [ordered]@{
    success=$true; assembly=$assemblyFile; mapping=(Resolve-Path -LiteralPath $MappingPath).Path
    checks=@('published-normalized-parse','metadata-roundtrip','cache-roundtrip','install-receipt-roundtrip','compatibility-receipt-roundtrip','launch-options-receipt-roundtrip','launch-options-entry-roundtrip','replacement-receipt-roundtrip')
}
$output = $result | ConvertTo-Json -Depth 4
if ($OutputPath) { [IO.File]::WriteAllText([IO.Path]::GetFullPath($OutputPath), $output, [Text.UTF8Encoding]::new($false)) }
Write-Output $output
