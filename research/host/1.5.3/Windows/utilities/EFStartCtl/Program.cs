using System.Text.Json;

namespace EFStartCtl;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 2;
            }

            return args[0].ToLowerInvariant() switch
            {
                "ping" => await SendAsync(args, "ping").ConfigureAwait(false),
                "status" => await SendAsync(args, "status").ConfigureAwait(false),
                "arm" => await SendAsync(args, "arm", ParseArmArgs(args)).ConfigureAwait(false),
                "snapshot" => await SendAsync(args, "snapshot").ConfigureAwait(false),
                "mark" => await SendAsync(args, "mark", ParseMarkArgs(args)).ConfigureAwait(false),
                "stop" => await SendAsync(args, "stop").ConfigureAwait(false),
                "report" => BuildReport(args),
                "self-test" => SelfTest(),
                _ => throw new ArgumentException($"Unknown command: {args[0]}")
            };
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.Message);
            return 1;
        }
    }

    private static async Task<int> SendAsync(
        IReadOnlyList<string> args,
        string command,
        IReadOnlyDictionary<string, object?>? commandArgs = null)
    {
        var processId = GetRequiredIntOption(args, "--pid");
        var request = new ProbeRequest(Guid.NewGuid().ToString("N"), command, commandArgs);
        var response = await ProbePipeClient.SendAsync(processId, request, TimeSpan.FromSeconds(10))
            .ConfigureAwait(false);

        Console.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions(ProbeProtocol.JsonOptions)
        {
            WriteIndented = true
        }));
        return response.Ok ? 0 : 1;
    }

    private static IReadOnlyDictionary<string, object?> ParseArmArgs(IReadOnlyList<string> args)
    {
        var seconds = GetOptionalIntOption(args, "--seconds", 90);
        if (seconds is < 1 or > 3600)
        {
            throw new ArgumentOutOfRangeException(nameof(args), "--seconds must be between 1 and 3600.");
        }

        return new Dictionary<string, object?> { ["durationSeconds"] = seconds };
    }

    private static IReadOnlyDictionary<string, object?> ParseMarkArgs(IReadOnlyList<string> args)
    {
        var label = GetRequiredStringOption(args, "--label");
        return new Dictionary<string, object?> { ["label"] = label };
    }

    private static int BuildReport(IReadOnlyList<string> args)
    {
        var runDirectory = Path.GetFullPath(GetRequiredStringOption(args, "--run"));
        var candidates = ReportBuilder.Build(runDirectory);
        Console.WriteLine($"Wrote report for {candidates.Count} candidate(s) to {runDirectory}");
        return 0;
    }

    private static int SelfTest()
    {
        var request = new ProbeRequest("test", "ping");
        var json = JsonSerializer.Serialize(request, ProbeProtocol.JsonOptions);
        var roundTrip = JsonSerializer.Deserialize<ProbeRequest>(json, ProbeProtocol.JsonOptions);
        if (roundTrip is null || roundTrip.Id != "test" || roundTrip.Command != "ping")
        {
            throw new InvalidOperationException("Protocol JSON round trip failed.");
        }

        Console.WriteLine("Self-test passed.");
        return 0;
    }

    private static int GetRequiredIntOption(IReadOnlyList<string> args, string name)
    {
        var value = GetRequiredStringOption(args, name);
        return int.TryParse(value, out var parsed)
            ? parsed
            : throw new ArgumentException($"{name} requires an integer value.");
    }

    private static int GetOptionalIntOption(IReadOnlyList<string> args, string name, int defaultValue)
    {
        var index = IndexOf(args, name);
        if (index < 0)
        {
            return defaultValue;
        }

        if (index + 1 >= args.Count || !int.TryParse(args[index + 1], out var value))
        {
            throw new ArgumentException($"{name} requires an integer value.");
        }

        return value;
    }

    private static string GetRequiredStringOption(IReadOnlyList<string> args, string name)
    {
        var index = IndexOf(args, name);
        if (index < 0 || index + 1 >= args.Count)
        {
            throw new ArgumentException($"Missing required option {name}.");
        }

        return args[index + 1];
    }

    private static int IndexOf(IReadOnlyList<string> args, string name)
    {
        for (var index = 0; index < args.Count; index++)
        {
            if (string.Equals(args[index], name, StringComparison.OrdinalIgnoreCase))
            {
                return index;
            }
        }

        return -1;
    }

    private static void PrintUsage()
    {
        Console.WriteLine("efctl ping|status|snapshot|stop --pid <pid>");
        Console.WriteLine("efctl arm --pid <pid> [--seconds 90]");
        Console.WriteLine("efctl mark --pid <pid> --label <text>");
        Console.WriteLine("efctl report --run <directory>");
        Console.WriteLine("efctl self-test");
    }
}

