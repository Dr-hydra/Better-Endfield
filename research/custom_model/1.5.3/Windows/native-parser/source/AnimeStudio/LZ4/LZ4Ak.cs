using System;

namespace AnimeStudio;
public class LZ4Ak : LZ4
{
    public new static LZ4Ak Instance => new();
    protected override (int encCount, int litCount) GetLiteralToken(ReadOnlySpan<byte> cmp, ref int cmpPos) => ((cmp[cmpPos] >> 4) & 0xf, (cmp[cmpPos++] >> 0) & 0xf);
    protected override int GetChunkEnd(ReadOnlySpan<byte> cmp, ref int cmpPos) => cmp[cmpPos++] << 8 | cmp[cmpPos++] << 0;
}