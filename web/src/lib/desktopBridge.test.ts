import { describe, it, expect } from "vitest";
import { BRIDGE, createReceiver } from "./desktopBridge";

describe("desktop transport", () => {
  it("transfers 8 MiB without URL or HTTP", () => {
    let result = "";
    const replies: any[] = [];
    const receive = createReceiver((_, text) => { result = text; }, x => replies.push(x));
    const send = (data: object) => receive({ protocol: BRIDGE, version: 1, id: "test", ...data });
    send({ type: "begin", kind: "probe", size: 8 * 1024 * 1024 });
    for (let i = 0; i < 32; i++) send({ type: "chunk", index: i, text: "x".repeat(262144) });
    send({ type: "end" });
    expect(result.length).toBe(8 * 1024 * 1024);
    expect(replies.every(x => x.type === "ack")).toBe(true);
  });
  it("rejects out of order chunks and clears the transfer", () => {
    const replies: any[] = [];
    const receive = createReceiver(() => { throw Error("must not deliver"); }, x => replies.push(x));
    const send = (data: object) => receive({ protocol: BRIDGE, version: 1, id: "test", ...data });
    send({ type: "begin", kind: "combat", size: 1 });
    send({ type: "chunk", index: 1, text: "x" });
    send({ type: "end" });
    expect(replies.slice(1).map(x => x.reason)).toEqual(["invalid_chunk", "unknown_transfer"]);
  });
});
