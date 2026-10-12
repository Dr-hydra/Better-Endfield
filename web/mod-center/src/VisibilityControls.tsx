import { useState } from 'preact/hooks';
import { write, type Resource, type Session } from './api';
import { localizeMessage, type Language } from './messages';

export function visibilityLabel(state: Resource['visibility'], lang: Language) {
  if (state === 'admin-hidden') return lang === 'zh' ? '管理员隐藏' : 'Hidden by admin';
  if (state === 'author-hidden') return lang === 'zh' ? '作者隐藏' : 'Hidden by author';
  return lang === 'zh' ? '公开展示' : 'Public';
}

export function VisibilityControls({ resource, session, lang, onChanged }: { resource: Resource; session: Session; lang: Language; onChanged: () => void }) {
  const t = (zh: string, en: string) => lang === 'zh' ? zh : en;
  const [busy, setBusy] = useState(false), [error, setError] = useState(''), [reason, setReason] = useState('');
  const admin = session.isAdmin, owned = session.user?.id === resource.author.id;
  if (!admin && !owned) return null;
  async function change(hidden: boolean) {
    setBusy(true); setError('');
    try { await write(`resources/${resource.id}/visibility`, 'PUT', { hidden, reason }, session.csrf); onChanged(); }
    catch (value) { setError((value as Error).message); } finally { setBusy(false); }
  }
  return <section class={`visibility-panel panel ${resource.visibility === 'admin-hidden' ? 'admin-hidden' : ''}`} aria-label={t('作品展示状态', 'Work visibility')}>
    <div class="visibility-summary"><div><strong>{visibilityLabel(resource.visibility, lang)}</strong><p>{resource.admin_hidden ? t('此作品已被管理员隐藏，只有管理员可以恢复公开展示。', 'An administrator has hidden this work. Only an administrator can restore it.') : resource.author_hidden ? t('此作品已由作者隐藏，不会出现在公开目录中。', 'The author has hidden this work. It does not appear in the public catalog.') : t('此作品正在公开目录中展示。', 'This work is visible in the public catalog.')}</p>{resource.hide_reason && <p class="visibility-reason">{t('原因：', 'Reason: ')}{resource.hide_reason}</p>}</div><div class="visibility-actions">
      {!resource.admin_hidden && (admin || !resource.author_hidden) && <button class="button secondary" disabled={busy} onClick={() => change(true)}>{admin ? t('管理隐藏', 'Hide as admin') : t('隐藏作品', 'Hide work')}</button>}
      {resource.visibility !== 'public' && <button class="button primary" disabled={busy || (!admin && resource.admin_hidden)} onClick={() => change(false)}>{t('恢复公开', 'Restore public')}</button>}
    </div></div>
    {admin && !resource.admin_hidden && <label class="hide-reason-input"><span>{t('隐藏原因（选填，会作为提示展示）', 'Reason (optional, shown in the notice)')}</span><input value={reason} maxLength={500} disabled={busy} onInput={event => setReason(event.currentTarget.value)} /></label>}
    {error && <p class="form-error" role="alert">{localizeMessage(error, lang)}</p>}
  </section>;
}
