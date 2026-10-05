# -*- coding: utf-8 -*-
"""逐帧追踪(指令 60)的分析器 —— 用来定位"随角色模式下角色走动/跳跃时的小幅抖动"。
只读: 读模块日志里的 [tr] 行, 不做任何写操作。

用法:
    python source\\tools\\analyze_trace.py [日志路径]
默认日志路径: <包根>\\game_mod\\EndfieldCamLink.log

判据(脚本会自己打印结论):
  · 某个信号在"其它信号在动"的帧里大量出现**零步长**(位置一点没变) → 那个信号是**阶梯**,
    它就是把抖动带进画面的那一环。
  · 各信号都在动、但"步长/帧时间"的速度序列抖动很大 → 是**时间相位/帧时间**的问题。
  · 包序号大量重复 / Blender 帧号重复多 → Blender 侧采样与帧推进不同步(发送端的重复采样)。
  · post 与 tgt 差得明显 → 写入没生效(别人在我们之后又写了相机)。
"""
import math
import os
import re
import statistics
import sys

try:
    sys.stdout.reconfigure(encoding="utf-8")
except Exception:
    pass


NUM = re.compile(r"-?\d+(?:\.\d+)?")


def nums(text):
    return [float(x) for x in NUM.findall(text)]


def parse(path):
    rows = []
    with open(path, encoding="utf-8", errors="replace") as fh:
        for line in fh:
            if not line.startswith("[tr] ") or " n=" not in line:
                continue
            parts = line.rstrip("\n").split(" | ")
            if len(parts) < 7:
                continue
            try:
                head = nums(parts[0])                      # n, t, dt
                eng = nums(parts[1])[:3]
                org = nums(parts[2])[:3]
                src = parts[2].rsplit("src=", 1)[-1].strip() if "src=" in parts[2] else "?"
                anc_ok = not parts[3].startswith("anc=--")
                anc = nums(parts[3])[:3]
                tgt = nums(parts[4])[:3]
                post = nums(parts[5])[:3]
                pkt = nums(parts[6])                       # seq, bf
            except Exception:
                continue
            if len(eng) < 3 or len(tgt) < 3:
                continue
            rows.append({
                "n": int(head[0]), "t": int(head[1]), "dt": int(head[2]),
                "eng": eng, "org": org, "src": src,
                "anc_ok": anc_ok, "anc": anc if anc_ok else None,
                "tgt": tgt, "post": post,
                "seq": int(pkt[0]) if pkt else -1, "bf": int(pkt[1]) if len(pkt) > 1 else -999,
            })
    return rows


def dist(a, b):
    return math.sqrt(sum((x - y) ** 2 for x, y in zip(a, b)))


def steps(rows, key):
    """逐帧步长(跳过缺该信号的帧); 返回 [(帧序号, 步长, 该帧的 dt, 该帧其它信号的最大步长)]"""
    out = []
    prev = None
    for r in rows:
        cur = r.get(key)
        if cur is None:
            prev = None
            continue
        if prev is not None:
            out.append((r["n"], dist(cur, prev), r["dt"]))
        prev = cur
    return out


def main():
    pkg = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
    path = sys.argv[1] if len(sys.argv) > 1 else os.path.join(pkg, "game_mod", "EndfieldCamLink.log")
    if not os.path.exists(path):
        print("找不到日志: " + path)
        return 2
    rows = parse(path)
    if len(rows) < 30:
        print("日志里 [tr] 行太少(%d 行) —— 先发一次指令 60 再让角色走动。" % len(rows))
        return 2

    print("=" * 78)
    print("逐帧追踪分析  (%s)" % path)
    print("=" * 78)
    span = (rows[-1]["t"] - rows[0]["t"]) / 1000.0
    print("帧数 %d | 时间跨度 %.1f s | 平均 %.1f 帧/秒" % (len(rows), span, len(rows) / max(span, 1e-6)))
    srcs = sorted({r["src"] for r in rows})
    print("原点来源: %s" % ", ".join(srcs))
    print("锚点读数可用帧: %d / %d" % (sum(1 for r in rows if r["anc_ok"]), len(rows)))

    # ---- 帧时间 ----
    dts = [r["dt"] for r in rows[1:] if r["dt"] > 0]
    if dts:
        mode = statistics.mode(dts) if len(set(dts)) < len(dts) else int(statistics.median(dts))
        odd = sum(1 for d in dts if abs(d - mode) > max(1, mode * 0.35))
        print("\n[帧时间] 中位 %d ms | 众数 %d ms | 最小 %d | 最大 %d | 偏离众数>35%%的帧: %d (%.1f%%)"
              % (int(statistics.median(dts)), mode, min(dts), max(dts), odd, 100.0 * odd / len(dts)))

    # ---- 各信号的逐帧步长 ----
    keys = [("eng", "引擎相机(游戏相机)"), ("org", "本帧原点(锚点+微调)"),
            ("anc", "vcamFollow(角色节点)"), ("tgt", "写入目标"), ("post", "写入后回读")]
    series = {k: steps(rows, k) for k, _ in keys}
    ref = series["tgt"]
    ref_map = {n: s for n, s, _ in ref}

    print("\n[逐帧步长]  单位: 米/帧")
    print("  %-22s %8s %8s %8s %8s %8s" % ("信号", "中位", "p90", "最大", "零步长帧", "动帧中零步长"))
    moving_total = 0
    for k, label in keys:
        s = series[k]
        if not s:
            continue
        vals = sorted(x[1] for x in s)
        zeros = sum(1 for x in s if x[1] < 1e-4)
        # "其它信号在动、自己没动" 的帧数 —— 阶梯的直接证据(以 tgt 为参考)
        stuck = 0
        moving = 0
        for n, v, _ in s:
            r = ref_map.get(n)
            if r is None:
                continue
            if r > 0.005:
                moving += 1
                if v < 1e-4:
                    stuck += 1
        if k == "tgt":
            moving_total = moving
        print("  %-22s %8.4f %8.4f %8.4f %8d %8d / %d" % (
            label, vals[len(vals) // 2], vals[int(len(vals) * 0.9)], vals[-1], zeros, stuck, moving))

    # ---- 速度抖动(仅在目标确实在动的帧上算) ----
    print("\n[速度抖动量]  仅在 |Δtgt| > 5mm 的帧上统计  (步长/帧时间 的 最大:中位 比)")
    for k, label in keys:
        s = series[k]
        if not s:
            continue
        v = [x[1] / max(x[2], 1) for x in s if ref_map.get(x[0], 0.0) > 0.005 and x[2] > 0]
        if len(v) < 5:
            continue
        med = statistics.median(v)
        if med <= 1e-6:
            continue
        print("  %-22s %6.2f  (中位 %.5f, 最大 %.5f, 样本 %d)" % (label, max(v) / med, med, max(v), len(v)))

    # ---- 包侧 ----
    seq = [r["seq"] for r in rows]
    dups = sum(1 for i in range(1, len(seq)) if seq[i] == seq[i - 1])
    gaps = sum(1 for i in range(1, len(seq)) if seq[i] - seq[i - 1] > 2)
    bfs = [r["bf"] for r in rows]
    bf_rep = sum(1 for i in range(1, len(bfs)) if bfs[i] == bfs[i - 1])
    print("\n[包] 序号重复帧 %d (%.1f%%) | 跳号(>1 包)帧 %d | Blender 帧号重复 %d (%.1f%%)"
          % (dups, 100.0 * dups / len(rows), gaps, bf_rep, 100.0 * bf_rep / len(rows)))

    # ---- 写入是否生效 ----
    worst = max(dist(r["tgt"], r["post"]) for r in rows)
    print("[写入] |目标 − 写入后回读| 最大 %.4f m  %s" % (worst, "OK(写入生效)" if worst < 0.002 else "!! 写入未完全生效"))

    # ---- 结论 ----
    print("\n" + "=" * 78)
    print("结论")
    print("=" * 78)
    def stuck_ratio(k):
        s = {n: v for n, v, _ in series[k]}
        tot = 0
        hit = 0
        for n, v, _ in series["tgt"]:
            if v > 0.005:
                tot += 1
                if s.get(n, 1.0) < 1e-4:
                    hit += 1
        return (hit, tot)

    for k, label in (("eng", "引擎相机"), ("org", "原点"), ("anc", "vcamFollow")):
        hit, tot = stuck_ratio(k)
        if tot:
            print("  %-12s 在目标移动的 %d 帧中有 %d 帧完全没动 (%.0f%%)" % (label, tot, hit, 100.0 * hit / tot))
    print("")
    eng_hit, eng_tot = stuck_ratio("eng")
    anc_hit, anc_tot = stuck_ratio("anc")
    if eng_tot and eng_hit / eng_tot > 0.25:
        print("  → 引擎相机(游戏自己的相机)**成片地保持不动再跳一格**: 抖动来自游戏相机的更新粒度/台阶。")
        print("     对策方向: 对位置做亚帧插值(把'保持'补成'斜坡'), 或改用另一个更细粒度的来源。")
    elif anc_tot and anc_hit / anc_tot > 0.25:
        print("  → vcamFollow(角色节点)成片保持不动: 抖动来自角色侧节点的更新粒度。")
    else:
        print("  → 各信号都在逐帧变化, 没有明显'阶梯'; 抖动更可能来自**时间相位/帧时间抖动**。")
        print("     看上面的 [帧时间] 与 [速度抖动量]: 帧时间偏离众数的帧若很多, 就是它。")
    print("")
    return 0


if __name__ == "__main__":
    sys.exit(main())
