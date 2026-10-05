using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using BepInEx.Logging;

namespace EFStartProbe;

internal sealed class EventSink : IDisposable
{
    private readonly BlockingCollection<ProbeEvent> queue = new(50_000);
    private readonly ManualLogSource logger;
    private readonly Thread writerThread;
    private readonly string eventsPath;
    private long dropped;
    private bool disposed;

    public EventSink(string outputDirectory, ManualLogSource logger)
    {
        this.logger = logger;
        Directory.CreateDirectory(outputDirectory);
        eventsPath = Path.Combine(outputDirectory, "events.jsonl");
        writerThread = new Thread(WriteLoop)
        {
            IsBackground = true,
            Name = "EFStartProbe.EventWriter"
        };
        writerThread.Start();
    }

    public long Dropped => Interlocked.Read(ref dropped);

    public bool TryWrite(ProbeEvent value)
    {
        if (disposed || queue.IsAddingCompleted)
        {
            return false;
        }

        if (queue.TryAdd(value))
        {
            return true;
        }

        Interlocked.Increment(ref dropped);
        return false;
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        queue.CompleteAdding();
        if (!writerThread.Join(TimeSpan.FromSeconds(5)))
        {
            logger.LogWarning("Event writer did not stop within five seconds.");
        }

        queue.Dispose();
    }

    private void WriteLoop()
    {
        try
        {
            using var stream = new FileStream(
                eventsPath,
                FileMode.Append,
                FileAccess.Write,
                FileShare.Read,
                64 * 1024,
                FileOptions.SequentialScan);
            using var writer = new StreamWriter(stream, new UTF8Encoding(false), 64 * 1024)
            {
                AutoFlush = false
            };

            var lastFlush = DateTime.UtcNow;
            foreach (var item in queue.GetConsumingEnumerable())
            {
                writer.WriteLine(JsonSerializer.Serialize(item, ProbeJson.Options));
                if ((DateTime.UtcNow - lastFlush).TotalSeconds >= 1)
                {
                    writer.Flush();
                    lastFlush = DateTime.UtcNow;
                }
            }

            writer.Flush();
        }
        catch (Exception exception)
        {
            logger.LogError($"Event writer failed: {exception}");
        }
    }
}

