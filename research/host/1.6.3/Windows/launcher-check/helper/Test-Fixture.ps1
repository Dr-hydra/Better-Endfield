param(
    [Parameter(Mandatory = $true)][string]$BuildDirectory
)
$ErrorActionPreference = 'Stop'
$fixtureBuild = (Resolve-Path -LiteralPath $BuildDirectory).Path
$bridge = Join-Path $fixtureBuild 'Release\LaunchBridge.exe'
$fixture = Join-Path $fixtureBuild 'Release\LaunchBridgeFixture.exe'
$reportPath = Join-Path $fixtureBuild 'fixture-report.json'
$testArguments = @('', 'space value', '中文参数', 'quote"value', 'C:\ends with space\', '\\server\share\', 'two\\"quotes', 'literal %command%')

function Invoke-BridgeFixture([string]$Target, [string[]]$TargetArguments) {
    $start = [System.Diagnostics.ProcessStartInfo]::new()
    $start.FileName = $bridge
    $start.UseShellExecute = $false
    $start.CreateNoWindow = $true
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    $start.Environment['BE_LAUNCHBRIDGE_TEST_VALUE'] = '继承环境 fixture'
    foreach ($argument in (@('--parent', $fixture, '--game', $Target, '--') + $TargetArguments)) {
        $start.ArgumentList.Add($argument)
    }
    $timer = [System.Diagnostics.Stopwatch]::StartNew()
    $process = [System.Diagnostics.Process]::Start($start)
    $process.WaitForExit()
    $timer.Stop()
    $result = [PSCustomObject]@{
        ExitCode = $process.ExitCode
        Output = $process.StandardOutput.ReadToEnd()
        Error = $process.StandardError.ReadToEnd()
        ElapsedMs = $timer.ElapsedMilliseconds
    }
    $process.Dispose()
    return $result
}

$result = Invoke-BridgeFixture $fixture (@('--report', $reportPath, '--exit', '4294967279') + $testArguments)
if ($result.ExitCode -ne -17) { throw "Exit code was not preserved: $($result.ExitCode) $($result.Error)" }
$report = Get-Content -LiteralPath $reportPath -Raw -Encoding utf8 | ConvertFrom-Json
$parentMatch = [regex]::Match($result.Output, 'Suspended parent created: (\d+)')
if (-not $parentMatch.Success) { throw 'Missing parent PID in helper output.' }
$fixtureParentId = [int]$parentMatch.Groups[1].Value
if ($report.parentPid -ne $fixtureParentId -or -not $report.parentAlive) { throw 'Child did not observe the live explicit parent.' }
if ($report.parentImage -ne $fixture) { throw 'Unexpected parent image.' }
if ($report.cwd -ne (Split-Path -Parent $fixture)) { throw 'Child working directory was not the target directory.' }
if ($report.environment -ne '继承环境 fixture') { throw 'Environment was not inherited.' }
if ($report.arguments.Count -ne $testArguments.Count) { throw 'Argument count changed.' }
for ($index = 0; $index -lt $testArguments.Count; ++$index) {
    if ($report.arguments[$index] -cne $testArguments[$index]) { throw "Argument $index changed." }
}
if ($result.ElapsedMs -lt 1100) { throw 'Helper did not wait for the child.' }
if (Get-Process -Id $fixtureParentId -ErrorAction SilentlyContinue) { throw 'The helper-owned parent was not cleaned.' }
if ($result.Output -notmatch 'Suspended parent cleaned:') { throw 'Missing cleanup confirmation.' }

# Exercise cleanup after target creation fails, still using only test files.
$notExecutable = Join-Path $fixtureBuild 'fixture-not-an-executable.txt'
Set-Content -LiteralPath $notExecutable -Value 'Inert fixture. Never executable.' -Encoding utf8
$failed = Invoke-BridgeFixture $notExecutable @()
if ($failed.ExitCode -ne 193 -or $failed.Error -notmatch 'Win32 193') { throw 'Expected explicit ERROR_BAD_EXE_FORMAT.' }
$failedParent = [regex]::Match($failed.Output, 'Suspended parent created: (\d+)')
if (-not $failedParent.Success -or $failed.Output -notmatch 'Suspended parent cleaned:') { throw 'Failure cleanup was not reported.' }
if (Get-Process -Id ([int]$failedParent.Groups[1].Value) -ErrorAction SilentlyContinue) { throw 'Failed launch left its own parent alive.' }

[PSCustomObject]@{
    Result = 'PASS'
    ExplicitParentPid = $fixtureParentId
    ChildPid = $report.pid
    ArgumentCases = $testArguments.Count
    EnvironmentPreserved = $true
    TargetWorkingDirectoryPreserved = $true
    WaitedMs = $result.ElapsedMs
    ExitCodeBits = '0xFFFFFFEF'
    ParentCleanup = $true
    FailedTargetCleanup = $true
    RealLauncherOrGameStarted = $false
} | ConvertTo-Json | Tee-Object -FilePath (Join-Path $fixtureBuild 'fixture-validation.json')
