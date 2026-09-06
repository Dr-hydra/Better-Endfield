import { useEffect, useState } from "preact/hooks";
import { ArchivePage } from "./components/ArchivePage";
import { ArchiveSaveDialog } from "./components/ArchiveSaveDialog";
import { CombatDetail } from "./components/CombatDetail";
import { GachaPage } from "./components/GachaPage";
import { LeaderboardPage, StageBoardPage } from "./components/LeaderboardPage";
import { apiConfigured, boardConfigured, getPublicRecord, publishRecord } from "./lib/api";
import { getOwnerToken } from "./lib/archive";
import { dictionary, stageName } from "./lib/dict";
import { SUPPORTED_SCHEMA } from "./lib/combat";
import { decodeGachaSnapshot, isGachaSnapshotFragment } from "./lib/gacha";
import { HANDOFF_PARAM, fetchHandoffRecord, parseHandoffRoute, parseHandoffValue } from "./lib/handoff";
import { loadGachaCloudSnapshot, saveGachaCloudSnapshot } from "./lib/gachaCloud";
import { recordUrl, requestToyProfile, shareRecord, type ToyProfile } from "./lib/toy";
import ShareDialog from "./components/ShareDialog";
import type { CombatRecord, GachaWebSnapshot, Route } from "./types";

function parseRoute(): Route {
  const params = new URLSearchParams(location.search);
  const sharedId = params.get("r");
  if (sharedId) return { page: "record", id: sharedId };
  // Checked before the fragment: on Toy only the query string survives into the
  // app's iframe.
  const query = parseHandoffValue(params.get(HANDOFF_PARAM));
  if (query) return { page: "import", ...query };
  if (params.get("mode") === "gacha" || isGachaSnapshotFragment(location.hash)) return { page: "gacha" };
  if (params.get("mode") === "combat") return { page: "analyze" };
  const value = location.hash.replace(/^#\/?/, "");
  const handoff = parseHandoffRoute(value);
  if (handoff) return { page: "import", ...handoff };
  if (value.startsWith("record/")) return { page: "record", id: value.slice(7) };
  if (value.startsWith("board/cat/")) return { page: "board", categoryId: value.slice(10) };
  if (value.startsWith("board/")) return { page: "board", dungeonId: value.slice(6) };
  if (value === "board" || value === "combat") return { page: "board" };
  if (value === "archive") return { page: "archive" };
  if (value === "gacha") return { page: "gacha" };
  if (value === "analyze") return { page: "analyze" };
  if (value === "download") return { page: "download" };
  return { page: "home" };
}

function navigate(path: string) {
  location.hash = path === "home" ? "#/" : `#/${path}`;
}

/**
 * Drops a query parameter once its value has been acted on.
 *
 * parseRoute reads the query before the hash and re-runs on every hashchange,
 * so a parameter left in place outranks navigation forever: tapping the bottom
 * nav on a shared `?r=` link re-resolves to that same record and refetches it
 * instead of going anywhere. replaceState fires no hashchange, so the route
 * state already in hand is left alone.
 */
function dropQueryParam(name: string) {
  const url = new URL(location.href);
  if (!url.searchParams.has(name)) return;
  url.searchParams.delete(name);
  history.replaceState(null, "", `${url.pathname}${url.search}${url.hash}`);
}

export default function App() {
  const [route, setRoute] = useState<Route>(parseRoute);
  const [theme, setTheme] = useState(() => localStorage.getItem("be-theme") || "light");
  const [railCollapsed, setRailCollapsed] = useState(() => localStorage.getItem("be-rail-collapsed") === "1");
  const [profile, setProfile] = useState<ToyProfile | null>(null);
  const [record, setRecord] = useState<CombatRecord | null>(null);
  const [sourceLabel, setSourceLabel] = useState("LOCAL / 本地记录");
  const [busy, setBusy] = useState(false);
  const [notice, setNotice] = useState("");
  const [error, setError] = useState("");
  /** Link the share dialog is showing, empty when it is closed. */
  const [shareUrl, setShareUrl] = useState("");
  const [gachaSnapshot, setGachaSnapshot] = useState<GachaWebSnapshot | null>(null);
  const [gachaError, setGachaError] = useState("");
  const [gachaSyncing, setGachaSyncing] = useState(false);
  const [gachaAutoSyncKey, setGachaAutoSyncKey] = useState("");
  const [savingArchive, setSavingArchive] = useState(false);
  /** Short id of the current record's upload, so 分享 and 参与排行榜 share one. */
  const [shareId, setShareId] = useState("");

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
    // A different fight is a different upload.
    setShareId("");
  }, [record?.sessionId]);

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
    dropQueryParam("r");
    getPublicRecord(route.id)
      .then(({ record: value, meta }) => {
        setRecord(value);
        setSourceLabel(`PUBLIC / ${meta.nickname || route.id}`);
      })
      .catch((reason) => setError(reason instanceof Error ? reason.message : "公开记录读取失败"))
      .finally(() => setBusy(false));
  }, [route]);

  // Desktop handoff. The port only answers for a few minutes and only once, so
  // a reloaded or shared link fails rather than showing someone stale data.
  useEffect(() => {
    if (route.page !== "import") return;
    setBusy(true);
    setError("");
    // Before fetching: a stale address here would send the app straight back to
    // this page the moment the parsed record navigates to 解析, against a port
    // that has already closed.
    dropQueryParam(HANDOFF_PARAM);
    fetchHandoffRecord({ port: route.port, nonce: route.nonce })
      .then((text) => parseRecordText(text, "DESKTOP / 桌面端记录"))
      .catch(() => {
        setBusy(false);
        setError("没能从桌面端取到记录。链接只在打开后几分钟内有效，且只能用一次——请回到 Better Endfield 重新点「在网页中解析」。");
      });
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

  function pickAnotherRecord() {
    const input = document.createElement("input");
    input.type = "file";
    input.accept = "application/json,.json";
    input.addEventListener("change", () => { void importFile(input.files?.[0] ?? undefined); });
    input.click();
  }

  /** Parses off the main thread, then shows the result. */
  function parseRecordText(text: string, label: string) {
    setBusy(true);
    setError("");
    const worker = new Worker(new URL("./workers/combat.worker.ts", import.meta.url), { type: "module" });
    worker.onmessage = (event: MessageEvent<{ ok: boolean; record?: CombatRecord; error?: string }>) => {
      setBusy(false);
      worker.terminate();
      if (!event.data.ok || !event.data.record) return setError(event.data.error || "解析失败");
      setRecord(event.data.record);
      setSourceLabel(label);
      navigate("analyze");
    };
    worker.onerror = () => {
      worker.terminate();
      setBusy(false);
      setError("解析工作线程异常");
    };
    worker.postMessage({ text, dictionary });
  }

  async function importFile(file?: File) {
    if (!file) return;
    if (file.size > 64 * 1024 * 1024) return setError("文件超过 64 MiB，请先确认记录是否完整");
    parseRecordText(await file.text(), "LOCAL / 本地记录");
  }

  /**
   * Uploads once per record. 分享 and 参与排行榜 are the same upload — sharing
   * joins the board by default — so the second button reuses the first's id
   * instead of publishing a duplicate that would compete with itself.
   */
  async function ensurePublished(): Promise<string> {
    if (!record) throw new Error("没有可上传的记录");
    if (!profile) throw new Error("上传需要先登录 Toy");
    if (shareId) return shareId;
    // The owner token lives in Toy cloud storage, so only this account's
    // browser can later edit or delete what it uploaded.
    const { shortId } = await publishRecord({
      record,
      ownerToken: await getOwnerToken(),
      nickname: profile.nickname,
      avatar: profile.avatar,
      toyOpenId: profile.toyOpenId,
    });
    setShareId(shortId);
    return shortId;
  }

  async function joinBoard() {
    setBusy(true);
    setError("");
    try {
      await ensurePublished();
      setNotice(`已参与「${stageName(record?.dungeonId ?? "")}」排行榜`);
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "上传失败");
    } finally {
      setBusy(false);
    }
  }

  async function shareCurrent() {
    setBusy(true);
    setError("");
    try {
      await shareRecordById(await ensurePublished());
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "分享失败");
    } finally {
      setBusy(false);
    }
  }

  async function shareRecordById(shortId: string) {
    // In the app the native sheet is the better answer; anywhere else the
    // dialog is, since the link is the whole point of the button and neither
    // the SDK's share nor the clipboard is reliably available on web.
    if (await shareRecord(shortId) === "sheet") return setNotice("已打开分享面板");
    setShareUrl(recordUrl(shortId));
  }

  // The ranking tab needs the storage endpoint the boards are read from; the
  // upload buttons additionally need the write API. A build with neither runs
  // as a pure local parser.
  const navItems: Array<[string, string]> = [
    ["home", "首页"],
    ...(boardConfigured ? [["board", "排行榜"] as [string, string]] : []),
    ["analyze", "战斗解析"],
    ["archive", "云存档"],
    ["gacha", "寻访统计"],
    ["download", "软件下载"],
  ];

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
          <span>SCHEMA<br /><b>{SUPPORTED_SCHEMA}</b></span>
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
          {record.provenance === "snapshot" && <span class="data-note">云端快照 · 不含逐次命中的乘区明细</span>}
          <button class="button secondary desktop-only" onClick={pickAnotherRecord} disabled={busy}>打开另一个记录</button>
          <button class="button secondary" onClick={() => setSavingArchive(true)} disabled={busy || !profile}>上传云存档</button>
          {apiConfigured && <button class="button secondary" onClick={() => void joinBoard()} disabled={busy}>{shareId ? "已参与排行榜" : "参与排行榜"}</button>}
          {apiConfigured && <button class="button primary" onClick={() => void shareCurrent()} disabled={busy}>分享</button>}
        </>} /> : <ImportPage onImport={importFile} busy={busy} onOpenArchive={() => navigate("archive")} />)}
        {route.page === "record" && (record ? <CombatDetail record={record} dictionary={dictionary} sourceLabel={sourceLabel} actions={<>
          <button class="button secondary" onClick={pickAnotherRecord} disabled={busy}>打开另一个记录</button>
          <button class="button secondary" onClick={() => { if (route.page === "record" && route.id) setShareUrl(recordUrl(route.id)); }}>二维码</button>
          <button class="button primary" onClick={() => { if (route.page === "record" && route.id) void shareRecordById(route.id); }}>分享记录</button>
        </>} /> : <LoadingState busy={busy} error={error} />)}
        {route.page === "board" && (route.dungeonId
          ? <StageBoardPage
            dungeonId={route.dungeonId}
            onBack={(categoryId) => navigate(`board/cat/${categoryId}`)}
            onOpenRecord={(id) => navigate(`record/${id}`)}
          />
          : <LeaderboardPage
            categoryId={route.categoryId}
            onSelectCategory={(id) => navigate(`board/cat/${id}`)}
            onSelectStage={(id) => navigate(`board/${id}`)}
            onOpenRecord={(id) => navigate(`record/${id}`)}
          />)}
        {route.page === "archive" && (
          <ArchivePage
            record={record}
            profileReady={Boolean(profile)}
            onLogin={login}
            onOpen={(value, label) => { setRecord(value); setSourceLabel(label); navigate("analyze"); }}
            onOpenRecord={(id) => navigate(`record/${id}`)}
          />
        )}
        {/* The handoff redirects to analyze as soon as the record parses, so
            this only shows while the loopback fetch is in flight or failed. */}
        {route.page === "import" && (
          <LoadingState
            busy={busy}
            error={error}
            busyHint="正在从桌面端读取，记录不经过网络。"
            failHint="回到 Better Endfield 重新点「在网页中解析」即可。"
          />
        )}
        {route.page === "download" && <DownloadPage />}
        {route.page === "gacha" && <GachaPage snapshot={gachaSnapshot} error={gachaError} profileReady={Boolean(profile)} onLogin={login} onLoadCloud={gachaSnapshot ? saveGachaCloud : loadGachaCloud} onSaveCloud={gachaSnapshot ? saveGachaCloud : undefined} busy={gachaSyncing} />}
      </div>
      <nav class="mobile-nav">{navItems.map(([path, label]) => <button class={route.page === path ? "active" : ""} onClick={() => navigate(path)} key={path}>{label}</button>)}</nav>
      {busy && <div class="busy-bar" />}
      {(notice || error) && <div class={`toast ${error ? "error" : ""}`} role="status"><span>{error || notice}</span><button onClick={() => { setNotice(""); setError(""); }}>×</button></div>}
      {savingArchive && record && (
        <ArchiveSaveDialog
          record={record}
          onClose={() => setSavingArchive(false)}
          onSaved={(meta) => { setSavingArchive(false); setNotice(`已保存「${meta.title}」，占用 ${meta.parts} 片`); }}
        />
      )}
      {shareUrl && <ShareDialog url={shareUrl} onClose={() => setShareUrl("")} />}
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

function ImportPage({ onImport, busy, onOpenArchive }: { onImport: (file?: File) => void; busy: boolean; onOpenArchive: () => void }) {
  return <main class="import-page"><section class="import-hero panel"><h1>上传战斗记录</h1><label class="drop-zone desktop-only"><input type="file" accept="application/json,.json" disabled={busy} onChange={(event) => onImport(event.currentTarget.files?.[0])} /><b>选择 JSON 文件</b><span>或拖放到此处</span><small>schema {SUPPORTED_SCHEMA} · 最大 64 MiB</small></label><div class="mobile-only mobile-disabled"><b>手机版不解析本地 JSON</b><p>请在 PC 浏览器导入并保存为云存档，手机即可打开。</p></div><div class="import-alt"><button class="button secondary" onClick={onOpenArchive}>打开云存档</button></div></section></main>;
}

function DownloadPage() {
  return <main class="download-page"><section class="download-hero"><div><span class="eyebrow">BETTER ENDFIELD / DESKTOP</span><h1>战斗数据，<br />从游戏内一直延伸到复盘。</h1><p>实时伤害统计、rDPS 贡献归因、战斗历史、桌面覆盖层，以及一键跳转网页分析。</p><a class="button primary large" href="https://github.com/Dr-hydra/Better-Endfield/releases/latest" target="_blank" rel="noreferrer">前往 GitHub 下载 ↗</a></div><div class="software-card"><span>WINDOWS / LATEST</span><b>BE</b><ul><li>实时 DPS 与 rDPS</li><li>队伍配置快照</li><li>战斗历史与 JSON 导出</li><li>网页时间轴复盘</li></ul></div></section></main>;
}

function LoadingState({ busy, error, busyHint, failHint }: {
  busy: boolean;
  error: string;
  busyHint?: string;
  failHint?: string;
}) {
  return <main class="loading-page"><section class="panel empty-state"><b>{busy ? "正在读取战斗记录…" : error || "记录不存在"}</b><p>{busy ? busyHint ?? "仅在打开详情时请求完整 JSON。" : failHint ?? "请检查分享链接或返回排行榜。"}</p><button class="button secondary" onClick={() => navigate("board")}>返回排行榜</button></section></main>;
}
