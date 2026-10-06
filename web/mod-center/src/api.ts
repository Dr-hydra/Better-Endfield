export interface User { id: string; login: string; avatar: string; profile_url: string }
export interface Release { id: string; number: number; url: string; version: string; notes: string; created_at: number }
export interface Resource {
  id: string; name: string; type: string; description: string; cover: string;
  created_at: number; updated_at: number; author: User; latest: Release;
  releases?: Release[];
}
export interface Session { user: User | null; csrf: string; githubEnabled: boolean; emailEnabled: boolean; email: string }
export interface Catalog { items: Resource[]; counts: Record<string, number>; total: number; page: number; pages: number }
export interface Draft { name: string; type: string; url: string; version: string; cover: string; description: string; notes: string }
export const blankDraft = (): Draft => ({ name: "", type: "", url: "", version: "", cover: "", description: "", notes: "" });
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
