using AnimeStudio;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.Json;
using System.Text.RegularExpressions;
using AssetObject = AnimeStudio.Object;

// Read only metadata and references. The external backend understands game
// serialization; this tool neither exports texture pixels nor rewrites assets.
static class Program
{
    static readonly JsonSerializerOptions Json = new() { WriteIndented = true, IncludeFields = true };
    static string Id(AssetObject obj) => obj.assetsFile.fileName.ToLowerInvariant() + ":" + obj.m_PathID;
    sealed class ShaderNamePrefix : NamedObject
    {
        public ShaderNamePrefix(ObjectReader reader) : base(reader) { }
    }
    static object Ref<T>(PPtr<T>? ptr, AssetObject owner) where T : AssetObject
    {
        if (ptr is null || ptr.m_PathID == 0) return new { id = (string?)null, resolved = true };
        string? file = ptr.m_FileID == 0 ? owner.assetsFile.fileName :
            ptr.m_FileID > 0 && ptr.m_FileID <= owner.assetsFile.m_Externals.Count
            ? owner.assetsFile.m_Externals[ptr.m_FileID - 1].fileName : null;
        return new { id = file is null ? null : file.ToLowerInvariant() + ":" + ptr.m_PathID,
                     file_id = ptr.m_FileID, path_id = ptr.m_PathID.ToString(),
                     resolved = ptr.TryGet(out _) };
    }
    static float[] Matrix(Matrix4x4 m) => new[] {
        m.M00,m.M10,m.M20,m.M30,m.M01,m.M11,m.M21,m.M31,
        m.M02,m.M12,m.M22,m.M32,m.M03,m.M13,m.M23,m.M33 };
    static object Channels(Mesh mesh)
    {
        // The backend's decoded ChannelInfo masks HG's high dimension bits.
        // Read the type-tree representation to preserve the serialized bytes.
        string? dump = mesh.Dump();
        if (dump is null) throw new InvalidDataException("Mesh type tree unavailable");
        int begin = dump.IndexOf("VertexData m_VertexData", StringComparison.Ordinal);
        int end = begin < 0 ? -1 : dump.IndexOf("m_DataSize", begin, StringComparison.Ordinal);
        if (begin < 0 || end < 0) throw new InvalidDataException("Raw vertex declaration unavailable");
        return Regex.Matches(dump[begin..end],
            @"UInt8 stream = (\d+)\s+UInt8 offset = (\d+)\s+UInt8 format = (\d+)\s+UInt8 dimension = (\d+)")
            .Select((m, i) => new { attribute = i, stream = int.Parse(m.Groups[1].Value),
                offset = int.Parse(m.Groups[2].Value), format = int.Parse(m.Groups[3].Value),
                dimension_raw = int.Parse(m.Groups[4].Value) }).ToArray();
    }
    static Dictionary<string, object?> Record(AssetObject obj)
    {
        var row = new Dictionary<string, object?> {
            ["id"] = Id(obj), ["file"] = obj.assetsFile.fileName, ["path_id"] = obj.m_PathID.ToString(),
            ["type"] = obj.type.ToString(), ["name"] = obj.Name };
        if (obj.type==ClassIDType.Shader)
        {
            // Read only the common NamedObject prefix, never Shader bytecode.
            row["name"]=new ShaderNamePrefix(obj.reader).m_Name;
            row["code_parsed"]=false;
            return row;
        }
        switch (obj)
        {
            case GameObject go:
                row["components"] = go.m_Components.Select(p => Ref(p, go)).ToArray(); break;
            case Transform t:
                row["game_object"] = Ref(t.m_GameObject,t); row["parent"] = Ref(t.m_Father,t);
                row["children"] = t.m_Children.Select(p => Ref(p,t)).ToArray();
                row["position"] = t.m_LocalPosition; row["rotation"] = t.m_LocalRotation; row["scale"] = t.m_LocalScale;
                break;
            case SkinnedMeshRenderer r:
                row["game_object"] = Ref(r.m_GameObject,r); row["mesh"] = Ref(r.m_Mesh,r);
                row["bones"] = r.m_Bones.Select(p => Ref(p,r)).ToArray(); row["root_bone"] = Ref(r.m_RootBone,r);
                row["materials"] = r.m_Materials.Select(p => Ref(p,r)).ToArray(); break;
            case Mesh m:
                row["vertex_count"] = m.m_VertexCount;
                row["submeshes"] = m.m_SubMeshes.Select(s => new { index_count=s.indexCount,
                    first_byte=s.firstByte, base_vertex=s.baseVertex, first_vertex=s.firstVertex,
                    vertex_count=s.vertexCount, topology=s.topology.ToString() }).ToArray();
                row["bindposes"] = (m.m_BindPose ?? Array.Empty<Matrix4x4>()).Select(Matrix).ToArray();
                row["bone_name_hashes"] = m.m_BoneNameHashes;
                try { row["serialized_channels"] = Channels(m); }
                catch (Exception ex) { row["layout_error"] = ex.Message; }
                break;
            case Material m:
                row["shader"] = Ref(m.m_Shader,m);
                try {
                    var materialTree=m.ToType();
                    row["keywords"]=materialTree?["m_ValidKeywords"] ?? materialTree?["m_ShaderKeywords"];
                    row["invalid_keywords"]=materialTree?["m_InvalidKeywords"];
                } catch(Exception ex) { row["keyword_error"]=ex.Message; }
                row["textures"] = m.m_SavedProperties.m_TexEnvs.Select(x => new {
                    property=x.Key, texture=Ref(x.Value.m_Texture,m), scale=x.Value.m_Scale, offset=x.Value.m_Offset }).ToArray();
                row["floats"] = m.m_SavedProperties.m_Floats;
                row["ints"] = m.m_SavedProperties.m_Ints; row["colors"] = m.m_SavedProperties.m_Colors;
                break;
            case Texture2D t:
                row["width"] = t.m_Width; row["height"] = t.m_Height;
                row["format"] = t.m_TextureFormat.ToString(); row["mips"] = t.m_MipCount;
                row["sampler"] = t.m_TextureSettings; break;
        }
        return row;
    }
    sealed class ReportLogger : ILogger
    {
        public List<string> errors = new();
        public void Log(LoggerEvent level, string text)
        {
            if (level == LoggerEvent.Error || level == LoggerEvent.Warning)
                errors.Add(text.Length > 2000 ? text[..2000] : text);
        }
    }
    static int Run(string[] args)
    {
        if (args.Length != 2 && !(args.Length==3 && args[2]=="--resource-identities") && !(args.Length==4 && (args[2]=="--shader-metadata" || args[2]=="--texture-bytes"))) {
            Console.Error.WriteLine("NativeAssetReader <bundle-directory> <raw-graph.json> [--resource-identities | --shader-metadata directory | --texture-bytes directory]"); return 1;
        }
        string input=Path.GetFullPath(args[0]), output=Path.GetFullPath(args[1]);
        bool resourceIdentities=args.Length==3 && args[2]=="--resource-identities";
        string? shaderDumps=args.Length==4 && args[2]=="--shader-metadata"?Path.GetFullPath(args[3]):null;
        string? textureBytes=args.Length==4 && args[2]=="--texture-bytes"?Path.GetFullPath(args[3]):null;
        if(shaderDumps!=null) Directory.CreateDirectory(shaderDumps);
        if(textureBytes!=null) Directory.CreateDirectory(textureBytes);
        var files=Directory.GetFiles(input,"*.ab",SearchOption.AllDirectories).Order().ToArray();
        if (files.Length==0) throw new InvalidDataException("No .ab bundles in input directory");
        var logger=new ReportLogger(); Logger.Default=logger; Logger.Flags=LoggerEvent.Error|LoggerEvent.Warning;
        foreach (ClassIDType type in Enum.GetValues<ClassIDType>()) TypeFlags.SetType(type,false,false);
        var supported=new[] { ClassIDType.GameObject,ClassIDType.Transform,ClassIDType.SkinnedMeshRenderer,
            ClassIDType.Mesh,ClassIDType.Material,ClassIDType.Texture2D,ClassIDType.AssetBundle };
        foreach (var type in supported) TypeFlags.SetType(type,true,false);
        var manager=new AssetsManager { Game=GameManager.GetGameByType(GameType.ArknightsEndfield)
            ?? throw new InvalidDataException("Backend has no Endfield support") };
        manager.LoadFiles(files);
        var assetPaths=new Dictionary<string,SortedSet<string>>();
        foreach (var bundle in manager.assetsFileList.SelectMany(f=>f.Objects).OfType<AssetBundle>())
            foreach (var entry in bundle.m_Container)
                if (entry.Value.asset.TryGet(out var target)) {
                    if (!assetPaths.TryGetValue(Id(target),out var paths)) assetPaths[Id(target)]=paths=new();
                    paths.Add(entry.Key);
                }
        var objects=new List<Dictionary<string,object?>>();
        foreach (var file in manager.assetsFileList.OrderBy(f=>f.fileName))
            foreach (var obj in file.Objects.OrderBy(o=>o.m_PathID))
                if ((supported.Contains(obj.type) && obj.type!=ClassIDType.AssetBundle) || obj.type==ClassIDType.Shader)
                {
                    try {
                        if(obj.type==ClassIDType.Shader && shaderDumps!=null) {
                            // Read only the serialized metadata prefix. Expanding compressed
                            // shader bytecode arrays as text can consume gigabytes of memory.
                            var nodes=obj.serializedType?.m_Type?.m_Nodes
                                ?? throw new InvalidDataException("Shader type tree unavailable");
                            int form=nodes.FindIndex(n=>n.m_Level==1 && n.m_Name=="m_ParsedForm");
                            if(form<1 || nodes.Skip(1).Take(form-1).Any(n=>n.m_Level==1 && n.m_Name!="m_Name"))
                                throw new InvalidDataException("Unsupported Shader metadata prefix");
                            int end=form+1;
                            while(end<nodes.Count && nodes[end].m_Level>1) ++end;
                            var prefix=new TypeTree { m_Nodes=nodes.Take(end).ToList() };
                            var metadata=TypeTreeHelper.ReadType(prefix,obj.reader);
                            long consumed=obj.reader.Position-obj.reader.byteStart;
                            if(consumed<=0 || consumed>obj.byteSize) throw new InvalidDataException("Shader metadata out of bounds");
                            string rawFile=Id(obj).Replace(':','_')+".bin";
                            File.WriteAllBytes(Path.Combine(shaderDumps,rawFile),obj.GetRawData());
                            File.WriteAllText(Path.Combine(shaderDumps,Id(obj).Replace(':','_')+".json"),
                                JsonSerializer.Serialize(new { id=Id(obj), metadata_only=true, bytecode_parsed=false,
                                    metadata_bytes=consumed, object_bytes=obj.byteSize, raw_file=rawFile,
                                    suffix_schema=nodes.Skip(end).Select(n=>new { type=n.m_Type,name=n.m_Name,level=n.m_Level,size=n.m_ByteSize,flags=n.m_MetaFlag }),
                                    parsed_form=metadata["m_ParsedForm"] },Json));
                        }
                        var row=Record(obj);
                        if(resourceIdentities) {
                            try {
                                if(obj is Mesh identityMesh) row["resource_identity"]=NativeResourceIdentity.MeshIdentity(identityMesh);
                                if(obj is Texture2D identityTexture) row["resource_identity"]=NativeResourceIdentity.TextureIdentity(identityTexture);
                            } catch(Exception ex) { row["resource_identity_error"]=ex.Message; }
                        }
                        if(obj is Texture2D tex && textureBytes!=null) {
                            try {
                                byte[] bytes=tex.image_data.GetData();
                                string filename=Id(obj).Replace(':','_')+".bin";
                                File.WriteAllBytes(Path.Combine(textureBytes,filename),bytes);
                                row["raw_texture_file"]=filename; row["raw_texture_bytes"]=bytes.Length;
                            } catch (Exception ex) { row["texture_export_error"]=ex.Message; }
                        }
                        row["asset_paths"]=assetPaths.TryGetValue(Id(obj),out var paths)?paths.ToArray():Array.Empty<string>();
                        objects.Add(row);
                    }
                    catch (Exception ex) { logger.errors.Add($"{Id(obj)} {obj.type}: {ex.Message}"); }
                }
        Directory.CreateDirectory(Path.GetDirectoryName(output)!);
        JsonElement? snapshot=null;
        var snapshotPath=Path.Combine(input,"extraction.json");
        if (File.Exists(snapshotPath)) {
            using var document=JsonDocument.Parse(File.ReadAllText(snapshotPath));
            snapshot=document.RootElement.Clone();
        }
        var result=new { schema=1, source="AnimeStudio reference-preserving offline reader",
            backend=typeof(AssetsManager).Assembly.GetName().Version?.ToString(),
            backend_patch=typeof(AssetsManager).Assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
                .FirstOrDefault(a=>a.Key=="BetterEndfield.NativeBackendPatch")?.Value,
            snapshot,
            input_directory=input, bundles=files.Select(f=>Path.GetRelativePath(input,f)).ToArray(),
            files=manager.assetsFileList.Select(f=>new { name=f.fileName, unity_version=string.Join('.',f.version),
                externals=f.m_Externals.Select(e=>e.fileName).ToArray() }).ToArray(), objects,
            backend_errors=logger.errors, runtime_verified=false };
        File.WriteAllText(output+".tmp",JsonSerializer.Serialize(result,Json)); File.Move(output+".tmp",output,true);
        Console.WriteLine($"files={manager.assetsFileList.Count} objects={objects.Count} backend_issues={logger.errors.Count}");
        manager.Clear(); return objects.Count>0?0:2;
    }
    static int Main(string[] args)
    {
        AssemblyLoadContext.Default.Resolving += (_,name) => {
            string path=Path.Combine(AppContext.BaseDirectory,name.Name+".dll");
            return File.Exists(path)?AssemblyLoadContext.Default.LoadFromAssemblyPath(path):null;
        };
        try { return Run(args); }
        catch (Exception ex) { Console.Error.WriteLine(ex.ToString()); return 1; }
    }
}
