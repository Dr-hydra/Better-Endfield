import { useEffect, useState } from "preact/hooks";
import type { JSX } from "preact";
import categories from "../shared/types.json";
import { base, blankDraft, request, write, type Catalog, type Draft, type Resource, type Session } from "./api";
import { localizeMessage, type Language } from "./messages";

const text = (lang: Language, zh: string, en: string) => lang === "zh" ? zh : en;
const category = (id: string) => categories.find(item => item.id === id) || categories[categories.length - 1];
const categoryName = (item: ReturnType<typeof category>, lang: Language) => lang === "zh" ? item.name : item.nameEn;
const categoryHint = (item: ReturnType<typeof category>, lang: Language) => lang === "zh" ? item.hint : item.hintEn;
const date = (value: number, lang: Language) => new Intl.DateTimeFormat(lang === "zh" ? "zh-CN" : "en-US", { year: "numeric", month: "short", day: "numeric" }).format(value);
const hostname = (value: string, lang: Language) => { try { return new URL(value).hostname; } catch { return text(lang, "下载地址", "download link"); } };
const releaseName = (resource: Resource, lang: Language) => resource.latest.version || (resource.latest.number === 1 ? text(lang, "初次发布", "Initial release") : text(lang, `第 ${resource.latest.number} 次发布`, `Release ${resource.latest.number}`));
const routeOf = () => location.hash.replace(/^#\/?/, "").split("?")[0] || "explore";
const go = (route: string) => { location.hash = `#/${route}`; };
const emptyCatalog: Catalog = { items: [], counts: {}, total: 0, page: 1, pages: 1 };

function Icon({ name, size = 20 }: { name: string; size?: number }) {
  const paths: Record<string, string> = {
    discover: "M12 3a9 9 0 1 0 0 18 9 9 0 0 0 0-18ZM16 8l-3 5-5 3 3-5 5-3Z",
    plus: "M12 5v14M5 12h14", user: "M8 7a4 4 0 1 0 8 0 4 4 0 0 0-8 0ZM4 21v-2a8 8 0 0 1 16 0v2",
    arrow: "M5 12h14m-6-6 6 6-6 6", external: "M14 3h7v7m0-7L10 14M10 3H4v17h17v-6",
    search: "M10 3a7 7 0 1 0 0 14 7 7 0 0 0 0-14Zm5 12 6 6", chevron: "m9 5 7 7-7 7",
    layers: "m12 3 10 5-10 5L2 8l10-5ZM2 12l10 5 10-5M2 16l10 5 10-5",
    edit: "m15 3 6 6-12 12H3v-6L15 3Zm-3 3 6 6", back: "M19 12H5m6-6-6 6 6 6",
    moon: "M20 15a9 9 0 0 1-11-11 9 9 0 1 0 11 11Z", sun: "M12 8a4 4 0 1 0 0 8 4 4 0 0 0 0-8ZM12 1v3m0 16v3M1 12h3m16 0h3M4 4l2 2m12 12 2 2M20 4l-2 2M6 18l-2 2",
    github: "M9 19c-4 1-4-2-6-2m12 5v-4a4 4 0 0 0-1-3c3 0 6-1 6-5a4 4 0 0 0-1-3c0-1 0-2-1-3l-3 1a12 12 0 0 0-6 0L6 4c-1 1-1 2-1 3a4 4 0 0 0-1 3c0 4 3 5 6 5a4 4 0 0 0-1 3v4",
  };
  return <svg width={size} height={size} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.6" strokeLinecap="round" strokeLinejoin="round" aria-hidden="true"><path d={paths[name] || paths.layers} /></svg>;
}

function ResourceCover({ resource, large = false, lang }: { resource: Resource; large?: boolean; lang: Language }) {
  const [broken, setBroken] = useState(false);
  useEffect(() => setBroken(false), [resource.cover]);
  const kind = category(resource.type);
  return <div class={`resource-cover type-${resource.type} ${large ? "large" : ""}`}>
    {resource.cover && !broken ? <img src={resource.cover} alt={`${resource.name} ${text(lang, "封面", "cover")}`} loading="lazy" referrerPolicy="no-referrer" onError={() => setBroken(true)} /> : <div class="cover-placeholder"><Icon name="layers" size={large ? 72 : 48} /><span>{kind.code}</span><small>ENDFIELD COMMUNITY</small></div>}
    <span class="cover-type">{categoryName(kind, lang)}</span>
  </div>;
}

export default function App() {
  const [route, setRoute] = useState(routeOf);
  const [theme, setTheme] = useState(() => localStorage.getItem("be-theme") || "light");
  const [lang, setLang] = useState<Language>(() => localStorage.getItem("be-lang") === "en" ? "en" : "zh");
  const [session, setSession] = useState<Session>({ user: null, csrf: "", githubEnabled: false });
  const [sessionLoading, setSessionLoading] = useState(true);
  const [catalog, setCatalog] = useState<Catalog>(emptyCatalog);
  const [selectedType, setSelectedType] = useState("");
  const [search, setSearch] = useState("");
  const [debounced, setDebounced] = useState("");
  const [sort, setSort] = useState("updated");
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [notice, setNotice] = useState("");
  const [resource, setResource] = useState<Resource | null>(null);
  const [revision, setRevision] = useState(0);
  const [deletePrompt, setDeletePrompt] = useState(false);
  const mine = route === "mine";
  const listing = route === "explore" || mine;
  const editor = route === "publish" || route.startsWith("edit/") || route.startsWith("release/");
  const editing = route.startsWith("edit/");
  const releasing = route.startsWith("release/");
  const resourceId = route.includes("/") ? route.split("/")[1] : "";
  const total = Object.values(catalog.counts).reduce((sum, value) => sum + value, 0);
  const t = (zh: string, en: string) => text(lang, zh, en);

  useEffect(() => {
    const changed = () => { setRoute(routeOf()); setError(""); setNotice(""); setDeletePrompt(false); window.scrollTo(0, 0); };
    addEventListener("hashchange", changed); return () => removeEventListener("hashchange", changed);
  }, []);
  useEffect(() => { document.documentElement.dataset.theme = theme; localStorage.setItem("be-theme", theme); }, [theme]);
  useEffect(() => { document.documentElement.lang = lang === "zh" ? "zh-CN" : "en"; document.title = lang === "zh" ? "终末地下载站 · Better Endfield" : "Better Endfield Downloads"; localStorage.setItem("be-lang", lang); }, [lang]);
  useEffect(() => {
    request<Session>("session").then(setSession).catch(reason => setError(reason.message)).finally(() => setSessionLoading(false));
  }, []);
  useEffect(() => { const timer = setTimeout(() => setDebounced(search), 250); return () => clearTimeout(timer); }, [search]);
  useEffect(() => setPage(1), [selectedType, debounced, sort, mine]);
  useEffect(() => {
    if (!listing || (mine && !session.user)) return;
    let stale = false;
    setLoading(true); setError("");
    const params = new URLSearchParams({ type: selectedType, q: debounced, sort, page: String(page), mine: mine ? "1" : "0" });
    request<Catalog>(`resources?${params}`).then(result => { if (!stale) setCatalog(result); }).catch(reason => { if (!stale) setError(reason.message); }).finally(() => { if (!stale) setLoading(false); });
    return () => { stale = true; };
  }, [listing, mine, session.user?.id, selectedType, debounced, sort, page, revision]);
  useEffect(() => {
    if (!resourceId) { setResource(null); return; }
    let stale = false;
    setResource(null); setLoading(true); setError("");
    request<Resource>(`resources/${resourceId}`).then(result => { if (!stale) setResource(result); }).catch(reason => { if (!stale) setError(reason.message); }).finally(() => { if (!stale) setLoading(false); });
    return () => { stale = true; };
  }, [resourceId, revision]);
  useEffect(() => {
    const hint = location.hash.split("?")[1];
    if (hint === "login=unavailable") setNotice("GitHub 登录暂未开放。你可以先浏览作品和预览发布表单。");
    if (hint === "login=failed") setError("GitHub 登录未完成，请重试。");
  }, [route]);

  function login() {
    if (!session.githubEnabled) { setNotice("GitHub 登录暂未开放。你可以先浏览作品和预览发布表单。"); return; }
    location.href = `${base}auth/github`;
  }
  function filter(id: string) { setSelectedType(id); setPage(1); go("explore"); }
  async function logout() {
    try { await write("logout", "POST", {}, session.csrf); setSession({ ...session, user: null, csrf: "" }); go("explore"); } catch (reason) { setError((reason as Error).message); }
  }
  async function remove() {
    if (!resource) return;
    try { await write(`resources/${resource.id}`, "DELETE", {}, session.csrf); setDeletePrompt(false); setRevision(value => value + 1); go("mine"); } catch (reason) { setError((reason as Error).message); }
  }
  const nav = [ { id: "explore", title: text(lang, "发现作品", "Explore"), icon: "discover" }, { id: "mine", title: text(lang, "我的发布", "My works"), icon: "user" }, { id: "publish", title: text(lang, "发布作品", "Publish"), icon: "plus" } ];
  const navButtons = nav.map(item => <button key={item.id} class={(item.id === route || (item.id === "publish" && editor)) ? "active" : ""} onClick={() => go(item.id)}><Icon name={item.icon} /><span>{item.title}</span></button>);
  const owned = resource?.author.id === session.user?.id;

  return <div class="app-shell mod-center">
    <aside class="side-rail">
      <button class="brand" onClick={() => go("explore")} aria-label={t("下载站首页", "Download site home")}><span>BE</span><b>BETTER ENDFIELD<br />{t("下载站", "DOWNLOADS")}</b></button>
      <nav aria-label={t("主导航", "Main navigation")}>{navButtons}<div class="rail-divider" /><span class="rail-label">{t("按类型发现", "BROWSE BY TYPE")}</span>{categories.map(item => <button key={item.id} class={`type-nav ${listing && !mine && selectedType === item.id ? "active" : ""}`} onClick={() => filter(item.id)}><i>{item.code.slice(0, 2)}</i><span>{categoryName(item, lang)}</span></button>)}</nav>
      <div class="rail-bottom"><div class="rail-tools"><button class="theme-toggle" aria-label={theme === "light" ? t("切换深色模式", "Switch to dark mode") : t("切换浅色模式", "Switch to light mode")} onClick={() => setTheme(theme === "light" ? "dark" : "light")}><Icon name={theme === "light" ? "moon" : "sun"} size={17} /></button><button class="language-toggle" aria-label={t("切换语言", "Switch language")} onClick={() => setLang(lang === "zh" ? "en" : "zh")}>{lang === "zh" ? "EN" : "中"}</button></div><span>ENDFIELD COMMUNITY<br /><b>CREATE. SHARE. DISCOVER.</b></span></div>
    </aside>
    <header class="mobile-header"><button class="brand compact" onClick={() => go("explore")}><span>BE</span><b>{t("下载站", "DOWNLOADS")}</b></button><button class="mobile-language" aria-label={t("切换语言", "Switch language")} onClick={() => setLang(lang === "zh" ? "en" : "zh")}>{lang === "zh" ? "EN" : "中"}</button><button class="mobile-theme" aria-label={t("切换主题", "Switch theme")} onClick={() => setTheme(theme === "light" ? "dark" : "light")}><Icon name={theme === "light" ? "moon" : "sun"} /></button><button class="mobile-login" onClick={session.user ? logout : login}>{session.user?.login || t("GitHub 登录", "Sign in with GitHub")}</button></header>
    <div class="top-status"><span><i class="status-dot" />ENDFIELD / {t("下载站", "DOWNLOADS")}</span><div class="account-actions"><button class="language-toggle top-language" onClick={() => setLang(lang === "zh" ? "en" : "zh")} aria-label={t("切换语言", "Switch language")}>{lang === "zh" ? "EN" : "中"}</button>{session.user && <button onClick={logout}>{t("退出", "Sign out")}</button>}<button class="profile-button" onClick={session.user ? () => go("mine") : login}>{session.user?.avatar ? <img src={session.user.avatar} alt="" referrerPolicy="no-referrer" /> : <Icon name="github" size={18} />}{sessionLoading ? t("正在连接…", "Connecting…") : session.user?.login || t("GitHub 登录", "Sign in with GitHub")}</button></div></div>
    <div class="page-content">
      {notice && <div class="portal-notice" role="status"><span>{localizeMessage(notice, lang)}</span><button onClick={() => setNotice("")} aria-label={t("关闭提示", "Dismiss notice")}>×</button></div>}
      {error && <div class="portal-notice error" role="alert"><span>{localizeMessage(error, lang)}</span><button onClick={() => setError("")} aria-label={t("关闭错误", "Dismiss error")}>×</button></div>}
      {listing && <>
        {!mine && <section class="portal-hero panel"><div class="hero-copy"><span class="eyebrow">BETTER ENDFIELD · {t("下载站", "DOWNLOADS")}</span><h1>{t("发现终末地", "Explore Endfield")}<br /><span>{t("社区创作。", "Community creations.")}</span></h1><p>{t("功能、外观、模型与更多可能。", "Mods, skins, models, and more.")}<br />{t("找到喜欢的作品，也让你的创作被看见。", "Find something you like, or share your own work.")}</p><button class="button primary large" onClick={() => go("publish")}>{t("发布你的作品", "Publish your work")} <Icon name="arrow" /></button><div class="hero-foot"><span><b>{total.toString().padStart(2, "0")}</b> {t("份社区作品", "community works")}</span><i /><span><b>07</b> {t("个资源分类", "categories")}</span></div></div><div class="hero-index"><div class="hero-index-heading"><span>COMMUNITY / {t("创作目录", "CATALOG")}</span><Icon name="layers" size={23} /></div>{categories.map((item, index) => <button key={item.id} onClick={() => filter(item.id)}><span class="index-number">0{index + 1}</span><span><strong>{categoryName(item, lang)}</strong><small>{categoryHint(item, lang)}</small></span><Icon name="chevron" size={16} /></button>)}<div class="hero-index-foot"><span>{t("由作者分享 · 前往原地址下载", "Shared by authors · Download from the original link")}</span><i class="status-dot" /></div></div></section>}
        <section class="catalog-heading"><div><span class="eyebrow">{mine ? t("YOUR COLLECTION / 创作管理", "YOUR COLLECTION") : t("EXPLORE / 作品目录", "EXPLORE")}</span><h2>{mine ? t("我的发布", "My works") : t("浏览作品", "Browse works")}<span>{catalog.total}</span></h2></div>{mine ? <button class="button primary" onClick={() => go("publish")}><Icon name="plus" size={17} />{t("发布作品", "Publish")}</button> : <span class="catalog-caption">{t("每一个想法，都可以从这里开始。", "Every idea can start here.")}</span>}</section>
        {mine && !session.user ? <section class="portal-empty panel"><Icon name="user" size={44} /><h3>{t("在这里管理你的创作", "Manage your creations here")}</h3><p>{t("使用 GitHub 登录后，发布作品、修改介绍或添加新版本。", "Sign in with GitHub to publish, edit, or add releases.")}</p><button class="button primary" onClick={login}><Icon name="github" size={18} />{t("GitHub 登录", "Sign in with GitHub")}</button></section> : <>
          <section class="catalog-toolbar panel"><label class="search-field"><Icon name="search" size={19} /><input aria-label={t("搜索作品", "Search works")} placeholder={t("搜索作品、介绍或作者…", "Search works, descriptions, or authors…")} value={search} maxLength={120} onInput={event => setSearch(event.currentTarget.value)} />{search && <button aria-label={t("清空搜索", "Clear search")} onClick={() => setSearch("")}>×</button>}</label><label class="sort-field"><span>{t("排序", "Sort")}</span><select aria-label={t("排序方式", "Sort order")} value={sort} onChange={event => setSort(event.currentTarget.value)}><option value="updated">{t("最近更新", "Recently updated")}</option><option value="newest">{t("最新发布", "Newest")}</option></select></label></section>
          <nav class="type-filters" aria-label={t("资源类型筛选", "Filter by type")}><button class={`filter-chip ${!selectedType ? "active" : ""}`} onClick={() => setSelectedType("")}>{t("全部", "All")}<span>{total}</span></button>{categories.map(item => <button key={item.id} class={`filter-chip ${selectedType === item.id ? "active" : ""}`} onClick={() => setSelectedType(item.id)}>{categoryName(item, lang)}<span>{catalog.counts[item.id] || 0}</span></button>)}</nav>
          {loading ? <div class="resource-grid" aria-label={t("正在读取作品", "Loading works")} aria-busy="true">{[0, 1, 2].map(value => <div key={value} class="resource-skeleton panel"><div /><span /><span /></div>)}</div> : catalog.items.length ? <div class="resource-grid">{catalog.items.map(item => <article class="resource-card panel" key={item.id}><a class="card-main" href={`#/resource/${item.id}`}><ResourceCover resource={item} lang={lang} /><div class="card-copy"><span class="card-version">{releaseName(item, lang)}</span><h3>{item.name}</h3><p>{item.description ? item.description.slice(0, 160) : t("打开作品，查看作者提供的下载链接。", "Open the work to see the author's download link.")}</p></div></a><div class="card-footer"><a href={item.author.profile_url} target="_blank" rel="noopener noreferrer" class="author-name">{item.author.avatar ? <img src={item.author.avatar} alt="" loading="lazy" referrerPolicy="no-referrer" /> : <Icon name="user" size={14} />}<span>{item.author.login}</span></a><span>{date(item.updated_at, lang)}</span></div></article>)}</div> : <section class="portal-empty panel"><div class="empty-mark"><Icon name={search || selectedType ? "search" : "layers"} size={44} /></div><span class="eyebrow">{search || selectedType ? "NO RESULTS" : "THE FIRST CHAPTER"}</span><h3>{search || selectedType ? t("暂时没有匹配的作品", "No matching works yet") : mine ? t("你的第一份作品，从这里开始", "Start with your first work") : t("成为这里的第一位创作者", "Be the first creator here")}</h3><p>{search || selectedType ? t("试试其他关键词，或换一个资源类型。", "Try another keyword or type.") : t("只需填写名字、类型和下载链接，就能分享作品。", "Share a work by filling in a name, type, and download link.")}</p><button class="button secondary" onClick={search || selectedType ? () => { setSearch(""); setSelectedType(""); } : () => go("publish")}>{search || selectedType ? t("清除筛选", "Clear filters") : t("发布第一份作品", "Publish the first work")}<Icon name="arrow" size={17} /></button></section>}
          {catalog.pages > 1 && <nav class="pagination" aria-label={t("分页", "Pagination")}><button class="button secondary" disabled={page <= 1} onClick={() => setPage(page - 1)}>{t("上一页", "Previous")}</button><span>{page} / {catalog.pages}</span><button class="button secondary" disabled={page >= catalog.pages} onClick={() => setPage(page + 1)}>{t("下一页", "Next")}</button></nav>}
        </>}
      </>}
      {editor && <>
        <button class="back-link" onClick={() => go(resourceId ? `resource/${resourceId}` : "explore")}><Icon name="back" size={17} />{resourceId ? t("返回作品", "Back to work") : t("返回作品目录", "Back to catalog")}</button>
        {resourceId && loading ? <div class="portal-empty panel">{t("正在读取作品…", "Loading work…")}</div> : resourceId && (!resource || !owned) ? <div class="portal-empty panel"><h3>{t("暂时无法编辑这份作品", "This work cannot be edited right now")}</h3><p>{t("请使用发布者的 GitHub 账号登录。", "Sign in with the author's GitHub account.")}</p><button class="button primary" onClick={login}>{t("GitHub 登录", "Sign in with GitHub")}</button></div> : <PublishForm key={`${route}-${resourceId}`} resource={resourceId ? resource : null} editing={editing} releasing={releasing} session={session} lang={lang} onLogin={login} onNotice={setNotice} onPublished={result => { setRevision(value => value + 1); go(`resource/${result.id}`); }} />}
      </>}
      {route.startsWith("resource/") && <>
        <button class="back-link" onClick={() => go("explore")}><Icon name="back" size={17} />{t("返回作品目录", "Back to catalog")}</button>
        {loading ? <div class="portal-empty panel">{t("正在读取作品…", "Loading work…")}</div> : resource && <>
          <section class="resource-detail-hero panel"><ResourceCover resource={resource} lang={lang} large /><div class="detail-copy"><span class="eyebrow">{category(resource.type).code} / {categoryName(category(resource.type), lang)}</span><h1>{resource.name}</h1><a class="detail-author" href={resource.author.profile_url} target="_blank" rel="noopener noreferrer">{resource.author.avatar && <img src={resource.author.avatar} alt="" referrerPolicy="no-referrer" />}<span>{t("由", "By")} <b>{resource.author.login}</b>{lang === "zh" && " 发布"}</span><Icon name="external" size={13} /></a><div class="detail-meta"><span>{releaseName(resource, lang)}</span><span>{t("更新于", "Updated")} {date(resource.updated_at, lang)}</span></div><a class="button primary large download-link" href={resource.latest.url} target="_blank" rel="noopener noreferrer">{t("前往下载", "Download")}<Icon name="external" size={18} /></a><small class="download-host">{hostname(resource.latest.url, lang)} · {t("作者提供的外部链接", "External link provided by the author")}</small>{owned && <div class="owner-actions"><button class="button secondary" onClick={() => go(`edit/${resource.id}`)}><Icon name="edit" size={15} />{t("编辑作品", "Edit")}</button><button class="button secondary" onClick={() => go(`release/${resource.id}`)}><Icon name="plus" size={15} />{t("发布新版本", "New release")}</button></div>}</div></section>
          <div class="detail-columns"><section class="description-panel panel"><div class="block-heading"><span class="eyebrow">{t("ABOUT / 关于作品", "ABOUT")}</span><h2>{t("作品介绍", "About this work")}</h2></div><div class="author-content">{resource.description || t("作者暂未填写介绍。", "The author has not added a description yet.")}</div></section><aside class="versions-panel panel"><div class="block-heading"><span class="eyebrow">{t("RELEASES / 发布记录", "RELEASES")}</span><h2>{t("版本记录", "Releases")}<span>{resource.releases?.length || 1}</span></h2></div>{resource.releases?.map((item, index) => <article class="release-item" key={item.id}><div><strong>{item.version || (item.number === 1 ? t("初次发布", "Initial release") : t(`第 ${item.number} 次发布`, `Release ${item.number}`))}</strong>{index === 0 && <span class="latest-label">{t("最新", "Latest")}</span>}</div><time>{date(item.created_at, lang)}</time>{item.notes && <p class="author-content">{item.notes}</p>}<a href={item.url} target="_blank" rel="noopener noreferrer">{t("下载此版本", "Download this release")}<Icon name="external" size={13} /></a></article>)}</aside></div>
          {owned && <div class="delete-area"><button onClick={() => setDeletePrompt(true)}>{t("删除作品", "Delete work")}</button></div>}
        </>}
      </>}
      <footer class="portal-footer"><span>BETTER ENDFIELD / {t("下载站", "DOWNLOADS")}</span><span>{t("社区创作 · 作者自助发布", "Community creations · Self-service publishing")}</span></footer>
    </div>
    <nav class="mobile-nav" aria-label={t("移动导航", "Mobile navigation")}>{navButtons}</nav>
    {deletePrompt && <div class="dialog-overlay"><section class="delete-dialog panel" role="dialog" aria-modal="true" aria-labelledby="delete-title"><span class="eyebrow">DELETE WORK</span><h2 id="delete-title">{t("删除这份作品？", "Delete this work?")}</h2><p>{t(`「${resource?.name}」及其发布记录会从目录中移除。`, `“${resource?.name}” and its releases will be removed from the catalog.`)}</p><div><button class="button secondary" autoFocus onClick={() => setDeletePrompt(false)}>{t("取消", "Cancel")}</button><button class="button danger" onClick={remove}>{t("确认删除", "Delete")}</button></div></section></div>}
  </div>;
}

function PublishForm({ resource, editing, releasing, session, lang, onLogin, onNotice, onPublished }: { resource: Resource | null; editing: boolean; releasing: boolean; session: Session; lang: Language; onLogin: () => void; onNotice: (value: string) => void; onPublished: (value: Resource) => void }) {
  const t = (zh: string, en: string) => text(lang, zh, en);
  const storageKey = `be-resource-draft:${editing ? "edit" : releasing ? "release" : "new"}:${resource?.id || ""}`;
  const [draft, setDraft] = useState<Draft>(() => {
    try { const saved = localStorage.getItem(storageKey); if (saved) return { ...blankDraft(), ...JSON.parse(saved) }; } catch { /* Ignore an invalid local draft. */ }
    return resource ? { name: resource.name, type: resource.type, description: resource.description, cover: resource.cover, version: releasing ? "" : resource.latest.version, url: releasing ? "" : resource.latest.url, notes: "" } : blankDraft();
  });
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState("");
  const [optional, setOptional] = useState(editing || releasing || Boolean(draft.cover || draft.version || draft.description));
  useEffect(() => { try { localStorage.setItem(storageKey, JSON.stringify(draft)); } catch { /* Form still works without browser storage. */ } }, [draft]);
  const change = (key: keyof Draft): JSX.GenericEventHandler<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement> => event => setDraft({ ...draft, [key]: event.currentTarget.value });
  async function submit(event: SubmitEvent) {
    event.preventDefault();
    if (!session.user) { onLogin(); return; }
    setBusy(true); setError("");
    try {
      const target = releasing ? `resources/${resource!.id}/releases` : editing ? `resources/${resource!.id}` : "resources";
      const result = await write<Resource>(target, editing ? "PUT" : "POST", draft, session.csrf);
      localStorage.removeItem(storageKey); onPublished(result);
    } catch (reason) { setError((reason as Error).message); } finally { setBusy(false); }
  }
  return <div class="publish-layout"><section class="publish-panel panel"><div class="publish-heading"><span class="eyebrow">{releasing ? "NEW RELEASE" : editing ? "EDIT WORK" : "SHARE YOUR CREATION"}</span><h1>{releasing ? t("发布新版本", "Publish a release") : editing ? t("编辑作品", "Edit work") : t("分享你的创作", "Share your creation")}</h1><p>{releasing ? t(`为「${resource?.name}」添加新的下载地址，历史记录会保留。`, `Add a download link for “${resource?.name}”; release history is kept.`) : t("名字、类型、下载链接。剩下的，由你自由发挥。", "Name, type, and download link. The rest is up to you.")}</p></div><form onSubmit={submit}>
    {!releasing && <div class="required-fields"><label><span>{t("作品名字", "Work name")} <b>*</b></span><input name="name" required maxLength={120} placeholder={t("给你的作品起个名字", "Name your work")} value={draft.name} onInput={change("name")} /></label><label><span>{t("资源类型", "Type")} <b>*</b></span><select name="type" required value={draft.type} onChange={change("type")}><option value="" disabled>{t("请选择资源类型", "Select a type")}</option>{categories.map(item => <option key={item.id} value={item.id}>{categoryName(item, lang)}</option>)}</select></label></div>}
    <label><span>{t("下载链接", "Download link")} <b>*</b></span><input name="url" type="url" required maxLength={2048} placeholder="https://…" value={draft.url} onInput={change("url")} /><small>{t("填写作者发布页、网盘或文件下载地址。提取码可以写在作品介绍中。", "Add an author page, cloud drive, or file download URL. Put access codes in the description.")}</small></label>
    <button class="optional-toggle" type="button" aria-expanded={optional} onClick={() => setOptional(!optional)}><span><Icon name="plus" size={16} />{releasing ? t("版本与更新说明", "Release notes") : t("多介绍一点你的作品", "Tell people more about your work")}<small>{t("选填", "Optional")}</small></span><span>{optional ? t("收起 −", "Collapse −") : t("展开 +", "Expand +")}</span></button>
    {optional && <div class="optional-fields"><label><span>{t("版本名字", "Version name")} <small>{t("选填", "Optional")}</small></span><input name="version" maxLength={80} placeholder={t("例如 1.0、秋季更新；也可以留空", "e.g. 1.0 or Autumn update; can be blank")} value={draft.version} onInput={change("version")} /></label>{!releasing && <><label><span>{t("封面链接", "Cover URL")} <small>{t("选填", "Optional")}</small></span><input name="cover" type="url" maxLength={2048} placeholder="https://…" value={draft.cover} onInput={change("cover")} /></label><label><span>{t("作品介绍", "Description")} <small>{t("选填", "Optional")}</small></span><textarea name="description" rows={10} maxLength={16000} placeholder={t("介绍作品、安装方法、兼容版本、提取码，或任何你想说的话…", "Describe the work, installation, compatibility, access codes, or anything else…")} value={draft.description} onInput={change("description")} /><small>{t("换行会保留。介绍与说明由你自由填写。", "Line breaks are preserved. Write anything you like.")}</small></label></>}{(releasing || editing) && <label><span>{t("更新说明", "Update notes")} <small>{t("选填", "Optional")}</small></span><textarea name="notes" rows={5} maxLength={4000} placeholder={t("这一次有哪些变化？", "What changed in this release?")} value={draft.notes} onInput={change("notes")} /></label>}</div>}
    {error && <p class="form-error" role="alert">{localizeMessage(error, lang)}</p>}
    {!session.user && <div class="publish-login-note"><Icon name="github" size={20} /><p>{session.githubEnabled ? t("使用 GitHub 登录后即可发布，已填写的内容会保留。", "Sign in with GitHub to publish. Your form is kept.") : t("GitHub 登录暂未开放。可以先填写表单，草稿会保留在当前浏览器。", "GitHub sign-in is not available yet. Your draft is saved in this browser.")}</p></div>}
    <div class="form-actions"><button class="button primary large" disabled={busy} type="submit">{busy ? t("正在保存…", "Saving…") : session.user ? releasing ? t("发布新版本", "Publish release") : editing ? t("保存更新", "Save changes") : t("发布作品", "Publish work") : t("登录后发布", "Sign in to publish")}<Icon name={session.user ? "arrow" : "github"} size={18} /></button><button class="button secondary" type="button" onClick={() => onNotice("草稿已保存在当前浏览器中。")}>{t("保存草稿", "Save draft")}</button></div>
  </form></section><aside class="publish-aside"><section class="publish-guide panel"><span class="eyebrow">YOUR SPACE / {t("自由创作", "CREATE FREELY")}</span><h2>{t("从一个链接开始。", "Start with a link.")}</h2><p>{t("发布功能 Mod、皮肤、模型或工具，让其他终末地玩家找到你的作品。", "Publish a gameplay mod, skin, model, or tool for other Endfield players.")}</p><ol><li><span>01</span><div><strong>{t("给作品一个名字", "Name your work")}</strong><p>{t("让大家知道你做了什么。", "Tell people what you made.")}</p></div></li><li><span>02</span><div><strong>{t("选择合适的类型", "Choose a type")}</strong><p>{t("作品会出现在对应的分类中。", "It will appear in the matching category.")}</p></div></li><li><span>03</span><div><strong>{t("贴上下载链接", "Add a download link")}</strong><p>{t("用户可以直接前往原地址下载。", "Users can download from the original link.")}</p></div></li></ol></section><div class="publish-side-note"><Icon name="layers" size={20} /><p>{t("文件由你选择的平台托管。", "Files are hosted by your chosen platform.")}<br />{t("介绍、封面和版本名字都可以留空。", "Descriptions, covers, and version names are optional.")}</p></div></aside></div>;
}
