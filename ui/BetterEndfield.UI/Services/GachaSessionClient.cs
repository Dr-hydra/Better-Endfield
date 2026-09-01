using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using BetterEndfield.UI.Models;

namespace BetterEndfield.UI.Services;

public sealed class GachaSessionClient
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    public async Task<GachaSession?> TryGetSessionAsync(CancellationToken cancellationToken = default)
    {
        foreach (Process process in Process.GetProcessesByName("Endfield"))
        {
            using (process)
            {
                string pipe = $"BetterEndfield.Gacha.{process.Id}";
                try
                {
                    await using var client = new NamedPipeClientStream(".", pipe, PipeDirection.InOut,
                        PipeOptions.Asynchronous);
                    await client.ConnectAsync(250, cancellationToken);
                    byte[] request = Encoding.ASCII.GetBytes("GET\n");
                    await client.WriteAsync(request, cancellationToken);
                    await client.FlushAsync(cancellationToken);
                    byte[] lengthBytes = new byte[4];
                    await ReadExactlyAsync(client, lengthBytes, cancellationToken);
                    int length = BitConverter.ToInt32(lengthBytes, 0);
                    if (length <= 0 || length > 32 * 1024) continue;
                    byte[] payload = new byte[length];
                    await ReadExactlyAsync(client, payload, cancellationToken);
                    var session = JsonSerializer.Deserialize<GachaSession>(payload, Options);
                    if (session is { Ready: true, Token.Length: > 0 }) return session;
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                catch (TimeoutException) { }
                catch (JsonException) { }
            }
        }
        return null;
    }

    private static async Task ReadExactlyAsync(Stream stream, byte[] buffer, CancellationToken token)
    {
        int offset = 0;
        while (offset < buffer.Length)
        {
            int read = await stream.ReadAsync(buffer.AsMemory(offset), token);
            if (read == 0) throw new EndOfStreamException();
            offset += read;
        }
    }
}
