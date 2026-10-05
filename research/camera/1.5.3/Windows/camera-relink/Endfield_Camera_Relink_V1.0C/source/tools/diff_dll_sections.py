# -*- coding: utf-8 -*-
"""按 PE 节区比较两个 DLL 的差异分布。

为什么需要它:
    本项目用 MinGW-w64 g++ -shared -static 构建的 EndfieldCamLink.dll **不是可复现构建** ——
    同一份源码连编两次, 逐字节也会差 ~6500 处(文件长度完全相同)。所以"新构建 vs 旧成品"
    直接逐字节比对**不能**用来判断源码有没有被改动, 必须先把差异按节区分类:
      · 如果差异只集中在少数节区(头部/校验/调试目录…), 而 .text / .rdata 完全一致
        → 两份产物在代码与常量上等价, 源码没有被改动;
      · 如果 .text 里也有大量差异 → 源码确实变了(或被换了编译器/选项)。

用法:
    python diff_dll_sections.py A.dll B.dll [--blocks 16]

输出:
    每个节区一行: 差异字节数 / 节区大小 / 行内直方图; 末尾给出结论。
"""
import argparse
import struct
import sys


def read_sections(path):
    """解析 PE 节表, 返回 [(name, raw_off, raw_size, vsize, va)]"""
    with open(path, "rb") as f:
        data = f.read()
    if data[:2] != b"MZ":
        raise SystemExit("%s 不是 PE 文件" % path)
    pe_off = struct.unpack_from("<I", data, 0x3C)[0]
    if data[pe_off:pe_off + 4] != b"PE\0\0":
        raise SystemExit("%s 找不到 PE 签名" % path)
    nsec = struct.unpack_from("<H", data, pe_off + 6)[0]
    opt_size = struct.unpack_from("<H", data, pe_off + 20)[0]
    sec_off = pe_off + 24 + opt_size
    out = []
    for i in range(nsec):
        o = sec_off + 40 * i
        name = data[o:o + 8].rstrip(b"\0").decode("ascii", "replace")
        vsize, va, raw_size, raw_off = struct.unpack_from("<IIII", data, o + 8)
        out.append((name, raw_off, raw_size, vsize, va))
    return data, out


def diff_hist(a, b, blocks):
    """返回 {(start,end): 差异字节数} 以及总差异"""
    n = min(len(a), len(b))
    step = max(1, n // blocks)
    hist = []
    for start in range(0, n, step):
        end = min(n, start + step)
        d = sum(1 for i in range(start, end) if a[i] != b[i])
        hist.append((start, end, d))
    total = sum(h[2] for h in hist)
    if len(a) != len(b):
        total += abs(len(a) - len(b))
    return hist, total


def bar(frac, width=24):
    fill = int(round(frac * width))
    return "#" * fill + "." * (width - fill)


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("a")
    ap.add_argument("b")
    ap.add_argument("--blocks", type=int, default=16)
    args = ap.parse_args()

    da, secs = read_sections(args.a)
    db, _ = read_sections(args.b)
    print("A = %s  (%d 字节)" % (args.a, len(da)))
    print("B = %s  (%d 字节)" % (args.b, len(db)))
    if len(da) != len(db):
        print("!! 长度不同: 差 %d 字节 —— 源码/选项确实变了" % (len(da) - len(db)))
    hist, total = diff_hist(da, db, args.blocks)
    print("全文件差异 = %d 字节 (%.3f%%)" % (total, 100.0 * total / max(1, len(da))))
    print()
    print("%-10s %10s %10s %8s  %s" % ("节区", "差异字节", "节区大小", "差异率", "分布"))
    header_end = 0
    for name, off, size, vsize, va in secs:
        a = da[off:off + size]
        b = db[off:off + size]
        d = sum(1 for i in range(min(len(a), len(b))) if a[i] != b[i])
        print("%-10s %10d %10d %7.2f%%  %s" % (name, d, size, 100.0 * d / max(1, size),
                                               bar(d / max(1, size))))
        header_end = max(header_end, off + size)
    # 节区之外(PE 头/节表/对齐填充)
    outside = 0
    covered = []
    for name, off, size, vsize, va in secs:
        covered.append((off, off + size))
    n = min(len(da), len(db))
    for i in range(n):
        if any(s <= i < e for s, e in covered):
            continue
        if da[i] != db[i]:
            outside += 1
    print("%-10s %10d" % ("(非节区)", outside))
    print()
    # 结论
    text_diff = 0
    rdata_diff = 0
    for name, off, size, vsize, va in secs:
        a = da[off:off + size]
        b = db[off:off + size]
        d = sum(1 for i in range(min(len(a), len(b))) if a[i] != b[i])
        if name.startswith(".text"):
            text_diff += d
        if name.startswith(".rdata") or name.startswith(".rodata"):
            rdata_diff += d
    print("== 结论 ==")
    print("  .text  差异 = %d 字节" % text_diff)
    print("  .rdata 差异 = %d 字节" % rdata_diff)
    if len(da) == len(db) and text_diff == 0 and rdata_diff == 0:
        print("  → 代码段与只读数据段**逐字节一致**: 两份产物在代码/常量上等价")
    else:
        print("  → 代码段或只读数据段有差异: 需要人工确认(可能是编译器非确定性, 也可能是源码变化)")


if __name__ == "__main__":
    sys.exit(main())
