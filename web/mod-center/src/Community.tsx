import { useEffect, useState } from 'preact/hooks';
import { request, write, type CommentPage, type Resource, type Session, type SocialStats } from './api';
import { localizeMessage, type Language } from './messages';

export function Community({ resource, session, lang, onLogin }: { resource: Resource; session: Session; lang: Language; onLogin: () => void }) {
  const t = (zh: string, en: string) => lang === 'zh' ? zh : en;
  const [stats, setStats] = useState<SocialStats>({ likes: resource.likes, comments: resource.comments, liked: resource.liked });
  const [comments, setComments] = useState<CommentPage>({ items: [], total: 0, page: 1, pages: 1 });
  const [page, setPage] = useState(1), [revision, setRevision] = useState(0);
  const [body, setBody] = useState(''), [error, setError] = useState('');
  const [loading, setLoading] = useState(false), [busy, setBusy] = useState(false);
  useEffect(() => { setStats({ likes: resource.likes, comments: resource.comments, liked: resource.liked }); }, [resource]);
  useEffect(() => {
    let stale = false; setLoading(true);
    request<CommentPage>(`resources/${resource.id}/comments?page=${page}`).then(result => { if (!stale) { setComments(result); setStats(value => ({ ...value, comments: result.total })); } }).catch(reason => { if (!stale) setError(reason.message); }).finally(() => { if (!stale) setLoading(false); });
    return () => { stale = true; };
  }, [resource.id, page, revision, session.user?.id]);
  async function like() {
    if (!session.user) return onLogin();
    setBusy(true); setError('');
    try { setStats(await write<SocialStats>(`resources/${resource.id}/likes`, stats.liked ? 'DELETE' : 'PUT', {}, session.csrf)); }
    catch (reason) { setError((reason as Error).message); } finally { setBusy(false); }
  }
  async function submit(event: SubmitEvent) {
    event.preventDefault(); if (!session.user) return onLogin();
    setBusy(true); setError('');
    try { setStats(await write<SocialStats>(`resources/${resource.id}/comments`, 'POST', { body }, session.csrf)); setBody(''); setPage(1); setRevision(value => value + 1); }
    catch (reason) { setError((reason as Error).message); } finally { setBusy(false); }
  }
  async function remove(id: string) {
    setBusy(true); setError('');
    try { setStats(await write<SocialStats>(`resources/${resource.id}/comments/${id}`, 'DELETE', {}, session.csrf)); setRevision(value => value + 1); }
    catch (reason) { setError((reason as Error).message); } finally { setBusy(false); }
  }
  return <section class="community-panel panel" aria-labelledby="comments-title">
    <div class="community-heading"><div><span class="eyebrow">COMMUNITY / {t('交流', 'DISCUSSION')}</span><h2 id="comments-title">{t('评论', 'Comments')} <small>{stats.comments}</small></h2></div><button class={`button secondary like-button ${stats.liked ? 'liked' : ''}`} disabled={busy} onClick={like} aria-pressed={stats.liked}>{stats.liked ? '♥' : '♡'} {stats.liked ? t('已点赞', 'Liked') : t('点赞', 'Like')} <span>{stats.likes}</span></button></div>
    {session.user ? <form class="comment-form" onSubmit={submit}><label for="comment-body">{t('分享你的体验或建议', 'Share your experience or suggestions')}</label><textarea id="comment-body" maxLength={2000} rows={3} required value={body} disabled={busy} onInput={event => setBody(event.currentTarget.value)} placeholder={t('写下评论…', 'Write a comment…')} /><div><small>{body.length} / 2000</small><button class="button primary" type="submit" disabled={busy || !body.trim()}>{busy ? t('请稍候…', 'Please wait…') : t('发送评论', 'Post comment')}</button></div></form> : <div class="comment-login"><p>{t('登录后可以评论和点赞。', 'Sign in to comment and like.')}</p><button class="button secondary" onClick={onLogin}>{t('登录', 'Sign in')}</button></div>}
    {error && <p class="form-error" role="alert">{localizeMessage(error, lang)}</p>}
    {loading ? <p class="comment-empty" aria-busy="true">{t('正在读取评论…', 'Loading comments…')}</p> : comments.items.length ? <div class="comment-list">{comments.items.map(comment => <article class="comment-item" key={comment.id}><div class="comment-meta"><span>{comment.author.avatar && <img src={comment.author.avatar} alt="" referrerPolicy="no-referrer" />}<b>{comment.author.login}</b></span><time>{new Intl.DateTimeFormat(lang === 'zh' ? 'zh-CN' : 'en-US', { dateStyle: 'medium', timeStyle: 'short' }).format(comment.created_at)}</time>{comment.canDelete && <button disabled={busy} onClick={() => remove(comment.id)}>{t('删除', 'Delete')}</button>}</div><p class="author-content">{comment.body}</p></article>)}</div> : <p class="comment-empty">{t('还没有评论，来分享第一条吧。', 'No comments yet. Start the conversation.')}</p>}
    {comments.pages > 1 && <nav class="pagination" aria-label={t('评论分页', 'Comment pages')}><button class="button secondary" disabled={page === 1 || loading} onClick={() => setPage(page - 1)}>{t('上一页', 'Previous')}</button><span>{page} / {comments.pages}</span><button class="button secondary" disabled={page >= comments.pages || loading} onClick={() => setPage(page + 1)}>{t('下一页', 'Next')}</button></nav>}
  </section>;
}
