using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStudio
{
    internal class WmvUtils
    {
        public static void Decrypt(Span<byte> buffer)
        {
            BlbUtils.InitKeys(CryptoHelper.Blb3RC4Key, CryptoHelper.WmvSBox, CryptoHelper.WmvShiftRow, CryptoHelper.WmvKey, CryptoHelper.WmvMul);

            // new xor using xor key
            for (int i = 0; i < CryptoHelper.WmvXORKey.Length; i++)
            {
                buffer[i % 0x10] ^= CryptoHelper.WmvXORKey[i];
            }

            // xor key instead of header
            BlbAES.Encrypt(buffer.Slice(0, 16).ToArray(), CryptoHelper.WmvXORKey[0..16]).CopyTo(buffer);

            if (buffer.Length > 16)
            {
                BlbUtils.RC4(buffer);
            }

            BlbUtils.Descramble(buffer.Slice(0, 16));
        }
    }
}
