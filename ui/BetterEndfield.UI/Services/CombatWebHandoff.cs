using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace BetterEndfield.UI.Services;

/// <summary>
/// Hands one combat record to the analysis page over loopback.
/// </summary>
/// <remarks>
/// <para>
/// 寻访快照走的是 <see cref="GachaSnapshotLink"/> 那条路：deflate + base64url 塞进 URL
/// fragment。战斗记录塞不进去。一份 40 秒的记录，把网页根本不读的
/// <c>ledger.attributeGroups</c> / <c>attributeWrites</c> / <c>zoneWrites</c> 全删掉之后
/// 仍有 69,000 个 base64 字符；连 <c>ledger</c> 整个删掉（代价是归因明细全没了）也还有
/// 40,500 个。而 <c>ShellExecute</c> 传 URL 的历史上限是 INTERNET_MAX_URL_LENGTH（2,083），
/// 就算现代浏览器自己能吃下更长的，中间这一段也不保证。差了一到两个数量级，不是压缩能补的。
/// </para>
/// <para>
/// 所以链接里只放坐标，不放数据：本进程在 127.0.0.1 上临时听一个端口，网页拿着
/// 端口号和一次性 nonce 回来取。<c>http://127.0.0.1</c> 属于 W3C 定义的
/// potentially trustworthy origin，HTTPS 页面 fetch 它不算混合内容，浏览器不拦。
/// </para>
/// <para>
/// 用裸 <see cref="TcpListener"/> 而不是 <see cref="HttpListener"/>：后者走 http.sys，
/// 非管理员注册 URL 前缀要先 <c>netsh http add urlacl</c>。绑 127.0.0.1 的 TCP 端口不需要
/// 任何权限，也不会弹防火墙。代价是要自己写这三十行 HTTP，而这里只需要 GET 和 OPTIONS。
/// </para>
/// </remarks>
public sealed class CombatWebHandoff : IDisposable
{
    /// <summary>How long the port stays open if the page never shows up.</summary>
    private static readonly TimeSpan Lifetime = TimeSpan.FromMinutes(3);
    private const int MaxRequestBytes = 8 * 1024;

    private static CombatWebHandoff? _current;
    private static readonly Lock CurrentLock = new();

    private readonly TcpListener _listener;
    private readonly byte[] _body;
    private readonly string _nonce;
    private readonly CancellationTokenSource _cancellation = new();
    private int _disposed;

    private CombatWebHandoff(TcpListener listener, string nonce, byte[] body)
    {
        _listener = listener;
        _nonce = nonce;
        _body = body;
    }

    public int Port => ((IPEndPoint)_listener.LocalEndpoint).Port;

    /// <summary>
    /// Publishes <paramref name="recordPath"/> and returns the page URL that
    /// fetches it. Any previous handoff is closed: one button, one record.
    /// </summary>
    public static string Publish(string baseUrl, string recordPath)
    {
        if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("网页地址不能为空", nameof(baseUrl));
        byte[] json = File.ReadAllBytes(recordPath);
        if (json.Length == 0) throw new InvalidDataException("战斗记录为空");

        // Gzipped on the way out. The record is mostly repeated keys, so this
        // turns a several-megabyte read into a few hundred kilobytes and the
        // browser inflates it for free. Sent unconditionally rather than gated
        // on Accept-Encoding: fetch decodes on Content-Encoding alone, and every
        // browser advertises gzip anyway.
        using var compressed = new MemoryStream();
        using (var gzip = new GZipStream(compressed, CompressionLevel.Fastest, leaveOpen: true))
            gzip.Write(json);

        string nonce = ToBase64Url(RandomNumberGenerator.GetBytes(16));
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var handoff = new CombatWebHandoff(listener, nonce, compressed.ToArray());

        lock (CurrentLock)
        {
            _current?.Dispose();
            _current = handoff;
        }
        handoff.Run();

        // Query string, not fragment. On Toy the page runs inside an iframe
        // whose src the outer page renders server-side: it copies the outer
        // query string across, but the fragment never reaches the server and
        // nothing forwards it client-side, so a `#/import/...` link arrives as
        // a plain homepage load.
        string separator = baseUrl.Contains('?') ? "&" : "?";
        return $"{baseUrl}{separator}import={handoff.Port}.{nonce}";
    }

    public static void CloseCurrent()
    {
        lock (CurrentLock)
        {
            _current?.Dispose();
            _current = null;
        }
    }

    private void Run()
    {
        _cancellation.CancelAfter(Lifetime);
        _ = Task.Run(async () =>
        {
            try
            {
                while (!_cancellation.IsCancellationRequested)
                {
                    using TcpClient client = await _listener.AcceptTcpClientAsync(_cancellation.Token);
                    // Served once. A second fetch would mean someone else asked.
                    if (await ServeAsync(client)) break;
                }
            }
            catch (OperationCanceledException) { }
            catch (SocketException) { }
            catch (IOException) { }
            finally { Dispose(); }
        });
    }

    /// <returns>true once the record has actually been delivered.</returns>
    private async Task<bool> ServeAsync(TcpClient client)
    {
        client.NoDelay = true;
        using NetworkStream stream = client.GetStream();
        string? request = await ReadRequestLineAsync(stream);
        if (request is null) return false;

        string[] parts = request.Split(' ');
        if (parts.Length < 2) return false;
        string method = parts[0];
        string target = parts[1];

        // Chrome's Private Network Access check preflights a public page
        // reaching into loopback, so OPTIONS has to be answered even though a
        // bare GET would otherwise be a "simple request".
        if (string.Equals(method, "OPTIONS", StringComparison.Ordinal))
        {
            await WriteAsync(stream,
                "HTTP/1.1 204 No Content\r\n" +
                "Access-Control-Allow-Origin: *\r\n" +
                "Access-Control-Allow-Methods: GET, OPTIONS\r\n" +
                "Access-Control-Allow-Headers: *\r\n" +
                "Access-Control-Allow-Private-Network: true\r\n" +
                "Access-Control-Max-Age: 600\r\n" +
                "Connection: close\r\n\r\n");
            return false;
        }

        bool authorised = string.Equals(method, "GET", StringComparison.Ordinal) &&
            string.Equals(target, $"/r/{_nonce}", StringComparison.Ordinal);
        if (!authorised)
        {
            await WriteAsync(stream,
                "HTTP/1.1 404 Not Found\r\n" +
                "Access-Control-Allow-Origin: *\r\n" +
                "Content-Length: 0\r\nConnection: close\r\n\r\n");
            return false;
        }

        await WriteAsync(stream,
            "HTTP/1.1 200 OK\r\n" +
            "Content-Type: application/json; charset=utf-8\r\n" +
            "Content-Encoding: gzip\r\n" +
            $"Content-Length: {_body.Length}\r\n" +
            "Access-Control-Allow-Origin: *\r\n" +
            "Access-Control-Allow-Private-Network: true\r\n" +
            "Cache-Control: no-store\r\n" +
            "Connection: close\r\n\r\n");
        await stream.WriteAsync(_body, _cancellation.Token);
        await stream.FlushAsync(_cancellation.Token);
        return true;
    }

    /// <summary>Reads the request line and drains the headers, capped so a
    /// misdirected client cannot make us buffer without bound.</summary>
    private async Task<string?> ReadRequestLineAsync(NetworkStream stream)
    {
        var buffer = new byte[1024];
        var text = new StringBuilder();
        while (text.Length < MaxRequestBytes)
        {
            int read = await stream.ReadAsync(buffer, _cancellation.Token);
            if (read <= 0) break;
            text.Append(Encoding.ASCII.GetString(buffer, 0, read));
            if (text.ToString().Contains("\r\n\r\n", StringComparison.Ordinal)) break;
        }
        string value = text.ToString();
        int end = value.IndexOf("\r\n", StringComparison.Ordinal);
        return end <= 0 ? null : value[..end];
    }

    private Task WriteAsync(NetworkStream stream, string headers) =>
        stream.WriteAsync(Encoding.ASCII.GetBytes(headers), _cancellation.Token).AsTask();

    private static string ToBase64Url(byte[] bytes) => Convert.ToBase64String(bytes)
        .Replace('+', '-').Replace('/', '_').TrimEnd('=');

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0) return;
        try { _cancellation.Cancel(); } catch (ObjectDisposedException) { }
        try { _listener.Stop(); } catch (SocketException) { }
        _cancellation.Dispose();
    }
}
