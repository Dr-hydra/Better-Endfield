export type Language = "zh" | "en";

const messages: Record<string, string> = {
  "当前账号没有作品文件上传权限。": "Your account cannot upload work files.",
  "上传文件不存在或不属于当前账号。": "This upload does not exist or belongs to another account.",
  "上传类型不正确。": "Invalid upload type.",
  "图片不能超过 5 MB。": "Images must be at most 5 MB each.",
  "作品文件不能超过 200 MB。": "Work files must be at most 200 MB each.",
  "每个作品最多上传 6 张图片。": "Each work can have up to 6 uploaded images.",
  "仅支持 JPG、PNG 和 WebP 图片。": "Only JPG, PNG, and WebP images are supported.",
  "请选择上传的作品文件。": "Select the uploaded work file.",
  "文件名格式不正确。": "Invalid file name.",
  "文件名不能为空。": "A file name is required.",
  "上传文件为空或传输未完成。": "The file is empty or the transfer is incomplete.",
  "存储空间不足，请稍后重试。": "Storage is full. Please try again later.",
  "账号图片存储额度已用完。": "Your image storage quota has been reached.",
  "未发布的上传文件过多，请先发布作品或删除文件。": "Too many unpublished uploads. Publish a work or remove unused uploads first.",
  "文件仍被作品或历史版本使用。": "A work or release still uses this file.",
  "文件不存在或已被删除。": "The file does not exist or has been removed.",
  "上传失败，请检查网络后重试。": "Upload failed. Check your connection and try again.",
  "评论不存在或已被删除。": "The comment does not exist or has been removed.",
  "评论间隔至少 15 秒。": "Wait at least 15 seconds between comments.",
  "只有评论者或管理员可以删除评论。": "Only the commenter or an administrator can delete this comment.",
  "GitHub 登录暂未开放。你可以先浏览作品和预览发布表单。": "GitHub sign-in is not available yet. You can still browse works and preview the publishing form.",
  "GitHub 登录未完成，请重试。": "GitHub sign-in was not completed. Please try again.",
  "草稿已保存在当前浏览器中。": "Draft saved in this browser.",
  "请求失败，请稍后重试。": "Request failed. Please try again later.",
  "网络连接失败，请检查网络后重试。": "Unable to connect. Check your connection and try again.",
  "服务响应异常，请稍后重试。": "The server returned an unexpected response. Please try again later.",
  "表单格式不正确。": "The form is invalid.",
  "请选择列表中的资源类型。": "Select a type from the list.",
  "资源类型不正确。": "The resource type is invalid.",
  "请先登录。": "Please sign in first.",
  "邮箱登录暂未配置。": "Email sign-in is not configured yet.",
  "邮箱地址格式不正确。": "The email address is invalid.",
  "登录邮件发送失败，请稍后重试。": "The sign-in email could not be sent. Please try again later.",
  "登录暂未开放。你可以先浏览作品和预览发布表单。": "Sign-in is not available yet. You can browse works and preview the publishing form.",
  "账号已绑定邮箱。": "This account already has a linked email.",
  "该邮箱已用于另一个账号，请使用其他邮箱。": "This email belongs to another account. Use a different email.",
  "请填写六位数字验证码。": "Enter a 6-digit code.",
  "验证码无效或已过期，请重新发送。": "The code is invalid or has expired. Request a new code.",
  "验证码不正确，请重试。": "The code is incorrect. Please try again.",
  "邮箱绑定成功。": "Email linked successfully.",
  "登录成功。": "Signed in successfully.",
  "请求来源不正确，请刷新页面后重试。": "Please refresh the page and try again.",
  "登录状态已变化，请刷新页面后重试。": "Your session has changed. Please refresh the page and try again.",
  "操作太频繁，请稍后再试。": "Too many requests. Please try again later.",
  "服务繁忙，请稍后再试。": "The server is busy. Please try again later.",
  "服务暂时不可用，请稍后重试。": "The service is temporarily unavailable. Please try again later.",
  "请求需要使用 JSON 格式。": "The request must use JSON.",
  "表单内容过长。": "The form is too large.",
  "作品不存在或已被删除。": "This work does not exist or has been deleted.",
  "只有发布者可以修改这份作品。": "Only the author can change this work.",
  "这个版本已发布，请填写新版本名或留空。": "This version has already been published. Use a new version name or leave it blank.",
  "页面不存在。": "Page not found.",
  "接口不存在。": "Endpoint not found.",
  "请求方法不支持。": "This request method is not supported.",
  "地址格式不正确。": "The address is invalid.",
};

const fields: Record<string, string> = {
  "类型": "Type", "名字": "Name", "下载链接": "Download link", "版本": "Version",
  "封面链接": "Cover link", "作品介绍": "Description", "更新说明": "Update notes",
  "搜索内容": "Search query", "GitHub 用户名": "GitHub username", "邮箱": "Email", "验证码": "Code",
  "评论": "Comment", "上传文件": "Upload",
};

// Keep the original message in state so an open notice also follows language changes.
export function localizeMessage(message: string, lang: Language): string {
  if (lang === "zh") return message;
  if (messages[message]) return messages[message];
  for (const [label, english] of Object.entries(fields)) {
    if (message === `${label}格式不正确。`) return `${english} is invalid.`;
    if (message === `${label}不能为空。`) return `${english} is required.`;
    if (message === `${label}需要填写完整的 HTTP 或 HTTPS 地址。`) return `${english} must be a complete HTTP or HTTPS URL.`;
    const limit = new RegExp(`^${label}不能超过 (\\d+) 个字符。$`).exec(message);
    if (limit) return `${english} must be at most ${limit[1]} characters.`;
  }
  return messages["请求失败，请稍后重试。"];
}
