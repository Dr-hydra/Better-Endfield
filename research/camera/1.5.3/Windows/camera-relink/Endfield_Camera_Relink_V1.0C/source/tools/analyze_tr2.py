# -*- coding: utf-8 -*-
"""分析 [tr2] dump: 帧时间分布 / 无间隙双读 / 真滞后 / 逐帧步长。
只读模块日志, 不做任何写操作。默认分析**最后一次** dump。
用法: python source\\tools\\analyze_tr2.py [日志路径]
"""
import math
import re
import sys
from collections import Counter

try:
    sys.stdout.reconfigure(encoding="utf-8")
except Exception:
    pass

PATH = sys.argv[1] if len(sys.argv) > 1 else r"D:\EndfieldCameraLink\game_mod\EndfieldCamLink.log"
NUM = re.compile(r"-?\d+(?:\.\d+)?")


def nums(s):
    """取字符串里的数字; 含括号时只取括号内的(避开 a1=/a2=/a3= 里的序号)。"""
    if "(" in s:
        s = s.split("(", 1)[1]
    return [float(x) for x in NUM.findall(s)]


def parse(path):
    blocks = []
    cur = None
    with open(path, encoding="utf-8", errors="replace") as fh:
        for line in fh:
            if "[tr2] === dump" in line and "结束" not in line:
                cur = []
                blocks.append(cur)
                continue
            if "[tr2] === dump 结束" in line:
                cur = None
                continue
            if cur is None or not line.startswith("[tr2] "):
                continue
            p = line.rstrip("\n").split(" | ")
            if len(p) < 8:
                continue
            head = nums(p[0].replace("[tr2]", " "))
            if len(head) < 6:
                continue
            cur.append(dict(i=int(head[0]), dtUs=int(head[1]), seq=int(head[2]), bf=int(head[3]),
                            src=int(head[4]), late=int(head[5]),
                            eng=nums(p[1])[:3],
                            a1=nums(p[2])[:3], a1L="a1=L" in p[2],
                            a2=nums(p[3])[:3], a2L="a2=L" in p[3],
                            aU=nums(p[4])[:3], aL=nums(p[5])[:3],
                            a3=nums(p[6])[:3], a3L="a3=L" in p[6],
                            tgt=nums(p[7])[:3],
                            post=nums(p[8])[:3] if len(p) > 8 else [0.0, 0.0, 0.0]))
    return [b for b in blocks if b]


def dist(a, b):
    return math.sqrt(sum((x - y) ** 2 for x, y in zip(a, b)))


def stat(name, vals, unit=""):
    if not vals:
        print("  %-28s (无样本)" % name)
        return
    v = sorted(vals)
    print("  %-28s 中位 %.4f  p10 %.4f  p90 %.4f  最大 %.4f %s"
          % (name, v[len(v) // 2], v[int(len(v) * 0.1)], v[int(len(v) * 0.9)], v[-1], unit))


def main():
    blocks = parse(PATH)
    print("=" * 78)
    print("逐帧追踪分析(低开销版)  %s" % PATH)
    print("=" * 78)
    print("dump 块数 %d | 各块帧数 %s" % (len(blocks), [len(b) for b in blocks]))
    if not blocks:
        print("没有 [tr2] dump: 先发指令 60 → 走动 → 指令 61")
        return 0
    rows = blocks[-1]
    print("分析最后一块: %d 帧 | late 开关=%s | 原点来源=%s"
          % (len(rows), sorted({r["late"] for r in rows}), sorted({r["src"] for r in rows})))
    span_s = sum(r["dtUs"] for r in rows[1:]) / 1e6
    print("覆盖时长 ≈ %.1f s | 平均调用间隔 %.2f ms (%.0f 次/秒)"
          % (span_s, 1000.0 * span_s / max(1, len(rows) - 1), (len(rows) - 1) / max(span_s, 1e-6)))

    print("\n=== 帧时间(调用间隔, QPC 微秒) ===")
    dt = [r["dtUs"] for r in rows[1:] if r["dtUs"] > 0]
    stat("dtUs", dt, "us")
    if dt:
        c = Counter(dt)
        med = sorted(dt)[len(dt) // 2]
        print("  最常见值: %s" % ", ".join("%dus×%d" % (k, v) for k, v in c.most_common(5)))
        print("  偏离中位 >20%% 的帧: %d (%.1f%%) | >2× 中位: %d"
              % (sum(1 for x in dt if abs(x - med) > 0.2 * med),
                 100.0 * sum(1 for x in dt if abs(x - med) > 0.2 * med) / len(dt),
                 sum(1 for x in dt if x > 2 * med)))

    print("\n=== 无间隙双读 |a1 − a2| (两次紧邻读之间零工作) ===")
    dd_all = [dist(r["a1"], r["a2"]) for r in rows]
    stat("|a1-a2|", dd_all, "m")
    print("  非零(>1mm)占比 %.1f%% | a1/a2 实时率 %d/%d, %d/%d"
          % (100.0 * sum(1 for x in dd_all if x > 0.001) / len(dd_all),
             sum(1 for r in rows if r["a1L"]), len(rows),
             sum(1 for r in rows if r["a2L"]), len(rows)))

    print("\n=== 真滞后 |aU − a3| (零 I/O 下 原点用的值 vs 末尾读) ===")
    stat("|aU-a3|", [dist(r["aU"], r["a3"]) for r in rows], "m")

    print("\n=== 逐帧步长(米) ===")
    for key, label in (("eng", "引擎相机"), ("a1", "锚点(调用开头 a1)"),
                       ("aU", "原点用的锚点"), ("a3", "末尾读锚点"), ("tgt", "写入目标"),
                       ("post", "写入后回读")):
        s = [dist(rows[i][key], rows[i - 1][key]) for i in range(1, len(rows))]
        stat(label, s)

    print("\n=== 移动帧(以 a3 逐帧位移 > 3cm 判定) ===")
    mv = []
    for i in range(1, len(rows)):
        step = dist(rows[i]["a3"], rows[i - 1]["a3"])
        if step > 0.03:
            mv.append((rows[i], step))
    print("  移动帧: %d / %d" % (len(mv), len(rows) - 1))
    if mv:
        stat("移动帧 位移", [s for _, s in mv], "m")
        stat("移动帧 双读 |a1-a2|", [dist(r["a1"], r["a2"]) for r, _ in mv], "m")
        stat("移动帧 |aU-a3|", [dist(r["aU"], r["a3"]) for r, _ in mv], "m")
        rr = sorted(dist(r["aU"], r["a3"]) / s for r, s in mv if s > 1e-6)
        print("  |aU-a3| / 位移 比值: 中位 %.3f   (≈1 = 落后一整拍, ≈0 = 不落后)" % rr[len(rr) // 2])
        if any(r["late"] for r, _ in mv):
            stat("移动帧 |aL-a3| (补读 vs 末尾读)", [dist(r["aL"], r["a3"]) for r, _ in mv], "m")
            rr2 = sorted(dist(r["aL"], r["a3"]) / s for r, s in mv if s > 1e-6)
            print("  |aL-a3| / 位移 比值: 中位 %.4f  ← 补读生效时应≈0" % rr2[len(rr2) // 2])
        dts = sorted(r["dtUs"] for r, _ in mv)
        print("  移动帧 dtUs: 中位 %d  p10 %d  p90 %d"
              % (dts[len(dts) // 2], dts[int(len(dts) * 0.1)], dts[int(len(dts) * 0.9)]))
    print("\n样例(前 6 个移动帧):")
    for r, s in mv[:6]:
        print("  i=%d dtUs=%d | a1=%s(%.3f,%.3f,%.3f) | a2=(%.3f,%.3f,%.3f) | aU=(%.3f,%.3f,%.3f) | "
              "a3=(%.3f,%.3f,%.3f) | step=%.3f | tgt=(%.3f,%.3f,%.3f)"
              % (r["i"], r["dtUs"], "L" if r["a1L"] else "C", r["a1"][0], r["a1"][1], r["a1"][2],
                 r["a2"][0], r["a2"][1], r["a2"][2], r["aU"][0], r["aU"][1], r["aU"][2],
                 r["a3"][0], r["a3"][1], r["a3"][2], s, r["tgt"][0], r["tgt"][1], r["tgt"][2]))
    return 0


if __name__ == "__main__":
    sys.exit(main())
