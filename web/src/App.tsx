import { useEffect, useState } from "preact/hooks";
import dictionaryJson from "./data/combat-dict.min.json";
import { CombatDetail } from "./components/CombatDetail";
import { GachaPage } from "./components/GachaPage";
import { getPublicRecord } from "./lib/api";
import { parseCombatRecord } from "./lib/combat";
import { decodeGachaSnapshot, isGachaSnapshotFragment } from "./lib/gacha";
import { loadGachaCloudSnapshot, saveGachaCloudSnapshot } from "./lib/gachaCloud";
import { recordQrCode, requestToyProfile, shareRecord, type ToyProfile } from "./lib/toy";
import type { CombatDictionary, CombatRecordV11, GachaWebSnapshot, Route } from "./types";

const dictionary = dictionaryJson as CombatDictionary;

function parseRoute(): Route {
  const params = new URLSearchParams(location.search);
  const sharedId = params.get("r");
  if (sharedId) return { page: "record", id: sharedId };
  if (params.get("mode") === "gacha" || isGachaSnapshotFragment(location.hash)) return { page: "gacha" };
  if (params.get("mode") === "combat") return { page: "analyze" };
  const value = location.hash.replace(/^#\/?/, "");
  if (value.startsWith("record/")) return { page: "record", id: value.slice(7) };
  // The public ranking and combat cloud archive are held back for this release.
  if (value === "combat" || value === "archive") return { page: "home" };
  if (value === "gacha") return { page: "gacha" };
  if (value === "analyze") return { page: "analyze" };
  if (value === "download") return { page: "download" };
  return { page: "home" };
}

function navigate(path: string) {
  location.hash = path === "home" ? "#/" : `#/${path}`;
}

export default function App() {
  const [route, setRoute] = useState<Route>(parseRoute);
  const [theme, setTheme] = useState(() => localStorage.getItem("be-theme") || "light");
  const [railCollapsed, setRailCollapsed] = useState(() => localStorage.getItem("be-rail-collapsed") === "1");
  const [profile, setProfile] = useState<ToyProfile | null>(null);
  const [record, setRecord] = useState<CombatRecordV11 | null>(null);
  const [sourceLabel, setSourceLabel] = useState("LOCAL / 本地记录");
  const [busy, setBusy] = useState(false);
  const [notice, setNotice] = useState("");
  const [error, setError] = useState("");
  const [qrCode, setQrCode] = useState("");
  const [gachaSnapshot, setGachaSnapshot] = useState<GachaWebSnapshot | null>(null);
  const [gachaError, setGachaError] = useState("");
  const [gachaSyncing, setGachaSyncing] = useState(false);
  const [gachaAutoSyncKey, setGachaAutoSyncKey] = useState("");

  useEffect(() => {
    const handler = () => setRoute(parseRoute());
    addEventListener("hashchange", handler);
    return () => removeEventListener("hashchange", handler);
  }, []);

  useEffect(() => {
    // Toy exposes the existing login session without requiring a second sign-in flow.
    requestToyProfile().then(setProfile).catch(() => undefined);
  }, []);

  useEffect(() => {
    if (route.page !== "gacha" || !isGachaSnapshotFragment(location.hash)) return;
    try {
      setGachaSnapshot(decodeGachaSnapshot(location.hash));
      setGachaError("");
      history.replaceState(null, "", `${location.pathname}#/gacha`);
    } catch (reason) {
      setGachaSnapshot(null);
      setGachaError(reason instanceof Error ? reason.message : "寻访快照读取失败");
    }
  }, [route]);

  async function loadGachaCloud() {
    setGachaSyncing(true);
    setGachaError("");
    try {
      const cloud = await loadGachaCloudSnapshot();
      if (!cloud) throw new Error("Toy 云端尚未保存寻访记录");
      setGachaSnapshot(cloud);
      setNotice("已读取云端寻访记录");
    } catch (reason) {
      setGachaError(reason instanceof Error ? reason.message : "寻访云存档读取失败");
    } finally {
      setGachaSyncing(false);
    }
  }

  async function saveGachaCloud() {
    if (!gachaSnapshot) return;
    setGachaSyncing(true);
    setGachaError("");
    try {
      const result = await saveGachaCloudSnapshot(gachaSnapshot);
      setGachaSnapshot(result.snapshot);
      setNotice(result.changed ? "寻访记录已增量合并并保存" : "寻访云端记录已是最新");
    } catch (reason) {
      setGachaError(reason instanceof Error ? reason.message : "寻访云存档同步失败");
    } finally {
      setGachaSyncing(false);
    }
  }

  useEffect(() => {
    if (route.page !== "gacha" || !profile || gachaSyncing) return;
    const key = gachaSnapshot ? `snapshot:${gachaSnapshot.createdAt}:${gachaSnapshot.pools.length}` : "cloud-only";
    if (gachaAutoSyncKey === key) return;
    setGachaAutoSyncKey(key);
    if (gachaSnapshot) void saveGachaCloud();
    else void loadGachaCloud();
  }, [route.page, profile, gachaSnapshot, gachaSyncing, gachaAutoSyncKey]);

  useEffect(() => {
    document.documentElement.dataset.theme = theme;
    localStorage.setItem("be-theme", theme);
  }, [theme]);

  useEffect(() => {
    localStorage.setItem("be-rail-collapsed", railCollapsed ? "1" : "0");
  }, [railCollapsed]);

  useEffect(() => {
    if (route.page !== "record" || !route.id) return;
    setBusy(true);
    setError("");
    getPublicRecord(route.id)
      .then((value) => {
        setRecord(value);
        setSourceLabel(`PUBLIC / ${route.id}`);
      })
      .catch((reason) => setError(reason instanceof Error ? reason.message : "公开记录读取失败"))
      .finally(() => setBusy(false));
  }, [route]);

  async function login() {
    try {
      setProfile(await requestToyProfile());
      setGachaError("");
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
    ["home", "首页"],
    ["analyze", "战斗解析"],
    ["gacha", "寻访统计"],
    ["download", "软件下载"],
  ] as const;

  return (
    <div class={`app-shell ${railCollapsed ? "rail-collapsed" : ""}`}>
      <aside class="side-rail">
        <button class="brand" onClick={() => navigate("home")} aria-label="回到首页">
          <span>BE</span><b>BETTER<br />ENDFIELD</b>
        </button>
        <button class="rail-toggle" onClick={() => setRailCollapsed((value) => !value)} aria-label={railCollapsed ? "展开导航" : "收起导航"} title={railCollapsed ? "展开导航" : "收起导航"}>{railCollapsed ? "›" : "‹"}</button>
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
        {route.page === "home" && <HomePage onNavigate={navigate} />}
        {route.page === "analyze" && (record ? <CombatDetail record={record} dictionary={dictionary} sourceLabel={sourceLabel} actions={<>
          <span class="data-note">当前版本仅提供本地解析</span>
        </>} /> : <ImportPage onImport={importFile} busy={busy} />)}
        {route.page === "record" && (record ? <CombatDetail record={record} dictionary={dictionary} sourceLabel={sourceLabel} actions={<>
          <button class="button secondary" onClick={showQr}>二维码</button><button class="button primary" onClick={shareCurrent}>分享记录</button>
        </>} /> : <LoadingState busy={busy} error={error} />)}
        {route.page === "download" && <DownloadPage />}
        {route.page === "gacha" && <GachaPage snapshot={gachaSnapshot} error={gachaError} profileReady={Boolean(profile)} onLogin={login} onLoadCloud={gachaSnapshot ? saveGachaCloud : loadGachaCloud} onSaveCloud={gachaSnapshot ? saveGachaCloud : undefined} busy={gachaSyncing} />}
      </div>
      <nav class="mobile-nav">{navItems.map(([path, label]) => <button class={route.page === path ? "active" : ""} onClick={() => navigate(path)} key={path}>{label}</button>)}</nav>
      {busy && <div class="busy-bar" />}
      {(notice || error) && <div class={`toast ${error ? "error" : ""}`} role="status"><span>{error || notice}</span><button onClick={() => { setNotice(""); setError(""); }}>×</button></div>}
      {qrCode && <div class="modal-backdrop" onClick={() => setQrCode("")}><div class="qr-modal" onClick={(event) => event.stopPropagation()}><span class="eyebrow">SCAN / 扫码查看</span><img src={qrCode} alt="当前战斗记录二维码" /><button class="button secondary" onClick={() => setQrCode("")}>关闭</button></div></div>}
    </div>
  );
}

function HomePage({ onNavigate }: { onNavigate: (path: string) => void }) {
  return <main class="calendar-home">
    <section class="calendar-hero"><img src="./version-calendar.png" alt="雪松幽梦版本日历" /></section>
    <section class="home-entry-grid">
      <button class="home-entry combat" onClick={() => onNavigate("analyze")}><span class="eyebrow">01 / COMBAT DATA</span><strong>战斗数据解析</strong><small>导入当前版本记录并进行本地复盘</small><i>进入解析 →</i></button>
      <button class="home-entry gacha" onClick={() => onNavigate("gacha")}><span class="eyebrow">02 / GACHA INTELLIGENCE</span><strong>寻访统计</strong><small>查看卡池历史、六星抽数、UP 与歪</small><i>打开寻访 →</i></button>
    </section>
  </main>;
}

function ImportPage({ onImport, busy }: { onImport: (file?: File) => void; busy: boolean }) {
  return <main class="import-page"><section class="import-hero panel"><span class="eyebrow">LOCAL PARSER / 本地解析</span><h1>导入 schema 11<br />战斗记录</h1><p>文件仅在浏览器本地解析，当前版本不会上传战斗记录或写入云存档。</p><label class="drop-zone desktop-only"><input type="file" accept="application/json,.json" disabled={busy} onChange={(event) => onImport(event.currentTarget.files?.[0])} /><b>选择 JSON 文件</b><span>或拖放最新版 Better Endfield 战斗记录</span><small>仅当前 schema 11 · 最大 64 MiB</small></label><div class="mobile-only mobile-disabled"><b>手机版不提供 JSON 解析</b><p>请从软件内跳转至 PC 浏览器完成解析；手机仍可查看已有分享记录。</p></div></section></main>;
}

function DownloadPage() {
  return <main class="download-page"><section class="download-hero"><div><span class="eyebrow">BETTER ENDFIELD / DESKTOP</span><h1>战斗数据，<br />从游戏内一直延伸到复盘。</h1><p>实时伤害统计、rDPS 贡献归因、战斗历史、桌面覆盖层，以及一键跳转网页分析。</p><a class="button primary large" href="https://github.com/Dr-hydra/Better-Endfield/releases/latest" target="_blank" rel="noreferrer">前往 GitHub 下载 ↗</a></div><div class="software-card"><span>WINDOWS / LATEST</span><b>BE</b><ul><li>实时 DPS 与 rDPS</li><li>队伍配置快照</li><li>战斗历史与 JSON 导出</li><li>网页时间轴复盘</li></ul></div></section></main>;
}

function LoadingState({ busy, error }: { busy: boolean; error: string }) {
  return <main class="loading-page"><section class="panel empty-state"><b>{busy ? "正在读取战斗记录…" : error || "记录不存在"}</b><p>{busy ? "仅在打开详情时请求完整 JSON。" : "请检查分享链接或返回排行榜。"}</p><button class="button secondary" onClick={() => navigate("home")}>返回排行榜</button></section></main>;
}
