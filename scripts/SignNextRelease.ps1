[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$Path, [string]$WorkspaceConfig = '')
$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$signWorkspace = Get-BEWorkspace -Config $WorkspaceConfig
$policy = $signWorkspace.windows_signing
if (-not $policy) { throw 'Next Windows signing policy is not configured.' }
$propertiesPath = Join-Path $signWorkspace.repo_root $policy.properties_file
$properties = Get-Content -LiteralPath $propertiesPath -Raw | ConvertFrom-Json
$certificatePath = "Cert:\CurrentUser\My\" + $properties.certificate_thumbprint
if (-not (Test-Path -LiteralPath $certificatePath)) {
    $password = ConvertTo-SecureString ([IO.File]::ReadAllText($properties.password_file)) -AsPlainText -Force
    Import-PfxCertificate -FilePath $properties.pfx_file -Password $password -CertStoreLocation Cert:\CurrentUser\My | Out-Null
}
$certificate = Get-Item -LiteralPath $certificatePath
$fingerprint = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($certificate.RawData)).ToLowerInvariant()
if ($fingerprint -ne $policy.certificate_sha256) { throw 'Windows signing certificate does not match the Next release identity.' }
$item = Get-Item -LiteralPath $Path
$files = if ($item.PSIsContainer) {
    Get-ChildItem -LiteralPath $item.FullName -Recurse -File | Where-Object { $_.Extension -in '.exe','.dll' }
} else { @($item) }
foreach ($file in $files) {
    # Leave vendor signatures intact, signing only this product's own binaries
    # plus its explicitly named XInput proxy.
    if ($file.Name -notmatch '^BetterEndfieldNext.*\.(exe|dll)$' -and $file.Name -ne 'xinput1_4.dll') { continue }
    $result = Set-AuthenticodeSignature -LiteralPath $file.FullName -Certificate $certificate -HashAlgorithm SHA256
    if (-not $result.SignerCertificate -or $result.SignerCertificate.Thumbprint -ne $certificate.Thumbprint) {
        throw "Signing failed: $($file.FullName) ($($result.StatusMessage))"
    }
    $check = Get-AuthenticodeSignature -LiteralPath $file.FullName
    if (-not $check.SignerCertificate -or $check.SignerCertificate.Thumbprint -ne $certificate.Thumbprint -or
        $check.Status -eq 'HashMismatch') { throw "Signature verification failed: $($file.FullName)" }
}
Write-Output "Next Windows signatures applied: $($item.FullName) ($($policy.trust))"
