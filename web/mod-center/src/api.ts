export interface User { id: string; login: string; avatar: string; profile_url: string }
export interface Release { id: string; number: number; url: string; version: string; notes: string; created_at: number }
export interface Resource {
  id: string; name: string; type: string; description: string; cover: string;
  created_at: number; updated_at: number; author: User; latest: Release;
  releases?: Release[];
}
export interface Session { user: User | null; csrf: string; githubEnabled: boolean }
export interface Catalog { items: Resource[]; counts: Record<string, number>; total: number; page: number; pages: number }
export interface Draft { name: string; type: string; url: string; version: string; cover: string; description: string; notes: string }
export const blankDraft = (): Draft => ({ name: "", type: "", url: "", version: "", cover: "", description: "", notes: "" });
export const base = import.meta.env.BASE_URL;

export async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${base}api/${path}`, { credentials: "same-origin", ...init });
  const result = await response.json();
  if (!response.ok) throw new Error(result.error || "请求失败，请稍后重试。");
  return result as T;
}

export function write<T>(path: string, method: string, body: unknown, csrf: string) {
  return request<T>(path, { method, headers: { "Content-Type": "application/json", "X-CSRF-Token": csrf }, body: JSON.stringify(body) });
}
