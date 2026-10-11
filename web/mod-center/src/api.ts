export interface User { id: string; login: string; avatar: string; profile_url: string }
export interface Upload { id: string; url: string; filename: string; size: number }
export interface SocialStats { likes: number; comments: number; liked: boolean }
export interface Comment { id: string; body: string; created_at: number; author: User; canDelete: boolean }
export interface CommentPage { items: Comment[]; total: number; page: number; pages: number }
export interface Release { id: string; number: number; url: string; version: string; notes: string; created_at: number; file: Upload | null }
export interface Resource {
  id: string; name: string; type: string; description: string; cover: string;
  created_at: number; updated_at: number; author: User; latest: Release;
  releases?: Release[]; images: Upload[]; cover_link: string; likes: number; comments: number; liked: boolean;
}
export interface Session { user: User | null; csrf: string; githubEnabled: boolean; emailEnabled: boolean; email: string; canUploadFiles: boolean; uploadLimits?: { imageBytes: number; fileBytes: number; imagesPerWork: number } }
export interface Catalog { items: Resource[]; counts: Record<string, number>; total: number; page: number; pages: number }
export interface Draft { name: string; type: string; url: string; version: string; cover: string; description: string; notes: string; file_id: string; image_ids: string[] }
export const blankDraft = (): Draft => ({ name: "", type: "", url: "", version: "", cover: "", description: "", notes: "", file_id: "", image_ids: [] });
export const base = import.meta.env.BASE_URL;

export async function request<T>(path: string, init?: RequestInit): Promise<T> {
  let response: Response;
  try { response = await fetch(`${base}api/${path}`, { credentials: "same-origin", ...init }); }
  catch { throw new Error("网络连接失败，请检查网络后重试。"); }
  let result;
  try { result = await response.json(); }
  catch { throw new Error("服务响应异常，请稍后重试。"); }
  if (!response.ok) throw new Error(result.error || "请求失败，请稍后重试。");
  return result as T;
}

export async function authRequest<T>(path: string, init?: RequestInit): Promise<T> {
  let response: Response;
  try { response = await fetch(`${base}auth/${path}`, { credentials: "same-origin", ...init }); }
  catch { throw new Error("网络连接失败，请检查网络后重试。"); }
  let result;
  try { result = await response.json(); }
  catch { throw new Error("服务响应异常，请稍后重试。"); }
  if (!response.ok) throw new Error(result.error || "请求失败，请稍后重试。");
  return result as T;
}

export function write<T>(path: string, method: string, body: unknown, csrf: string) {
  return request<T>(path, { method, headers: { "Content-Type": "application/json", "X-CSRF-Token": csrf }, body: JSON.stringify(body) });
}

export function upload(file: File, kind: "image" | "file", csrf: string, onProgress: (value: number) => void): Promise<Upload> {
  return new Promise((resolve, reject) => {
    const xhr = new XMLHttpRequest();
    xhr.open("POST", `${base}api/uploads?kind=${kind}`); xhr.timeout = 300000;
    xhr.setRequestHeader("Content-Type", "application/octet-stream"); xhr.setRequestHeader("X-CSRF-Token", csrf); xhr.setRequestHeader("X-Upload-Name", encodeURIComponent(file.name));
    xhr.upload.onprogress = event => { if (event.lengthComputable) onProgress(Math.round(event.loaded / event.total * 100)); };
    xhr.onerror = xhr.ontimeout = () => reject(new Error("上传失败，请检查网络后重试。"));
    xhr.onload = () => { try { const result = JSON.parse(xhr.responseText); if (xhr.status < 200 || xhr.status >= 300) reject(new Error(result.error || "上传失败，请检查网络后重试。")); else resolve(result); } catch { reject(new Error("服务响应异常，请稍后重试。")); } };
    xhr.send(file);
  });
}
