using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Buffers;
using System.IO;
using K4os.Hash.xxHash;

namespace AnimeStudio
{
    public static class Texture2DExtensions
    {
        private static Configuration _configuration;

        static Texture2DExtensions()
        {
            _configuration = Configuration.Default.Clone();
            _configuration.PreferContiguousImageBuffers = true;
        }

        public static string GetImageHash(this Texture2D m_Texture2D)
        {
            var image = m_Texture2D.ConvertToImage(true);
            var hashstring = "";
            if (image != null)
            {
                try
                {
                    // TODO: be not only for png's since people may not always use that format, but in the end the hash is still unique and different from the raw data one
                    using var ms = new MemoryStream();
                    image.Save(ms, new PngEncoder());
                    ms.Position = 0;
                    Span<byte> span = ms.GetBuffer().AsSpan(0, (int)ms.Length);
                    var hash = XXH64.DigestOf(span);
                    hashstring = hash.ToString("x");
                }
                catch
                {
                    hashstring = "";
                }

                image.Dispose();
            }

            return hashstring;
        }

        public static Image<Bgra32> ConvertToImage(this Texture2D m_Texture2D, bool flip)
        {
            // Sanity check: width/height 可能是损坏字段（垃圾值几百万），
            // 直接相乘会 overflow 成几十 GB → ArrayPool.Rent → OOM
            const int MaxTextureDim = 1 << 15;  // 32768，覆盖所有合理 Unity 纹理上限
            if (m_Texture2D.m_Width <= 0 || m_Texture2D.m_Height <= 0 ||
                m_Texture2D.m_Width > MaxTextureDim || m_Texture2D.m_Height > MaxTextureDim)
            {
                Logger.Warning($"Texture2D dimensions out of range: {m_Texture2D.m_Width}x{m_Texture2D.m_Height}, skipping");
                return null;
            }
            long pixelCount = (long)m_Texture2D.m_Width * m_Texture2D.m_Height;
            // MaxPixels 必须 < int.MaxValue / 4 = 536870911，因为 ArrayPool.Rent(min(int, w*h*4))
            const long MaxPixels = 500_000_000L;  // 500M pixels ≈ 2 GB BGRA buffer，安全上限
            if (pixelCount > MaxPixels)
            {
                Logger.Warning($"Texture2D pixel count too large: {m_Texture2D.m_Width}x{m_Texture2D.m_Height} = {pixelCount:N0}, skipping");
                return null;
            }

            var converter = new Texture2DConverter(m_Texture2D);
            byte[] buff = ArrayPool<byte>.Shared.Rent(m_Texture2D.m_Width * m_Texture2D.m_Height * 4);
            try
            {
                if (converter.DecodeTexture2D(buff))
                {
                    var image = Image.LoadPixelData<Bgra32>(_configuration, buff, m_Texture2D.m_Width, m_Texture2D.m_Height);
                    if (flip)
                    {
                        image.Mutate(x => x.Flip(FlipMode.Vertical));
                    }
                    return image;
                }
                return null;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buff, true);
            }
        }

        public static MemoryStream ConvertToStream(this Texture2D m_Texture2D, ImageFormat imageFormat, bool flip)
        {
            var image = ConvertToImage(m_Texture2D, flip);
            if (image != null)
            {
                using (image)
                {
                    return image.ConvertToStream(imageFormat);
                }
            }
            return null;
        }
    }
}
