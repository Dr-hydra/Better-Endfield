import { useMemo, useRef, useState } from "preact/hooks";
import { qrMatrix, qrSvgPath } from "../lib/qr";
import { copyText } from "../lib/toy";

/**
 * Everything needed to get a record link onto another device: a code to scan,
 * and the link itself, selectable, with a copy button that says whether it
 * worked. Shown instead of the native share sheet outside the B站 app.
 */
export default function ShareDialog({ url, onClose }: { url: string; onClose: () => void }) {
  const [state, setState] = useState<"idle" | "copied" | "failed">("idle");
  const field = useRef<HTMLInputElement>(null);

  const code = useMemo(() => {
    try {
      const matrix = qrMatrix(url);
      return { path: qrSvgPath(matrix), span: matrix.length + 8 };
    } catch {
      return null;
    }
  }, [url]);

  async function copy() {
    if (await copyText(url)) return setState("copied");
    // Nothing left to automate — select it so one keystroke finishes the job.
    setState("failed");
    field.current?.select();
  }

  return (
    <div class="modal-backdrop" onClick={onClose}>
      <div class="share-modal" onClick={(event) => event.stopPropagation()}>
        <span class="eyebrow">SHARE / 分享这条记录</span>
        {code
          ? <svg class="share-qr" viewBox={`0 0 ${code.span} ${code.span}`} role="img" aria-label="记录链接二维码">
              <rect width={code.span} height={code.span} fill="#fff" />
              <path d={code.path} fill="#000" />
            </svg>
          : <p class="share-note">二维码生成失败，请直接使用下面的链接。</p>}
        <input
          ref={field}
          class="share-url"
          value={url}
          readOnly
          onFocus={(event) => (event.currentTarget as HTMLInputElement).select()}
        />
        <p class="share-note">
          {state === "copied" && "已复制到剪贴板。"}
          {state === "failed" && "浏览器不允许自动复制，链接已选中，按 Ctrl+C 即可。"}
          {state === "idle" && "扫码或复制链接，浏览器和 B站 App 都能打开。"}
        </p>
        <div class="share-actions">
          <button class="button primary" onClick={() => void copy()}>复制链接</button>
          <button class="button secondary" onClick={onClose}>关闭</button>
        </div>
      </div>
    </div>
  );
}
