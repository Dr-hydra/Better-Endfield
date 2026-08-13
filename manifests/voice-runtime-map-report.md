# 注入器语音运行时映射表

- 格式版本：`2`
- 角色：31
- 可路由 Event：595 / 937
- 原生 Wwise 容器 Event：595
- v9 显式降级 Event：342
- 语言路由：2380
- Media 引用：6792（唯一 6424）
- 原生 Wwise 叶子槽位：1606
- 字符串区：21748 bytes
- 二进制表：110160 bytes
- 相对 JSON 清单缩小：99.00%
- SHA-256：`9C08874684A0D830E9DD86A0D30199EAF87CD385E370E064877D4B973A2F3051`

二进制表和生成的 C++ 头文件内容完全一致。发布时映射表编译进
`il2cpp.dll`，不增加外部运行时文件；缺失映射、PCK 身份不匹配或预热失败时保留
v9 `_PlayExternal` 降级行为。原生路线由 Wwise Random/Sequence Container 选择
Sound，再由 `SetMedia` 将该 Sound 的源语言 Media 内容替换为目标语言 WEM。
