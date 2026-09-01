using System.IO.Compression;
using System.Text.Json;
using BetterEndfield.UI.Models;

namespace BetterEndfield.UI.Services;

public static class GachaSnapshotLink
{
    private const string Prefix = "gacha:v";
    private const int MaxEncodedChars = 64 * 1024;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    public static string Encode(GachaWebSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        if (snapshot.SchemaVersion != GachaWebSnapshotBuilder.CurrentSchemaVersion)
            throw new NotSupportedException($"不支持的寻访快照版本：{snapshot.SchemaVersion}");
        if (!string.Equals(snapshot.Kind, GachaWebSnapshotBuilder.SnapshotKind, StringComparison.Ordinal))
            throw new InvalidDataException("寻访快照类型不正确");

        byte[] json = JsonSerializer.SerializeToUtf8Bytes(snapshot, JsonOptions);
        using var output = new MemoryStream();
        using (var deflate = new DeflateStream(output, CompressionLevel.SmallestSize, leaveOpen: true))
            deflate.Write(json);
        string payload = ToBase64Url(output.ToArray());
        string encoded = $"{Prefix}{snapshot.SchemaVersion}:{payload}";
        if (encoded.Length > MaxEncodedChars)
            throw new InvalidDataException("寻访快照过大，无法安全放入网页链接");
        return encoded;
    }

    public static string BuildUrl(string baseUrl, GachaWebSnapshot snapshot)
    {
        if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("网页地址不能为空", nameof(baseUrl));
        string separator = baseUrl.Contains('?', StringComparison.Ordinal) ? "&" : "?";
        return $"{baseUrl}{separator}mode=gacha#{Encode(snapshot)}";
    }

    public static GachaWebSnapshot Decode(string fragment)
    {
        string value = fragment.Trim().TrimStart('#');
        if (!value.StartsWith(Prefix, StringComparison.Ordinal))
            throw new InvalidDataException("不是寻访快照链接");
        int colon = value.IndexOf(':', Prefix.Length);
        if (colon < 0 || !int.TryParse(value.AsSpan(Prefix.Length, colon - Prefix.Length), out int version))
            throw new InvalidDataException("寻访快照版本格式无效");
        if (version != GachaWebSnapshotBuilder.CurrentSchemaVersion)
            throw new NotSupportedException($"不支持的寻访快照版本：{version}");
        byte[] compressed = FromBase64Url(value[(colon + 1)..]);
        using var input = new MemoryStream(compressed);
        using var deflate = new DeflateStream(input, CompressionMode.Decompress);
        using var json = new MemoryStream();
        deflate.CopyTo(json);
        GachaWebSnapshot? snapshot = JsonSerializer.Deserialize<GachaWebSnapshot>(json.ToArray(), JsonOptions);
        if (snapshot is null || snapshot.SchemaVersion != version ||
            !string.Equals(snapshot.Kind, GachaWebSnapshotBuilder.SnapshotKind, StringComparison.Ordinal))
            throw new InvalidDataException("寻访快照内容无效");
        return snapshot;
    }

    private static string ToBase64Url(byte[] bytes) => Convert.ToBase64String(bytes)
        .Replace('+', '-').Replace('/', '_').TrimEnd('=');

    private static byte[] FromBase64Url(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MaxEncodedChars)
            throw new InvalidDataException("寻访快照数据长度无效");
        string padded = value.Replace('-', '+').Replace('_', '/') +
            new string('=', (4 - value.Length % 4) % 4);
        try { return Convert.FromBase64String(padded); }
        catch (FormatException exception) { throw new InvalidDataException("寻访快照编码无效", exception); }
    }
}
