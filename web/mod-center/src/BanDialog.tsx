import { useEffect, useRef, useState } from 'preact/hooks';
import { write, type AdminUser, type Session } from './api';
import { localizeMessage, type Language } from './messages';

export interface BanTarget {
  id: string;
  login: string;
  banned: boolean;
  reason?: string;
}

export function BanDialog({
  target,
  session,
  lang,
  onClose,
  onDone,
}: {
  target: BanTarget;
  session: Session;
  lang: Language;
  onClose: () => void;
  onDone: (result: AdminUser, action: { banned: boolean; hideWorks: boolean; deleteComments: boolean }) => void;
}) {
  const t = (zh: string, en: string) => (lang === 'zh' ? zh : en);
  const unban = target.banned;
  const [reason, setReason] = useState(unban ? target.reason || '' : '');
  const [hideWorks, setHideWorks] = useState(true);
  const [deleteComments, setDeleteComments] = useState(false);
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState('');
  const dialog = useRef<HTMLElement>(null);

  useEffect(() => {
    const onKey = (event: KeyboardEvent) => {
      if (event.key === 'Escape' && !busy) onClose();
    };
    document.addEventListener('keydown', onKey);
    return () => document.removeEventListener('keydown', onKey);
  }, [busy]);

  async function submit(event: SubmitEvent) {
    event.preventDefault();
    setBusy(true);
    setError('');
    try {
      const payload = unban
        ? { banned: false }
        : { banned: true, reason, hideWorks, deleteComments };
      const updated = await write<AdminUser>(`users/${target.id}/ban`, 'PUT', payload, session.csrf);
      onDone(updated, { banned: !unban, hideWorks: !unban && hideWorks, deleteComments: !unban && deleteComments });
    } catch (value) {
      setError((value as Error).message);
    } finally {
      setBusy(false);
    }
  }

  return (
    <div class="dialog-overlay" onClick={event => { if (event.target === event.currentTarget && !busy) onClose(); }}>
      <section ref={dialog} class="delete-dialog ban-dialog panel" role="dialog" aria-modal="true" aria-labelledby="ban-title">
        <span class="eyebrow">{unban ? 'UNBAN USER' : 'BAN USER'}</span>
        <h2 id="ban-title">
          {unban ? t(`解封「${target.login}」？`, `Unban “${target.login}”?`) : t(`封禁「${target.login}」？`, `Ban “${target.login}”?`)}
        </h2>
        <p>
          {unban
            ? t('解封后该账号可以重新登录和发布内容。此前已被管理员隐藏的作品不会自动恢复公开。', 'This account will be able to sign in and post again. Works hidden by administrators stay hidden.')
            : t('封禁后该账号的现有会话会立即失效，且无法再通过同一 GitHub 账号或已绑定邮箱登录。', 'Active sessions are revoked immediately, and the same GitHub account or linked email can no longer sign in.')}
        </p>
        <form class="ban-form" onSubmit={submit}>
          {unban ? (
            target.reason && (
              <p class="visibility-reason">
                {t('原封禁原因：', 'Ban reason: ')}
                {target.reason}
              </p>
            )
          ) : (
            <>
              <label class="hide-reason-input">
                <span>{t('封禁原因（选填）', 'Reason (optional)')}</span>
                <input
                  value={reason}
                  maxLength={500}
                  disabled={busy}
                  placeholder={t('例如：重复刷屏、恶意链接…', 'e.g. spam, malicious links…')}
                  onInput={event => setReason(event.currentTarget.value)}
                />
              </label>
              <label class="ban-checkbox">
                <input
                  type="checkbox"
                  checked={hideWorks}
                  disabled={busy}
                  onChange={event => setHideWorks(event.currentTarget.checked)}
                />
                <span>{t('同时将此作者的公开作品设为管理员隐藏', "Hide this author's public works as admin")}</span>
              </label>
              <label class="ban-checkbox">
                <input
                  type="checkbox"
                  checked={deleteComments}
                  disabled={busy}
                  onChange={event => setDeleteComments(event.currentTarget.checked)}
                />
                <span>{t('同时删除此用户的全部评论', "Delete all of this user's comments")}</span>
              </label>
            </>
          )}
          {error && <p class="form-error" role="alert">{localizeMessage(error, lang)}</p>}
          <div class="ban-actions">
            <button class="button secondary" type="button" disabled={busy} onClick={onClose}>
              {t('取消', 'Cancel')}
            </button>
            <button class={`button ${unban ? 'primary' : 'danger'}`} type="submit" disabled={busy}>
              {busy ? t('处理中…', 'Working…') : unban ? t('确认解封', 'Unban') : t('确认封禁', 'Ban user')}
            </button>
          </div>
        </form>
      </section>
    </div>
  );
}
