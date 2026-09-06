// Reading the leaderboard from object storage.
//
// The board objects live in a public bucket behind a CDN, but the bucket sends
// no `access-control-allow-origin`, so a cross-origin `fetch` from the Toy page
// is blocked outright. A classic <script> load is not a CORS request at all —
// it sends no `Origin` — so that is how the data comes in. The objects are
// written as `__be("<key>", <json>);` by the backend.
//
// The point of going around the cloud function is cost: in this environment the
// database bills for being awake and function egress is the priciest line on
// the price list, while a CDN read is neither.

type Resolver = (value: unknown) => void;

const pending = new Map<string, Resolver[]>();

declare global {
  interface Window {
    __be?: (key: string, value: unknown) => void;
  }
}

function ensureCallback() {
  if (typeof window === "undefined" || window.__be) return;
  // One callback for every object, keyed by what was asked for, so several
  // loads can be in flight without inventing a callback name per request.
  window.__be = (key, value) => {
    const waiting = pending.get(key);
    pending.delete(key);
    waiting?.forEach((resolve) => resolve(value));
  };
}

export interface JsonpOptions {
  /** Cache-bust granularity in ms; the query is bucketed so the CDN can still work. */
  freshness?: number;
  timeoutMs?: number;
}

/**
 * Loads one storage object. Resolves `null` when it is not there — a stage with
 * no records has no board object, and that is a normal answer, not an error.
 */
export function loadJsonp<T>(base: string, key: string, options: JsonpOptions = {}): Promise<T | null> {
  const { freshness = 30_000, timeoutMs = 12_000 } = options;
  if (!base) return Promise.resolve(null);
  ensureCallback();

  return new Promise<T | null>((resolve) => {
    let settled = false;
    const finish = (value: T | null) => {
      if (settled) return;
      settled = true;
      clearTimeout(timer);
      script.remove();
      const waiting = (pending.get(key) ?? []).filter((item) => item !== onData);
      if (waiting.length) pending.set(key, waiting);
      else pending.delete(key);
      resolve(value);
    };
    const onData = (value: unknown) => finish(value as T);

    pending.set(key, [...(pending.get(key) ?? []), onData]);

    const script = document.createElement("script");
    script.src = `${base}/${key}.js?t=${Math.floor(Date.now() / freshness)}`;
    script.async = true;
    script.addEventListener("error", () => finish(null));
    const timer = setTimeout(() => finish(null), timeoutMs);
    document.head.appendChild(script);
  });
}
