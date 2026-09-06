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
  navigate(req: { type: string; id: string; extra?: Record<string, string> }): Promise<void>;
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

/**
 * Whether the account-backed SDK calls can be made at all.
 *
 * The host bridge guards getUserProfile, getAuthorRelation and
 * getVideoUserActions with the same test — a mobile user agent that is not the
 * B站 app — and on the way to throwing "unsupported" it first tries to open the
 * app. So on mobile web the call cannot succeed and does bounce the reader out
 * of the page; a shared link opened on a phone would jump to the app before
 * showing anything. This mirrors the bridge's own condition so the call is
 * never made there.
 */
export function toyAccountAvailable(): boolean {
  const agent = typeof navigator === "undefined" ? "" : navigator.userAgent;
  if (/BiliApp/i.test(agent)) return true;
  return !/Android|iPhone|iPad|iPod|Mobile/i.test(agent);
}

export async function requestToyProfile(): Promise<ToyProfile> {
  if (!toyAccountAvailable()) throw new Error("手机浏览器里读不到 Toy 账号，请在 B站 App 内打开");
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

export function recordUrl(shortId: string) {
  return `https://www.bilibili.com/toy/endfield/index.html?r=${encodeURIComponent(shortId)}`;
}

export type ShareOutcome = "sheet" | "fallback";

/**
 * Copies `text`, returning whether it landed.
 *
 * On Toy the page runs in a cross-origin iframe whose `allow` attribute lists
 * only pointer-lock, so the async Clipboard API is not delegated and rejects.
 * `execCommand` predates permission policy and still works there during a user
 * gesture, so it gets a turn before the caller falls back to "select it
 * yourself".
 */
export async function copyText(text: string): Promise<boolean> {
  try {
    await navigator.clipboard.writeText(text);
    return true;
  } catch {
    // Falls through to the legacy path.
  }
  try {
    const field = document.createElement("textarea");
    field.value = text;
    field.setAttribute("readonly", "");
    field.style.cssText = "position:fixed;top:0;left:0;opacity:0;pointer-events:none";
    document.body.appendChild(field);
    field.select();
    const copied = document.execCommand("copy");
    field.remove();
    return copied;
  } catch {
    return false;
  }
}

/**
 * Tries the native share sheet, and says whether the caller has to show the
 * link itself.
 *
 * `isSupport("share")` answers for the API surface, not the platform: in a
 * browser it says true and then `share()` throws "share is not supported on
 * web". So the ability check is not trusted — the call is attempted and any
 * failure falls through. Only the sheet is app-only; the link itself works
 * everywhere.
 */
export async function shareRecord(shortId: string): Promise<ShareOutcome> {
  if (hasToyAbility("share")) {
    try {
      await sdk().share({ path: `index.html?r=${encodeURIComponent(shortId)}` });
      return "sheet";
    } catch {
      // Falls through to the dialog.
    }
  }
  return "fallback";
}

export async function openBilibiliVideo(bvid: string) {
  // The SDK routes by resource type and id; it has no free-form url form.
  if (hasToyAbility("navigate")) return sdk().navigate({ type: "video", id: bvid, extra: { from: "toy" } });
  window.open(`https://www.bilibili.com/video/${encodeURIComponent(bvid)}`, "_blank", "noopener,noreferrer");
}
