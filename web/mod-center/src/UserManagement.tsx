import { useEffect, useState } from 'preact/hooks';
import { request, type AdminUserPage, type Session } from './api';
import { type BanTarget } from './BanDialog';
import { localizeMessage, type Language } from './messages';

export function UserManagement({
  session,
  lang,
  revision,
  onSelectBan,
}: {
  session: Session;
  lang: Language;
  revision: number;
  onSelectBan: (target: BanTarget) => void;
}) {
  const t = (zh: string, en: string) => (lang === 'zh' ? zh : en);
  const [query, setQuery] = useState('');
  const [debounced, setDebounced] = useState('');
  const [bannedFilter, setBannedFilter] = useState('1');
  const [page, setPage] = useState(1);
  const [data, setData] = useState<AdminUserPage>({ items: [], total: 0, bannedTotal: 0, page: 1, pages: 1 });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    const timer = setTimeout(() => setDebounced(query), 250);
    return () => clearTimeout(timer);
  }, [query]);

  useEffect(() => setPage(1), [debounced, bannedFilter]);

  useEffect(() => {
    if (!session.isAdmin) return;
    let stale = false;
    setLoading(true);
    setError('');
    const params = new URLSearchParams({ q: debounced, banned: bannedFilter, page: String(page) });
    request<AdminUserPage>(`admin/users?${params}`)
      .then(result => { if (!stale) setData(result); })
      .catch(reason => { if (!stale) setError((reason as Error).message); })
      .finally(() => { if (!stale) setLoading(false); });
    return () => { stale = true; };
  }, [session.isAdmin, debounced, bannedFilter, page, revision]);

  return (
    <section class="user-management">
      <div class="catalog-toolbar panel">
        <label class="search-field">
          <input
            aria-label={t('搜索用户', 'Search users')}
            placeholder={t('按用户名、GitHub ID 或账号 ID 搜索…', 'Search by login, GitHub ID, or user ID…')}
            value={query}
            maxLength={120}
            onInput={event => setQuery(event.currentTarget.value)}
          />
          {query && <button aria-label={t('清空搜索', 'Clear search')} onClick={() => setQuery('')}>×</button>}
        </label>
        <label class="sort-field">
          <span>{t('账号状态', 'Status')}</span>
          <select value={bannedFilter} onChange={event => setBannedFilter(event.currentTarget.value)}>
            <option value="1">{t(`已封禁 (${data.bannedTotal})`, `Banned (${data.bannedTotal})`)}</option>
            <option value="">{t('全部用户', 'All users')}</option>
            <option value="0">{t('正常账号', 'Active')}</option>
          </select>
        </label>
      </div>

      {error && <p class="form-error" role="alert">{localizeMessage(error, lang)}</p>}

      {loading ? (
        <div class="portal-empty panel">{t('正在读取用户列表…', 'Loading users…')}</div>
      ) : data.items.length ? (
        <div class="admin-user-list">
          {data.items.map(item => (
            <article class={`admin-user-row panel ${item.banned ? 'is-banned' : ''}`} key={item.id}>
              <div class="admin-user-info">
                {item.avatar && <img src={item.avatar} alt="" referrerPolicy="no-referrer" />}
                <div>
                  <div class="admin-user-title">
                    <strong>{item.login}</strong>
                    {item.is_admin && <span class="visibility-badge public">{t('管理员', 'Admin')}</span>}
                    {item.banned && <span class="visibility-badge admin-hidden">{t('已封禁', 'Banned')}</span>}
                  </div>
                  <p class="admin-user-meta">
                    {item.github_id ? `GitHub #${item.github_id}` : t('邮箱账号', 'Email account')}
                    {' · '}
                    {t(`作品 ${item.works_count} · 评论 ${item.comments_count}`, `${item.works_count} works · ${item.comments_count} comments`)}
                  </p>
                  {item.banned && item.ban_reason && (
                    <p class="visibility-reason">{t('原因：', 'Reason: ')}{item.ban_reason}</p>
                  )}
                </div>
              </div>
              {!item.is_admin && item.id !== session.user?.id && (
                <button
                  class={`button ${item.banned ? 'secondary' : 'danger'}`}
                  onClick={() => onSelectBan({ id: item.id, login: item.login, banned: item.banned, reason: item.ban_reason })}
                >
                  {item.banned ? t('解封', 'Unban') : t('封禁', 'Ban')}
                </button>
              )}
            </article>
          ))}
        </div>
      ) : (
        <section class="portal-empty panel">
          <h3>{t('没有匹配的用户', 'No matching users')}</h3>
          <p>{bannedFilter === '1' ? t('当前没有被封禁的账号。', 'No banned accounts right now.') : t('试试换个关键词。', 'Try another keyword.')}</p>
        </section>
      )}

      {data.pages > 1 && (
        <nav class="pagination" aria-label={t('用户分页', 'User pages')}>
          <button class="button secondary" disabled={page <= 1} onClick={() => setPage(page - 1)}>{t('上一页', 'Previous')}</button>
          <span>{page} / {data.pages}</span>
          <button class="button secondary" disabled={page >= data.pages} onClick={() => setPage(page + 1)}>{t('下一页', 'Next')}</button>
        </nav>
      )}
    </section>
  );
}
