#pragma once
// 轻量 INI 配置 (仅 [link] 节)
#include <cstdint>
#include <string>

namespace ecl {

struct LinkConfig {
    bool enabled = true;
    uint16_t port = 9601;
    bool unix_space = false;   // true=发送端已提供 Unity 坐标
    float pos_scale = 1.0f;
    bool fov_enabled = true;
    bool rotation_enabled = true;
    uint32_t timeout_ms = 500; // 超过该时长无新帧则让出控制
    uint32_t hook_delay_ms = 5000; // 解析就绪后等待毫秒数再装钩(默认5s)
    bool inventory = false;        // true=输出相机符号清单到 inventory.log
    bool shmem = false;            // true=位姿来源改为 Phase-1 共享内存
    bool gamesys_probe = false;    // true=启动后探测游戏相机系统(营销相机等)
    bool probe_open_marketing = false; // 探测: 置门控并打开营销相机
    float probe_fov = 0.0f;        // 探测: >0 时调用 SetOverrideFOVForCurrCamera
    float probe_aperture = 0.0f;   // 探测: >0 时调用快照相机 SetAperture
    // v2: 位姿写入模式。0=关闭(默认, 避开 Cinemachine 冲突) 1=直接写 Camera.main transform
    //     2=通过营销相机模拟输入做闭环伺服(伺服精度见 v3)
    int pose_mode = 0;
    bool lens_enabled = true;      // true=把 Blender 镜头参数应用到 Unity 物理相机
    // v11: 位姿用"相对基线"换算。
    // 原因: Blender 场景坐标与游戏世界坐标毫无关系, 绝对套用会把相机搬到几百单位
    //       之外的空区(实测表现为画面虚空/发灰)。相对模式以两侧基线之差为准:
    //       目标 = 游戏基线 + (Blender当前 - Blender基线)
    bool pose_relative = true;
    // 相对位移上限(单位); 0 = 不限。防止映射错误把相机甩到无穷远。
    float pose_max_offset = 800.0f;
    // v16: 朝向映射模式。
    //   true (推荐) = 绝对: 游戏相机朝向 = 映射后的 Blender 相机朝向(不再叠加游戏基线)。
    //     本地轴一一对应: Blender 里绕相机本地 X/Y/Z 转, 游戏相机就绕自己的本地 X/Y/Z 转,
    //     俯仰不会带出滚转。数值符号因左右手系不同而与 Blender 相反, 物理方向一致。
    //   false = 相对(旧行为): 把 Blender 的旋转增量叠加到"游戏相机自己的基线朝向"上。
    //     两者基线朝向天然相差一个常量角 Δ, 于是 Blender 的本地轴被 Δ 转过 ——
    //     表现为"绕 xyz 旋转不是标准直角坐标系/俯仰会歪"。
    // 位置在两种模式下都仍是相对增量(不会被搬到关卡外)。
    bool pose_rot_absolute = true;
    // v1.0.0at: **相对模式的旋转增量算在哪个系**(仅 pose_rot_absolute=false 时有意义)。
    //   背景(实测, 见 source/tools/verify_rotation_mapping.py 第 6 节):
    //     相对模式的零点是"游戏相机自己的基线朝向", 它与映射后的 Blender 基线相差 Δ
    //     (现场实测 53.3°/89.0°, 随每次武装变化)。增量四元数只能左乘(世界系)或右乘
    //     (相机本地系), 因此 **Δ≠0 时不可能同时让两个系都 1:1** —— 换一个系就换一种歪法,
    //     这正是"绕本地轴转对了、绕世界轴转又歪"(以及反过来的历史投诉)的根源。
    //     数值: 世界系增量 → 世界轴偏 0.00°, 本地轴偏 52.6~63.1°;
    //           本地帧增量 → 本地轴偏 0.00°, 世界轴偏 38.8~76.9°(换姿态最大漂移 87°);
    //           Δ=0(对齐基线)时四种写法全部 0.000°。
    //   0 = 标准(默认, 推荐): 旋转与绝对模式一致(等价武装时对齐基线 Δ=0), 两系同时 1:1。
    //       相对管线只对**位置**有意义 —— 与 v16 的结论"位置用增量是对的, 旋转不能"一致。
    //   1 = 世界系增量: 绕 Blender 世界 X/Y/Z 转 → 游戏绕对应世界轴(0.00°),
    //       但绕相机自己的轴转会在游戏里带出滚转(偏 Δ)。
    //   2 = 本地帧增量(1.0.0z 旧语义): 绕相机自己的轴 1:1, 世界轴偏 Δ。
    // 指令 38 可热覆盖: a0=1 绝对 / 0 相对(按本键) / 2 相对·世界系 / 3 相对·本地帧。
    int pose_rot_increment_frame = 0;
    // v1.1: 位置基线锚点。
    //   true (推荐) = 以 Blender 的"参考物件"(场景里的初始方块)为原点做绝对映射:
    //       目标 = 游戏基线点 + (Blender相机 − 参考物件) × pos_scale
    //     游戏基线点 = 武装那一刻游戏相机的位置(拍照模式下它是相机相对角色的位置),
    //     所以方块摆在哪里、相机相对方块怎么摆 → 游戏里就是相对角色的同一摆放, 可复现。
    //     需要插件在包里带上参考物件位置(flag 位 kFlagReference); 老插件会自动退回旧行为。
    //   false = 旧行为, 以"Blender 相机武装那一刻的位置"为原点(增量)。
    bool pose_anchor_reference = true;
    // v1.2: "游戏侧原点"的取法。锚点应当落在**角色**身上, 而不是"武装那一刻相机在哪"。
    //   0 = armed   : 武装那一刻的相机位置(旧行为)
    //   1 = vcamfollow / 2 = vcamloolat : 当前 vcam 的 Follow / LookAt 目标
    //   3 = controllertrans : 当前激活控制器的 cameraTrans
    //   4 = camminusoffset  : 引擎相机位置 − SnapshotCameraController.GetCameraOffset()
    // 可用指令 42 热切换; 指令 43 打印全部候选(诊断)。
    int pose_anchor_source = 0;
    // v1.2: 原点修正量(游戏单位, 世界轴), 叠加在"武装时刻相机位置"上。
    // 用法: 武装后把 Blender 相机放到原点(0,0,0), 用 Blender 把游戏相机挪到正好与角色
    //       重合, 然后发指令 41 —— 模块会把当时的位移吸收成这个修正量, 于是 (0,0,0)
    //       从此正好落在角色身上(同一次会话内后续武装继续有效)。
    float pose_origin_dx = 0.f;
    float pose_origin_dy = 0.f;
    float pose_origin_dz = 0.f;
    // v1.4: 偏移驱动(pose_mode=4)用的"偏移坐标系"基矩阵(行主序 9 个数)。
    // 实测: SnapshotCameraController.SetCameraOffset 的偏移**不在世界轴上** ——
    //   X 轴精确对齐世界 X, 但 Y/Z 在世界 Y-Z 平面内被转过约 11°(带俯仰的机架系):
    //   写 (0,1,0) → 相机世界位移 (0, 0.982, 0.191); 写 (0,0,1) → (0, -0.191, 0.982)
    // 即 世界位移 = R · 偏移, R 为绕世界 X 转 +11° 的旋转(cos=0.98163 sin=0.19081)。
    // 默认单位阵(不校正)。指令 45 会实测这个矩阵。
    float pose_offset_basis[9] = {1.f, 0.f, 0.f, 0.f, 1.f, 0.f, 0.f, 0.f, 1.f};
    // v1.6: 角色锚点微调量(游戏世界轴, 游戏单位)。
    // 实测: vcamFollow 给的是"机架瞄准点" —— 相机落上去正好在**角色脚底**,
    //       而 Blender 侧约定 (0,0,0) = 角色**头部中心** → 需要往上抬 1.4。
    // 游戏世界是 Y 向上的(Unity 惯例; 插件已把 Blender 的 Z 换算成包的 Y), 所以默认 (0, 1.4, 0)。
    // 运行时热调: tools\send_cmd2.ps1 -Id 48 -A0 <x> -A1 <y> -A2 <z>
    float pose_anchor_ox = 0.f;
    float pose_anchor_oy = 1.4f;
    float pose_anchor_oz = 0.f;

    // v1.0.0ah(默认 **true**): 写入时刻补读锚点。
    // 为什么需要(1.0.0af 零 I/O 逐帧追踪实测, 走动段 1471 行):
    //   目标的锚点是在 OnGameTick(TailLateTick 组, **帧内早**)读的, 而相机是在
    //   CinemachineBrain.LateUpdate 之后(**帧末**)写的 → 相机用的角色坐标老了大半个帧;
    //   实测 |aU − a3| / 位移 = 1.000(整整一个相位), 而帧时间还在抖(中位 ~20ms、最大 49ms、
    //   37% 的帧偏离中位 >20%) → 相位差 = v × 帧时间 → **幅度 ∝ 速度的抖动**。
    //   写入前重读一次同一来源的锚点并补差, 实测 |aL − a3| 降到 **0.0000 m**(1471 帧),
    //   用户主观评价"完美解决"。
    // 关掉它: ini 写 pose_late_anchor=false, 或运行时 -Id 62 -A0 0。
    bool pose_late_anchor = true;

    // ---- v1.0.0am: "隐藏游戏 UI"的两档参数 ----
    // ui_hide_strict(默认 **true**): 隐藏策略。
    //   true  = 严格保持: 每帧把 UI 相机遮罩压回 0, **连 ESC 菜单/背包也看不见**
    //           (用户 2026-09-14 明确选择"全藏")。
    //   false = 尊重游戏: 游戏主动加层(呼出界面)时放行, 回到常规后自动重新隐藏
    //           —— 但战斗中游戏频繁加层, 会导致隐藏"悄悄失效"(体力条/血条回来), 故非默认。
    // 运行时切换: -Id 66 -A0 1 / 0。
    bool ui_hide_strict = true;
    // ui_hide_maincam_clear_bits(**默认 65536 = 位 16 WorldUI**): 隐藏时从 MainCamera 遮罩里清掉的层。
    // 为什么需要: 屏幕 HUD 由只渲染层 5 的 UICamera 画, 压它就能藏; 但**跟着角色/怪物跑的
    //   UI**(怪物血条/状态条、角色体力条)在**层 16 = WorldUI**, 由 MainCamera 渲染
    //   (MainCamera 的遮罩 0xAFFFDBDF 明确包含位 16, 而它不渲染位 5/10/13/28/30) ——
    //   压 UI 相机永远盖不住它们。
    // 实测(2026-09-15, 指令 71 一次性测试 15 秒): 清掉位 16 后
    //   **怪物血条 + 角色体力条都消失, 世界画面完好无损**(角色/怪物/建筑/地形都在) → 用户确认
    //   "两个都消失了 / 世界正常没缺东西", 于是设为默认。
    // 想连世界里的交互提示("按 F 交互", 层 15 = UIInteract)一起藏: 65536 + 32768 = 98304。
    // 不想动主相机: 写 0。运行时: -Id 71 -A0 <位掩码> [-A1 秒](一次性测试, 自动还原)/ 不带 -A1 = 持续。
    int ui_hide_maincam_clear_bits = 65536;

    // ---- v1.0.0ar: 主(世界)相机"保活"(默认 **false**, 待实测后再定默认) ----
    // 问题(用户 2026-09-15 实测): 按 ESC 时游戏会 _SetMainCameraCullingMask(0) —— **世界完全不画**,
    //   同一时刻 UI 相机的遮罩从常规 0x20 扩到 0x420(位 5 UI + 位 10 UIPP), 也就是说那一刻
    //   **整幅画面只由 UI 相机产出**。于是"把 UI 相机压到 0"= 把屏幕唯一的产出源按掉 → 全黑。
    //   (隔离实验: 面板取消勾选、我们的配置条目已撤, 主相机**仍然是 0** → 这个 0 是游戏的行为。)
    // 打开后: 帧末发现主相机被游戏关掉, 就按回期望遮罩(GetMainCamDefaultCullingMask() & ~清位)
    //   → "隐藏 UI + 保活" = **ESC 时也能看到时停的正常世界画面, 且画面里没有任何 UI**。
    // **闸门(用户 2026-09-15 要求): 只在勾选了"隐藏游戏 UI"时才会触发** —— 没勾选时一次都不碰主相机
    //   (那时画面完全是游戏的事, 包括它按 ESC 主动把世界相机写成 0)。
    // 安全: 绝不写 0(那就是黑屏); 目标遮罩推导不出来就什么都不做; 隐藏 UI 结束/关闭保活时交还游戏重算。
    // 运行时: -Id 73 -A0 1 / 0; 先试一把: -Id 73 -A1 15(15 秒后自动还原) / 激进档加 -A3 1。
    bool ui_hide_world_keepalive = false;

    // ---- v1.0.0ar: "隐藏 UI"时**禁用 UI 相机**(默认 **true**) ----
    // 为什么需要(2026-09-15 实测, 这条是"ESC 时看到时停世界"的最后一块):
    //   隐藏 UI = 把 UICamera 遮罩压成 0(它什么都不画), 但**遮罩=0 的相机照样会清屏** ——
    //   按 ESC 那一刻游戏把画面交给 UICamera 渲染(它还带清屏), 于是主相机刚画好的世界
    //   被擦成黑的, 看起来像"世界没渲染"(实为主相机一直在画, 用指令 73 保活可证)。
    //   实测对照(ESC 界面态, UI 相机遮罩=0):
    //     · 写 clearFlags=3(Depth)  -> **仍然全黑**(本作是自定义渲染管线, clearFlags 不被采纳)
    //     · 写 enabled=false        -> **世界画面出来了(时停/静止的世界)** ✓
    //   所以隐藏时把 UI 相机整体禁用(它既不画也不擦), 还原时写回原值。
    //   世界态下该相机本来就什么都不画, 禁用它不改变任何可见效果(可逆、由本行 / 指令 74 控制)。
    bool ui_hide_disable_uicam = true;

    // ---- v1.0.0as: 光圈的"景深保活"(**默认 false**, 2026-09-15 晚改为默认关) ----
    // 由来(2026-09-15 用户实测 + 指令 77 的双向对照):
    //   · `SnapshotCameraController.SetAperture/SetFocusDistance` 只是**写数值**;
    //     要把它们挂到主相机(我们自己的自由相机)上, 必须调一次 `ApplySnapshotDofSettings()`。
    //   · 实测: f/1.4 + Apply -> 明显虚化; f/22 + Apply -> 几乎全清楚(双向成立);
    //     而"已激活"之后, 单纯 SetAperture 也能实时改变虚化 ⇒ Apply 的作用是**激活这条挂接**。
    //   · 用户现象: "武装后自由镜头有虚化, 但进一次官方相机再退出后就失效了" —— 根因是**快照相机实例
    //     被游戏换掉了**(我们那个陈旧), 修法是**重取实例**(指令 78 / 面板「修复虚化」按钮)。
    // **为什么默认关**: 这个"每 3 秒重施加"① 救不了上面的实例陈旧(往陈旧实例 Apply 等于没做);
    //   ② 用户实测"相机会有规律地间隔抽动" —— 与这个 3 秒周期的重施加高度吻合。
    //   而它的唯一用处(游戏把景深覆盖掉后再施加)已经被"改数值时补一次 Apply"与面板按钮覆盖。
    // 需要时写 true: 每次改完光圈/对焦补一次 Apply + 每 3 秒重施加一次(只在数据流活着时做)。
    bool lens_dof_keepalive = false;

    // ---- v1.0.0ap: 锚点来源"自动回退"(默认 **true**) ----
    // 问题(2026-09-15 实测): 进**干员/角色界面**后, 世界角色的锚点(vcamFollow)读不到 —— 而
    //   GameSysAnchorPosition() 在实时读失败时会**返回 true 并附上粘性缓存值**, 于是那 709 米外、
    //   211 秒前的旧值被当成有效原点(兜底扫描形同虚设), 相机就"找不到界面里的角色"。
    //   而"重置基线"确实会重新抓锚点(该行为与旧版逐行一致), 但抓的还是同一个死源 → 按了也没用。
    // 做法(用户提的方案, 已实现): 首选仍是 vcamFollow(它更准); 一旦它"读到的不是新鲜值"(而是缓存),
    //   就**自动改用引擎相机**(游戏自己的机位, 永远可用) —— 抓锚点时如此, 每帧取原点时同样如此;
    //   等 vcamFollow 恢复实时可读, 自动切回。滞回: 连续失效 >500ms 才切, 恢复 >300ms 才切回,
    //   避免单帧失败来回跳。
    // 关掉: ini 写 pose_anchor_auto_fallback=false, 或运行时 -Id 72 -A0 0。
    bool pose_anchor_auto_fallback = true;
};

// 解析 iniPath (UTF-8 或 ANSI)。失败时返回默认值。
LinkConfig LoadConfig(const std::string& iniPath);

}  // namespace ecl
