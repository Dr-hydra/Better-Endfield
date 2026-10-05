namespace AnimeStudio
{
    public interface IYAMLExportable
    {
        YAMLNode ExportYAML(int[] version);
    }
}
