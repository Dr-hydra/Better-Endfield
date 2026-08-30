export interface ToyProfile {
  avatar: string;
  nickname: string;
  toyOpenId?: string;
}

interface ToySdk {
  isSupport(ability: string): boolean;
  getUserProfile(): Promise<ToyProfile>;
  setCloudStorage(items: Record<string, string>): Promise<void>;
  getCloudStorage(keys?: string[]): Promise<Record<string, string>>;
  removeCloudStorage(keys: string[]): Promise<void>;
  share(req: { path: string }): Promise<void>;
  getQrCode(req?: { path?: string; size?: number }): Promise<{ base64: string; url: string }>;
  navigate(req: { url: string }): Promise<void>;
}

declare global {
  interface Window {
    toy?: ToySdk;
  }
}

function sdk(): ToySdk {
  if (!window.toy) throw new Error("当前环境未加载 Toy SDK，请在B站 Toy 页面中使用此功能");
  return window.toy;
}

export function hasToyAbility(name: string): boolean {
  try {
    return Boolean(window.toy?.isSupport(name));
  } catch {
    return false;
  }
}

export async function requestToyProfile(): Promise<ToyProfile> {
  if (!hasToyAbility("getUserProfile")) throw new Error("当前环境不支持 Toy 登录");
  return sdk().getUserProfile();
}

export async function getCloudStorage(keys?: string[]) {
  if (!hasToyAbility("getCloudStorage")) throw new Error("当前环境不支持 Toy 云存档");
  return sdk().getCloudStorage(keys);
}

export async function setCloudStorage(items: Record<string, string>) {
  if (!hasToyAbility("setCloudStorage")) throw new Error("当前环境不支持 Toy 云存档");
  return sdk().setCloudStorage(items);
}

export async function removeCloudStorage(keys: string[]) {
  if (!hasToyAbility("removeCloudStorage")) throw new Error("当前环境不支持 Toy 云存档");
  return sdk().removeCloudStorage(keys);
}

export async function shareRecord(shortId: string) {
  const path = `index.html?r=${encodeURIComponent(shortId)}`;
  if (hasToyAbility("share")) return sdk().share({ path });
  await navigator.clipboard.writeText(
    `https://www.bilibili.com/toy/endfield/index.html?r=${encodeURIComponent(shortId)}`,
  );
}

export async function recordQrCode(shortId: string) {
  if (!hasToyAbility("getQrCode")) throw new Error("当前环境不支持二维码生成");
  return sdk().getQrCode({
    path: `index.html?r=${encodeURIComponent(shortId)}`,
    size: 360,
  });
}

export async function openBilibiliVideo(bvid: string) {
  const url = `https://www.bilibili.com/video/${encodeURIComponent(bvid)}`;
  if (hasToyAbility("navigate")) return sdk().navigate({ url });
  window.open(url, "_blank", "noopener,noreferrer");
}
