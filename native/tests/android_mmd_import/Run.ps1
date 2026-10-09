param([Parameter(Mandatory=$true)][string]$DependencyDirectory)
$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../..')).Path
$mmdOutput = Join-Path ([IO.Path]::GetTempPath()) ('be-mmd-import-jvm-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $mmdOutput | Out-Null
$mmdClasspath = (Get-ChildItem -LiteralPath $DependencyDirectory -Filter '*.jar' | ForEach-Object FullName) -join ';'
$mmdSources = @('MmdVmdParser.java', 'MmdImportArchive.java', 'MmdImportPlan.java') | ForEach-Object {
    Join-Path $repoRoot ('android/app/src/main/java/dev/betterendfield/next/' + $_)
}
$mmdTest = Join-Path $PSScriptRoot 'src/dev/betterendfield/next/MmdImportJvmTest.java'
& javac -encoding UTF-8 --release 17 -cp $mmdClasspath -d $mmdOutput @mmdSources $mmdTest
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
& java -cp ($mmdOutput + ';' + $mmdClasspath) dev.betterendfield.next.MmdImportJvmTest
exit $LASTEXITCODE
