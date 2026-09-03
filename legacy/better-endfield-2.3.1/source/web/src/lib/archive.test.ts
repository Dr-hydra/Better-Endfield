import { describe, expect, it } from "vitest";
import { decodeArchiveText, encodeArchiveText } from "./archive";

describe("Toy archive codec", () => {
  it("restores the original UTF-8 JSON losslessly", () => {
    const source = JSON.stringify({ schemaVersion: 11, note: "终末地⚙️", effects: Array(200).fill({ type: "damage", value: 12345 }) });
    expect(decodeArchiveText(encodeArchiveText(source))).toBe(source);
  });

  it("compresses a repetitive record below one Toy value", () => {
    const source = JSON.stringify({ schemaVersion: 11, effects: Array(200).fill({ type: "damage", value: 12345 }) });
    expect(encodeArchiveText(source).length).toBeLessThan(960);
  });
});
