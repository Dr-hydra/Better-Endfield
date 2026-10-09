using System.Globalization;
using System.Text;

namespace BetterEndfieldNext.UI.Services;

// One imported MMD work: <install>\mmd\<Folder>\set.ini plus its files.
// ExtraMotions/ExtraFaces: squad playback parts motion2..motion4 / face2..face4.
internal sealed record MmdWork(
    string Folder,
    string Name,
    string Motion,
    string Camera,
    string Face,
    string Music,
    double AudioOffset,
    IReadOnlyList<string> ExtraMotions,
    IReadOnlyList<string> ExtraFaces)
{
    public const int ExtraDancers = 3;

    public int Dancers => (Motion.Length > 0 ? 1 : 0) + ExtraMotions.Count(motion => motion.Length > 0);

    public override string ToString()
    {
        var parts = new List<string>();
        bool zh = LocalizationService.Instance.IsChinese;
        if (Motion.Length > 0) parts.Add(Dancers > 1 ? (zh ? $"{Dancers} 人动作" : $"{Dancers}-dancer motion") : (zh ? "动作" : "motion"));
        if (Camera.Length > 0) parts.Add(zh ? "镜头" : "camera");
        if (Face.Length > 0) parts.Add(zh ? "表情" : "face");
        if (Music.Length > 0) parts.Add(zh ? "音乐" : "music");
        string offset = Math.Abs(AudioOffset) > 1e-6
            ? (zh ? "，音乐偏移 " : ", music offset ") + AudioOffset.ToString("0.###", CultureInfo.InvariantCulture) + " s"
            : string.Empty;
        return $"{Name}    [{string.Join(" / ", parts)}{offset}]";
    }
}

internal sealed record MmdImportRequest(
    string Name,
    string MotionPath,
    string CameraPath,
    string FacePath,
    string MusicPath,
    double AudioOffset,
    IReadOnlyList<string> ExtraMotionPaths,
    IReadOnlyList<string> ExtraFacePaths);

// The manager copies imported files into the library next to runtime/ and
// modules/; the camera module and the MMD overlay read the same folder.
internal static class MmdLibraryService
{
    private const long MaxVmdBytes = 64L << 20;
    private const long MaxMusicBytes = 1L << 30;
    private static readonly string[] MusicExtensions = [".wav", ".mp3", ".m4a", ".aac", ".flac", ".wma"];

    public static string LibraryRoot
    {
        get
        {
            string root = ConfigurationService.TryGetInstallRoot() ??
                (Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory);
            return Path.Combine(root, "mmd");
        }
    }

    public static IReadOnlyList<MmdWork> List()
    {
        var works = new List<MmdWork>();
        string root = LibraryRoot;
        if (!Directory.Exists(root)) return works;
        foreach (string directory in Directory.EnumerateDirectories(root))
        {
            string folder = Path.GetFileName(directory);
            if (folder.EndsWith(".importing", StringComparison.OrdinalIgnoreCase)) continue;
            if (TryRead(directory, out MmdWork? work) && work is not null) works.Add(work);
        }
        works.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.CurrentCulture));
        return works;
    }

    public static async Task<MmdWork> ImportAsync(MmdImportRequest request)
    {
        bool zh = LocalizationService.Instance.IsChinese;
        string motion = request.MotionPath.Trim().Trim('"');
        string camera = request.CameraPath.Trim().Trim('"');
        string face = request.FacePath.Trim().Trim('"');
        string music = request.MusicPath.Trim().Trim('"');
        if (motion.Length == 0 && camera.Length == 0)
            throw new InvalidOperationException(zh ? "至少需要动作 VMD 或镜头 VMD。" : "A motion or camera VMD is required.");
        if (motion.Length > 0) ValidateVmd(motion, zh ? "动作" : "Motion");
        if (camera.Length > 0) ValidateVmd(camera, zh ? "镜头" : "Camera");
        if (face.Length > 0) ValidateVmd(face, zh ? "表情" : "Face");
        if (music.Length > 0) ValidateMusic(music);
        string[] extraMotions = new string[MmdWork.ExtraDancers];
        string[] extraFaces = new string[MmdWork.ExtraDancers];
        for (int index = 0; index < MmdWork.ExtraDancers; index++)
        {
            extraMotions[index] = index < request.ExtraMotionPaths.Count ? request.ExtraMotionPaths[index].Trim().Trim('"') : string.Empty;
            extraFaces[index] = index < request.ExtraFacePaths.Count ? request.ExtraFacePaths[index].Trim().Trim('"') : string.Empty;
            string dancer = (zh ? "第 " : "Dancer ") + (index + 2) + (zh ? " 人" : string.Empty);
            if (extraMotions[index].Length > 0 && motion.Length == 0)
                throw new InvalidOperationException(zh ? "多人动作需要先有第 1 人的动作 VMD。" : "Extra dancers need the first dancer's motion VMD.");
            if (extraMotions[index].Length > 0) ValidateVmd(extraMotions[index], dancer + (zh ? "动作" : " motion"));
            if (extraFaces[index].Length > 0 && extraMotions[index].Length == 0)
                throw new InvalidOperationException(dancer + (zh ? "的表情 VMD 需要配动作 VMD。" : " face VMD needs that dancer's motion VMD."));
            if (extraFaces[index].Length > 0) ValidateVmd(extraFaces[index], dancer + (zh ? "表情" : " face"));
        }
        if (!double.IsFinite(request.AudioOffset) || Math.Abs(request.AudioOffset) > 600)
            throw new InvalidOperationException(zh ? "音乐偏移需在 ±600 秒以内。" : "The music offset must be within ±600 s.");

        string name = request.Name.Trim();
        if (name.Length == 0)
            name = Path.GetFileNameWithoutExtension(motion.Length > 0 ? motion : camera);
        name = name.Replace('\r', ' ').Replace('\n', ' ');
        if (name.Length > 120) name = name[..120];

        string root = LibraryRoot;
        Directory.CreateDirectory(root);
        string folder = UniqueFolder(root, SafeFolderName(name));
        string staging = Path.Combine(root, folder + ".importing");
        if (Directory.Exists(staging)) Directory.Delete(staging, recursive: true);
        Directory.CreateDirectory(staging);
        try
        {
            string motionName = motion.Length > 0 ? "motion.vmd" : string.Empty;
            string cameraName = camera.Length > 0 ? "camera.vmd" : string.Empty;
            string faceName = face.Length > 0 ? "face.vmd" : string.Empty;
            string musicName = music.Length > 0 ? "music" + Path.GetExtension(music).ToLowerInvariant() : string.Empty;
            string[] extraMotionNames = new string[MmdWork.ExtraDancers];
            string[] extraFaceNames = new string[MmdWork.ExtraDancers];
            for (int index = 0; index < MmdWork.ExtraDancers; index++)
            {
                extraMotionNames[index] = extraMotions[index].Length > 0 ? $"motion{index + 2}.vmd" : string.Empty;
                extraFaceNames[index] = extraFaces[index].Length > 0 ? $"face{index + 2}.vmd" : string.Empty;
            }
            await Task.Run(() =>
            {
                if (motionName.Length > 0) File.Copy(motion, Path.Combine(staging, motionName));
                if (cameraName.Length > 0) File.Copy(camera, Path.Combine(staging, cameraName));
                if (faceName.Length > 0) File.Copy(face, Path.Combine(staging, faceName));
                if (musicName.Length > 0) File.Copy(music, Path.Combine(staging, musicName));
                for (int index = 0; index < MmdWork.ExtraDancers; index++)
                {
                    if (extraMotionNames[index].Length > 0) File.Copy(extraMotions[index], Path.Combine(staging, extraMotionNames[index]));
                    if (extraFaceNames[index].Length > 0) File.Copy(extraFaces[index], Path.Combine(staging, extraFaceNames[index]));
                }
            });
            var work = new MmdWork(folder, name, motionName, cameraName, faceName, musicName, request.AudioOffset,
                extraMotionNames, extraFaceNames);
            WriteSet(staging, work, request.AudioOffset);
            string destination = Path.Combine(root, folder);
            Directory.Move(staging, destination);
            return work;
        }
        catch
        {
            try { Directory.Delete(staging, recursive: true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            throw;
        }
    }

    public static void Delete(MmdWork work)
    {
        string directory = WorkDirectory(work.Folder);
        if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
    }

    public static void UpdateOffset(MmdWork work, double offset)
    {
        string directory = WorkDirectory(work.Folder);
        WriteSet(directory, work, offset);
    }

    public static string WorkDirectory(string folder)
    {
        if (!IsPlainName(folder)) throw new InvalidOperationException("Invalid work folder.");
        return Path.Combine(LibraryRoot, folder);
    }

    private static bool TryRead(string directory, out MmdWork? work)
    {
        work = null;
        string file = Path.Combine(directory, "set.ini");
        try
        {
            if (!File.Exists(file) || new FileInfo(file).Length > 16 * 1024) return false;
            string folder = Path.GetFileName(directory);
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string raw in File.ReadAllLines(file, Encoding.UTF8))
            {
                string line = raw.Trim();
                if (line.Length == 0 || line[0] is ';' or '#' or '[') continue;
                int equals = line.IndexOf('=');
                if (equals > 0) values[line[..equals].Trim()] = line[(equals + 1)..].Trim();
            }
            string Value(string key) => values.TryGetValue(key, out string? text) && IsPlainName(text) ? text : string.Empty;
            double offset = values.TryGetValue("audio_offset", out string? offsetText) &&
                double.TryParse(offsetText, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsed) &&
                double.IsFinite(parsed) ? parsed : 0.0;
            string name = values.TryGetValue("name", out string? displayName) && displayName.Length > 0 ? displayName : folder;
            var extraMotions = new string[MmdWork.ExtraDancers];
            var extraFaces = new string[MmdWork.ExtraDancers];
            for (int index = 0; index < MmdWork.ExtraDancers; index++)
            {
                extraMotions[index] = Value($"motion{index + 2}");
                extraFaces[index] = Value($"face{index + 2}");
            }
            work = new MmdWork(folder, name, Value("motion"), Value("camera"), Value("face"), Value("music"), offset,
                extraMotions, extraFaces);
            return work.Motion.Length > 0 || work.Camera.Length > 0;
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    private static void WriteSet(string directory, MmdWork work, double offset)
    {
        var text = new StringBuilder();
        text.Append("[set]\r\n");
        text.Append("name=").Append(work.Name).Append("\r\n");
        text.Append("motion=").Append(work.Motion).Append("\r\n");
        text.Append("camera=").Append(work.Camera).Append("\r\n");
        text.Append("face=").Append(work.Face).Append("\r\n");
        text.Append("music=").Append(work.Music).Append("\r\n");
        for (int index = 0; index < MmdWork.ExtraDancers; index++)
        {
            string motion = index < work.ExtraMotions.Count ? work.ExtraMotions[index] : string.Empty;
            string face = index < work.ExtraFaces.Count ? work.ExtraFaces[index] : string.Empty;
            if (motion.Length > 0) text.Append($"motion{index + 2}=").Append(motion).Append("\r\n");
            if (face.Length > 0) text.Append($"face{index + 2}=").Append(face).Append("\r\n");
        }
        text.Append("audio_offset=").Append(offset.ToString("0.###", CultureInfo.InvariantCulture)).Append("\r\n");
        string path = Path.Combine(directory, "set.ini");
        string temporary = path + ".tmp";
        File.WriteAllText(temporary, text.ToString(), new UTF8Encoding(false));
        File.Move(temporary, path, overwrite: true);
    }

    private static void ValidateVmd(string path, string label)
    {
        bool zh = LocalizationService.Instance.IsChinese;
        var info = new FileInfo(path);
        if (!info.Exists) throw new FileNotFoundException((zh ? "找不到" : "Not found: ") + label + " VMD", path);
        if (info.Length < 30 || info.Length > MaxVmdBytes)
            throw new InvalidOperationException(label + (zh ? " VMD 大小无效（上限 64 MB）。" : " VMD has an invalid size (limit 64 MB)."));
        byte[] header = new byte[20];
        using (FileStream stream = File.OpenRead(path))
        {
            if (stream.Read(header, 0, header.Length) != header.Length)
                throw new InvalidOperationException(label + (zh ? " VMD 无法读取。" : " VMD cannot be read."));
        }
        if (!Encoding.ASCII.GetString(header).StartsWith("Vocaloid Motion Data", StringComparison.Ordinal))
            throw new InvalidOperationException(label + (zh ? " 文件不是 VMD。" : " file is not a VMD."));
    }

    private static void ValidateMusic(string path)
    {
        bool zh = LocalizationService.Instance.IsChinese;
        var info = new FileInfo(path);
        if (!info.Exists) throw new FileNotFoundException(zh ? "找不到音乐文件" : "Music file not found", path);
        if (Array.IndexOf(MusicExtensions, info.Extension.ToLowerInvariant()) < 0)
            throw new InvalidOperationException(zh ? "音乐需为 WAV/MP3/M4A/AAC/FLAC/WMA。" : "Music must be WAV/MP3/M4A/AAC/FLAC/WMA.");
        if (info.Length == 0 || info.Length > MaxMusicBytes)
            throw new InvalidOperationException(zh ? "音乐文件大小无效（上限 1 GB）。" : "Music file has an invalid size (limit 1 GB).");
    }

    private static bool IsPlainName(string name) =>
        name.Length > 0 && name.Length <= 200 && name is not "." and not ".." &&
        name.IndexOfAny(['/', '\\', ':']) < 0 && !name.Any(char.IsControl);

    private static string SafeFolderName(string name)
    {
        var builder = new StringBuilder();
        foreach (char character in name)
        {
            builder.Append(Array.IndexOf(Path.GetInvalidFileNameChars(), character) >= 0 ? '_' : character);
        }
        string result = builder.ToString().Trim().TrimEnd('.');
        return result.Length == 0 ? "work" : result.Length > 60 ? result[..60] : result;
    }

    private static string UniqueFolder(string root, string baseName)
    {
        string candidate = baseName;
        for (int index = 2; Directory.Exists(Path.Combine(root, candidate)) ||
             Directory.Exists(Path.Combine(root, candidate + ".importing")); index++)
        {
            candidate = $"{baseName} ({index})";
        }
        return candidate;
    }
}
