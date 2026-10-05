using AnimeStudio;
using System.Text.Json;
var manager = new AssetsManager { Game = GameManager.GetGame(GameType.ArknightsEndfield), SkipProcess = true };
manager.LoadFiles(Directory.GetFiles(Path.GetFullPath(args[0]), "*.ab"));
var output = Path.GetFullPath(args[1]);
var effectsMode = args.Contains("--effects");
Directory.CreateDirectory(output);
var options = new JsonSerializerOptions { IncludeFields = true, WriteIndented = true, MaxDepth = 256, NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals };
foreach (var file in manager.assetsFileList)
{
    var objects = new List<object>();
    foreach (var info in file.m_Objects)
    {
        var reader = new ObjectReader(file.reader, file, info, manager.Game);
        objects.Add(new { info.m_PathID, info.classID, info.byteSize });
        if (effectsMode) {
            if (reader.type != ClassIDType.GameObject && reader.type != ClassIDType.Transform && reader.type != ClassIDType.ParticleSystem && reader.type != ClassIDType.MonoBehaviour && reader.type != ClassIDType.Animator) continue;
        } else {
            if (reader.type != ClassIDType.AnimatorController && reader.type != ClassIDType.AnimationClip) continue;
            if (reader.type == ClassIDType.AnimationClip && info.m_PathID != -242825731236656247 && info.m_PathID != -633966152451736911) continue;
        }
        reader.Reset();
        File.WriteAllBytes(Path.Combine(output, $"{file.fileName}-{info.m_PathID}.bin"), reader.ReadBytes((int)info.byteSize));
        var tree = info.serializedType?.m_Type;
        if (tree?.m_Nodes.Count > 0)
        {
            File.WriteAllText(Path.Combine(output, $"{file.fileName}-{info.m_PathID}.tree.txt"), TypeTreeHelper.ReadTypeString(tree, reader));
            File.WriteAllText(Path.Combine(output, $"{file.fileName}-{info.m_PathID}.tree.json"), JsonSerializer.Serialize(TypeTreeHelper.ReadType(tree, reader), options));
        }
        Console.WriteLine($"{file.fileName} type={reader.type} pathID={info.m_PathID} bytes={info.byteSize} treeNodes={tree?.m_Nodes.Count ?? 0}");
    }
    File.WriteAllText(Path.Combine(output, file.fileName + ".meta.json"), JsonSerializer.Serialize(new { file.fileName, file.unityVersion, objects, externals = file.m_Externals }, options));
}
