# 1.6.3 MemoryPack 字段与 union schema

`memorypack-schema.json` 是当前 Windows 客户端的 845 个相关类型定义，仅供开发资源导出器读取 SkillData/BuffData。正常更新只需要本文件和游戏 JsonData/StringPool，不读取 GameAssembly。

来源：当前 metadata 的 `*ForMemoryPack` setter 及父类字段，原生非引用值类型的大小，18 个 formatter `.cctor` 内的实际 `Dictionary<Type, ushort>.Add` 标签。构造函数仅局部反汇编；具体地址和读取证据保存在 `research/host/1.6.3/Windows/hook-update/`。

维护脚本：

1. `extract_memorypack_schema.py --game <游戏目录> --output <schema>` 提取两个根类型的可达字段。
2. `extract_memorypack_unions.py --game <游戏目录> --schema <schema> --output <unions>` 提取显式标签。
3. 将标签中的实际类型写成 JSON 数组，用步骤 1 的 `--extra-types <数组文件>` 补齐注册类型；重复步骤 2，直到标签目标及新增 union 全部闭合。
4. 将每项 `unions[type].tags` 合入 `schema[type].union`，保存至本目录。

类型候选排序不能代替 union 标签。指针/整数不能当字符串引用。USPC 索引 `-2` 表示 null、`-1` 表示空串；其他值按每块 10,000 项索引。值类型数组采用裸结构，List 调用逐项 formatter；DispelConfig 为 8 字节，GameplayTag 为 4 字节。AudioId、AnimationCurve、SerializeFieldDictionary 的专用格式由局部函数确认。

本轮 2,928 个 SkillData、3,395 个 BuffData 均完整消费到文件末尾，再发布 Buff 来源映射。解析错误不会退回字符串扫描或旧版本输入。对应小测试登记为 `resource.formats_1_6_3`。
