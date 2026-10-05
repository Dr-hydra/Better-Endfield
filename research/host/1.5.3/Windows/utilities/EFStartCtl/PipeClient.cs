using System.IO.Pipes;
using System.Text;
using System.Text.Json;

namespace EFStartCtl;

public static class ProbePipeClient
{
    public static async Task<ProbeResponse> SendAsync(
        int processId,
        ProbeRequest request,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutSource.CancelAfter(timeout);

        await using var pipe = new NamedPipeClientStream(
            ".",
            $"ef-start-probe-{processId}",
            PipeDirection.InOut,
            PipeOptions.Asynchronous,
            System.Security.Principal.TokenImpersonationLevel.Identification);

        await pipe.ConnectAsync(timeoutSource.Token).ConfigureAwait(false);

        using var writer = new StreamWriter(pipe, new UTF8Encoding(false), leaveOpen: true)
        {
            AutoFlush = true
        };
        using var reader = new StreamReader(pipe, Encoding.UTF8, leaveOpen: true);

        var payload = JsonSerializer.Serialize(request, ProbeProtocol.JsonOptions);
        await writer.WriteLineAsync(payload.AsMemory(), timeoutSource.Token).ConfigureAwait(false);

        var line = await reader.ReadLineAsync(timeoutSource.Token).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(line))
        {
            throw new InvalidDataException("The probe closed the pipe without a response.");
        }

        return JsonSerializer.Deserialize<ProbeResponse>(line, ProbeProtocol.JsonOptions)
            ?? throw new InvalidDataException("The probe returned invalid JSON.");
    }
}

