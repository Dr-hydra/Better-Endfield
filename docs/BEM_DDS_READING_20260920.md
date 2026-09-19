# 7z 内未压缩 DDS 读取修复

测试来源：用户提供的 `laevatain-cheshire_slot15.7z`，BEM Tools 1.1.4。

7-Zip 已成功读取内部资源；原失败发生在 DDS 像素声明解析。`Components-0-10-7-8-9 t=9529b478.dds` 为 256×256、32 位 RGBA，FourCC 为零，DDPF_RGB|DDPF_ALPHAPIXELS 标志与 RGBA 通道 mask 完整。包内另有一张 RGBA32 和一张 legacy L8（映射 R8_UNORM）贴图。

原解析器只接受 BC/DXT，误将合法未压缩头作为不支持格式退出。现增加：

- Legacy RGBA32/R8 与 DX10 RGBA8/R8；BGRA8 明确交换 R/B 保留 alpha。
- 长宽、行距、mip 字节数、单层二维边界校验，拒绝截断、未知 mask、行填充、数组/立方体/体积。
- BEM Python 校验、C++ 读取和纹理格式日志同步支持 Unity TextureFormat 4 / 63；RGBA 每像素 4 字节，R8 每像素 1 字节，保持单纹理 64 MiB 限制。
- 未改变原生 Texture2D 创建和上传路径，继续检查实际 GraphicsFormat/尺寸，R8 只允许 linear。
- 材质映射限制具体说明 `ps-t*` 槽位，避免再次被误解为压缩包打不开。

真实 7z 完整解析成功，报告 29 条纹理记录；仍为 `requires_mapping`，原因是来源直接设置逐绘制段贴图槽，尚无自动原生材质属性映射。没有把读取成功标记为整个 Mod 转换完成。

针对性检查覆盖新 DDS 读取/通道顺序/mip/拒绝条件，Python 与原生 BEM 对新格式的有效包和截断 payload 判定一致，已有 Hash/LOD 转换相关检查通过。不重复游戏实机测试。

定义依据：[微软 DDS_PIXELFORMAT](https://learn.microsoft.com/en-us/windows/win32/direct3ddds/dds-pixelformat)、[DirectXTex DDS legacy 格式映射](https://github.com/microsoft/DirectXTex/blob/main/DirectXTex/DirectXTexDDS.cpp)。
