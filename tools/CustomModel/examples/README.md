# 转换示例

`conversion.recipe.json` 是需要填写真实角色资料路径的模板，不是可直接用于任何角色的放行配方。
可在 appearances 数组添加固定外观。各外观 target 契约必须一致。

直接导出项目结构为 `{"manifest": {正式 BEM Manifest}, "payload_files": ["vb0.bin", "vb1.bin", ...]}`。
文件顺序就是原始 Payload ID；打包器会重新共享相同字节，并更新引用。
使用 `pack project.json -o output.bem`。

参见随软件发布的 `docs/BEM_CREATOR_GUIDE.md`、`docs/BEM_V1_SPEC.md`。
