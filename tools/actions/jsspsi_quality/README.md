# 祀高质量过渡数据生成工具

`build_quality_data.py` 生成 `native/modules/actions/jsspsi_quality_data.h`，即动作模块可选项 `jsspsi_quality` 用的内置数据：祀和身下墨兽的身体接缝差，以及尾巴、后发的附属件循环。数据编进模块，运行时不读、不写任何文件。选项怎么用这些数据，见 [特殊冲刺动画持续播放](../../../docs/actions/SPECIAL_DASH_CONTINUOUS_ANIMATION.md) 的「祀的可选高质量过渡」。

下文 u 是祀第 0 层冲刺状态的归一化时间，0–1 对应剪辑的 0–5.667 秒；回绕区间是 target 0.126 到 begin 0.315；圈内相位 φ = frac((u − 0.126)/(0.315 − 0.126))。

## 输入

四类输入都由参数给出，仓库里只有蒙皮骨目录。生成仓库里那份头文件用的是下面这批，录制和差表都是在 1.6.4 Windows 客户端上录的，SHA-256 也写在头文件开头：

| 参数 | 文件 | 来源 | SHA-256 |
| --- | --- | --- | --- |
| `--seam` | `seam_jsspsi.bin`，身体接缝差表 BESEAM03，5 条 | 本仓库 fork 的数据分支 `data/jsspsi-quality-inputs` 的 `inputs/` | `52282fa11202f2b041768db2d340c038a264933a8082f6276103a0edbcc27a81` |
| `--accessory` | `accessory_jsspsi.bin`，附属件循环表 BEACC001，每侧每圈 1024 个样本、30 根骨 | 完整版分支 `feat/actions-jsspsi-sustained-dash` 的 `native/modules/actions/assets/` | `b6159d6878b173559c81abd28a32fd22780556c705b9aa13af85df5b0a365dc4` |
| `--capture` | `capture_jsspsi-20261018-180810-right.bin`，原版录制 BECAP001 | 数据分支 `inputs/` | `ca706331474b3671a1074190f085c23a8fb33220bd5cf0f1dc82281741ccf612` |
| `--catalog` | `tools/CustomModel/catalog/chr_0036_jsspsi.json`，蒙皮骨目录 | 本仓库 | `ec7c85d4a3f9a8ef5da5b4d9df0cf3a55955d45f5b918a6119ff6307c595df27` |

- 接缝差表和附属件表都是完整版做出来的东西，格式见完整版分支上的 `docs/actions/research/1.6.3/jsspsi-dash/JSSPSI_SUSTAINED_DASH.md` 附录 A 和 `JSSPSI_ACCESSORY_LOOP.md` §3.1。接缝差表按骨的叶名作键，附属件表的 1024 个样本是完整版从全组表重采样来的（`JSSPSI_ACCESSORY_LOOP.md` §3.2）。
- `--capture` 只用来取 Transform 的路径：把叶名换成从各自 Animator 根起的完整路径（祀用分组 0，身下墨兽用分组 1，即 inkbiter01），运行时按路径 `Transform.Find`，不用枚举整棵树。
- `--compare-right`、`--compare-left` 可选，只用于误差报告：给生成完整版附属件表时选中的两份录制（右侧 `capture_jsspsi-20261018-180810-right.bin`、左侧录制 3 `capture_jsspsi-20261018-180728-left.bin`，同在数据分支 `inputs/`）。

## 生成

在仓库根目录运行，只依赖 NumPy（在 Python 3.13、NumPy 2.5.4、Linux x86_64 上跑过，约 1 秒）：

```bash
python3 -B tools/actions/jsspsi_quality/build_quality_data.py \
  --seam <数据目录>/seam_jsspsi.bin \
  --accessory <完整版表目录>/accessory_jsspsi.bin \
  --capture <数据目录>/capture_jsspsi-20261018-180810-right.bin \
  --compare-right <数据目录>/capture_jsspsi-20261018-180810-right.bin \
  --compare-left <数据目录>/capture_jsspsi-20261018-180728-left.bin \
  --catalog tools/CustomModel/catalog/chr_0036_jsspsi.json \
  --output native/modules/actions/jsspsi_quality_data.h \
  --report <工作目录>/jsspsi_quality_data.json
```

用上表那批输入重新生成，结果与仓库里的头文件逐字节一致（SHA-256 `f83dcd38b21249635636601245ec9a86d1e2f35191f188245934b7a2c3bdf57a`）。不给 `--report` 时报告写在 `<output>.json`；报告里有输入的绝对路径之外的全部数字，不要随头文件提交。

## 做了什么

**接缝**：从差表里取祀左右两条（状态 hash 右 `-445642423`、左 `528430122`）和身下墨兽那一条（`340743731`，与祀同一回绕区间），两侧墨兽的两条（回绕区间 0.116–0.317）不用。每根骨的旋转差 D 小于 0.1°、位置差 dp 小于 0.1 mm 的丢掉；留下的旋转存 xyz 三个 int16（w ≥ 0，运行时重建），位置以 0.01 mm 为单位存三个 int16。重名的叶名（例如两个 `L_skirtA_02_jnt_Collider`）按完整版的做法共用一行。

**附属件**：每侧每圈 128 个样本，就是 1024 样本表里每隔 8 个取一个（φ = j/128 正好落在原样本上，不插值）。旋转按 int16 存四个分量；局部位置在一圈里几乎不变（最大跨度 1.35 mm，在不蒙皮的 `tail_10_jnt`），每根骨只存一圈的平均值。运行时和完整版一样：在相邻两个样本间按最短弧 slerp。

**样本数怎么选**：在 φ = i/8192 的网格上，拿精简表和 1024 样本表比（只算蒙皮骨，30 根里有 27 根；链末端按尾巴、两束后发各自的链在父骨坐标里算末端点位置）：

| 每圈样本数 | 旋转数据（两侧） | 右侧：蒙皮骨最大角度差 / 链末端最大位置差 | 左侧：同左 |
| --- | --- | --- | --- |
| 64 | 30 KB | 0.236° / 1.57 mm | 0.188° / 1.18 mm |
| 96 | 46 KB | 0.200° / 1.27 mm | 0.170° / 0.66 mm |
| **128（采用）** | **61 KB** | **0.123° / 1.00 mm** | **0.067° / 0.54 mm** |
| 160 | 77 KB | 0.086° / 1.00 mm | 0.070° / 0.34 mm |
| 192 | 92 KB | 0.060° / 1.00 mm | 0.037° / 0.26 mm |

右侧链末端 1.00 mm 的底是「位置取一圈平均」带来的（`tail_10_jnt` 不蒙皮，它的局部位置决定尾尖点），与样本数无关。128 时蒙皮骨误差不到 0.13°，链末端约 1 mm；作为对照，原生物理权重约 0.01 时布料写回本身就会让骨头偏 0.5° 左右（完整版 `JSSPSI_SUSTAINED_DASH.md` §6），所以取 128。

## 结果（仓库里这份头文件）

| 项 | 数 |
| --- | --- |
| 接缝：祀右 / 左 / 身下墨兽 | 410 / 398 / 32 行；差表原有 638 / 638 / 63 个名字，低于阈值丢掉 228 / 240 / 31 个；运行时附属件那 30 根骨不走接缝，实际写 380 / 368 / 32 根 |
| 接缝量化误差 | 旋转 ≤ 0.003°，位置 ≤ 0.008 mm |
| 附属件 | 2 侧 × 128 样本 × 30 根骨 |
| 附属件与 1024 样本表比（φ = i/8192） | 蒙皮骨最大 0.123°（右侧 `tail_06_jnt`）/ 0.067°；含不蒙皮末端骨 0.845° / 0.193°；局部位置 0.68 / 0.23 mm（都在不蒙皮的 `tail_10_jnt`）；链末端尾巴 1.00 / 0.54 mm，后发 0.44 / 0.42 mm |
| 附属件与原版录制比（借用段的录制帧，尾巴、后发两组分开取：右 74、58 帧，左 84、56 帧） | 精简表蒙皮骨最大 0.124° / 0.067°，局部位置最大 0.67 / 0.11 mm；1024 样本表本身是 0.004° / 0.002° |
| 编译后数据量 | 约 86 KB：节点名和父链 12.2 KB（含每个名字的指针），接缝 11.8 KB，附属件 62.2 KB |
| 头文件文本 | 241 KB |

「借用段」是附属件循环里直接取原版的两段：φ 从 0 到 u3 那段取原版第三步（u 0.315 起），φ 从 uk 到 1 那段取原版第二步的末段；连接段是合成的，没有原版可比。各组的 u3、uk 见完整版 `JSSPSI_ACCESSORY_LOOP.md` §2.1，工具里写死为同样的值。

## 什么时候要重新生成

和完整版的两张表一样，数据不带游戏或动画版本：

- 两侧状态 hash、身下墨兽的状态 hash 或回绕区间变了：模块对不上就不启用这一侧（日志 `rider binding rejected` 或 `seam unavailable`），轻量版照常。要重录差表和附属件表，再跑本工具。
- hash 和区间没变、剪辑内容变了：模块不会发现，接缝补的是旧的差。游戏更新后要按完整版的流程重录、重做两张表，再跑本工具，进游戏确认。
- 骨架上多了别的 Transform（换装备）：按路径找，不影响；找不到的骨只跳过，找到的不到八成时整次冲刺不启用（日志 `rider binding rejected`）。
