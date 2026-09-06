// Desktop -> web handoff: the桌面端 opens this page with a loopback address in
// the URL and serves one record from it.
//
// The gacha page carries its whole snapshot inside the fragment. A combat
// record cannot: even after dropping every ledger field the web never reads it
// is ~69,000 base64 characters, and dropping the ledgers outright still leaves
// ~40,500 — one to two orders of magnitude past what a shell-launched URL can
// be relied on to survive. So the URL carries a port and a nonce, and the
// bytes come over 127.0.0.1.
//
// The address goes in the *query string*, not the fragment. On Toy the page we
// ship runs inside a sandboxed iframe whose src the outer page renders
// server-side; it copies the outer query string across but the fragment never
// reaches the server and no host script forwards it, so a `#/import/...` route
// arrives as a bare homepage load. `?r=<id>` sharing already relies on the same
// forwarding.
//
// Fetching http://127.0.0.1 from an https page is allowed: loopback is a
// "potentially trustworthy" origin, so this is not mixed content.

export interface HandoffTarget {
  port: number;
  nonce: string;
}

/** Query parameter the desktop puts `<port>.<nonce>` in. */
export const HANDOFF_PARAM = "import";

/**
 * Reads `<port>.<nonce>`.
 *
 * Both halves are validated rather than pasted into a URL, because the value is
 * attacker-controlled in the sense that anyone can send a link: the port must
 * be a real ephemeral port and the nonce must look like the base64url the
 * desktop generates, so no other host or path can be reached.
 */
export function parseHandoffValue(value: string | null | undefined): HandoffTarget | null {
  if (!value) return null;
  const match = /^(\d{1,5})\.([A-Za-z0-9_-]{16,64})$/.exec(value);
  if (!match) return null;
  const port = Number(match[1]);
  if (!Number.isInteger(port) || port < 1024 || port > 65535) return null;
  return { port, nonce: match[2] };
}

/**
 * Reads `import/<port>/<nonce>` off the hash route. Kept for the dev server and
 * for anyone opening the app outside the Toy wrapper, where the fragment does
 * survive.
 */
export function parseHandoffRoute(value: string): HandoffTarget | null {
  const match = /^import\/(\d{1,5})\/([A-Za-z0-9_-]{16,64})$/.exec(value);
  if (!match) return null;
  return parseHandoffValue(`${match[1]}.${match[2]}`);
}

/** The record JSON, as text ready for the parse worker. */
export async function fetchHandoffRecord(target: HandoffTarget): Promise<string> {
  const response = await fetch(`http://127.0.0.1:${target.port}/r/${target.nonce}`, {
    cache: "no-store",
    // The desktop end serves exactly one record and closes; a redirect would
    // mean something else answered on that port.
    redirect: "error",
  });
  if (!response.ok) throw new Error(`桌面端返回 ${response.status}`);
  return response.text();
}
