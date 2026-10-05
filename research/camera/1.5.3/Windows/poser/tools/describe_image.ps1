param(
  [Parameter(Mandatory=$true)][string]$ImagePath,
  [string]$Prompt = "请详细描述这张游戏截图的内容：画面整体布局、可见的 UI 窗口与文字、角色姿势、3D 手柄、以及任何异常（如黑屏、模型错位、中文乱码、卡死画面）。",
  [string]$Model = "glm-4v-flash"
)

$ErrorActionPreference = 'Stop'

$keyFile = Join-Path $PSScriptRoot 'zhipu_api_key.txt'
$apiKey = $env:ZHIPU_API_KEY
if ([string]::IsNullOrEmpty($apiKey) -and (Test-Path $keyFile)) {
  $apiKey = (Get-Content $keyFile -Raw).Trim()
}
if ([string]::IsNullOrEmpty($apiKey)) {
  Write-Error "No Zhipu API key. Set env ZHIPU_API_KEY or create tools\zhipu_api_key.txt"
  exit 1
}
if (-not (Test-Path $ImagePath)) { Write-Error "Image not found: $ImagePath"; exit 1 }

$bytes = [System.IO.File]::ReadAllBytes((Resolve-Path $ImagePath).Path)
$b64 = [Convert]::ToBase64String($bytes)
$ext = [System.IO.Path]::GetExtension($ImagePath).TrimStart('.').ToLower()
if ($ext -eq 'jpg') { $ext = 'jpeg' }
if ([string]::IsNullOrEmpty($ext)) { $ext = 'png' }

$body = @{
  model = $Model
  messages = @(
    @{
      role = 'user'
      content = @(
        @{ type = 'image_url'; image_url = @{ url = "data:image/$ext;base64,$b64" } },
        @{ type = 'text'; text = $Prompt }
      )
    }
  )
  max_tokens = 1024
} | ConvertTo-Json -Depth 10

$headers = @{ Authorization = "Bearer $apiKey" }
$resp = Invoke-RestMethod -Uri 'https://open.bigmodel.cn/api/paas/v4/chat/completions' `
  -Method Post -Headers $headers -ContentType 'application/json; charset=utf-8' -Body $body

$resp.choices[0].message.content
