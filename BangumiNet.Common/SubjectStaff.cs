using BangumiNet.Common.Attributes;
using System.ComponentModel;

namespace BangumiNet.Common;

public enum StaffCategory
{
    #region Anime Categories

    [NameEn("director")]
    [NameCn("导演类")]
    AnimeDirector = 1,

    [NameEn("storyboard")]
    [NameCn("分镜/脚本类")]
    AnimeStoryboard = 2,

    [NameEn("direction")]
    [NameCn("演出类")]
    AnimeDirection = 3,

    [NameEn("animation")]
    [NameCn("作画类")]
    AnimeAnimation = 4,

    [NameEn("design")]
    [NameCn("设定类")]
    AnimeDesign = 5,

    [NameEn("art")]
    [NameCn("美术类")]
    AnimeArt = 6,

    [NameEn("music")]
    [NameCn("声音类")]
    AnimeMusic = 7,

    [NameEn("production")]
    [NameCn("制作类")]
    AnimeProduction = 8,

    [NameEn("producer")]
    [NameCn("制片类")]
    AnimeProducer = 9,

    [NameEn("colorist")]
    [NameCn("色彩类")]
    AnimeColor = 10,

    [NameEn("visual")]
    [NameCn("视觉类")]
    AnimeVisual = 11,

    [NameEn("assistant")]
    [NameCn("助理类")]
    AnimeAssistant = 12,

    #endregion
    #region Game Categories

    [NameEn("producer")]
    [NameCn("发行类")]
    GameProducer = 1001,

    [NameEn("director")]
    [NameCn("导演类")]
    GameDirector = 1002,

    [NameEn("storyboard")]
    [NameCn("脚本类")]
    GameStoryboard = 1003,

    [NameEn("design")]
    [NameCn("设定类")]
    GameDesign = 1004,

    [NameEn("art")]
    [NameCn("美术类")]
    GameArt = 1005,

    [NameEn("animation")]
    [NameCn("动画类")]
    GameAnimation = 1006,

    [NameEn("music")]
    [NameCn("声音类")]
    GameMusic = 1007,

    [NameEn("production")]
    [NameCn("制作/程序类")]
    GameProduction = 1008,

    [NameEn("assistant")]
    [NameCn("助理类")]
    GameAssistant = 1000,

    #endregion
}

public enum PresentationGroup
{
    #region Anime Presentation Groups

    AnimeSource,

    AnimeDirector,

    AnimeScreenplay,

    AnimeEpisodeDirection,

    AnimePreproduction,

    Animation,

    AnimeInShow,

    AnimeSupervision,

    AnimePostproduction,

    [Keywords("记录")]
    AnimeSound,

    AnimeMusic,

    AnimeAnimationProduction,

    AnimeProducers,

    AnimeCooperation,

    #endregion
}

public enum SubjectStaff
{
    #region Anime

    [NameEn("Original Creator/Original Work")]
    [NameCn("原作")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSource)]
    OriginalWork = 1,

    [NameEn("Chief Director")]
    [NameCn("总导演")]
    [NameJp("総監督 / チーフディレクター")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeDirector)]
    ChiefDirector = 74,

    /// <summary>
    /// RDF: directedBy
    /// </summary>
    [NameEn("Director/Direction")]
    [NameCn("导演")]
    [NameJp("監督 シリーズ監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeDirector)]
    Director = 2,

    [NameEn("Assistant Director")]
    [NameCn("副导演")]
    [NameJp("助監督 / 監督補佐 / 監督助手")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeDirector)]
    AssistantDirector = 72,

    [NameEn("Script/Screenplay")]
    [NameCn("脚本")]
    [NameJp("シナリオ")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeScreenplay)]
    Script = 3,

    [NameEn("Storyboard")]
    [NameCn("分镜")]
    [NameJp("コンテ  ストーリーボード  画コンテ  絵コンテ")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    Storyboard = 4,

    [NameCn("分镜协力")]
    [NameJp("絵コンテ協力 コンテ協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    [Folded]
    AssistantStoryboard = 104,

    [NameCn("分镜抄写")]
    [NameJp("絵コンテ清書 コンテ清書")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    [Folded]
    StoryboardCopy = 105,

    [NameEn("Chief Episode Direction")]
    [NameCn("主演出")]
    [NameJp("チーフ演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeDirector)]
    ChiefEpisodeDirection = 89,

    [NameEn("Episode Direction")]
    [NameCn("演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    EpisodeDirection = 5,

    [NameEn("Assistant Episode Direction")]
    [NameCn("演出助理")]
    [NameJp("演出助手 演出補佐 演出協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    [Folded]
    AssistantEpisodeDirection = 91,

    [NameEn("OP・ED Direction")]
    [NameCn("OP・ED 演出")]
    [NameJp("OP・ED 演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    OPEDDirection = 128,

    [NameEn("Bank Storyboard Direction")]
    [NameCn("Bank 分镜演出")]
    [NameJp("バンク コンテ・演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    BankStoryboardDirection = 129,

    [NameEn("Live Storyboard Direction")]
    [NameCn("Live 分镜演出")]
    [NameJp("ライブ コンテ・演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    LiveStoryboardDirection = 130,

    [NameEn("Meta-story Storyboard Direction")]
    [NameCn("剧中剧分镜演出")]
    [NameJp("劇中劇 コンテ・演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeInShow)]
    MetaStoryStoryboardDirection = 131,

    [NameEn("Music")]
    [NameCn("音乐")]
    [NameJp("楽曲  音楽")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    Music = 6,

    [NameEn("Original Character Design")]
    [NameCn("人物原案")]
    [NameJp("キャラ原案")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    OriginalCharacterDesign = 7,

    [NameEn("Character Design")]
    [NameCn("人物设定")]
    [NameJp("キャラ设定")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    CharacterDesign = 8,

    [NameEn("Sub-Character Design")]
    [NameCn("副人物设定")]
    [NameJp("サブキャラクターデザイン")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    SubCharacterDesign = 106,

    [NameEn("Guest Character Design")]
    [NameCn("客座人物设定")]
    [NameJp("ゲストキャラクターデザイン")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    GuestCharacterDesign = 107,

    [NameEn("Meta-story Character Design")]
    [NameCn("剧中剧人设")]
    [NameJp("劇中劇 キャラクターデザイン")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeInShow)]
    MetaStoryCharacterDesign = 132,

    [NameEn("Layout")]
    [NameCn("构图")]
    [NameJp("レイアウト")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    Layout = 9,

    [NameCn("构图监修")]
    [NameJp("レイアウト監修")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    LayoutSupervisor = 108,

    [NameEn("Series Composition")]
    [NameCn("系列构成")]
    [NameJp("シナリオディレクター  構成  シリーズ構成  脚本構成")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeScreenplay)]
    SeriesComposition = 10,

    [NameEn("Art Direction")]
    [NameCn("美术监督")]
    [NameJp("美術監督 アートディレクション 背景監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeArt, StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    ArtDirection = 11,

    [NameEn("Art Design")]
    [NameCn("美术设计")]
    [NameJp("美術設定")]
    [Categories<StaffCategory>(StaffCategory.AnimeArt, StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    ArtDesign = 71,

    [NameCn("美术板")]
    [NameJp("美術ボード")]
    [Categories<StaffCategory>(StaffCategory.AnimeArt)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    ArtBoard = 96,

    [NameCn("美术")]
    [NameJp("美術")]
    [Categories<StaffCategory>(StaffCategory.AnimeArt)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    Art = 97,

    [NameCn("印象板")]
    [NameJp("イメージボード")]
    [Categories<StaffCategory>(StaffCategory.AnimeArt, StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    ImageBoard = 98,

    [NameEn("Color Design")]
    [NameCn("色彩设计")]
    [NameJp("色彩設定")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign, StaffCategory.AnimeColor)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    ColorDesign = 13,

    [NameEn("Mechanical Design")]
    [NameCn("机械设定")]
    [NameJp("メカニック設定")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    MechanicalDesign = 16,

    [NameEn("Concept Design")]
    [NameCn("概念设计")]
    [NameJp("コンセプトデザイン")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    ConceptDesign = 112,

    [NameEn("Costume Design")]
    [NameCn("服装设计")]
    [NameJp("衣装デザイン 衣装設定")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    CostumeDesign = 113,

    [NameEn("Title Design")]
    [NameCn("标题设计")]
    [NameJp("タイトルデザイン")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    TitleDesign = 114,

    [NameEn("Setting Cooperation")]
    [NameCn("设定协力")]
    [NameJp("設定協力 デザイン協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    [Folded]
    SettingCooperation = 115,

    [NameEn("Prop Design")]
    [NameCn("道具设计")]
    [NameJp("プロップデザイン")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    PropDesign = 19,

    [NameEn("2D WORKS")]
    [NameCn("2D 设计")]
    [NameJp("2D ワークス")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    Works2D = 99,

    [NameEn("3D WORKS")]
    [NameCn("3D 设计")]
    [NameJp("3D ワークス")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    Works3D = 100,

    [NameEn("Chief Animation Director")]
    [NameCn("总作画监督")]
    [NameJp("チーフ作画監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    ChiefAnimationDirector = 14,

    [NameCn("总作画监督助理")]
    [NameJp("総作画監督補佐 総作監補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    [Folded]
    AssistantChiefAnimationDirection = 110,

    [NameEn("Animation Director")]
    [NameCn("作画监督")]
    [NameJp("作監 アニメーション演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    AnimationDirector = 15,

    [NameEn("Assistant Animation Direction")]
    [NameCn("作画监督助理")]
    [NameJp("作画監督補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    [Folded]
    AssistantAnimationDirection = 90,

    [NameCn("构图作画监督")]
    [NameJp("レイアウト作画監督 レイアウト作監")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    LayoutAnimationDirection = 109,

    [NameEn("Mechanical Animation Direction")]
    [NameCn("机械作画监督")]
    [NameJp("メカニック作監")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    MechanicalAnimationDirection = 70,

    [NameEn("Action Animation Direction")]
    [NameCn("动作作画监督")]
    [NameJp("アクション作画監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    ActionAnimationDirection = 77,

    [NameEn("Director of Photography")]
    [NameCn("摄影监督")]
    [NameJp("撮影監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    PhotographyDirection = 17,

    [NameCn("道具作画监督")]
    [NameJp("プロップ作画監督 プロップ作監")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    PropDirection = 111,

    [NameEn("CG Director")]
    [NameCn("CG 导演")]
    [NameJp("CG 監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    CGDirector = 69,

    [NameEn("3DCG Director")]
    [NameCn("3DCG 导演")]
    [NameJp("3DCG 監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    CGDirector3D = 86,

    [NameEn("Technical Director")]
    [NameCn("技术导演")]
    [NameJp("テクニカルディレクター")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSupervision)]
    TechnicalDirector = 101,

    [NameCn("特技导演")]
    [NameJp("特撮監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSupervision)]
    TokusatsuDirector = 102,

    [NameCn("动作导演")]
    [NameJp("アクション監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSupervision)]
    ActionDirector = 137,

    [NameEn("Supervision/Supervisor")]
    [NameCn("监修")]
    [NameJp("シリーズ監修 スーパーバイザー")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSupervision)]
    Supervisor = 18,

    [NameEn("Key Animation")]
    [NameCn("原画")]
    [NameJp("作画 原画")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    [Folded]
    KeyAnimation = 20,

    [NameEn("2nd Key Animation")]
    [NameCn("第二原画")]
    [NameJp("原画協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    [Folded]
    SecondaryKeyAnimation = 21,

    [NameEn("Main Animator")]
    [NameCn("主动画师")]
    [NameJp("メインアニメーター")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    MainAnimator = 92,

    [NameEn("Animation Check")]
    [NameCn("动画检查")]
    [NameJp("動画チェック")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirection, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    [Folded]
    AnimationCheck = 22,

    [NameEn("Animation Work")]
    [NameCn("动画制作")]
    [NameJp("アニメーション制作 アニメ制作 アニメーション")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    AnimationWork = 67,

    [NameEn("OP ED")]
    [NameCn("OP・ED 分镜")]
    [NameJp("OP・ED 分鏡")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    OPED = 73,

    [NameEn("Photography")]
    [NameCn("摄影")]
    [NameJp("撮影")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    Photography = 82,

    [NameEn("Music Work")]
    [NameCn("音乐制作")]
    [NameJp("楽曲制作 音楽制作")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    MusicWork = 65,

    [NameEn("Music Producer")]
    [NameCn("音乐制作人")]
    [NameJp("音楽プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    AnimeMusicProducer = 85,

    [NameEn("Music Assistant")]
    [NameCn("音乐助理")]
    [NameJp("音楽アシスタント")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    [Folded]
    MusicAssistant = 55,

    [NameEn("Music Director")]
    [NameCn("音乐监督")]
    [NameJp("音楽ディレクター")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    MusicDirector = 116,

    [NameEn("Music Selection")]
    [NameCn("选曲")]
    [NameJp("選曲")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    MusicSelection = 117,

    [NameEn("Inserted Song Lyrics")]
    [NameCn("插入歌作词")]
    [NameJp("Insert Song Lyrics")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    InsertedSongLyrics = 118,

    [NameEn("Inserted Song Composition")]
    [NameCn("插入歌作曲")]
    [NameJp("Inserted Song Composition")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    InsertedSongComposition = 119,

    [NameEn("Inserted Song Arrangement")]
    [NameCn("插入歌编曲")]
    [NameJp("Inserted Song Arrangement")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    InsertedSongArrangement = 120,

    /// <summary>
    /// 制作助理 Associate Producer
    /// </summary>
    [NameEn("Associate Producer")]
    [NameCn("制作助理")]
    [NameJp("製作補佐 アソシエイトプロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    AssociateProducer = 24,

    [NameEn("Background Art")]
    [NameCn("背景美术")]
    [NameJp("背景")]
    [Categories<StaffCategory>(StaffCategory.AnimeArt, StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    BackgroundArt = 25,

    [NameEn("Color Setting")]
    [NameCn("色彩指定")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign, StaffCategory.AnimeColor)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    ColorSetting = 26,

    [NameCn("上色")]
    [NameJp("仕上")]
    [Categories<StaffCategory>(StaffCategory.AnimeColor)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    Coloring = 93,

    [NameCn("上色检查")]
    [NameJp("仕上検查")]
    [Categories<StaffCategory>(StaffCategory.AnimeColor)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    ColoringCheck = 94,

    [NameCn("色彩检查")]
    [NameJp("色検查")]
    [Categories<StaffCategory>(StaffCategory.AnimeColor)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    ColorCheck = 95,

    [NameEn("Color Script")]
    [NameCn("色彩脚本")]
    [NameJp("カラースクリプト")]
    [Categories<StaffCategory>(StaffCategory.AnimeColor)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    ColorScript = 103,

    [NameEn("Digital Paint")]
    [NameCn("数码绘图")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    [Folded]
    DigitalPaint = 27,

    [NameEn("3DCG")]
    [NameCn("3DCG")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    CG3D = 75,

    [NameEn("Production Manager")]
    [NameCn("制作管理")]
    [NameJp("制作マネージャー 制作担当 制作班長")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    ProductionManager = 37,

    [NameEn("Editing")]
    [NameCn("剪辑")]
    [NameJp("編集")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    Editing = 28,

    [NameEn("Original Plan")]
    [NameCn("原案")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSource)]
    OriginalPlan = 29,

    [NameEn("Theme Song Arrangement")]
    [NameCn("主题歌编曲")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    ThemeSongArrangement = 30,

    [NameEn("Theme Song Composition")]
    [NameCn("主题歌作曲")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    ThemeSongComposition = 31,

    [NameEn("Theme Song Lyrics")]
    [NameCn("主题歌作词")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    ThemeSongLyrics = 32,

    [NameEn("Theme Song Performance")]
    [NameCn("主题歌演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    ThemeSongPerformance = 33,

    [NameEn("Inserted Song Performance")]
    [NameCn("插入歌演出")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    InsertedSongPerformance = 34,

    [NameEn("Planning")]
    [NameCn("企画")]
    [NameJp("プランニング  企画開発")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    Planning = 35,

    [NameEn("Planning Producer")]
    [NameCn("企划制作人")]
    [NameJp("企画プロデューサー 企画営業プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    PlanningProducer = 36,

    [NameEn("Publicity")]
    [NameCn("宣传")]
    [NameJp("パブリシティ  宣伝  広告宣伝  番組宣伝  製作宣伝")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    [Folded]
    Publicity = 38,

    [NameEn("Recording")]
    [NameCn("录音")]
    [NameJp("録音")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    Recording = 39,

    [NameEn("Recording Assistant")]
    [NameCn("录音助理")]
    [NameJp("録音アシスタント  録音助手")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    [Folded]
    RecordingAssistant = 40,

    [NameEn("Series Production Director")]
    [NameCn("系列监督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeDirector)]
    SeriesProductionDirector = 41,

    [NameEn("Production")]
    [NameCn("製作")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    Production = 42,

    [NameEn("Production")]
    [NameCn("制作")]
    [NameJp("製作 製作スタジオ")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    ProductionStudio = 63,

    [NameEn("Setting")]
    [NameCn("设定")]
    [NameJp("設定")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    Setting = 43,

    [NameEn("Design Manager")]
    [NameCn("设定制作")]
    [NameJp("設定制作 制作設定")]
    [Description("有时需要额外的设计工作，联系负责部门并监督工作确保交付")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    DesignManager = 84,

    [NameEn("Sound Director")]
    [NameCn("音响监督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    SoundDirector = 44,

    [NameEn("Sound")]
    [NameCn("音响")]
    [NameJp("音響 音声")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    Sound = 45,

    [NameEn("Sound Effects")]
    [NameCn("音效")]
    [NameJp("音響効果")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    SoundEffects = 46,

    [NameEn("Special Effects Animation Direction")]
    [NameCn("特效作画监督")]
    [NameJp("エフェクト作画監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction, StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    SpecialEffectsAnimationDirection = 88,

    [NameEn("Special Effects")]
    [NameCn("特效")]
    [NameJp("特殊効果")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    SpecialEffects = 47,

    [NameEn("Visual Director")]
    [NameCn("视觉导演")]
    [NameJp("ビジュアルディレクター")]
    [Categories<StaffCategory>(StaffCategory.AnimeVisual, StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSupervision)]
    VisualDirector = 133,

    [NameEn("Visual Effects")]
    [NameCn("视觉效果")]
    [NameJp("ビジュアルエフェクト")]
    [Categories<StaffCategory>(StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    VisualEffects = 136,

    [NameEn("Creative Supervisor/Director")]
    [NameCn("创意总监")]
    [NameJp("クリエイティブスーパーバイザー クリエイティブディレクター")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSupervision)]
    CreativeSupervisor = 134,

    [NameEn("Tokusatsu Effects")]
    [NameCn("特摄效果")]
    [NameJp("特撮")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    TokusatsuEffects = 135,

    [NameEn("Eyecatch Art")]
    [NameCn("转场绘")]
    [NameJp("アイキャッチ")]
    [Categories<StaffCategory>(StaffCategory.AnimeAnimation, StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeEpisodeDirection)]
    EyecatchArt = 138,

    [NameEn("Illustration")]
    [NameCn("插画")]
    [NameJp("イラスト")]
    [Categories<StaffCategory>(StaffCategory.AnimeAnimation, StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    Illustration = 139,

    [NameEn("Character Animation Director")]
    [NameCn("角色作画监督")]
    [NameJp("キャラクター作画監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    CharacterAnimationDirector = 140,

    [NameEn("Animation Supervisor")]
    [NameCn("作画监修")]
    [NameJp("作画監修")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    AnimationSupervisor = 141,

    [NameEn("Mechanical Design Concept")]
    [NameCn("机设原案")]
    [NameJp("メカニカル原案")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    MechanicalDesignConcept = 142,

    [NameEn("Concept Art")]
    [NameCn("概念艺术")]
    [NameJp("コンセプトアート")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    ConceptArt = 143,

    [NameEn("Visual Concept")]
    [NameCn("视觉概念")]
    [NameJp("ビジュアルコンセプト")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign, StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSupervision)]
    VisualConcept = 144,

    [NameEn("Scene Design")]
    [NameCn("画面设计")]
    [NameJp("画面設計")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign, StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    SceneDesign = 145,

    [NameEn("Monster Design")]
    [NameCn("怪物设计")]
    [NameJp("モンスターデザイン")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    MonsterDesign = 146,

    [NameEn("Story Concept")]
    [NameCn("故事概念")]
    [NameJp("ストーリーコンセプト")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeScreenplay)]
    StoryConcept = 147,

    [NameEn("Scenario Coordinator")]
    [NameCn("剧本协调")]
    [NameJp("シナリオコーディネーター")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeScreenplay)]
    ScenarioCoordinator = 148,

    [NameEn("Script Cooperation")]
    [NameCn("脚本协力")]
    [NameJp("脚本協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeScreenplay)]
    [Folded]
    ScriptCooperation = 149,

    [NameEn("Associate Series Composition")]
    [NameCn("副系列构成")]
    [NameJp("副シリーズ構成")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeScreenplay)]
    AssociateSeriesComposition = 150,

    [NameEn("Series Composition Cooperation")]
    [NameCn("构成协力")]
    [NameJp("構成協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeScreenplay)]
    SeriesCompositionCooperation = 151,

    [NameEn("Recording Studio")]
    [NameCn("录音工作室")]
    [NameJp("録音スタジオ")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    RecordingStudio = 152,

    [NameEn("Sound Mixing")]
    [NameCn("整音")]
    [NameJp("整音")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    SoundMixing = 153,

    [NameEn("Sound Production Coordinator")]
    [NameCn("音响制作担当")]
    [NameJp("音響制作担当")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    SoundProductionCoordinator = 154,

    [NameEn("Online Editing")]
    [NameCn("在线剪辑")]
    [NameJp("オンライン編集")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    OnlineEditing = 155,

    [NameEn("Offline Editing")]
    [NameCn("离线剪辑")]
    [NameJp("オフライン編集")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    OfflineEditing = 156,

    [NameEn("3D Animator")]
    [NameCn("3D 动画师")]
    [NameJp("3Dアニメーター  3Dアニメーション")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    Animator3D = 157,

    [NameEn("CG Producer")]
    [NameCn("CG 制作人")]
    [NameJp("CGプロデューサー  CGIプロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    CGProducer = 158,

    [NameEn("Publicity Producer")]
    [NameCn("宣传制片人")]
    [NameJp("宣伝プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    PublicityProducer = 159,

    [NameEn("Art Producer")]
    [NameCn("美术制作人")]
    [NameJp("美術プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeArt)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    ArtProducer = 160,

    [NameEn("Sound Producer")]
    [NameCn("音响制作人")]
    [NameJp("音響プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeMusic)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    SoundProducer = 161,

    [NameEn("CG Production Coordinator")]
    [NameCn("CG 制作进行")]
    [NameJp("CG進行")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    CGProductionCoordinator = 162,

    [NameEn("Art Production Coordinator")]
    [NameCn("美术制作进行")]
    [NameJp("美術進行")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeArt, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    ArtProductionCoordinator = 163,

    [NameEn("Assistant Art Director")]
    [NameCn("美术监督助理")]
    [NameJp("美術監督補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant, StaffCategory.AnimeArt)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    AssistantArtDirector = 164,

    [NameEn("Assistant Color Designer")]
    [NameCn("色彩设计助理")]
    [NameJp("色彩設計補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant, StaffCategory.AnimeColor)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    AssistantColorDesigner = 165,

    [NameEn("Assistant Director of Photography")]
    [NameCn("摄影监督助理")]
    [NameJp("撮影監督補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    AssistantPhotographyDirector = 166,

    [NameEn("Assistant Production Desk")]
    [NameCn("制作管理助理")]
    [NameJp("制作デスク補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    AssistantProductionDesk = 167,

    [NameEn("Assistant Design Manager")]
    [NameCn("设定制作助理")]
    [NameJp("設定制作補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant, StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    AssistantDesignManager = 168,

    [NameEn("ADR Director")]
    [NameCn("配音监督")]
    [Categories<StaffCategory>(StaffCategory.AnimeMusic, StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    ADRDirector = 48,

    [NameEn("Co-Director")]
    [NameCn("联合导演")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeDirector)]
    CoDirector = 49,

    [NameEn("Setting")]
    [NameCn("背景设定")]
    [NameJp("基本設定  場面設定  場面設計  設定")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePreproduction)]
    BackgroundSetting = 50,

    [NameEn("In-Between Animation")]
    [NameCn("补间动画")]
    [NameJp("動画")]
    [Categories<StaffCategory>(StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.Animation)]
    [Folded]
    InBetweenAnimation = 51,

    [NameEn("Chief Producer")]
    [NameCn("总制片人")]
    [NameJp("チーフプロデューサー チーフ制作 総合プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    ChiefProducer = 58,

    [NameEn("Co-Producer")]
    [NameCn("联合制片人")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    CoProducer = 59,

    [NameEn("Producer")]
    [NameCn("制片人")]
    [NameJp("プロデュース  プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    Producer = 54,

    [NameEn("Executive Producer")]
    [NameCn("执行制片人")]
    [NameJp("製作総指揮")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    ExecutiveProducer = 52,

    [NameEn("Assistant Producer")]
    [NameCn("助理制片人")]
    [NameJp("協力プロデューサー  アシスタントプロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    [Folded]
    AssistantProducer = 53,

    [NameEn("Assistant Producer")]
    [NameCn("助理制片人")]
    [NameJp("協力プロデューサー（⚠️ 待合并）")]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    [Folded]
    AssistantProducerDeprecated = 23,

    [NameEn("Animation Producer")]
    [NameCn("动画制片人")]
    [NameJp("アニメプロデューサー  アニメーションプロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    AnimationProducer = 87,

    [NameEn("Creative Producer")]
    [NameCn("创意制片人")]
    [NameJp("クリエイティブプロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    CreativeProducer = 121,

    /// <summary>
    /// 副制片人 Associate Producer
    /// </summary>
    [NameEn("Associate Producer")]
    [NameCn("副制片人")]
    [NameJp("アソシエイトプロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    ViceProducer = 122,

    [NameEn("Chief Production Supervisor")]
    [NameCn("制作统括")]
    [NameJp("制作統括")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    ChiefProductionSupervisor = 123,

    [NameEn("Line Producer")]
    [NameCn("现场制片人")]
    [NameJp("ラインプロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    LineProducer = 124,

    [NameEn("Literary Producer")]
    [NameCn("文艺制作")]
    [NameJp("文芸制作")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    LiteraryProducer = 125,

    [NameEn("Planning Cooperation")]
    [NameCn("企画协力")]
    [NameJp("企画協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    [Folded]
    PlanningCooperation = 127,

    [NameEn("Supervising Producer")]
    [NameCn("监制")]
    [NameJp("プロデュース")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    SupervisingProducer = 80,

    [NameEn("Assistant Production Manager")]
    [NameCn("制作进行")]
    [NameJp("制作進行")]
    [Description("管理动画的制作时程、协调各部门作业、回收作画原稿等")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    AssistantProductionManager = 56,

    [NameEn("Assistant Production Manager Assistance")]
    [NameCn("制作进行协力")]
    [NameJp("制作進行協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    AssistantProductionManagerAssistance = 83,

    [NameEn("Casting Director")]
    [NameCn("演员监督")]
    [NameJp("キャスティングコーディネーター監督")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeDirector)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSound)]
    CastingDirector = 57,

    [NameEn("Dialogue Editing")]
    [NameCn("台词编辑")]
    [NameJp("台詞編集")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    DialogueEditing = 60,

    [NameEn("Post-Production Assistant")]
    [NameCn("后期制片协调")]
    [NameJp("ポストプロダクション協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    [Folded]
    PostProductionAssistant = 61,

    [NameEn("Production Assistant")]
    [NameCn("制作助理")]
    [NameJp("制作アシスタント 制作補佐 製作補")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    ProductionAssistant = 62,

    [NameEn("Production Coordination")]
    [NameCn("制作协调")]
    [NameJp("制作コーディネーター")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    ProductionCoordination = 64,

    [NameEn("Work Assistance")]
    [NameCn("制作协力")]
    [NameJp("制作協力 / 作品協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    WorkAssistance = 76,

    [NameEn("Special Thanks")]
    [NameCn("特别鸣谢")]
    [NameJp("友情協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeCooperation)]
    SpecialThanks = 66,

    [NameEn("Assistance")]
    [NameCn("协力")]
    [NameJp("協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeCooperation)]
    Assistance = 81,

    [NameEn("Assistant Editor")]
    [NameCn("剪辑助理")]
    [NameJp("編集助手")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    AssistantEditor = 169,

    [NameEn("Publicity Cooperation")]
    [NameCn("宣传协力")]
    [NameJp("宣伝協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    [Folded]
    PublicityCooperation = 170,

    [NameEn("Modeling")]
    [NameCn("建模/模型")]
    [NameJp("モデリング モデラー CGモデラー")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    Modeling = 171,

    [NameEn("Look Development")]
    [NameCn("视觉开发/外观开发")]
    [NameJp("ルックデヴ ルックデベロップメント")]
    [Categories<StaffCategory>(StaffCategory.AnimeDesign, StaffCategory.AnimeProduction, StaffCategory.AnimeVisual)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    LookDevelopment = 172,

    [NameEn("Animation Director")]
    [NameCn("动画导演")]
    [NameJp("アニメーションディレクター")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeAnimation)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeDirector)]
    AnimeAnimationDirector = 173,

    [NameEn("Distribution")]
    [NameCn("发行")]
    [NameJp("配給")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    Distribution = 174,

    [NameEn("Original Work Cooperation")]
    [NameCn("原作协力")]
    [NameJp("原作協力 原案協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeStoryboard, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeSource)]
    [Folded]
    OriginalWorkCooperation = 175,

    [NameEn("Production Cooperation")]
    [NameCn("製作协力")]
    [NameJp("製作協力")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    [Folded]
    ProductionCooperation = 176,

    [NameEn("Production Supervisor")]
    [NameCn("製作统括")]
    [NameJp("製作統括")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeProducers)]
    ProductionSupervisor = 177,

    [NameEn("Production Administration")]
    [NameCn("制作事务")]
    [NameJp("制作事務")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    [Folded]
    ProductionAdministration = 178,

    [NameEn("OP・ED Animation Production")]
    [NameCn("OP・ED 动画制作")]
    [NameJp("OP・ED アニメーション制作")]
    [Categories<StaffCategory>(StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeMusic)]
    [Folded]
    OPEDAnimationProduction = 179,

    [NameEn("Assistant 3DCG Director")]
    [NameCn("3DCG 导演助理")]
    [NameJp("3DCG 監督補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    Assistant3DCGDirector = 180,

    [NameEn("Assistant CG Director")]
    [NameCn("CG 导演助理")]
    [NameJp("CG 監督補佐")]
    [Categories<StaffCategory>(StaffCategory.AnimeDirector, StaffCategory.AnimeProduction, StaffCategory.AnimeAssistant)]
    [Categories<PresentationGroup>(PresentationGroup.AnimePostproduction)]
    [Folded]
    AssistantCGDirector = 181,

    [NameEn("Production Producer")]
    [NameCn("制作制片人")]
    [NameJp("制作プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.AnimeProducer, StaffCategory.AnimeProduction)]
    [Categories<PresentationGroup>(PresentationGroup.AnimeAnimationProduction)]
    ProductionProducer = 182,

    #endregion
    #region Game

    [NameEn("Developer")]
    [NameCn("开发")]
    [NameJp("開発元")]
    [Categories<StaffCategory>(StaffCategory.GameProducer)]
    Developer = 1001,

    [NameEn("Publisher")]
    [NameCn("发行")]
    [NameJp("発売元")]
    [Categories<StaffCategory>(StaffCategory.GameProducer)]
    Publisher = 1002,

    [NameEn("Game Designer")]
    [NameCn("游戏设计师")]
    [NameJp("ゲームクリエイター")]
    [Categories<StaffCategory>(StaffCategory.GameDirector)]
    GameDesigner = 1003,

    [NameCn("原作")]
    [Categories<StaffCategory>(StaffCategory.GameDesign)]
    GameOriginalWork = 1015,

    [NameEn("Director/Direction")]
    [NameCn("导演")]
    [NameJp("監督 演出 シリーズ監督")]
    [Categories<StaffCategory>(StaffCategory.GameDirector)]
    GameDirector = 1016,

    [NameEn("Producer")]
    [NameCn("制作人")]
    [NameJp("プロデューサー")]
    [Categories<StaffCategory>(StaffCategory.GameDirector)]
    GameProducer = 1032,

    [NameCn("企画")]
    [Categories<StaffCategory>(StaffCategory.GameProduction)]
    GamePlanning = 1028,

    [NameCn("监修")]
    [NameJp("監修")]
    [Categories<StaffCategory>(StaffCategory.GameDirector)]
    GameSupervisor = 1026,

    [NameCn("剧本")]
    [NameJp("腳本")]
    [Categories<StaffCategory>(StaffCategory.GameStoryboard)]
    GameStoryboard = 1004,

    [NameCn("系列构成")]
    [NameJp("シリーズ構成")]
    [Categories<StaffCategory>(StaffCategory.GameStoryboard)]
    GameSeriesComposition = 1027,

    /// <summary>
    /// 作画监督
    /// </summary>
    [NameCn("作画监督")]
    [NameJp("作画監督")]
    [Categories<StaffCategory>(StaffCategory.GameArt)]
    GameAnimationDirector = 1031,

    [NameCn("原画")]
    [Categories<StaffCategory>(StaffCategory.GameArt)]
    GameKeyAnimation = 1013,

    [NameEn("Character Design")]
    [NameCn("人物设定")]
    [NameJp("キャラ設定 キャラクターデザイン")]
    [Categories<StaffCategory>(StaffCategory.GameDesign)]
    GameCharacterDesign = 1008,

    [NameEn("Mechanical Design")]
    [NameCn("机械设定")]
    [NameJp("メカニック設定")]
    [Categories<StaffCategory>(StaffCategory.GameDesign)]
    GameMechanicalDesign = 1029,

    [NameCn("美工")]
    [NameJp("美術")]
    [Categories<StaffCategory>(StaffCategory.GameArt)]
    GameArt = 1005,

    [NameCn("CG 监修")]
    [NameJp("CG 監修")]
    [Categories<StaffCategory>(StaffCategory.GameArt)]
    GameCGSupervisor = 1023,

    [NameCn("SD原画")]
    [Categories<StaffCategory>(StaffCategory.GameArt)]
    GameSDKeyAnimation = 1024,

    [NameCn("背景")]
    [Categories<StaffCategory>(StaffCategory.GameArt)]
    GameBackgroundArt = 1025,

    [NameEn("Cover Art")]
    [NameCn("背景")]
    [NameJp("表紙")]
    [Categories<StaffCategory>(StaffCategory.GameArt)]
    GameCoverArt = 1033,

    [NameEn("Sound Director")]
    [NameCn("音响监督")]
    [Categories<StaffCategory>(StaffCategory.GameMusic)]
    GameSoundDirector = 1030,

    [NameCn("音乐")]
    [NameJp("音楽")]
    [Categories<StaffCategory>(StaffCategory.GameMusic)]
    GameMusic = 1006,

    [NameEn("Program")]
    [NameCn("程序")]
    [NameJp("プログラム")]
    [Categories<StaffCategory>(StaffCategory.GameProduction)]
    GameProgram = 1021,

    [NameEn("Animation Work")]
    [NameCn("动画制作")]
    [NameJp("アニメーション制作 アニメ制作 アニメーション")]
    [Categories<StaffCategory>(StaffCategory.GameAnimation)]
    GameAnimationWork = 1014,

    /// <summary>
    /// 动画监督
    /// </summary>
    [NameCn("动画监督")]
    [NameJp("アニメーション監督")]
    [Categories<StaffCategory>(StaffCategory.GameAnimation)]
    GameAnimationDirection = 1017,

    [NameCn("动画剧本")]
    [NameJp("アニメーション脚本")]
    [Categories<StaffCategory>(StaffCategory.GameAnimation)]
    GameAnimationScript = 1020,

    [NameCn("制作总指挥")]
    [Categories<StaffCategory>(StaffCategory.GameDirector)]
    GameExecutiveProducer = 1018,

    [NameEn("QC")]
    [NameCn("QC")]
    [Categories<StaffCategory>(StaffCategory.GameProduction)]
    GameQC = 1019,

    [NameCn("关卡设计")]
    [Categories<StaffCategory>(StaffCategory.GameDesign)]
    GameStageDesign = 1007,

    [NameEn("Theme Song Composition")]
    [NameCn("主题歌作曲")]
    [Categories<StaffCategory>(StaffCategory.GameMusic)]
    GameThemeSongComposition = 1009,

    [NameEn("Theme Song Lyrics")]
    [NameCn("主题歌作词")]
    [Categories<StaffCategory>(StaffCategory.GameMusic)]
    GameThemeSongLyrics = 1010,

    [NameEn("Theme Song Performance")]
    [NameCn("主题歌演出")]
    [Categories<StaffCategory>(StaffCategory.GameMusic)]
    GameThemeSongPerformance = 1011,

    [NameEn("Inserted Song Performance")]
    [NameCn("插入歌演出")]
    [Categories<StaffCategory>(StaffCategory.GameMusic)]
    GameInsertedSongPerformance = 1012,

    [NameCn("协力")]
    [NameJp("協力")]
    [Categories<StaffCategory>(StaffCategory.GameAssistant)]
    GameAssistance = 1022,

    [NameEn("User Interface")]
    [NameCn("UI")]
    [NameJp("UI")]
    [Categories<StaffCategory>(StaffCategory.GameDesign)]
    GameUserInterface = 1034,

    [NameEn("Voice Director")]
    [NameCn("配音导演")]
    [Categories<StaffCategory>(StaffCategory.GameDirector, StaffCategory.GameMusic)]
    GameVoiceDirector = 1035,

    #endregion
    #region Book

    [NameEn("Original Creator/Original Work")]
    [NameCn("原作")]
    BookOriginalWork = 2007,

    [NameCn("作者")]
    BookAuthor = 2001,

    [NameCn("作画")]
    BookDrawing = 2002,

    [NameCn("插图")]
    [NameJp("イラスト")]
    BookIllustration = 2003,

    [NameCn("脚本")]
    [NameJp("シナリオ")]
    BookScenario = 2010,

    [NameCn("出版社")]
    BookPublisher = 2004,

    [NameCn("连载杂志")]
    [NameJp("掲載誌")]
    BookMagazine = 2005,

    [NameCn("译者")]
    BookTranslator = 2006,

    [NameEn("Guest")]
    [NameCn("客串")]
    [NameJp("ゲスト")]
    BookGuest = 2008,

    [NameEn("Original Character Design")]
    [NameCn("人物原案")]
    [NameJp("キャラクター原案")]
    BookOriginalCharacterDesign = 2009,

    [NameEn("Label")]
    [NameCn("书系")]
    BookLabel = 2011,

    [NameCn("出品方")]
    BookProducer = 2012,

    [NameEn("Brand")]
    [NameCn("图书品牌")]
    [NameJp("ブランド")]
    BookBrand = 2013,

    [NameEn("Compilation")]
    [NameCn("编著")]
    [NameJp("編纂 編")]
    BookCompilation = 2014,

    [NameEn("Composition")]
    [NameCn("构成")]
    [NameJp("構成")]
    BookComposition = 2015,

    [NameEn("Supervision")]
    [NameCn("监修")]
    [NameJp("監修")]
    BookSupervision = 2016,

    [NameEn("Commentary")]
    [NameCn("解说")]
    [NameJp("解説")]
    BookCommentary = 2017,

    #endregion
    #region Music

    [NameEn("Artist")]
    [NameCn("艺术家")]
    MusicArtist = 3001,

    [NameEn("Producer")]
    [NameCn("制作人")]
    MusicProducer = 3002,

    [NameEn("Label")]
    [NameCn("厂牌")]
    [NameJp("レーベル")]
    MusicLabel = 3004,

    [NameEn("Composer")]
    [NameCn("作曲")]
    MusicComposer = 3003,

    [NameEn("Lyric")]
    [NameCn("作词")]
    MusicLyric = 3006,

    [NameEn("Arrange")]
    [NameCn("编曲")]
    MusicArrange = 3008,

    [NameEn("Instrument")]
    [NameCn("乐器")]
    MusicInstrument = 3014,

    [NameEn("Vocal")]
    [NameCn("声乐")]
    [NameJp("ボーカル")]
    MusicVocal = 3015,

    [NameEn("Featured")]
    [NameCn("客串")]
    MusicFeatured = 3019,

    [NameEn("Chorus")]
    [NameCn("和声")]
    [NameJp("コーラス")]
    MusicChorus = 3022,

    [NameEn("Choir")]
    [NameCn("合唱")]
    MusicChoir = 3021,

    [NameEn("Narration")]
    [NameCn("念白/旁白")]
    [NameJp("ナレーション")]
    MusicNarration = 3016,

    [NameEn("Recording")]
    [NameCn("录音")]
    MusicRecording = 3007,

    [NameEn("Mixing")]
    [NameCn("混音")]
    MusicMixing = 3013,

    [NameEn("Mastering")]
    [NameCn("母带制作")]
    MusicMastering = 3012,

    [NameEn("Vocal Edit")]
    [NameCn("人声编辑")]
    [NameJp("ボーカルエディット")]
    MusicVocalEdit = 3017,

    [NameEn("Vocal Direction")]
    [NameCn("人声指导")]
    [NameJp("ボーカルディレクション")]
    MusicVocalDirection = 3018,

    [NameEn("Sound Director")]
    [NameCn("音响监督")]
    [NameJp("サウンドディレクション")]
    MusicSoundDirector = 3020,

    [NameEn("Illustrator")]
    [NameCn("插图")]
    MusicIllustrator = 3009,

    [NameEn("Original Creator/Original Work")]
    [NameCn("原作")]
    MusicOriginalWork = 3005,

    [NameEn("Scenario")]
    [NameCn("脚本")]
    [NameJp("シナリオ")]
    MusicScenario = 3010,

    [NameEn("O.P.")]
    [NameCn("出版方")]
    [NameJp("音楽出版社")]
    MusicOP = 3011,

    [NameEn("Design")]
    [NameCn("设计")]
    [NameJp("デザイン")]
    MusicDesign = 3023,

    #endregion
    #region Real

    [NameEn("Creator")]
    [NameCn("原作")]
    RealCreator = 4001,

    [NameEn("Director")]
    [NameCn("导演")]
    RealDirector = 4002,

    [NameEn("Writer")]
    [NameCn("编剧")]
    RealWriter = 4003,

    [NameEn("Actor")]
    [NameCn("主演")]
    RealActor = 4016,

    [NameEn("Supporting Actor")]
    [NameCn("配角")]
    RealSupportingActor = 4017,

    [NameEn("Creative Director")]
    [NameCn("创意总监")]
    RealCreativeDirector = 4013,

    [NameEn("Composer")]
    [NameCn("音乐")]
    RealComposer = 4004,

    [NameEn("Executive Producer")]
    [NameCn("执行制片人")]
    [NameJp("製作総指揮")]
    RealExecutiveProducer = 4005,

    [NameEn("Co-Executive Producer")]
    [NameCn("共同执行制作")]
    RealCoExecutiveProducer = 4006,

    [NameEn("Producer")]
    [NameCn("制片人/制作人")]
    [NameJp("プロデューサー")]
    RealProducer = 4007,

    [NameEn("Supervising Producer")]
    [NameCn("监制")]
    RealSupervisingProducer = 4008,

    [NameEn("Consulting Producer")]
    [NameCn("副制作人/制作顾问")]
    RealConsultingProducer = 4009,

    [NameEn("Story")]
    [NameCn("故事")]
    RealStory = 4010,

    [NameEn("Story Editor")]
    [NameCn("编审")]
    RealStoryEditor = 4011,

    [NameEn("Editor")]
    [NameCn("剪辑")]
    RealEditor = 4012,

    [NameEn("Cinematography")]
    [NameCn("摄影")]
    RealCinematography = 4014,

    [NameEn("ADR Director")]
    [NameCn("配音导演")]
    RealADRDirector = 4020,

    [NameEn("Recording")]
    [NameCn("录音")]
    [NameJp("録音")]
    RealRecording = 4021,

    [NameEn("Theme Song Performance")]
    [NameCn("主题歌演出")]
    RealThemeSongPerformance = 4015,

    [NameEn("Poster")]
    [NameCn("海报")]
    RealPoster = 4022,

    [NameEn("Production")]
    [NameCn("制作")]
    [NameJp("製作 製作スタジオ")]
    RealProduction = 4018,

    [NameEn("Present")]
    [NameCn("出品")]
    RealPresent = 4019,

    [NameEn("Planning")]
    [NameCn("企划/策划")]
    [NameJp("企画")]
    RealPlanning = 4023,

    [NameEn("Assistant Director")]
    [NameCn("副导演")]
    [NameJp("助監督")]
    RealAssistantDirector = 4024,

    [NameEn("Art")]
    [NameCn("美术")]
    [NameJp("美術")]
    RealArt = 4025,

    [NameEn("Associate Producer")]
    [NameCn("联合制片人/副制片人")]
    [NameJp("アソシエイトプロデューサー")]
    RealAssociateProducer = 4026,

    [NameEn("Action Director")]
    [NameCn("动作导演/武术指导")]
    [NameJp("アクション監督")]
    RealActionDirector = 4027,

    [NameEn("Stunt Coordinator")]
    [NameCn("特技指导")]
    RealStuntCoordinator = 4028,

    [NameEn("Tokusatsu Director")]
    [NameCn("特摄导演")]
    [NameJp("特撮監督")]
    RealTokusatsuDirector = 4029,

    [NameEn("Special Effects")]
    [NameCn("实拍特效")]
    [NameJp("SFX")]
    RealSpecialEffects = 4030,

    [NameEn("Visual Effects")]
    [NameCn("视觉特效")]
    [NameJp("VFX")]
    RealVisualEffects = 4031,

    [NameEn("3DCG")]
    [NameCn("3DCG")]
    [NameJp("3DCG")]
    Real3DCG = 4032,

    [NameEn("Setting")]
    [NameCn("设定")]
    [NameJp("設定 デザインワークス")]
    RealSetting = 4033,

    [NameEn("Character Design")]
    [NameCn("人物设计/皮套设计")]
    [NameJp("キャラクターデザイン")]
    RealCharacterDesign = 4034,

    [NameEn("Character Modeling")]
    [NameCn("造型设计")]
    [NameJp("キャラクター造型製作")]
    RealCharacterModeling = 4035,

    [NameEn("Creature Design")]
    [NameCn("生物设计")]
    [NameJp("クリーチャーデザイン")]
    RealCreatureDesign = 4036,

    [NameEn("Monster Design")]
    [NameCn("怪兽设计")]
    [NameJp("怪獣デザイン")]
    RealMonsterDesign = 4037,

    [NameEn("Production Company")]
    [NameCn("承制方/摄制")]
    [NameJp("制作プロダクション")]
    RealProductionCompany = 4038,

    [NameEn("Publicity")]
    [NameCn("宣传")]
    [NameJp("宣伝")]
    RealPublicity = 4039,

    [NameEn("Assistance")]
    [NameCn("协力")]
    [NameJp("協力")]
    RealAssistance = 4040,

    [NameEn("Special Thanks")]
    [NameCn("特别鸣谢")]
    RealSpecialThanks = 4041,

    [NameEn("Sound Director")]
    [NameCn("音响监督")]
    RealSoundDirector = 4042,

    [NameEn("Post-Production Editor")]
    [NameCn("后期")]
    RealPostProductionEditor = 4043,

    [NameEn("Distribution")]
    [NameCn("发行")]
    [NameJp("配給")]
    RealDistribution = 4044,

    #endregion
}
