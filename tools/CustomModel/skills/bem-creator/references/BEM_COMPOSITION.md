# 外观组合与身体分区 / Appearance composition

用于把同一角色的已制成 BEM 包组合为新包，也可附带整件武器或大招资源。
调用 skill 的 scripts/bem_compose.py；组合算法由共享原生 CustomModelCore 实现。
脚本只需 Python 标准库，另需 Windows 原生 BetterEndfieldNext.BemCompose.exe。
仓库构建和应用安装目录会自动查找，也可设置 BEM_COMPOSER 为完整路径。
Android 是输出平台选项；这不表示 Windows EXE 可以在 Termux 上执行。

## 调用

以下路径均是占位示例；替换为真实源包、输出目录和脚本路径。

    python scripts/bem_compose.py plan --source A.bem --source B.bem --platform android-arm64 --name "我的组合" --json plan.json
    python scripts/bem_compose.py summary plan.json
    python scripts/bem_compose.py draft --plan plan.json --choices choices.json --output-dir preview-draft
    python scripts/bem_compose.py build --plan plan.json --choices choices.json --output-dir composition-output

PC 使用 --platform windows-x64。已有完整请求也可用 plan --request request.json --json plan.json。
不手填源版本：原生规划根据来源版本生成组合版本。计划包含来源路径、大小、时间等 generation 信息，
来源变化后重新 plan。输出目录和 --json 文件必须尚不存在，源包不会被覆盖。
summary 省略长三角形数组，给出真实部件 ID、来源 ID、选项分组、分区及三角形数量。
脚本不自动安装、不启用源包、不启动游戏、不计算产物哈希。

## 选择与批量操作

choices.json 是“计划中的 ID → 选择值”对象，省略项使用原默认值。
不要猜 ID；先读取 plan 或 summary。下例的 ID 仅表示格式：

    {
      "part0": "split",
      "part0.d0": "on",
      "part0.d1": "off",
      "part1": "s1",
      "atomic": "original",
      "s1.g0": "one",
      "body_edits": {"s0.m0.d0": {"42": "torso"}}
    }

- 整个部件使用候选来源（如 s0、s1）或 original；按段混合时使用 split，再分别写段 ID 的 on/off。
- 从整包来源切到 split 时，先把各段当前实际开关全部写入，再修改目标段，避免误启用其他来源。
- 批量操作同时写多个段 ID。复选框只表示操作目标，不代表启用；高亮是临时查看，不写回正式 choices。
- 一般按源包 option_bindings 的名字分组；可展开到原始绘制记录。多选项共同控制的记录保留联动。
- 原始源选项、可用条件、互斥约束保留，命名空间如 s0.g0。项目输出可能带 p0. 等前缀。
  多输出的 source_options 在 projects[].plan 内；前缀和组次序以实际计划为准。
- 不按修订号直接拒绝跨版本组合；比较接收器、资源、布局和骨骼契约。1.4 的大招/武器整件可选，
  不拆成任意小段。最终读取 report.outputs，可能有多个伴随包，不能只取第一个文件。

## 身体分区

自动分区键：neck、head、torso、pelvis、left_arm、right_arm、left_leg、right_leg、unclassified。
依据骨骼名字及三角形的蒙皮权重分组，不依赖任意空间切块；未知骨骼归入 unclassified。
它只重排/选取索引，不改顶点、UV、材质或权重，不产生源包不存在的身体。
0 个三角形表示该来源未提供区域；不要当作完整身体来启用。
某些叫 body 的包实际上只有颈部补丁，顶点数量也不能证明它包含四肢。

手工修正写入 body_edits[body_draw][原始三角形序号] = 区域键。
body_draw 和 triangles 来自计划；序号从 0 开始，是原始绘制记录中的三角形，不是预览场景的顶点编号。
修正后启用目标区域对应的段；保留未修改段。撤销恢复先前 choices；恢复自动分区只移除 body_edits。
重叠部件会同时绘制，不自动去重、裁切、补缝或消除穿模。

## 预览和最终输出

draft 仅验证当前选择，用于临时预览，不能作为最终交付或安装包。
build 进行完整组合验证，输出正式 composition.bem 和 compose-report.json；不能把 draft 改名后交付。
需要场景时再调用：

    python scripts/bem_compose.py preview --package preview-draft/composition.bem --output-dir preview-scene --gpu '{"compositionDraft":true}'

该命令导出 scene.json/二进制资源，不自动打开渲染窗口。
应用内 PC 组合预览可常开；Android 组合界面点“预览”才创建独立 WebView 和临时场景，关闭后释放。
Android 预览可切换部件并“应用到组合”；身体修正使用点按，拖动旋转，双指平移/缩放，支持撤销。
最终生成安装仍是独立操作。预览画面不等同于游戏内效果保证。

当前组合入口会明确拒绝尚未实现的来源特性，如参数/形变/drivers、root offset 或材质属性覆盖；
整个格式支持这些功能不代表组合器已能保留它们。遇到 COMPOSE_UNSUPPORTED_FEATURE，应说明具体特性，
不要为凑合组合而删除它们。布局冲突、源包变化等错误应按原生错误处理，不能绕过验证或盲目放大限制。
