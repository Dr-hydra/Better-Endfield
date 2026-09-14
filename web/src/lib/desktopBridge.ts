export const BRIDGE = "better-endfield-desktop";
type Kind = "combat" | "gacha" | "probe";
interface NativeBridge {
  postMessage(value: unknown): void;
  addEventListener(name: "message", handler: (event: MessageEvent) => void): void;
  removeEventListener(name: "message", handler: (event: MessageEvent) => void): void;
}

// Bounded, ordered, one-transfer-at-a-time transport. Raw JSON remains local.
export function createReceiver(deliver: (kind: Kind, text: string) => void, reply: (data: unknown) => void) {
  let active: { id: string; kind: Kind; size: number; parts: string[]; received: number } | undefined;
  return (value: unknown) => {
    const m = value as Record<string, unknown> | null;
    if (!m || m.protocol !== BRIDGE) return;
    try {
      if (m.version !== 1) throw new Error("unsupported_version");
      if (m.type === "begin") {
        active = undefined;
        if (typeof m.id !== "string" || m.id.length > 128 || !["combat", "gacha", "probe"].includes(String(m.kind)) || !Number.isSafeInteger(m.size) || Number(m.size) < 0 || Number(m.size) > 64 * 1024 * 1024) throw new Error("invalid_begin");
        active = { id: m.id, kind: m.kind as Kind, size: Number(m.size), parts: [], received: 0 };
      } else {
        if (!active || m.id !== active.id) throw new Error("unknown_transfer");
        if (m.type === "chunk") {
          if (m.index !== active.parts.length || typeof m.text !== "string" || m.text.length > 262144) throw new Error("invalid_chunk");
          const bytes = new TextEncoder().encode(m.text).length;
          if (active.received + bytes > active.size || active.parts.length >= 4096) throw new Error("size_exceeded");
          active.parts.push(m.text); active.received += bytes;
        } else if (m.type === "end") {
          if (active.received !== active.size) throw new Error("incomplete_transfer");
          const done = active; active = undefined;
          deliver(done.kind, done.parts.join(""));
        } else throw new Error("unknown_message");
      }
      reply({ protocol: BRIDGE, version: 1, type: "ack", id: m.id, step: m.type, index: m.index });
    } catch (e) {
      active = undefined;
      reply({ protocol: BRIDGE, version: 1, type: "error", reason: e instanceof Error ? e.message : "invalid_data" });
    }
  };
}

export function connectDesktopBridge(deliver: (kind: Kind, text: string) => void) {
  const bridge = (window as unknown as { chrome?: { webview?: NativeBridge } }).chrome?.webview;
  if (!bridge) return;
  const receive = createReceiver(deliver, data => bridge.postMessage(data));
  const handler = (event: MessageEvent) => receive(event.data);
  bridge.addEventListener("message", handler);
  bridge.postMessage({ protocol: BRIDGE, version: 1, type: "ready" });
  return () => bridge.removeEventListener("message", handler);
}
