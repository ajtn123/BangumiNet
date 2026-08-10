namespace BangumiNet.Common;

public static class Config
{
    /// <summary>bangumi/common 仓库的最后 commit</summary>
    public const string Commit = "2ee87b9995353f77cf02e390e8635bf97dbc1fa2";

    public static Dictionary<string, string[]> AppTags => new()
    {
        ["功能增强"] = ["列表管理", "搜索与发现", "目录与标签", "评分与统计", "进度管理", "编辑器", "外部链接"],
        ["工具"] = ["娱乐"],
        ["界面优化"] = ["交互体验", "导航", "显示增强", "样式与布局", "移动端优化"],
        ["社交互动"] = ["好友与用户", "时间线", "表情与投票", "讨论与评论", "内容分享"],
        ["隐私与过滤"] = ["内容屏蔽", "剧透防护", "隐私保护", "数据与导出"],
        ["维基编辑"] = ["批量操作", "编辑辅助"],
        ["小圣杯"] = ["数据与分析", "综合辅助"],
    };
}
