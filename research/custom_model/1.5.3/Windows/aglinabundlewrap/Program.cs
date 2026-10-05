using AnimeStudio;
using System.Buffers.Binary;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.Json;

if (args.Length != 3) throw new ArgumentException("source UnityFS, native header template, output bundle required");
var source = Path.GetFullPath(args[0]);
var template = Path.GetFullPath(args[1]);
var output = Path.GetFullPath(args[2]);
using var originalReader = new FileReader(source);
var original = new BundleFile(originalReader, GameManager.GetGame(GameType.Normal));
if (Environment.GetEnvironmentVariable("AGLINA_INSPECT_SCHEMA") == "1") {
    var manager = new AssetsManager { Game = GameManager.GetGame(GameType.Normal) };
    foreach (var file in original.fileList) {
        file.stream.Position = 0;
        using var reader = new FileReader(file.path, file.stream, true);
        var serialized = new SerializedFile(reader, manager);
        foreach (var type in serialized.m_Types.Where(t => t.classID == 74)) {
            var fields = type.m_Type.m_Nodes.Select(n => new { n.m_Level, n.m_Type, n.m_Name, n.m_ByteSize });
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(output)!, "standard-clip-schema.json"),
                JsonSerializer.Serialize(fields, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Standard clip fields: " + string.Join(", ", type.m_Type.m_Nodes.Where(n => n.m_Level == 1).Select(n => n.m_Name)));
        }
    }
    return;
}
using var nativeReader = new FileReader(template);
var native = new VFSFile(nativeReader, template, GameType.ArknightsEndfield);
Console.WriteLine($"Native header: {native.m_Header}, encFlags={native.m_Header.encFlags}");
if (native.m_Header.encFlags < 7) throw new InvalidDataException("Requires current 48-byte header template");
var header = File.ReadAllBytes(template).Take(48).ToArray();
// Existing VFSUtils uses ror64(18) here, but actual shipped headers use rol64(14).
// Its front-directory reader never relies on total size, hiding the decoder bug.
var samples = Directory.GetFiles(Path.GetDirectoryName(template)!, "*.ab");
foreach (var sample in samples) {
    using var r = new FileReader(sample);
    if (!VFSUtils.IsValidHeader(r, GameType.ArknightsEndfield)) continue;
    CheckHeaderSize(File.ReadAllBytes(sample));
}
Console.WriteLine($"Verified actual 64-bit size formula against {samples.Length} original archives.");
var nodes = Nodes(original);
var encoded = Wrap(original.fileList, nodes, header);
Directory.CreateDirectory(Path.GetDirectoryName(output)!);
File.WriteAllBytes(output, encoded);
Verify(output, original.fileList, nodes);

// Exercise the same encoder with original game contents and a different node layout.
var nativeNodes = Nodes(native);
var nativeRewrapped = Wrap(native.fileList, nativeNodes, header);
using (var roundtripReader = new FileReader("native-roundtrip.ab", new MemoryStream(nativeRewrapped))) {
    var roundtrip = new VFSFile(roundtripReader, template, GameType.ArknightsEndfield);
    Compare(roundtrip, native.fileList, nativeNodes);
}
File.WriteAllText(Path.Combine(Path.GetDirectoryName(output)!, "wrap-report.json"), JsonSerializer.Serialize(new {
    source, template, output, sourceBytes = new FileInfo(source).Length, outputBytes = encoded.Length,
    codec = "Endfield release VFS, 48-byte header, uncompressed info/data, 128KiB blocks",
    templateEncFlags = native.m_Header.encFlags, embeddedFiles = nodes.Select(n => new { n.path, n.size, n.flags }),
    independentDecoder = "AnimeStudio.VFSFile", payloadByteEquality = true, nativeSampleRoundtrip = true,
    headerSizeValidationSamples = samples.Length,
    decoderCaveat = "VFSUtils.ReadHeader size rotation is wrong; corrected rol64(14) independently checked against native file lengths",
    runtimeAcceptance = "NOT TESTED", serializedPayload = "unchanged embedded files; wrapper performs no serialized-schema conversion",
    sourceUnityRevision = original.m_Header.unityRevision
}, new JsonSerializerOptions { WriteIndented = true }));
Console.WriteLine($"PASS: {nodes.Count} embedded files byte-equal, native sample roundtrip passed. Output {encoded.Length} bytes.");

static List<BundleFile.Node> Nodes(object bundle) => (List<BundleFile.Node>)bundle.GetType()
    .GetField("m_DirectoryInfo", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(bundle)!;
static byte[] Bytes(Stream stream) { stream.Position = 0; using var copy = new MemoryStream(); stream.CopyTo(copy); return copy.ToArray(); }
static void Verify(string path, List<StreamFile> files, List<BundleFile.Node> nodes) {
    using var reader = new FileReader(path);
    var decoded = new VFSFile(reader, path, GameType.ArknightsEndfield);
    CheckHeaderSize(File.ReadAllBytes(path));
    Compare(decoded, files, nodes);
}
static void CheckHeaderSize(byte[] bytes) {
    uint low = BinaryPrimitives.ReadUInt32BigEndian(bytes.AsSpan(18));
    uint high = BinaryPrimitives.ReadUInt32BigEndian(bytes.AsSpan(34)) ^ low ^ 0xDAD76848;
    ulong actual = BitOperations.RotateLeft(((ulong)high << 32) | low, 14) ^ 0xA4F1A11747816520UL;
    if (actual != (ulong)bytes.Length) throw new InvalidDataException($"Native header size mismatch: {actual} != {bytes.Length}");
}
static void Compare(VFSFile decoded, List<StreamFile> files, List<BundleFile.Node> nodes) {
    var decodedNodes = Nodes(decoded);
    if (decoded.fileList.Count != files.Count) throw new InvalidDataException("File count mismatch");
    for (int i = 0; i < files.Count; i++)
        if (decoded.fileList[i].path != files[i].path || decodedNodes[i].flags != nodes[i].flags ||
            !Bytes(decoded.fileList[i].stream).AsSpan().SequenceEqual(Bytes(files[i].stream)))
            throw new InvalidDataException($"Node {i} mismatch");
}
static (ushort hi, ushort lo) Size32(uint value) {
    uint x = BitOperations.RotateLeft(value ^ 0xF74324EEu, 18);
    return ((ushort)((x >> 16) ^ x ^ 0xA121), (ushort)x);
}
static void U16(Stream stream, ushort v) { Span<byte> b = stackalloc byte[2]; BinaryPrimitives.WriteUInt16BigEndian(b, v); stream.Write(b); }
static void U32(Stream stream, uint v) { Span<byte> b = stackalloc byte[4]; BinaryPrimitives.WriteUInt32BigEndian(b, v); stream.Write(b); }
static void Count(Stream stream, uint value, uint xor, uint endianXor) {
    uint x = BitOperations.RotateLeft(value ^ xor, 18);
    uint folded = ((((x >> 16) ^ x) & 0xFFFF) << 16) | (x & 0xFFFF);
    U32(stream, folded ^ BinaryPrimitives.ReverseEndianness(endianXor));
}
static byte[] Wrap(List<StreamFile> files, List<BundleFile.Node> nodes, byte[] headerTemplate) {
    using var payload = new MemoryStream();
    var offsets = new List<ulong>();
    foreach (var f in files) { offsets.Add((ulong)payload.Position); payload.Write(Bytes(f.stream)); }
    const int chunk = 128 * 1024;
    uint blockCount = (uint)((payload.Length + chunk - 1) / chunk);
    using var info = new MemoryStream();
    Count(info, blockCount, 0x91CE0A4F, 0x8A7BF723);
    for (uint i = 0; i < blockCount; i++) {
        uint size = (uint)Math.Min(chunk, payload.Length - i * (long)chunk);
        var (a, c) = Size32(size);
        // Plain data block flags=0. Inverse of c ^ rol16(unfold(flags),14) ^ 0x523F.
        ushort x = (ushort)(c ^ 0x523F);
        ushort unrotated = (ushort)((x << 2) | (x >> 14));
        ushort folded = (ushort)((((unrotated >> 8) ^ unrotated) & 255) << 8 | (unrotated & 255));
        U16(info, a); U16(info, a); U16(info, c); U16(info, (ushort)(folded ^ 0x9CD6)); U16(info, c);
    }
    Count(info, (uint)files.Count, 0xE4C1D9F2, 0x5DE50A6B);
    for (int i = 0; i < files.Count; i++) {
        ulong sz = BitOperations.RotateRight((ulong)files[i].stream.Length ^ 0xA4F1A11747816520UL, 14);
        uint e = (uint)sz, b = (uint)(sz >> 32) ^ e ^ 0xDAD76848;
        ulong off = BitOperations.RotateRight(offsets[i] ^ 0xA4F1A11747816520UL, 14);
        uint c = (uint)off, d = (uint)(off >> 32) ^ c ^ 0xDAD76848;
        uint flags = BitOperations.RotateLeft(nodes[i].flags ^ 0xF13927C4 ^ b, 18);
        uint a = ((((flags >> 16) ^ flags) & 65535) << 16) | (flags & 65535);
        U32(info, a ^ 0x8E06A9F8); U32(info, b); U32(info, c); U32(info, d);
        var name = Encoding.ASCII.GetBytes(files[i].path);
        if (name.Length >= 64 || name.Where((v, j) => (byte)(v ^ j ^ 0x97) == 0).Any())
            throw new InvalidDataException("Unsupported archive node name");
        for (int j = 0; j < name.Length; j++) info.WriteByte((byte)(name[j] ^ j ^ 0x97));
        info.WriteByte(0); U32(info, e);
    }
    var header = headerTemplate.ToArray();
    uint flags2 = BinaryPrimitives.ReadUInt32BigEndian(header.AsSpan(10));
    BinaryPrimitives.WriteUInt32BigEndian(header.AsSpan(22), 0x240u ^ flags2 ^ 0xA7F49310);
    var (hi, lo) = Size32((uint)info.Length);
    BinaryPrimitives.WriteUInt16BigEndian(header.AsSpan(8), lo);
    BinaryPrimitives.WriteUInt16BigEndian(header.AsSpan(38), hi);
    BinaryPrimitives.WriteUInt16BigEndian(header.AsSpan(26), hi);
    BinaryPrimitives.WriteUInt16BigEndian(header.AsSpan(32), lo);
    ulong sizeAll = (ulong)(48 + ((info.Length + 15) & ~15L) + payload.Length);
    ulong sizeEncoded = BitOperations.RotateRight(sizeAll ^ 0xA4F1A11747816520UL, 14);
    uint size2 = (uint)sizeEncoded, size1 = (uint)(sizeEncoded >> 32) ^ size2 ^ 0xDAD76848;
    BinaryPrimitives.WriteUInt32BigEndian(header.AsSpan(18), size2);
    BinaryPrimitives.WriteUInt32BigEndian(header.AsSpan(34), size1);
    using var result = new MemoryStream(); result.Write(header); result.Write(info.ToArray());
    while (result.Position % 16 != 0) result.WriteByte(0);
    result.Write(payload.ToArray());
    if ((ulong)result.Length != sizeAll) throw new InvalidDataException("Output length mismatch");
    return result.ToArray();
}
