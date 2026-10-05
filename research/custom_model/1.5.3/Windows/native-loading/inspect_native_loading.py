"""Read only selected dump declarations and bounded PE code windows; never read a DLL in full."""
from pathlib import Path
import bisect, json, re, struct, subprocess

ROOT = Path(r"F:\Better Endfield")
OUT = ROOT / "artifacts/native-loading-20261003"
DUMP = ROOT / "research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal"
DLL = Path(r"D:\Arknights Endfield\GameAssembly.dll")
OBJDUMP = r"D:\work\MSYS2\mingw64\bin\objdump.exe"
OUT.mkdir(parents=True, exist_ok=True)
symbols = {}
methods = []
for filename in ("Common.Beyond.dll.cs", "UnityEngine.CoreModule.dll.cs", "UnityEngine.AssetBundleModule.dll.cs"):
    typename, token, pending = "", "", None
    with (DUMP / filename).open(encoding="utf-8-sig") as source:
        for number, line in enumerate(source, 1):
            m = re.search(r"TypeToken: (0x[0-9A-Fa-f]+)", line)
            if m:
                token = m[1]
            m = re.match(r"    (?:public|private|internal).*?\b(?:class|struct|interface) (\S+)", line)
            if m:
                typename = m[1]
            m = re.search(r"// RVA: (0x[0-9A-Fa-f]+)", line)
            if m:
                pending = int(m[1], 16)
            elif pending is not None and "{ }" in line:
                signature = line.strip().removesuffix(" { }")
                item = dict(file=filename, line=number, type=typename, type_token=token,
                            rva=pending, signature=signature)
                methods.append(item)
                symbols.setdefault(pending, []).append(f"{typename}: {signature}")
                pending = None
            elif "// RVA: -1" in line:
                pending = None
            if "/* RVA:" in line:
                prop = line.strip().split(" {", 1)[0]
                for accessor, rva in re.findall(r"(get|set); /\* RVA: (0x[0-9A-Fa-f]+)", line):
                    symbols.setdefault(int(rva, 16), []).append(f"{typename}.{accessor}_{prop}")

targets = {
    ("I18NAssetLoader", "LoadAsync("),
    ("ResourceManager", "LoadAsync(Beyond.Resource.StringPathHash path, System.Type type,"),
    ("BundleResourceManager", "LoadAsync(Beyond.Resource.StringPathHash hash, System.Type type,"),
    ("BundleResourceManager", "_LoadAssetInternal(Beyond.Resource.StringPathHash hash,"),
    ("FAssetProxyHandle", "Get()"),
    ("FAssetProxyHandle", "LoadImmediate("),
    ("FAssetProxyHandle", "AddOnProxyCompleted("),
    ("FAssetProxyUntrackedHandle", "Get()"),
    ("AssetProxy", "LoadAsync("),
    ("AssetProxy", "Get()"),
    ("AssetProxy", "UpdateLoading("),
    ("AssetProxy", "_FinishAsyncRequest("),
    ("AssetProxy", "_OnAsyncCompleted("),
    ("AssetProxy", "_FinishWithAsset("),
    ("AssetProxy", "ForceFinishAsyncRequest("),
    ("BundleProxy", "_LoadAssetBundleAsync("),
    ("BundleProxy", "LoadAssetAsync("),
    ("Manager", "UpdateAll("),
    ("Manager", "TailedUpdateAll("),
    ("Manager", "_InvokePendingNextTickOnCompleted("),
    ("Manager", "RegisterOnCompleteInNextFrame("),
    ("Manager", "ConvertToSyncLoad("),
    ("Manager", "_UpdateOperationState("),
    ("Manager", "_UpdateOperationLoadObjectFromBundle("),
    ("Manager", "_LoadAssetProxy("),
    ("Manager", "LoadAsset("),
    ("Manager", "_UpdatePriorityOperationState("),
}

with DLL.open("rb") as source:
    head = source.read(0x400)
    pe = struct.unpack_from("<I", head, 0x3C)[0]
    source.seek(pe)
    header = source.read(24)
    count, optsize = struct.unpack_from("<H", header, 6)[0], struct.unpack_from("<H", header, 20)[0]
    opt = source.read(optsize)
    base = struct.unpack_from("<Q", opt, 24)[0]
    image_size = struct.unpack_from("<I", opt, 56)[0]
    sections = []
    for _ in range(count):
        s = source.read(40)
        vs, va, rs, rp = struct.unpack_from("<IIII", s, 8)
        sections.append((s[:8].rstrip(b"\0").decode(), va, vs, rp, rs))
    def read_rva(rva, size):
        for _, va, vs, rp, rs in sections:
            if va <= rva < va + rs:
                source.seek(rp + rva - va)
                return source.read(min(size, va + rs - rva))
        return b""
    pdata = next(s for s in sections if s[0] == ".pdata")
    # Only the small exception directory, not code/data sections.
    exception = read_rva(pdata[1], pdata[4])
    bounds = [struct.unpack_from("<III", exception, i)[:2] for i in range(0, len(exception) - 11, 12)]
    starts = [x[0] for x in bounds]
    def function_end(rva):
        # x64 .pdata can describe only the first fragment of a function.
        # Extend a bounded window to the next known method/property, never infer
        # that one RUNTIME_FUNCTION entry represents the whole managed method.
        future = [x for x in symbols if rva < x < rva + 0x2000]
        end = min(future, default=rva + 0x1000)
        i = bisect.bisect_right(starts, rva) - 1
        if i >= 0 and bounds[i][0] == rva:
            return min(max(bounds[i][1], end), rva + 0x2000)
        return end
    report = {"dll": str(DLL), "image_base": hex(base), "image_size": hex(image_size),
              "limitations": "Bounded windows may include adjacent code and omit split cold blocks; manually verify call sites before attributing edges. PC static evidence only, not Android ABI or runtime proof.",
              "dump": str(DUMP), "functions": []}
    selected = [m for m in methods if m["file"] == "Common.Beyond.dll.cs" and
                (m["type"] != "Manager" or m["type_token"] == "0x2000335") and
                any(m["type"] == t and (" " + n) in m["signature"] for t, n in targets)]
    for item in selected:
        rva, end = item["rva"], function_end(item["rva"])
        raw = subprocess.check_output([OBJDUMP, "-d", "-M", "intel",
            f"--start-address={base+rva:#x}", f"--stop-address={base+end:#x}", str(DLL)], text=True)
        annotated, calls = [], []
        for line in raw.splitlines():
            m = re.search(r"\b(call|jmp)\s+0x([0-9a-f]+)", line)
            if m:
                target = int(m[2], 16) - base
                names = symbols.get(target, [])
                if names:
                    line += "  ; " + " / ".join(names)
                    calls.append(dict(kind=m[1], target=hex(target), names=names))
            annotated.append(line)
        stem = f"{item['type']}-{rva:08x}"
        (OUT / f"{stem}.asm.txt").write_text("\n".join(annotated), encoding="utf-8")
        report["functions"].append(dict(**item, end=end, evidence=f"{stem}.asm.txt", calls=calls))
    (OUT / "named-method-windows.json").write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding="utf-8")
    for item in report["functions"]:
        print(f"{item['type']} {item['signature']} RVA={item['rva']:#x} dump:{item['line']}")
        for call in item["calls"]:
            print("  ", call["kind"], call["target"], " / ".join(call["names"]))
