using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Threading;
using BepInEx.Logging;

namespace EFStartProbe;

internal sealed class ProbePipeServer : IDisposable
{
    private readonly ProbeRuntime runtime;
    private readonly ManualLogSource logger;
    private readonly CancellationTokenSource cancellation = new();
    private readonly Thread thread;
    private bool disposed;

    public ProbePipeServer(ProbeRuntime runtime, ManualLogSource logger, int processId)
    {
        this.runtime = runtime;
        this.logger = logger;
        PipeName = $"ef-start-probe-{processId}";
        thread = new Thread(Run)
        {
            IsBackground = true,
            Name = "EFStartProbe.PipeServer"
        };
    }

    public string PipeName { get; }

    public void Start() => thread.Start();

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        cancellation.Cancel();
        if (!thread.Join(TimeSpan.FromSeconds(3)))
        {
            logger.LogWarning("Pipe server did not stop within three seconds.");
        }

        cancellation.Dispose();
    }

    private void Run()
    {
        while (!cancellation.IsCancellationRequested)
        {
            try
            {
                using var pipe = new NamedPipeServerStream(
                    PipeName,
                    PipeDirection.InOut,
                    1,
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);

                pipe.WaitForConnectionAsync(cancellation.Token).GetAwaiter().GetResult();
                Serve(pipe);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception exception)
            {
                logger.LogWarning($"Pipe server connection failed: {exception.Message}");
            }
        }
    }

    private void Serve(Stream pipe)
    {
        using var reader = new StreamReader(pipe, Encoding.UTF8, false, 4096, leaveOpen: true);
        using var writer = new StreamWriter(pipe, new UTF8Encoding(false), 4096, leaveOpen: true)
        {
            AutoFlush = true
        };

        while (!cancellation.IsCancellationRequested && pipe.CanRead && pipe.CanWrite)
        {
            var line = reader.ReadLine();
            if (line is null)
            {
                return;
            }

            var response = HandleLine(line);
            writer.WriteLine(response);
        }
    }

    private string HandleLine(string line)
    {
        string id = string.Empty;
        try
        {
            using var document = JsonDocument.Parse(line);
            var root = document.RootElement;
            id = root.GetProperty("id").GetString() ?? string.Empty;
            var command = root.GetProperty("command").GetString() ?? string.Empty;
            var args = root.TryGetProperty("args", out var argsNode) ? argsNode.Clone() : default;
            var envelope = new ProbeCommand { Id = id, Name = command, Args = args };
            runtime.EnqueueCommand(envelope);

            if (!envelope.Completion.Task.Wait(TimeSpan.FromSeconds(10)))
            {
                return SerializeResponse(id, false, null, "The Unity main thread did not process the command in time.");
            }

            var result = envelope.Completion.Task.Result;
            return SerializeResponse(id, result.Ok, result.Result, result.Error);
        }
        catch (Exception exception)
        {
            return SerializeResponse(id, false, null, exception.Message);
        }
    }

    private static string SerializeResponse(string id, bool ok, object? result, string? error)
    {
        return JsonSerializer.Serialize(new { id, ok, result, error }, ProbeJson.Options);
    }
}

