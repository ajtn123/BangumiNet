namespace BangumiNet.Common;

public static class WikiTemplates
{
    public const string TVAnime = """
        {{Infobox animanga/TVAnime
        |中文名=
        |别名= {

        }
        |话数= *
        |放送开始= *
        |放送星期=
        |官方网站=
        |在线播放平台=
        |播放电视台=
        |其他电视台=
        |播放结束=
        |导演=
        |音乐=
        |链接= {

        }
        |其他=
        |Copyright=
        }}
        """;
    public const string OVA = """
        {{Infobox animanga/OVA
        |中文名=
        |别名= {

        }
        |话数= *
        |发售日= *
        |官方网站=
        |开始=
        |结束=
        |链接= {

        }
        |其他=
        }}
        """;
    public const string Movie = """
        {{Infobox animanga/Movie
        |中文名=
        |别名= {

        }
        |上映年度= *
        |片长=
        |官方网站=
        |链接= {

        }
        |其他=
        |Copyright=
        }}
        """;
    public const string Anime = """
        {{Infobox animanga/Anime
        |中文名=
        |别名= {

        }
        |上映年度= *
        |片长=
        |官方网站=
        |链接= {

        }
        |其他=
        |Copyright=
        }}
        """;
    public const string Book = """
        {{Infobox animanga/Book
        |中文名=
        |别名= {

        }
        |作者=
        |插图=
        |出版社=
        |价格=
        |其他出版社=
        |连载杂志=
        |发售日=
        |页数=
        |ISBN=
        |链接= {

        }
        |其他=
        }}
        """;
    public const string PhotoBook = """
        {{Infobox Book/PhotoBook
        |中文名=
        |别名= {

        }
        |作者=
        |摄影=
        |出版社=
        |价格=
        |其他出版社=
        |连载杂志=
        |发售日=
        |页数=
        |ISBN=
        |链接= {

        }
        |其他=
        }}
        """;
    public const string Manga = """
        {{Infobox animanga/Manga
        |中文名=
        |别名= {

        }
        |作者=
        |作画=
        |脚本=
        |原作=
        |出版社= *
        |价格=
        |其他出版社=
        |连载杂志=
        |发售日=
        |册数=
        |页数=
        |话数=
        |ISBN=
        |链接= {

        }
        |其他=
        }}
        """;
    public const string Novel = """
        {{Infobox animanga/Novel
        |中文名=
        |别名= {

        }
        |作者=
        |插图=
        |出版社= *
        |价格=
        |连载杂志=
        |发售日=
        |册数=
        |页数=
        |话数=
        |ISBN=
        |链接= {

        }
        |其他=
        }}
        """;
    public const string BookSeries = """
        {{Infobox animanga/BookSeries
        |中文名=
        |别名= {

        }
        |出版社= *
        |连载杂志=
        |开始=
        |结束=
        |册数=
        |话数=
        |原作=
        |链接= {

        }
        |其他=
        }}
        """;
    public const string Album = """
        {{Infobox Album
        |中文名=
        |别名= {

        }
        |艺术家=
        |作曲=
        |编曲=
        |作词=
        |厂牌=
        |发售日期=
        |价格=
        |版本特性=
        |播放时长=
        |录音=
        |碟片数量=
        |链接= {

        }
        }}
        """;
    public const string Game = """
        {{Infobox Game
        |中文名=
        |别名= {

        }
        |平台= {

        }
        |游戏类型=
        |游戏引擎=
        |游玩人数=
        |发行日期=
        |售价=
        |开发=
        |发行=
        |剧本=
        |程序=
        |website=
        |链接= {

        }
        }}
        """;
    public const string TV = """
        {{Infobox real/Television
        |中文名=
        |别名= {

        }
        |集数=
        |季数=
        |放送星期=
        |开始=
        |结束=
        |主演=
        |导演=
        |音乐=
        |原作=
        |制作=
        |类型=
        |国家/地区=
        |语言=
        |每集长=
        |在线播放平台=
        |电视网=
        |电视台=
        |频道=
        |视频制式=
        |音频制式=
        |首播国家=
        |首播地区=
        |台湾名称=
        |港澳名称=
        |马新名称=
        |官方网站=
        |链接= {

        }
        |imdb_id=
        |tvdb_id=
        }}
        """;
    public const string RealMovie = """
        {{Infobox real/Movie
        |中文名=
        |别名= {

        }
        |上映日=
        |片长=
        |类型=
        |国家/地区=
        |语言=
        |官方网站=
        |链接= {

        }
        |imdb_id=
        |tmdb_id=
        |tvdb_id=
        }}
        """;
    public const string Crt = """
        {{Infobox Crt
        |简体中文名=
        |别名={
        [第二中文名|]
        [英文名|]
        [日文名|]
        [纯假名|]
        [罗马字|]
        [昵称|]
        }
        |性别=
        |生日=
        |血型=
        |身高=
        |体重=
        |BWH=
        |引用来源={
        }
        }}
        """;
    public const string DoujinBook = """
        {{Infobox doujin/Book
        |作者={

        }
        |原作=
        |CP=
        |语言=
        |页数=
        |尺寸=
        |价格=
        |发售日=
        }}
        """;
    public const string DoujinMusic = """
        {{Infobox doujin/Album
        |艺术家={

        }
        |原作=
        |语言=
        |版本特性=
        |碟片数量=
        |播放时长=
        |价格=
        |发售日=
        }}
        """;
    public const string DoujinGame = """
        {{Infobox doujin/Game
        |别名= {

        }
        |开发者={

        }
        |原作=
        |平台=
        |游戏类型=
        |游戏引擎=
        |游玩人数=
        |语言=
        |价格=
        |发售日=
        }
        """;
}
