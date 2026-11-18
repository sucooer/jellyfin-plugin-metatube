using Jellyfin.Plugin.MetaTube.Helpers;
using Jellyfin.Plugin.MetaTube.Translation;
#if __EMBY__
using System.ComponentModel;
using Emby.Web.GenericEdit;
using MediaBrowser.Model.Attributes;

#else
using MediaBrowser.Model.Plugins;
#endif

namespace Jellyfin.Plugin.MetaTube.Configuration;

#if __EMBY__
public class PluginConfiguration : EditableOptionsBase
{
    public override string EditorTitle => Plugin.ProviderName;
#else
public class PluginConfiguration : BasePluginConfiguration
{
#endif

#if __EMBY__
    [DisplayName("服务器地址 / Server")]
    [Description("MetaTube 服务端的完整地址，建议使用 HTTPS。 / Full url of the MetaTube Server, HTTPS protocol is recommended.")]
    [Required]
#endif
    public string Server { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("令牌 / Token")]
    [Description("MetaTube 服务端的访问令牌，后端没设置令牌时留空。 / Access token for the MetaTube Server, or blank if no token is set by the backend.")]
#endif
    public string Token { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("启用自动更新 / Enable auto update")]
    [Description("通过定时任务自动更新插件。 / Automatically update the plugin through scheduled tasks.")]
    public bool EnableAutoUpdate { get; set; } = true;
#endif

#if __EMBY__
    [DisplayName("启用合集 / Enable collections")]
    [Description("按系列自动创建合集。 / Automatically create collections by series.")]
#endif
    public bool EnableCollections { get; set; } = false;

#if __EMBY__
    [DisplayName("启用导演信息 / Enable directors")]
    [Description("将导演添加到对应影片的元数据中。 / Add directors to corresponding video metadata.")]
#endif
    public bool EnableDirectors { get; set; } = true;

#if __EMBY__
    [DisplayName("启用评分 / Enable ratings")]
    [Description("显示原站的社区评分。 / Display community ratings from the original website.")]
#endif
    public bool EnableRatings { get; set; } = true;

#if __EMBY__
    [DisplayName("启用预告片 / Enable trailers")]
    [Description("生成 strm 格式的在线预告片。 / Generate online video trailers in strm format.")]
#endif
    public bool EnableTrailers { get; set; } = false;

#if __EMBY__
    [DisplayName("启用演员本名 / Enable real actor names")]
    [Description("从 AVBASE 搜索并替换为演员本名。 / Search and replace with real actor names from AVBASE.")]
#endif
    public bool EnableRealActorNames { get; set; } = false;

#if __EMBY__
    [DisplayName("启用角标 / Enable badges")]
    [Description("给主图添加中文字幕角标。 / Add Chinese subtitle badges to primary images.")]
#endif
    public bool EnableBadges { get; set; } = false;

#if __EMBY__
    [DisplayName("角标地址 / Badge url")]
    [Description("自定义角标地址，建议 PNG 格式。（默认：zimu.png） / Custom badge url, PNG format is recommended. (default: zimu.png)")]
#endif
    public string BadgeUrl { get; set; } = "zimu.png";

#if __EMBY__
    [DisplayName("主图宽高比 / Primary image ratio")]
    [Description("主图的宽高比，设为负值使用默认值。 / Aspect ratio for primary images, set a negative value to use the default.")]
#endif
    public double PrimaryImageRatio { get; set; } = -1;

#if __EMBY__
    [DisplayName("默认图片质量 / Default image quality")]
    [Description("JPEG 图片的默认压缩质量，取值 0–100。（默认：90） / Default compression quality for JPEG images, set between 0 and 100. (default: 90)")]
    [MinValue(0)]
    [MaxValue(100)]
    [Required]
#endif
    public int DefaultImageQuality { get; set; } = 90;

#if __EMBY__
    [DisplayName("启用影片源过滤 / Enable movie provider filter")]
    [Description("过滤并重新排序影片源的搜索结果。 / Filter and reorder search results from movie providers.")]
#endif
    public bool EnableMovieProviderFilter { get; set; } = false;

#if __EMBY__
    [DisplayName("影片源过滤器 / Movie provider filter")]
    [Description("源名称不区分大小写，从左到右优先级递减，以逗号分隔。 / Provider names are case-insensitive, with decreasing precedence from left to right, separated by commas.")]
#endif
    public string RawMovieProviderFilter
    {
        get => _movieProviderFilter?.Any() == true ? string.Join(',', _movieProviderFilter) : string.Empty;
        set => _movieProviderFilter = value?.Split(',').Select(s => s.Trim()).Where(s => s.Any())
            .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    public List<string> GetMovieProviderFilter()
    {
        return _movieProviderFilter;
    }

    private List<string> _movieProviderFilter;

#if __EMBY__
    [DisplayName("启用模板 / Enable template")]
#endif
    public bool EnableTemplate { get; set; } = false;

#if __EMBY__
    [DisplayName("名称模板 / Name template")]
#endif
    public string NameTemplate { get; set; } = DefaultNameTemplate;

#if __EMBY__
    [DisplayName("标语模板 / Tagline template")]
#endif
    public string TaglineTemplate { get; set; } = DefaultTaglineTemplate;

    public static string DefaultNameTemplate => "{number} {title}";

    public static string DefaultTaglineTemplate => "配信開始日 {date}";

#if __EMBY__
    [DisplayName("翻译模式 / Translation mode")]
#endif
    public TranslationMode TranslationMode { get; set; } = TranslationMode.Disabled;

#if __EMBY__
    [DisplayName("翻译引擎 / Translation engine")]
#endif
    public TranslationEngine TranslationEngine { get; set; } = TranslationEngine.Baidu;

#if __EMBY__
    [DisplayName("百度 App ID / Baidu app id")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Baidu)]
#endif
    public string BaiduAppId { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("百度 App Key / Baidu app key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Baidu)]
#endif
    public string BaiduAppKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Google API 密钥 / Google api key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Google)]
#endif
    public string GoogleApiKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Google API 地址 / Google api url")]
    [Description("自定义 Google 翻译 API 地址。（可选） / Custom Google translate api url. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Google)]
#endif
    public string GoogleApiUrl { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("DeepL API 密钥 / DeepL api key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.DeepL)]
#endif
    public string DeepLApiKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("DeepL API 地址 / DeepL api url")]
    [Description("自定义 DeepL 兼容 API 地址。（可选） / Custom DeepL-compatible api url. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.DeepL)]
#endif
    public string DeepLApiUrl { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("OpenAI API 密钥 / OpenAI api key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.OpenAi)]
#endif
    public string OpenAiApiKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("OpenAI API 地址 / OpenAI api url")]
    [Description("自定义 OpenAI 兼容 API 地址。（可选） / Custom OpenAI-compatible api url. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.OpenAi)]
#endif
    public string OpenAiApiUrl { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("OpenAI 模型 / OpenAI model")]
    [Description("自定义 OpenAI 兼容 API 模型。（可选） / Custom OpenAI-compatible api model. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.OpenAi)]
#endif
    public string OpenAiModel { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("启用标题替换 / Enable title substitution")]
#endif
    public bool EnableTitleSubstitution { get; set; } = false;

#if __EMBY__
    [DisplayName("标题替换表 / Title substitution table")]
    [Description("每行一条记录，以等号分隔，目标子串留空则删除源子串。 / One record per line, separated by equal signs. Leave the target substring blank to delete the source substring.")]
    [EditMultiline(5)]
#endif
    public string TitleRawSubstitutionTable
    {
        get => _titleSubstitutionTable?.ToString();
        set => _titleSubstitutionTable = SubstitutionTable.Parse(value);
    }

    public SubstitutionTable GetTitleSubstitutionTable()
    {
        return _titleSubstitutionTable;
    }

    private SubstitutionTable _titleSubstitutionTable;

#if __EMBY__
    [DisplayName("启用演员替换 / Enable actor substitution")]
#endif
    public bool EnableActorSubstitution { get; set; } = false;

#if __EMBY__
    [DisplayName("演员替换表 / Actor substitution table")]
    [Description("每行一条记录，以等号分隔，目标演员留空则删除源演员。 / One record per line, separated by equal signs. Leave the target actor blank to delete the source actor.")]
    [EditMultiline(5)]
#endif
    public string ActorRawSubstitutionTable
    {
        get => _actorSubstitutionTable?.ToString();
        set => _actorSubstitutionTable = SubstitutionTable.Parse(value);
    }

    public SubstitutionTable GetActorSubstitutionTable()
    {
        return _actorSubstitutionTable;
    }

    private SubstitutionTable _actorSubstitutionTable;

#if __EMBY__
    [DisplayName("启用类型替换 / Enable genre substitution")]
#endif
    public bool EnableGenreSubstitution { get; set; } = false;

#if __EMBY__
    [DisplayName("类型替换表 / Genre substitution table")]
    [Description("每行一条记录，以等号分隔，目标类型留空则删除源类型。 / One record per line, separated by equal signs. Leave the target genre blank to delete the source genre.")]
    [EditMultiline(5)]
#endif
    public string GenreRawSubstitutionTable
    {
        get => _genreSubstitutionTable?.ToString();
        set => _genreSubstitutionTable = SubstitutionTable.Parse(value);
    }

    public SubstitutionTable GetGenreSubstitutionTable()
    {
        return _genreSubstitutionTable;
    }

    private SubstitutionTable _genreSubstitutionTable;
}
