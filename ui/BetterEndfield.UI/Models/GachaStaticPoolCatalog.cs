namespace BetterEndfield.UI.Models;

public static class GachaStaticPoolCatalog
{
    public static IReadOnlyList<GachaPoolInfo> Entries { get; } =
    [
        Info("special_1_0_1", "熔火灼痕", "special", "限定", "chr_0016_laevat"),
        Info("special_1_0_2", "热烈色彩", "special", "限定", "chr_0017_yvonne"),
        Info("special_1_0_3", "轻飘飘的信使", "special", "限定", "chr_0013_aglina"),
        Info("special_1_1_1", "河流的女儿", "special", "限定", "chr_0027_tangtang"),
        Info("special_1_1_2", "狼珀", "special", "限定", "chr_0028_wulfa"),
        Info("special_1_2_1", "春雷动，万物生", "special", "限定", "chr_0030_zhuangfy"),
        Info("joint_1_2_2", "辉光庆典", "extra", "庆典", "chr_0016_laevat", "chr_0013_aglina", "chr_0025_ardelia", "chr_0029_pograni"),
        Info("special_1_3_1", "拳出无悔", "special", "限定", "chr_0031_mifu"),
        Info("special_1_3_2", "逐罪者", "special", "限定", "chr_0033_camille"),
        Info("special_1_4_1", "临渊望北", "special", "限定", "chr_0032_lizhiyan"),
        Info("special_1_4_2", "晨星于此闪耀", "special", "限定", "chr_0035_liino"),
        Info("standard", "基础寻访", "normal", "常驻"),
        Info("weaponbox_constant_2", "星声申领", "constant", "武器", "wpn_funnel_0013"),
        Info("weponbox_1_0_1", "熔铸申领", "special", "武器", "wpn_sword_0006"),
        Info("weponbox_1_0_2", "绘涂申领", "special", "武器", "wpn_pistol_0010"),
        Info("weponbox_1_0_3", "迅行申领", "special", "武器", "wpn_funnel_0011"),
        Info("weponbox_1_1_1", "新芽申领", "special", "武器", "wpn_pistol_0011"),
        Info("weponbox_1_1_2", "绯珀申领", "special", "武器", "wpn_sword_0022"),
        Info("weponbox_1_2_1", "行舟申领", "special", "武器", "wpn_funnel_0015"),
        Info("weponbox_1_3_1", "绛结申领", "special", "武器", "wpn_claym_0017"),
        Info("weponbox_1_3_2", "染赤申领", "special", "武器", "wpn_lance_0015"),
        Info("weponbox_1_4_1", "军列申领", "special", "武器", "wpn_funnel_0016"),
        Info("weponbox_1_4_2", "明曜申领", "special", "武器", "wpn_lance_0014")
    ];

    private static GachaPoolInfo Info(string id, string name, string type, string category, params string[] upIds) =>
        new() { PoolId = id, PoolName = name, PoolType = type, Category = category,
            CategoryId = GachaCategoryKeys.FromLabel(category), UpIds = [.. upIds] };
}
