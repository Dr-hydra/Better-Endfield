[CmdletBinding()]
param([string]$JdkRoot = '', [string]$WorkspaceConfig = '')
$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$signWorkspace = Get-BEWorkspace -Config $WorkspaceConfig
$repoRoot = $signWorkspace.repo_root
$secretRoot = Join-Path $repoRoot 'config/secrets/next'
$androidProperties = Join-Path $repoRoot 'config/android-next-signing.local.properties'
$windowsProperties = Join-Path $repoRoot 'config/next-windows-signing.local.json'
if ((Test-Path -LiteralPath $androidProperties) -or (Test-Path -LiteralPath $windowsProperties)) {
    throw 'Next signing is already initialized. Existing release keys are never overwritten.'
}
$keytool = if ($JdkRoot) { Join-Path $JdkRoot 'bin/keytool.exe' } else { (Get-Command keytool -ErrorAction Stop).Source }
if (-not (Test-Path -LiteralPath $keytool -PathType Leaf)) { throw 'A JDK keytool is required.' }
New-Item -ItemType Directory -Path $secretRoot -Force | Out-Null
$passwordFile = Join-Path $secretRoot 'release-password.txt'
$passwordBytes = [System.Security.Cryptography.RandomNumberGenerator]::GetBytes(32)
$releasePassword = [Convert]::ToBase64String($passwordBytes)
[IO.File]::WriteAllText($passwordFile, $releasePassword, [Text.UTF8Encoding]::new($false))
$androidStore = Join-Path $secretRoot 'android-next.p12'
& $keytool -genkeypair -keystore $androidStore -storetype PKCS12 -alias next-release `
    '-storepass:file' $passwordFile '-keypass:file' $passwordFile -keyalg RSA -keysize 3072 `
    -sigalg SHA256withRSA -validity 10950 -dname 'CN=Better Endfield Next' -noprompt
if ($LASTEXITCODE -ne 0) { throw 'Android Next signing key generation failed.' }
$androidCertificate = Join-Path $secretRoot 'android-next.cer'
& $keytool -exportcert -keystore $androidStore '-storepass:file' $passwordFile -alias next-release -file $androidCertificate
if ($LASTEXITCODE -ne 0) { throw 'Android signing certificate export failed.' }
$androidFingerprint = (Get-FileHash -LiteralPath $androidCertificate -Algorithm SHA256).Hash.ToLowerInvariant()
# This certificate signs internal builds. Public CA trust is a separate property;
# do not add this self-signed certificate to a trust store automatically.
$certificate = New-SelfSignedCertificate -Subject 'CN=Better Endfield Next' -Type CodeSigningCert `
    -CertStoreLocation Cert:\CurrentUser\My -KeyAlgorithm RSA -KeyLength 3072 -HashAlgorithm SHA256 `
    -NotAfter (Get-Date).AddYears(5) -KeyExportPolicy Exportable
$windowsPfx = Join-Path $secretRoot 'windows-next.pfx'
$securePassword = ConvertTo-SecureString $releasePassword -AsPlainText -Force
Export-PfxCertificate -Cert $certificate -FilePath $windowsPfx -Password $securePassword | Out-Null
$windowsFingerprint = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($certificate.RawData)).ToLowerInvariant()
[IO.File]::WriteAllText($androidProperties, @"
storeFile=$($androidStore.Replace('\','/'))
keyAlias=next-release
storePassword=$releasePassword
keyPassword=$releasePassword
"@, [Text.UTF8Encoding]::new($false))
@{certificate_thumbprint=$certificate.Thumbprint;certificate_sha256=$windowsFingerprint;pfx_file=$windowsPfx;password_file=$passwordFile;trust='self-signed-internal'} |
    ConvertTo-Json | Set-Content -LiteralPath $windowsProperties -Encoding utf8
$policyPath = Join-Path $repoRoot 'config/workspace.defaults.json'
$policy = Get-Content -LiteralPath $policyPath -Raw | ConvertFrom-Json
$policy.android_signing.properties_file = 'config/android-next-signing.local.properties'
$policy.android_signing.certificate_sha256 = $androidFingerprint
$policy | Add-Member -NotePropertyName windows_signing -NotePropertyValue @{
    properties_file='config/next-windows-signing.local.json';certificate_sha256=$windowsFingerprint;trust='self-signed-internal'
} -Force
$policy | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath $policyPath -Encoding utf8
Write-Output 'New Android and Windows signing identities initialized; private material stays under ignored config/secrets/next.'
Write-Output "Android certificate SHA-256: $androidFingerprint"
Write-Output "Windows certificate SHA-256: $windowsFingerprint (self-signed internal certificate)"
