(() => {
  if (window.top !== window || window.betterEndfield) return;
  let next = 0; const pending = new Map(), listeners = new Set();
  const session = Math.random().toString(36).slice(2);
  function invoke(operation, payload) {
    const request_id = session + ':' + (++next);
    return new Promise((resolve, reject) => {
      const timeout = setTimeout(() => { pending.delete(request_id); reject(new Error('Host request timed out')); }, 15000);
      pending.set(request_id, { resolve, reject, timeout });
      const message = JSON.stringify({ protocol: 'better-endfield.module-ui.v1', request_id, operation, payload });
      try {
        if (window.chrome?.webview) window.chrome.webview.postMessage(JSON.parse(message));
        else window.BetterEndfieldModuleHost.postMessage(message);
      } catch (error) { clearTimeout(timeout); pending.delete(request_id); reject(error); }
    });
  }
  window.__beModuleDeliver = envelope => {
    if (envelope.kind === 'bridge_reply') {
      const item = pending.get(envelope.request_id); if (!item) return;
      clearTimeout(item.timeout); pending.delete(envelope.request_id);
      envelope.error ? item.reject(new Error(envelope.error)) : item.resolve(envelope.value);
    } else if (envelope.kind === 'runtime_message') {
      for (const listener of listeners) { try { listener(envelope.message); } catch (error) { console.error(error); } }
    }
  };
  if (window.chrome?.webview) window.chrome.webview.addEventListener('message', e => window.__beModuleDeliver(e.data));
  Object.defineProperty(window, 'betterEndfield', { value: Object.freeze({
    readConfig: () => invoke('readConfig'),
    saveConfig: configuration => invoke('saveConfig', configuration),
    send: body => invoke('send', body),
    status: () => invoke('status'),
    onmessage: listener => { if (typeof listener !== 'function') throw new TypeError('Expected a function'); listeners.add(listener); return () => listeners.delete(listener); }
  }) });
})();
