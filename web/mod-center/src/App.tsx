import { useEffect, useState } from "preact/hooks";
import type { JSX } from "preact";
import categories from "../shared/types.json";
import { base, blankDraft, request, write, type Catalog, type Draft, type Resource, type Session } from "./api";

const category = (id: string) => categories.find(item => item.id === id) || categories[categories.length - 1];
const date = (value: number) => new Intl.DateTimeFormat("zh-CN", { year: "numeric", month: "short", day: "numeric" }).format(value);
const hostname = (value: string) => { try { return new URL(value).hostname; } catch { return "下载地址"; } };
const releaseName = (resource: Resource) => resource.latest.version || (resource.latest.number === 1 ? "初次发布" : `第 ${resource.latest.number} 次发布`);
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

function ResourceCover({ resource, large = false }: { resource: Resource; large?: boolean }) {
  const [broken, setBroken] = useState(false);
  useEffect(() => setBroken(false), [resource.cover]);
  const kind = category(resource.type);
  return <div class={`resource-cover type-${resource.type} ${large ? "large" : ""}`}>
    {resource.cover && !broken ? <img src={resource.cover} alt={`${resource.name}封面`} loading="lazy" referrerPolicy="no-referrer" onError={() => setBroken(true)} /> : <div class="cover-placeholder"><Icon name="layers" size={large ? 72 : 48} /><span>{kind.code}</span><small>ENDFIELD COMMUNITY</small></div>}
    <span class="cover-type">{kind.name}</span>
  </div>;
}

export default function App() {
  const [route, setRoute] = useState(routeOf);
  const [theme, setTheme] = useState(() => localStorage.getItem("be-theme") || "light");
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

  useEffect(() => {
    const changed = () => { setRoute(routeOf()); setError(""); setNotice(""); setDeletePrompt(false); window.scrollTo(0, 0); };
    addEventListener("hashchange", changed); return () => removeEventListener("hashchange", changed);
  }, []);
  useEffect(() => { document.documentElement.dataset.theme = theme; localStorage.setItem("be-theme", theme); }, [theme]);
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
  const nav = [ { id: "explore", title: "发现作品", icon: "discover" }, { id: "mine", title: "我的发布", icon: "user" }, { id: "publish", title: "发布作品", icon: "plus" } ];
  const navButtons = nav.map(item => <button key={item.id} class={(item.id === route || (item.id === "publish" && editor)) ? "active" : ""} onClick={() => go(item.id)}><Icon name={item.icon} /><span>{item.title}</span></button>);
  const owned = resource?.author.id === session.user?.id;

  return <div class="app-shell mod-center">
    <aside class="side-rail">
      <button class="brand" onClick={() => go("explore")} aria-label="资源中心首页"><span>BE</span><b>BETTER ENDFIELD<br />资源中心</b></button>
      <nav aria-label="主导航">{navButtons}<div class="rail-divider" /><span class="rail-label">按类型发现</span>{categories.map(item => <button key={item.id} class={`type-nav ${listing && !mine && selectedType === item.id ? "active" : ""}`} onClick={() => filter(item.id)}><i>{item.code.slice(0, 2)}</i><span>{item.name}</span></button>)}</nav>
      <div class="rail-bottom"><button class="theme-toggle" aria-label={theme === "light" ? "切换深色模式" : "切换浅色模式"} onClick={() => setTheme(theme === "light" ? "dark" : "light")}><Icon name={theme === "light" ? "moon" : "sun"} size={17} /></button><span>ENDFIELD COMMUNITY<br /><b>CREATE. SHARE. DISCOVER.</b></span></div>
    </aside>
    <header class="mobile-header"><button class="brand compact" onClick={() => go("explore")}><span>BE</span><b>资源中心</b></button><button class="mobile-theme" aria-label="切换主题" onClick={() => setTheme(theme === "light" ? "dark" : "light")}><Icon name={theme === "light" ? "moon" : "sun"} /></button><button class="mobile-login" onClick={session.user ? logout : login}>{session.user?.login || "GitHub 登录"}</button></header>
    <div class="top-status"><span><i class="status-dot" />ENDFIELD / COMMUNITY RESOURCES</span><div class="account-actions">{session.user && <button onClick={logout}>退出</button>}<button class="profile-button" onClick={session.user ? () => go("mine") : login}>{session.user?.avatar ? <img src={session.user.avatar} alt="" referrerPolicy="no-referrer" /> : <Icon name="github" size={18} />}{sessionLoading ? "正在连接…" : session.user?.login || "GitHub 登录"}</button></div></div>
    <div class="page-content">
      {notice && <div class="portal-notice" role="status"><span>{notice}</span><button onClick={() => setNotice("")} aria-label="关闭提示">×</button></div>}
      {error && <div class="portal-notice error" role="alert"><span>{error}</span><button onClick={() => setError("")} aria-label="关闭错误">×</button></div>}
      {listing && <>
        {!mine && <section class="portal-hero panel"><div class="hero-copy"><span class="eyebrow">BETTER ENDFIELD · RESOURCE HUB</span><h1>发现终末地<br /><span>社区创作。</span></h1><p>功能、外观、模型与更多可能。<br />找到喜欢的作品，也让你的创作被看见。</p><button class="button primary large" onClick={() => go("publish")}>发布你的作品 <Icon name="arrow" /></button><div class="hero-foot"><span><b>{total.toString().padStart(2, "0")}</b> 份社区作品</span><i /><span><b>07</b> 个资源分类</span></div></div><div class="hero-index"><div class="hero-index-heading"><span>COMMUNITY / 创作目录</span><Icon name="layers" size={23} /></div>{categories.map((item, index) => <button key={item.id} onClick={() => filter(item.id)}><span class="index-number">0{index + 1}</span><span><strong>{item.name}</strong><small>{item.hint}</small></span><Icon name="chevron" size={16} /></button>)}<div class="hero-index-foot"><span>由作者分享 · 前往原地址下载</span><i class="status-dot" /></div></div></section>}
        <section class="catalog-heading"><div><span class="eyebrow">{mine ? "YOUR COLLECTION / 创作管理" : "EXPLORE / 作品目录"}</span><h2>{mine ? "我的发布" : "浏览作品"}<span>{catalog.total}</span></h2></div>{mine ? <button class="button primary" onClick={() => go("publish")}><Icon name="plus" size={17} />发布作品</button> : <span class="catalog-caption">每一个想法，都可以从这里开始。</span>}</section>
        {mine && !session.user ? <section class="portal-empty panel"><Icon name="user" size={44} /><h3>在这里管理你的创作</h3><p>使用 GitHub 登录后，发布作品、修改介绍或添加新版本。</p><button class="button primary" onClick={login}><Icon name="github" size={18} />GitHub 登录</button></section> : <>
          <section class="catalog-toolbar panel"><label class="search-field"><Icon name="search" size={19} /><input aria-label="搜索作品" placeholder="搜索作品、介绍或作者…" value={search} maxLength={120} onInput={event => setSearch(event.currentTarget.value)} />{search && <button aria-label="清空搜索" onClick={() => setSearch("")}>×</button>}</label><label class="sort-field"><span>排序</span><select aria-label="排序方式" value={sort} onChange={event => setSort(event.currentTarget.value)}><option value="updated">最近更新</option><option value="newest">最新发布</option></select></label></section>
          <nav class="type-filters" aria-label="资源类型筛选"><button class={`filter-chip ${!selectedType ? "active" : ""}`} onClick={() => setSelectedType("")}>全部<span>{total}</span></button>{categories.map(item => <button key={item.id} class={`filter-chip ${selectedType === item.id ? "active" : ""}`} onClick={() => setSelectedType(item.id)}>{item.name}<span>{catalog.counts[item.id] || 0}</span></button>)}</nav>
          {loading ? <div class="resource-grid" aria-label="正在读取作品" aria-busy="true">{[0, 1, 2].map(value => <div key={value} class="resource-skeleton panel"><div /><span /><span /></div>)}</div> : catalog.items.length ? <div class="resource-grid">{catalog.items.map(item => <article class="resource-card panel" key={item.id}><a class="card-main" href={`#/resource/${item.id}`}><ResourceCover resource={item} /><div class="card-copy"><span class="card-version">{releaseName(item)}</span><h3>{item.name}</h3><p>{item.description ? item.description.slice(0, 160) : "打开作品，查看作者提供的下载链接。"}</p></div></a><div class="card-footer"><a href={item.author.profile_url} target="_blank" rel="noopener noreferrer" class="author-name">{item.author.avatar ? <img src={item.author.avatar} alt="" loading="lazy" referrerPolicy="no-referrer" /> : <Icon name="user" size={14} />}<span>{item.author.login}</span></a><span>{date(item.updated_at)}</span></div></article>)}</div> : <section class="portal-empty panel"><div class="empty-mark"><Icon name={search || selectedType ? "search" : "layers"} size={44} /></div><span class="eyebrow">{search || selectedType ? "NO RESULTS" : "THE FIRST CHAPTER"}</span><h3>{search || selectedType ? "暂时没有匹配的作品" : mine ? "你的第一份作品，从这里开始" : "成为这里的第一位创作者"}</h3><p>{search || selectedType ? "试试其他关键词，或换一个资源类型。" : "只需填写名字、类型和下载链接，就能分享作品。"}</p><button class="button secondary" onClick={search || selectedType ? () => { setSearch(""); setSelectedType(""); } : () => go("publish")}>{search || selectedType ? "清除筛选" : "发布第一份作品"}<Icon name="arrow" size={17} /></button></section>}
          {catalog.pages > 1 && <nav class="pagination" aria-label="分页"><button class="button secondary" disabled={page <= 1} onClick={() => setPage(page - 1)}>上一页</button><span>{page} / {catalog.pages}</span><button class="button secondary" disabled={page >= catalog.pages} onClick={() => setPage(page + 1)}>下一页</button></nav>}
        </>}
      </>}
      {editor && <>
        <button class="back-link" onClick={() => go(resourceId ? `resource/${resourceId}` : "explore")}><Icon name="back" size={17} />{resourceId ? "返回作品" : "返回作品目录"}</button>
        {resourceId && loading ? <div class="portal-empty panel">正在读取作品…</div> : resourceId && (!resource || !owned) ? <div class="portal-empty panel"><h3>暂时无法编辑这份作品</h3><p>请使用发布者的 GitHub 账号登录。</p><button class="button primary" onClick={login}>GitHub 登录</button></div> : <PublishForm key={`${route}-${resourceId}`} resource={resourceId ? resource : null} editing={editing} releasing={releasing} session={session} onLogin={login} onNotice={setNotice} onPublished={result => { setRevision(value => value + 1); go(`resource/${result.id}`); }} />}
      </>}
      {route.startsWith("resource/") && <>
        <button class="back-link" onClick={() => go("explore")}><Icon name="back" size={17} />返回作品目录</button>
        {loading ? <div class="portal-empty panel">正在读取作品…</div> : resource && <>
          <section class="resource-detail-hero panel"><ResourceCover resource={resource} large /><div class="detail-copy"><span class="eyebrow">{category(resource.type).code} / {category(resource.type).name}</span><h1>{resource.name}</h1><a class="detail-author" href={resource.author.profile_url} target="_blank" rel="noopener noreferrer">{resource.author.avatar && <img src={resource.author.avatar} alt="" referrerPolicy="no-referrer" />}<span>由 <b>{resource.author.login}</b> 发布</span><Icon name="external" size={13} /></a><div class="detail-meta"><span>{releaseName(resource)}</span><span>更新于 {date(resource.updated_at)}</span></div><a class="button primary large download-link" href={resource.latest.url} target="_blank" rel="noopener noreferrer">前往下载<Icon name="external" size={18} /></a><small class="download-host">{hostname(resource.latest.url)} · 作者提供的外部链接</small>{owned && <div class="owner-actions"><button class="button secondary" onClick={() => go(`edit/${resource.id}`)}><Icon name="edit" size={15} />编辑作品</button><button class="button secondary" onClick={() => go(`release/${resource.id}`)}><Icon name="plus" size={15} />发布新版本</button></div>}</div></section>
          <div class="detail-columns"><section class="description-panel panel"><div class="block-heading"><span class="eyebrow">ABOUT / 关于作品</span><h2>作品介绍</h2></div><div class="author-content">{resource.description || "作者暂未填写介绍。"}</div></section><aside class="versions-panel panel"><div class="block-heading"><span class="eyebrow">RELEASES / 发布记录</span><h2>版本记录<span>{resource.releases?.length || 1}</span></h2></div>{resource.releases?.map((item, index) => <article class="release-item" key={item.id}><div><strong>{item.version || (item.number === 1 ? "初次发布" : `第 ${item.number} 次发布`)}</strong>{index === 0 && <span class="latest-label">最新</span>}</div><time>{date(item.created_at)}</time>{item.notes && <p class="author-content">{item.notes}</p>}<a href={item.url} target="_blank" rel="noopener noreferrer">下载此版本<Icon name="external" size={13} /></a></article>)}</aside></div>
          {owned && <div class="delete-area"><button onClick={() => setDeletePrompt(true)}>删除作品</button></div>}
        </>}
      </>}
      <footer class="portal-footer"><span>BETTER ENDFIELD / 终末地资源中心</span><span>社区创作 · 作者自助发布</span></footer>
    </div>
    <nav class="mobile-nav" aria-label="移动导航">{navButtons}</nav>
    {deletePrompt && <div class="dialog-overlay"><section class="delete-dialog panel" role="dialog" aria-modal="true" aria-labelledby="delete-title"><span class="eyebrow">DELETE RESOURCE</span><h2 id="delete-title">删除这份作品？</h2><p>「{resource?.name}」及其发布记录会从目录中移除。</p><div><button class="button secondary" autoFocus onClick={() => setDeletePrompt(false)}>取消</button><button class="button danger" onClick={remove}>确认删除</button></div></section></div>}
  </div>;
}

function PublishForm({ resource, editing, releasing, session, onLogin, onNotice, onPublished }: { resource: Resource | null; editing: boolean; releasing: boolean; session: Session; onLogin: () => void; onNotice: (value: string) => void; onPublished: (value: Resource) => void }) {
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
  return <div class="publish-layout"><section class="publish-panel panel"><div class="publish-heading"><span class="eyebrow">{releasing ? "NEW RELEASE" : editing ? "EDIT RESOURCE" : "SHARE YOUR CREATION"}</span><h1>{releasing ? "发布新版本" : editing ? "编辑作品" : "分享你的创作"}</h1><p>{releasing ? `为「${resource?.name}」添加新的下载地址，历史记录会保留。` : "名字、类型、下载链接。剩下的，由你自由发挥。"}</p></div><form onSubmit={submit}>
    {!releasing && <div class="required-fields"><label><span>作品名字 <b>*</b></span><input name="name" required maxLength={120} placeholder="给你的作品起个名字" value={draft.name} onInput={change("name")} /></label><label><span>资源类型 <b>*</b></span><select name="type" required value={draft.type} onChange={change("type")}><option value="" disabled>请选择资源类型</option>{categories.map(item => <option key={item.id} value={item.id}>{item.name}</option>)}</select></label></div>}
    <label><span>下载链接 <b>*</b></span><input name="url" type="url" required maxLength={2048} placeholder="https://…" value={draft.url} onInput={change("url")} /><small>填写作者发布页、网盘或文件下载地址。提取码可以写在作品介绍中。</small></label>
    <button class="optional-toggle" type="button" aria-expanded={optional} onClick={() => setOptional(!optional)}><span><Icon name="plus" size={16} />{releasing ? "版本与更新说明" : "多介绍一点你的作品"}<small>选填</small></span><span>{optional ? "收起 −" : "展开 +"}</span></button>
    {optional && <div class="optional-fields"><label><span>版本名字 <small>选填</small></span><input name="version" maxLength={80} placeholder="例如 1.0、秋季更新；也可以留空" value={draft.version} onInput={change("version")} /></label>{!releasing && <><label><span>封面链接 <small>选填</small></span><input name="cover" type="url" maxLength={2048} placeholder="https://… 图片地址" value={draft.cover} onInput={change("cover")} /></label><label><span>作品介绍 <small>选填</small></span><textarea name="description" rows={10} maxLength={16000} placeholder="介绍作品、安装方法、兼容版本、提取码，或任何你想说的话…" value={draft.description} onInput={change("description")} /><small>换行会保留。介绍与说明由你自由填写。</small></label></>}{(releasing || editing) && <label><span>更新说明 <small>选填</small></span><textarea name="notes" rows={5} maxLength={4000} placeholder="这一次有哪些变化？" value={draft.notes} onInput={change("notes")} /></label>}</div>}
    {error && <p class="form-error" role="alert">{error}</p>}
    {!session.user && <div class="publish-login-note"><Icon name="github" size={20} /><p>{session.githubEnabled ? "使用 GitHub 登录后即可发布，已填写的内容会保留。" : "GitHub 登录暂未开放。可以先填写表单，草稿会保留在当前浏览器。"}</p></div>}
    <div class="form-actions"><button class="button primary large" disabled={busy} type="submit">{busy ? "正在保存…" : session.user ? releasing ? "发布新版本" : editing ? "保存更新" : "发布作品" : "登录后发布"}<Icon name={session.user ? "arrow" : "github"} size={18} /></button><button class="button secondary" type="button" onClick={() => onNotice("草稿已保存在当前浏览器中。")}>保存草稿</button></div>
  </form></section><aside class="publish-aside"><section class="publish-guide panel"><span class="eyebrow">YOUR SPACE / 自由创作</span><h2>从一个链接开始。</h2><p>发布功能 Mod、皮肤、模型或工具，让其他终末地玩家找到你的作品。</p><ol><li><span>01</span><div><strong>给作品一个名字</strong><p>让大家知道你做了什么。</p></div></li><li><span>02</span><div><strong>选择合适的类型</strong><p>作品会出现在对应的分类中。</p></div></li><li><span>03</span><div><strong>贴上下载链接</strong><p>用户可以直接前往原地址下载。</p></div></li></ol></section><div class="publish-side-note"><Icon name="layers" size={20} /><p>文件由你选择的平台托管。<br />介绍、封面和版本名字都可以留空。</p></div></aside></div>;
}
