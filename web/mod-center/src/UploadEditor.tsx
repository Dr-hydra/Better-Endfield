import { useState } from 'preact/hooks';
import { base, upload, write, type Draft, type Resource, type Session } from './api';
import { localizeMessage, type Language } from './messages';

export function UploadEditor({ draft, setDraft, session, resource, releasing, lang, onBusy, onLogin }: { draft: Draft; setDraft: (update: (value: Draft) => Draft) => void; session: Session; resource: Resource | null; releasing: boolean; lang: Language; onBusy: (value: boolean) => void; onLogin: () => void }) {
  const t = (zh: string, en: string) => lang === 'zh' ? zh : en;
  const [busy, setBusy] = useState(false), [progress, setProgress] = useState(0), [error, setError] = useState('');
  const [fileName, setFileName] = useState(resource?.latest.file?.filename || '');
  const limits = session.uploadLimits || { imagesPerWork: 6, imageBytes: 5 * 1024 ** 2, fileBytes: 200 * 1024 ** 2 };
  async function receive(input: HTMLInputElement, kind: 'image' | 'file') {
    const files = Array.from(input.files || []); input.value = ''; if (!files.length) return;
    if (!session.user) return onLogin();
    setError('');
    if (kind === 'image' && draft.image_ids.length + files.length > limits.imagesPerWork) { setError('每个作品最多上传 6 张图片。'); return; }
    if (files.some(file => file.size > (kind === 'image' ? limits.imageBytes : limits.fileBytes))) { setError(kind === 'image' ? '图片不能超过 5 MB。' : '作品文件不能超过 200 MB。'); return; }
    setBusy(true); onBusy(true);
    try {
      for (const file of files) {
        setProgress(0); const result = await upload(file, kind, session.csrf, setProgress);
        if (kind === 'image') setDraft(value => ({ ...value, image_ids: [...value.image_ids, result.id] }));
        else { setDraft(value => ({ ...value, file_id: result.id, url: '' })); setFileName(result.filename); }
      }
    } catch (reason) { setError((reason as Error).message); } finally { setBusy(false); onBusy(false); }
  }
  async function removeImage(id: string) {
    setError('');
    if (!resource?.images.some(image => image.id === id)) {
      try { await write(`uploads/${id}`, 'DELETE', {}, session.csrf); } catch { /* A previously published image may still be referenced by the saved work. */ }
    }
    setDraft(value => ({ ...value, image_ids: value.image_ids.filter(item => item !== id) }));
  }
  return <div class="upload-editor">
    {session.canUploadFiles && <section class="file-upload-field"><strong>{t('作品文件', 'Work file')} <small>{t('或使用下方下载链接', 'or use a download link below')}</small></strong><label class="upload-input"><span>{t('上传文件（最大 200 MB）', 'Upload file (up to 200 MB)')}</span><input type="file" disabled={busy} onChange={event => receive(event.currentTarget, 'file')} /></label>{draft.file_id && <div class="uploaded-file"><span>{fileName || t('已上传的作品文件', 'Uploaded work file')}</span><button type="button" disabled={busy} onClick={() => setDraft(value => ({ ...value, file_id: '', url: '' }))}>{t('改用链接', 'Use a link')}</button></div>}</section>}
    {!releasing && <section class="image-upload-field"><strong>{t('展示图片', 'Gallery images')} <small>{draft.image_ids.length} / {limits.imagesPerWork}</small></strong><p>{t('每作品最多 6 张，单张 5 MB。支持 JPG、PNG、WebP，第一张作为封面。', 'Up to 6 images per work, 5 MB each. JPG, PNG, WebP. The first image is the cover.')}</p>{session.user ? <label class="upload-input"><span>{t('选择图片', 'Choose images')}</span><input type="file" accept="image/jpeg,image/png,image/webp" multiple disabled={busy || draft.image_ids.length >= limits.imagesPerWork} onChange={event => receive(event.currentTarget, 'image')} /></label> : <button class="button secondary" type="button" onClick={onLogin}>{t('登录后上传图片', 'Sign in to upload images')}</button>}
      <div class="upload-previews">{draft.image_ids.map((id, index) => <div key={id}><img src={`${base}media/${id}`} alt={t(`展示图 ${index + 1}`, `Gallery image ${index + 1}`)} />{index === 0 ? <span>{t('封面', 'Cover')}</span> : <button type="button" disabled={busy} onClick={() => setDraft(value => ({ ...value, image_ids: [id, ...value.image_ids.filter(item => item !== id)] }))}>{t('设为封面', 'Set as cover')}</button>}<button type="button" disabled={busy} onClick={() => removeImage(id)}>{t('移除', 'Remove')}</button></div>)}</div>
    </section>}
    {busy && <p class="upload-progress" role="status">{t('正在上传', 'Uploading')} {progress}%<progress value={progress} max={100} /></p>}
    {error && <p class="form-error" role="alert">{localizeMessage(error, lang)}</p>}
  </div>;
}
