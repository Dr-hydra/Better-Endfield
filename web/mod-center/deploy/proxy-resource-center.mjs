// Added to the existing HTTPS proxy without altering its other routes.
import http from 'node:http';

const agent = new http.Agent({ keepAlive: true, maxSockets: 16, maxFreeSockets: 2 });
export function serveResourceCenter(req, res) {
  const pathname = (req.url || '/').split('?')[0];
  if (pathname !== '/endfield' && !pathname.startsWith('/endfield/')) return false;
  const headers = { ...req.headers };
  for (const name of ['forwarded', 'connection', 'keep-alive', 'proxy-authorization', 'proxy-authenticate', 'proxy-connection', 'te', 'trailer', 'transfer-encoding', 'upgrade', ...String(req.headers.connection || '').split(',').map(value => value.trim().toLowerCase())]) delete headers[name];
  headers.host = '127.0.0.1:9017';
  headers['x-forwarded-for'] = req.socket.remoteAddress || '';
  headers['x-forwarded-proto'] = 'https';
  const target = http.request({ hostname: '127.0.0.1', port: 9017, method: req.method, path: req.url, headers, agent, timeout: 20000 }, response => {
    const forwarded = { ...response.headers };
    for (const name of ['connection', 'keep-alive', 'transfer-encoding', ...String(response.headers.connection || '').split(',').map(value => value.trim().toLowerCase())]) delete forwarded[name];
    res.writeHead(response.statusCode || 502, forwarded);
    response.pipe(res); response.on('error', () => res.destroy());
  });
  target.on('timeout', () => target.destroy());
  target.on('error', () => {
    if (res.headersSent) return res.destroy();
    res.writeHead(502, { 'Content-Type': 'application/json; charset=utf-8', 'Cache-Control': 'no-store' });
    res.end(JSON.stringify({ error: '资源中心暂时不可用，请稍后重试。' }));
  });
  req.on('aborted', () => target.destroy());
  res.on('close', () => { if (!res.writableFinished) target.destroy(); });
  req.pipe(target);
  return true;
}
