"""Selected Texture2D constructor windows from existing dump RVAs; no DLL-wide scan."""
from pathlib import Path
import bisect, json, re, subprocess

root = Path(r"F:\Better Endfield")
out = root / "artifacts/native-loading-20261003"
dump = root / "research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal/UnityEngine.CoreModule.dll.cs"
lines = dump.read_text(encoding="utf-8-sig").splitlines()
all_rvas = sorted({int(m[1], 16) for s in lines for m in re.finditer(r"RVA: (0x[0-9A-Fa-f]+)", s)})
signatures = [
    "public System.Void .ctor(System.Int32 width, System.Int32 height, UnityEngine.TextureFormat textureFormat, System.Int32 mipCount, System.Boolean linear)",
    "private System.Void .ctor(System.Int32 width, System.Int32 height, UnityEngine.TextureFormat textureFormat, System.Int32 mipCount, System.Boolean linear, System.IntPtr nativeTex)",
    "public System.Void .ctor(System.Int32 width, System.Int32 height, UnityEngine.Experimental.Rendering.GraphicsFormat format, System.Int32 mipCount, UnityEngine.Experimental.Rendering.TextureCreationFlags flags)",
    "private System.Void .ctor(System.Int32 width, System.Int32 height, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.Experimental.Rendering.TextureCreationFlags flags, System.Int32 mipCount, System.IntPtr nativeTex)",
    "private static System.Boolean Internal_CreateImpl(UnityEngine.Texture2D mono, System.Int32 w, System.Int32 h, System.Int32 mipCount, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.TextureColorSpace colorSpace, UnityEngine.Experimental.Rendering.TextureCreationFlags flags, System.IntPtr nativeTex)",
    "private static System.Void Internal_Create(UnityEngine.Texture2D mono, System.Int32 w, System.Int32 h, System.Int32 mipCount, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.TextureColorSpace colorSpace, UnityEngine.Experimental.Rendering.TextureCreationFlags flags, System.IntPtr nativeTex)",
]
report = []
for signature in signatures:
    matches = [i for i, s in enumerate(lines) if s.strip() == signature + " { }"]
    assert len(matches) == 1, (signature, matches)
    i = matches[0]
    rva = int(re.search(r"RVA: (0x[0-9A-Fa-f]+)", lines[i-1])[1], 16)
    end = min(all_rvas[bisect.bisect_right(all_rvas, rva)], rva + 0x1000)
    raw = subprocess.check_output([r"D:\work\MSYS2\mingw64\bin\objdump.exe", "-d", "-M", "intel",
        f"--start-address={0x180000000+rva:#x}", f"--stop-address={0x180000000+end:#x}",
        r"D:\Arknights Endfield\GameAssembly.dll"], text=True)
    name = f"Texture2D-{rva:08x}.asm.txt"
    (out / name).write_text(raw, encoding="utf-8")
    report.append(dict(signature=signature, dump_line=i+1, rva=hex(rva), window_end=hex(end), evidence=name))
    print(i+1, hex(rva), signature)
(out / "texture-creation-windows.json").write_text(json.dumps(report, indent=2), encoding="utf-8")
print("createUninitialized declarations:", sum("createUninitialized" in s for s in lines))
print("Dont* creation flag declarations:", [s.strip() for s in lines if "DontUploadUponCreate" in s or "DontInitializePixels" in s])
