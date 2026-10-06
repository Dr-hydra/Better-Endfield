import { readFile, writeFile } from "node:fs/promises";
import path from "node:path";
import { fileURLToPath } from "node:url";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const sourcePath = path.join(root, "supporters.json");
const generatedPath = path.join(root, "src", "supporters.generated.ts");
const source = JSON.parse(await readFile(sourcePath, "utf8"));

if (!/^\d{4}-\d{2}-\d{2}$/.test(source.cutoffDate)) throw new Error("supporters.json cutoffDate must use YYYY-MM-DD");
const totals = new Map();
for (const entry of source.entries) {
  if (!Number.isFinite(entry.amount) || entry.amount < 0) throw new Error("supporters.json contains an invalid amount");
  if (!/^\d{4}-\d{2}-\d{2}$/.test(entry.date)) throw new Error("supporters.json contains an invalid date");
  const name = String(entry.name || "").trim() || "anonymous";
  const current = totals.get(name);
  totals.set(name, {
    amount: (current?.amount || 0) + entry.amount,
    latest: current && current.latest > entry.date ? current.latest : entry.date,
  });
}

const names = [...totals.entries()]
  .map(([name, value]) => ({ name, ...value }))
  .sort((a, b) => b.amount - a.amount || b.latest.localeCompare(a.latest) || a.name.localeCompare(b.name))
  .map(({ name }) => ({ name }));

const output = `// Generated from ../supporters.json. Edit the JSON table, then run mods:build.\nexport const supporterCutoffDate = ${JSON.stringify(source.cutoffDate)};\nexport const supporterNames = ${JSON.stringify(names, null, 2)} as const;\n`;
await writeFile(generatedPath, output, "utf8");
