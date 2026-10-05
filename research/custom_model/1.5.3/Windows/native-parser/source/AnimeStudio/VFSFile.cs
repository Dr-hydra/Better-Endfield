using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStudio
{
    public class VFSFile
    {
        private List<BundleFile.StorageBlock> m_BlocksInfo;
        private List<BundleFile.Node> m_DirectoryInfo;

        public BundleFile.Header m_Header;
        public List<StreamFile> fileList;
        public long Offset;

        public VFSFile(FileReader reader, string path, GameType game)
        {
            Offset = reader.Position;
            reader.Endian = EndianType.BigEndian;


            if (!VFSUtils.IsValidHeader(reader, game))
            {
                throw new Exception("Not a VFS file / VFS version mismatch");
            }

            // read header
            reader.ReadBytes(8);
            m_Header = VFSUtils.ReadHeader(reader, game);
            Logger.Verbose($"Header : {m_Header.ToString()}");

            // Sanity check: VFS header 字段经过 descramble 后应该合理
            // 文件才 6 KB 但 descramble 出 uncompressedBlocksInfoSize=14 GB → key/常量不匹配
            const uint MaxVFSBlocksInfoSize = 64 * 1024 * 1024;  // 64 MB
            if (m_Header.uncompressedBlocksInfoSize > MaxVFSBlocksInfoSize ||
                m_Header.compressedBlocksInfoSize > MaxVFSBlocksInfoSize)
            {
                throw new IOException(
                    $"VFS header sanity check failed: uncompressedBlocksInfoSize={m_Header.uncompressedBlocksInfoSize:N0} " +
                    $"compressedBlocksInfoSize={m_Header.compressedBlocksInfoSize:N0} " +
                    $"(file size={reader.BaseStream.Length:N0}). Likely VFS key/constant mismatch.");
            }

            // go to blocks info
            uint blockInfosOffset;

            if ((m_Header.flags & ArchiveFlags.BlocksInfoAtTheEnd) != 0)
                blockInfosOffset = (uint)(m_Header.size) - m_Header.compressedBlocksInfoSize;
            else
            {
                if (m_Header.encFlags >= 7)
                    blockInfosOffset = 48;
                else
                    blockInfosOffset = 40;
            }

            reader.Position = Offset + blockInfosOffset;
            ReadBlocksInfoAndDirectory(reader, game);

            // go to data
            uint dataOffset;

            if (m_Header.encFlags >= 7)
                dataOffset = 48;
            else
                dataOffset = 40;
            if (((m_Header.flags) & ArchiveFlags.BlocksInfoAtTheEnd) == 0)
            {
                var temp = m_Header.compressedBlocksInfoSize;
                if (((m_Header.flags) & ArchiveFlags.BlockInfoNeedPaddingAtStart) != 0)
                    temp = (temp + 15) & 0xFFFFFFF0;
                dataOffset += temp;
            }

            reader.Position = Offset + dataOffset;

            //
            using var blocksStream = CreateBlocksStream(path);
            ReadBlocks(reader, blocksStream, game);
            ReadFiles(blocksStream, path);
        }

        private void ReadBlocksInfoAndDirectory(FileReader reader, GameType game)
        {
            byte[] blocksInfoBytes = reader.ReadBytes((int)m_Header.compressedBlocksInfoSize);

            MemoryStream blocksInfoUncompressedStream = new MemoryStream();
            if (((int)m_Header.flags & 0x3F) != 0)
            {
                // compressed + encrypted
                VFSUtils.DecryptBlock(blocksInfoBytes, game);

                var uncompressedSize = m_Header.uncompressedBlocksInfoSize;
                var blocksInfoBytesSpan = blocksInfoBytes.AsSpan(0, blocksInfoBytes.Length);
                var uncompressedBytes = ArrayPool<byte>.Shared.Rent((int)uncompressedSize);

                try
                {
                    var uncompressedBytesSpan = uncompressedBytes.AsSpan(0, (int)uncompressedSize);
                    // normal LZ4
                    var numWrite = LZ4.Instance.Decompress(blocksInfoBytesSpan, uncompressedBytesSpan);

                    if (numWrite != uncompressedSize)
                    {
                        throw new IOException($"Lz4 decompression error, write {numWrite} bytes but expected {uncompressedSize} bytes");
                    }
                    blocksInfoUncompressedStream = new MemoryStream(uncompressedBytesSpan.ToArray());
                } catch (Exception e)
                {
                    throw new IOException($"Lz4 decompression error {e.Message}");
                } finally
                {
                    ArrayPool<byte>.Shared.Return(uncompressedBytes);
                }
            } else
            {
                blocksInfoUncompressedStream = new MemoryStream(blocksInfoBytes);
            }

            // read
            using (var blocksInfoReader = new EndianBinaryReader(blocksInfoUncompressedStream))
            {
                reader.Endian = EndianType.BigEndian;
                m_BlocksInfo = VFSUtils.ReadBlocksInfos(blocksInfoReader, game);
                m_DirectoryInfo = VFSUtils.ReadDirectoryInfos(blocksInfoReader, game);
            }
        }

        private Stream CreateBlocksStream(string path)
        {
            Stream blocksStream;
            long uncompressedSizeSum = m_BlocksInfo.Sum(x => (long)x.uncompressedSize);
            Logger.Verbose($"Total size of decompressed blocks: 0x{uncompressedSizeSum:X8}");
            // Sanity: 总解压大小不应超过单个文件大小 × 50（极保守）或 2GB 硬上限
            const long MaxBlocksStreamSize = 2L * 1024 * 1024 * 1024;  // 2 GB
            if (uncompressedSizeSum >= MaxBlocksStreamSize)
                throw new IOException($"VFS blocks stream too large: {uncompressedSizeSum:N0} bytes (max {MaxBlocksStreamSize:N0})");
            blocksStream = new MemoryStream((int)uncompressedSizeSum);
            return blocksStream;
        }

        private void ReadBlocks(FileReader reader, Stream blocksStream, GameType game)
        {
            foreach (var blockInfo in m_BlocksInfo)
            {
                // Sanity check: 单个 block 的 size 不应超过文件本身大小 × 合理膨胀比
                long fileSize = reader.BaseStream.Length;
                // Compression ratio is not a validity check (shared face maps exceed 20x).
                // Keep absolute allocation limits and validate the remaining compressed input.
                if (reader.BaseStream.Position < 0 || reader.BaseStream.Position > fileSize ||
                    blockInfo.compressedSize > fileSize - reader.BaseStream.Position ||
                    blockInfo.uncompressedSize > 512L * 1024 * 1024 ||
                    (blockInfo.flags == 0 && blockInfo.compressedSize != blockInfo.uncompressedSize))
                {
                    throw new IOException(
                        $"VFS block sanity check failed: compressedSize={blockInfo.compressedSize:N0} " +
                        $"uncompressedSize={blockInfo.uncompressedSize:N0} (file size={fileSize:N0})");
                }

                var compressionType = (int)blockInfo.flags; // no mask
                Logger.Verbose($"Block compression type {compressionType}");

                switch (compressionType)
                {
                    case 0:
                        var size = (int)blockInfo.uncompressedSize;
                        var buffer = reader.ReadBytes(size);
                        blocksStream.Write(buffer);
                        break;
                    case 5:
                        var compressedSize = (int)blockInfo.compressedSize;
                        var uncompressedSize = (int)blockInfo.uncompressedSize;

                        var compressedBytes = ArrayPool<byte>.Shared.Rent(compressedSize);
                        var uncompressedBytes = ArrayPool<byte>.Shared.Rent(uncompressedSize);

                        var compressedBytesSpan = compressedBytes.AsSpan(0, compressedSize);
                        var uncompressedBytesSpan = uncompressedBytes.AsSpan(0, uncompressedSize);

                        try
                        {
                            reader.BaseStream.ReadExactly(compressedBytesSpan);

                            VFSUtils.DecryptBlock(compressedBytesSpan, game);

                            // LZ4Inv this time
                            var numWrite = LZ4Inv.Instance.Decompress(compressedBytesSpan, uncompressedBytesSpan);
                            if (numWrite != uncompressedSize)
                            {
                                throw new IOException($"Lz4 decompression error, write {numWrite} bytes but expected {uncompressedSize} bytes");
                            }
                            blocksStream.Write(uncompressedBytesSpan);
                        }
                        catch (Exception e)
                        {
                            throw new IOException("VFS block decompression failed", e);
                        }
                        finally
                        {
                            ArrayPool<byte>.Shared.Return(compressedBytes, true);
                            ArrayPool<byte>.Shared.Return(uncompressedBytes, true);
                        }

                        break;
                    default:
                        throw new Exception($"Unsupported block compression type {compressionType}");
                }
            }
        }

        private void ReadFiles(Stream blocksStream, string path)
        {
            Logger.Verbose($"Writing files from blocks stream...");

            fileList = new List<StreamFile>();
            for (int i = 0; i < m_DirectoryInfo.Count; i++)
            {
                var node = m_DirectoryInfo[i];
                var file = new StreamFile();
                fileList.Add(file);
                file.path = node.path;
                file.fileName = Path.GetFileName(node.path);
                if (node.size >= int.MaxValue || node.size > 512 * 1024 * 1024)  // 512 MB per file
                {
                    throw new IOException($"VFS node size too large: {node.size:N0} bytes for file {node.path}");
                }
                else
                    file.stream = new MemoryStream((int)node.size);
                blocksStream.Position = node.offset;
                blocksStream.CopyTo(file.stream, node.size);
                file.stream.Position = 0;
            }
        }
    }
}
