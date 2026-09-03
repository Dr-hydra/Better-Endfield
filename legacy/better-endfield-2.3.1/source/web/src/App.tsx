import { useEffect, useMemo, useState } from "preact/hooks";
import dictionaryJson from "./data/combat-dict.min.json";
import { CombatDetail } from "./components/CombatDetail";
import { deleteArchive, getOwnerToken, listArchives, loadArchive, saveArchive, type ArchiveMeta } from "./lib/archive";
import { getHome, getPublicRecord, publishRecord } from "./lib/api";
import { dungeonName, formatDuration, formatNumber, parseCombatRecord, totalRdps } from "./lib/combat";
import { recordQrCode, requestToyProfile, shareRecord, type ToyProfile } from "./lib/toy";
import type { CombatDictionary, CombatRecordV11, HomePayload, LeaderboardEntry, Route } from "./types";

const dictionary = dictionaryJson as CombatDictionary;
const WAR_SERIES = "indie_group_twdg";

function parseRoute(): Route {
  const sharedId = new URLSearchParams(location.search).get("r");
  if (sharedId) return { page: "record", id: sharedId };
  const value = location.hash.replace(/^#\/?/, "");
  if (value.startsWith("record/")) return { page: "record", id: value.slice(7) };
  if (value === "analyze") return { page: "analyze" };
  if (value === "archive") return { page: "archive" };
  if (value === "download") return { page: "download" };
  return { page: "home" };
}

function navigate(path: string) {
  location.hash = path === "home" ? "#/" : `#/${path}`;
}

function normalizeBvid(value: string): string {
  const match = value.trim().match(/(?:video\/)?(BV[0-9A-Za-z]{10})/i);
  return match?.[1] ?? "";
}

function demoRows(dungeonId: string): LeaderboardEntry[] {
  const chars = ["chr_0004_pelica", "chr_0005_chen", "chr_0006_wolfgd", "chr_0017_yvonne"];
  return ["工业协议", "塔卫记录", "前线样本", "终末地勤务组"].map((nickname, index) => ({
    shortId: `example-${index}`,
    rank: index + 1,
    nickname,
    durationSeconds: 57.24 + index * 7.83,
    dps: 184320 - index * 17340,
    rdps: 191880 - index * 16620,
    uploadedAt: new Date(Date.now() - index * 86400000).toISOString(),
    dungeonId,
    squad: chars.map((charId, charIndex) => ({ charId, level: 90 - charIndex * 5, potential: 5 - charIndex })),
    example: true,
  }));
}

function imageFallback(event: Event) {
  (event.currentTarget as HTMLImageElement).style.display = "none";
}

export default function App() {
  const [route, setRoute] = useState<Route>(parseRoute);
  const [theme, setTheme] = useState(() => localStorage.getItem("be-theme") || "light");
  const [profile, setProfile] = useState<ToyProfile | null>(null);
  const [record, setRecord] = useState<CombatRecordV11 | null>(null);
  const [rawText, setRawText] = useState("");
  const [sourceLabel, setSourceLabel] = useState("LOCAL / 本地记录");
  const [busy, setBusy] = useState(false);
  const [notice, setNotice] = useState("");
  const [error, setError] = useState("");
  const [qrCode, setQrCode] = useState("");

  useEffect(() => {
    const handler = () => setRoute(parseRoute());
    addEventListener("hashchange", handler);
    return () => removeEventListener("hashchange", handler);
  }, []);

  useEffect(() => {
    document.documentElement.dataset.theme = theme;
    localStorage.setItem("be-theme", theme);
  }, [theme]);

  useEffect(() => {
    if (route.page !== "record" || !route.id) return;
    setBusy(true);
    setError("");
    getPublicRecord(route.id)
      .then((value) => {
        setRecord(value);
        setRawText(JSON.stringify(value));
        setSourceLabel(`PUBLIC / ${route.id}`);
      })
      .catch((reason) => setError(reason instanceof Error ? reason.message : "公开记录读取失败"))
      .finally(() => setBusy(false));
  }, [route]);

  async function login() {
    try {
      setProfile(await requestToyProfile());
      setNotice("Toy 登录成功");
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "登录失败");
    }
  }

  async function importFile(file?: File) {
    if (!file) return;
    if (file.size > 64 * 1024 * 1024) return setError("文件超过 64 MiB，请先确认记录是否完整");
    setBusy(true);
    setError("");
    const text = await file.text();
    const worker = new Worker(new URL("./workers/combat.worker.ts", import.meta.url), { type: "module" });
    worker.onmessage = (event: MessageEvent<{ ok: boolean; record?: CombatRecordV11; error?: string }>) => {
      setBusy(false);
      worker.terminate();
      if (!event.data.ok || !event.data.record) return setError(event.data.error || "解析失败");
      setRawText(text);
      setRecord(event.data.record);
      setSourceLabel("LOCAL / 本地记录");
      navigate("analyze");
    };
    worker.onerror = () => {
      worker.terminate();
      setBusy(false);
      setError("解析工作线程异常");
    };
    worker.postMessage({ text, dictionary });
  }

  async function privateSave() {
    if (!record || !rawText) return;
    setBusy(true);
    setError("");
    try {
      if (!profile) setProfile(await requestToyProfile());
      const meta = await saveArchive(rawText, {
        title: dungeonName(dictionary, record.dungeonId),
        dungeonId: record.dungeonId,
        durationSeconds: record.durationSeconds,
        dps: record.dps,
      });
      setNotice(`已写入私密云存档 ${meta.id}`);
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "云存档写入失败");
    } finally {
      setBusy(false);
    }
  }

  async function publicSave() {
    if (!record || !rawText) return;
    setBusy(true);
    setError("");
    try {
      const currentProfile = profile ?? await requestToyProfile();
      setProfile(currentProfile);
      await saveArchive(rawText, {
        title: dungeonName(dictionary, record.dungeonId),
        dungeonId: record.dungeonId,
        durationSeconds: record.durationSeconds,
        dps: record.dps,
      });
      const bvidInput = window.prompt("可选：输入要绑定的 BVID 或B站视频链接", "") || "";
      const bvid = bvidInput ? normalizeBvid(bvidInput) : "";
      if (bvidInput && !bvid) throw new Error("无法识别该 BVID");
      const result = await publishRecord({
        text: rawText,
        ownerToken: await getOwnerToken(),
        nickname: currentProfile.nickname,
        avatar: currentProfile.avatar,
        bvid,
      });
      setNotice(`公开记录已生成：${result.shortId}`);
      history.replaceState(null, "", `${location.pathname}?r=${encodeURIComponent(result.shortId)}#/record/${result.shortId}`);
      setSourceLabel(`PUBLIC / ${result.shortId}`);
      setRoute({ page: "record", id: result.shortId });
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "公开上传失败");
    } finally {
      setBusy(false);
    }
  }

  async function shareCurrent() {
    if (route.page !== "record" || !route.id) return;
    try {
      await shareRecord(route.id);
      setNotice("已打开分享面板；Web 端则已复制链接");
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "分享失败");
    }
  }

  async function showQr() {
    if (route.page !== "record" || !route.id) return;
    try {
      const result = await recordQrCode(route.id);
      setQrCode(result.base64);
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "二维码生成失败");
    }
  }

  const navItems = [
    ["home", "排行榜"],
    ["analyze", "战斗解析"],
    ["archive", "云存档"],
    ["download", "软件下载"],
  ] as const;

  return (
    <div class="app-shell">
      <aside class="side-rail">
        <button class="brand" onClick={() => navigate("home")} aria-label="回到排行榜">
          <span>BE</span><b>BETTER<br />ENDFIELD</b>
        </button>
        <nav>{navItems.map(([path, label], index) => <button class={route.page === path ? "active" : ""} onClick={() => navigate(path)} key={path}><i>{String(index + 1).padStart(2, "0")}</i><span>{label}</span></button>)}</nav>
        <div class="rail-bottom">
          <button class="theme-toggle" onClick={() => setTheme(theme === "light" ? "dark" : "light")} aria-label="切换明暗主题">{theme === "light" ? "◐" : "◑"}</button>
          <span>SCHEMA<br /><b>08</b></span>
        </div>
      </aside>
      <header class="mobile-header">
        <button class="brand compact" onClick={() => navigate("home")}><span>BE</span><b>BETTER ENDFIELD</b></button>
        <button class="theme-toggle" onClick={() => setTheme(theme === "light" ? "dark" : "light")}>◐</button>
      </header>
      <div class="top-status">
        <span><i class="status-dot" /> DATA LINK / {import.meta.env.VITE_CLOUDBASE_API ? "ONLINE" : "LOCAL PREVIEW"}</span>
        {profile ? <button class="profile-button"><img src={profile.avatar} alt="" />{profile.nickname}</button> : <button onClick={login}>TOY 登录</button>}
      </div>
      <div class="page-content">
        {route.page === "home" && <HomePage dictionary={dictionary} onOpen={(id) => navigate(`record/${id}`)} />}
        {route.page === "analyze" && (record ? <CombatDetail record={record} dictionary={dictionary} sourceLabel={sourceLabel} actions={<>
          <button class="button secondary" onClick={privateSave} disabled={busy}>私密云存档</button>
          <button class="button primary" onClick={publicSave} disabled={busy}>公开并参加排行</button>
        </>} /> : <ImportPage onImport={importFile} busy={busy} />)}
        {route.page === "record" && (record ? <CombatDetail record={record} dictionary={dictionary} sourceLabel={sourceLabel} actions={<>
          <button class="button secondary" onClick={showQr}>二维码</button><button class="button primary" onClick={shareCurrent}>分享记录</button>
        </>} /> : <LoadingState busy={busy} error={error} />)}
        {route.page === "archive" && <ArchivePage onOpen={(text) => {
          try { setRecord(parseCombatRecord(text)); setRawText(text); setSourceLabel("PRIVATE / Toy 云存档"); navigate("analyze"); }
          catch (reason) { setError(reason instanceof Error ? reason.message : "存档解析失败"); }
        }} onError={setError} />}
        {route.page === "download" && <DownloadPage />}
      </div>
      <nav class="mobile-nav">{navItems.map(([path, label]) => <button class={route.page === path ? "active" : ""} onClick={() => navigate(path)} key={path}>{label}</button>)}</nav>
      {busy && <div class="busy-bar" />}
      {(notice || error) && <div class={`toast ${error ? "error" : ""}`} role="status"><span>{error || notice}</span><button onClick={() => { setNotice(""); setError(""); }}>×</button></div>}
      {qrCode && <div class="modal-backdrop" onClick={() => setQrCode("")}><div class="qr-modal" onClick={(event) => event.stopPropagation()}><span class="eyebrow">SCAN / 扫码查看</span><img src={qrCode} alt="当前战斗记录二维码" /><button class="button secondary" onClick={() => setQrCode("")}>关闭</button></div></div>}
    </div>
  );
}

function HomePage({ dictionary, onOpen }: { dictionary: CombatDictionary; onOpen: (id: string) => void }) {
  const series = Object.entries(dictionary.ds);
  const initialSeries = dictionary.ds[WAR_SERIES] ? WAR_SERIES : series[0]?.[0] || "";
  const [seriesId, setSeriesId] = useState(initialSeries);
  const dungeonOptions = useMemo(() => Object.entries(dictionary.d).filter(([, value]) => typeof value !== "string" && value.s === seriesId), [dictionary, seriesId]);
  const [dungeonId, setDungeonId] = useState(dungeonOptions.at(-1)?.[0] || "");
  const [sort, setSort] = useState<"dps" | "time">("dps");
  const [home, setHome] = useState<HomePayload | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const options = Object.entries(dictionary.d).filter(([, value]) => typeof value !== "string" && value.s === seriesId);
    if (!options.some(([id]) => id === dungeonId)) setDungeonId(options.at(-1)?.[0] || "");
  }, [dictionary, dungeonId, seriesId]);

  useEffect(() => {
    if (!dungeonId || !import.meta.env.VITE_CLOUDBASE_API) return setHome(null);
    setLoading(true);
    getHome(dungeonId).then(setHome).catch(() => setHome(null)).finally(() => setLoading(false));
  }, [dungeonId]);

  const rows = home?.[sort]?.length ? home[sort] : demoRows(dungeonId);
  return <main class="home-page">
    <section class="leaderboard-hero">
      <div><span class="eyebrow">COMBAT INTELLIGENCE / 战斗情报</span><h1>把每一次操作，<br /><em>拆成可验证的数据。</em></h1><p>schema 11 可验证操作与结果事件 · DPS / rDPS 归因 · 分轨时间轴</p></div>
      <div class="hero-mark"><b>08</b><span>LIVE<br />ANALYSIS</span></div>
    </section>
    <section class="leaderboard panel">
      <div class="section-heading leaderboard-title"><div><span class="eyebrow">RANKING / 实战排行</span><h2>{dictionary.ds[seriesId] || "关卡排行榜"}</h2></div><span class="refresh-state">{loading ? "SYNCING" : home ? "UPDATED" : "DEMO DATA"}</span></div>
      <div class="leaderboard-controls">
        <label><span>关卡类型</span><select value={seriesId} onChange={(event) => setSeriesId(event.currentTarget.value)}>{series.map(([id, name]) => <option value={id} key={id}>{name}</option>)}</select></label>
        <label><span>具体关卡</span><select value={dungeonId} onChange={(event) => setDungeonId(event.currentTarget.value)}>{dungeonOptions.map(([id]) => <option value={id} key={id}>{dungeonName(dictionary, id)}</option>)}</select></label>
        <div class="sort-tabs"><button class={sort === "dps" ? "active" : ""} onClick={() => setSort("dps")}>DPS</button><button class={sort === "time" ? "active" : ""} onClick={() => setSort("time")}>用时</button></div>
      </div>
      <div class="rank-table">
        <div class="rank-head"><span>RANK</span><span>玩家 / 队伍</span><span>通关用时</span><span>DPS</span><span>rDPS</span><span>记录</span></div>
        {rows.slice(0, 50).map((entry) => <button class="rank-row" key={entry.shortId} disabled={entry.example} onClick={() => onOpen(entry.shortId)}>
          <strong class={entry.rank <= 3 ? "top-rank" : ""}>{String(entry.rank).padStart(2, "0")}</strong>
          <span class="player-cell"><span class="mini-avatar">{entry.avatar && <img src={entry.avatar} alt="" onError={imageFallback} />}{entry.nickname.slice(0, 1)}</span><span><b>{entry.nickname}</b><small>{entry.squad.map((item) => {
            const char = dictionary.c[item.charId]; const data = typeof char === "string" ? null : char;
            return <span class="team-face" key={item.charId}>{data?.i && <img src={`./${data.i}`} alt={data.n} onError={imageFallback} />}</span>;
          })}</small></span></span>
          <b>{formatDuration(entry.durationSeconds)}</b><b>{formatNumber(entry.dps)}</b><b class="rdps-value">{formatNumber(entry.rdps)}</b><span>{entry.example ? "示例" : new Date(entry.uploadedAt).toLocaleDateString("zh-CN")}</span>
        </button>)}
      </div>
    </section>
    <section class="feature-strip"><article><b>01</b><span>本地解析</span><p>JSON 只在浏览器工作线程中解析。</p></article><article><b>02</b><span>贡献归因</span><p>区分直接伤害与队友增益贡献。</p></article><article><b>03</b><span>逐帧复盘</span><p>把技能、Buff 与失衡窗口放进同一时间轴。</p></article></section>
  </main>;
}

function ImportPage({ onImport, busy }: { onImport: (file?: File) => void; busy: boolean }) {
  return <main class="import-page"><section class="import-hero panel"><span class="eyebrow">LOCAL PARSER / 本地解析</span><h1>导入 schema 11<br />战斗记录</h1><p>文件在浏览器本地解析。只有当你选择公开上传时，记录才会发送到 CloudBase。</p><label class="drop-zone desktop-only"><input type="file" accept="application/json,.json" disabled={busy} onChange={(event) => onImport(event.currentTarget.files?.[0])} /><b>选择 JSON 文件</b><span>或拖放最新版 Better Endfield 战斗记录</span><small>仅 schema 11 · 最大 64 MiB</small></label><div class="mobile-only mobile-disabled"><b>手机版不提供 JSON 解析</b><p>请从软件内跳转至 PC 浏览器完成解析；手机仍可查看分享记录。</p></div></section></main>;
}

function ArchivePage({ onOpen, onError }: { onOpen: (text: string) => void; onError: (message: string) => void }) {
  const [archives, setArchives] = useState<ArchiveMeta[]>([]);
  const [loading, setLoading] = useState(false);
  async function refresh() { setLoading(true); try { await requestToyProfile(); setArchives(await listArchives()); } catch (reason) { onError(reason instanceof Error ? reason.message : "云存档读取失败"); } finally { setLoading(false); } }
  return <main class="archive-page"><section class="page-heading"><span class="eyebrow">TOY CLOUD / 私密存档</span><h1>云存档管理</h1><p>无损压缩、按需分片；空间不足时由你决定删除哪一条。</p><button class="button primary" onClick={refresh} disabled={loading}>{loading ? "读取中…" : "登录并读取"}</button></section><section class="archive-list panel">{archives.length ? archives.map((item) => <article key={item.id}><div><span class="eyebrow">{item.id}</span><h3>{item.title}</h3><p>{new Date(item.createdAt).toLocaleString("zh-CN")} · {formatDuration(item.durationSeconds)} · {formatNumber(item.dps)} DPS</p></div><span>{item.parts} 片<br />{(item.bytes / 1024).toFixed(1)} KiB</span><div><button class="button secondary" onClick={async () => { try { onOpen(await loadArchive(item.id)); } catch (reason) { onError(reason instanceof Error ? reason.message : "读取失败"); } }}>打开</button><button class="text-danger" onClick={async () => { if (!confirm(`确定删除“${item.title}”吗？此操作无法恢复。`)) return; try { await deleteArchive(item.id); await refresh(); } catch (reason) { onError(reason instanceof Error ? reason.message : "删除失败"); } }}>删除</button></div></article>) : <div class="empty-state"><b>尚未读取云存档</b><p>登录 Toy 后，此处会显示当前账号在本 Toy 下保存的记录。</p></div>}</section></main>;
}

function DownloadPage() {
  return <main class="download-page"><section class="download-hero"><div><span class="eyebrow">BETTER ENDFIELD / DESKTOP</span><h1>战斗数据，<br />从游戏内一直延伸到复盘。</h1><p>实时伤害统计、rDPS 贡献归因、战斗历史、桌面覆盖层，以及一键跳转网页分析。</p><a class="button primary large" href="https://github.com/Dr-hydra/Better-Endfield/releases/latest" target="_blank" rel="noreferrer">前往 GitHub 下载 ↗</a></div><div class="software-card"><span>WINDOWS / LATEST</span><b>BE</b><ul><li>实时 DPS 与 rDPS</li><li>队伍配置快照</li><li>战斗历史与 JSON 导出</li><li>网页时间轴复盘</li></ul></div></section></main>;
}

function LoadingState({ busy, error }: { busy: boolean; error: string }) {
  return <main class="loading-page"><section class="panel empty-state"><b>{busy ? "正在读取战斗记录…" : error || "记录不存在"}</b><p>{busy ? "仅在打开详情时请求完整 JSON。" : "请检查分享链接或返回排行榜。"}</p><button class="button secondary" onClick={() => navigate("home")}>返回排行榜</button></section></main>;
}
