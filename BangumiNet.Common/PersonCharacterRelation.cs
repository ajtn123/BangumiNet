using BangumiNet.Common.Attributes;
using System.ComponentModel;

namespace BangumiNet.Common;

public enum PersonCharacterRelationCategory { Person, Character, PersonCharacter }

public enum PersonCharacterRelationType
{
    // Person relations

    [NameCn("家人")]
    Family = 1001,

    [NameCn("配偶")]
    Spouse = 1002,

    [NameCn("离异")]
    Divorced = 1003,

    [NameCn("创始人")]
    [Description("组织或公司的创始人")]
    Founder = 1004,

    [NameCn("员工")]
    [Description("组织或公司的员工")]
    Employee = 1005,

    [NameCn("成员")]
    [Description("偶像组合或特别企划的成员")]
    Member = 1006,

    [NameCn("原成员")]
    [Description("偶像组合、特别企划已离开或毕业的成员")]
    FormerMember = 1011,

    //[NameCn("中之人")]
    //[Description("VTuber 的扮演者或声优")]
    //VActor = 1007,

    [NameCn("老师")]
    [ViceVersa<PersonCharacterRelationType>(Student)]
    Teacher = 1013,

    [NameCn("学生")]
    [Description("与老师有师徒关系的学生、助手、助理等")]
    [ViceVersa<PersonCharacterRelationType>(Teacher)]
    Student = 1014,

    [NameCn("同事")]
    Colleague = 1015,

    [NameCn("主体事业")]
    [Description("从属事业的主体公司或组织")]
    [ViceVersa<PersonCharacterRelationType>(Subsidiary)]
    ParentCompany = 1009,

    [NameCn("从属事业")]
    [Description("主体事业下属的公司或组织")]
    [ViceVersa<PersonCharacterRelationType>(ParentCompany)]
    Subsidiary = 1008,

    [NameCn("品牌")]
    [Description("公司下属的品牌")]
    [ViceVersa<PersonCharacterRelationType>(OperatingCompany)]
    Brand = 1010,

    [NameCn("运营")]
    [Description("品牌的运营公司或组织")]
    [ViceVersa<PersonCharacterRelationType>(Brand)]
    OperatingCompany = 1016,

    [NameCn("团体")]
    [Description("公司下属的团体、企划，此项目用于与公司、组织关联")]
    [ViceVersa<PersonCharacterRelationType>(Agency)]
    Group = 1012,

    [NameCn("经纪公司")]
    [Description("团体的经纪公司，如厂牌、经纪人公司、事务所等")]
    [ViceVersa<PersonCharacterRelationType>(Group)]
    Agency = 1017,

    // Person - character relations

    [NameCn("CV")]
    [Description("作品首发时角色的原声")]
    [Primary]
    CV = 0,

    [NameCn("演员")]
    [Description("三次元真人出演角色的演员")]
    [Primary]
    Actor = 2,

    [NameCn("译配")]
    [Description("译制配音，作品在其他地区发行时，角色的配音演员")]
    Dubbing = 1,

    [NameCn("中配")]
    ChineseDub = 3,

    [NameCn("日配")]
    JapaneseDub = 4,

    [NameCn("英配")]
    EnglishDub = 5,

    [NameCn("韩配")]
    KoreanDub = 6,

    // Character relations

    [NameCn("形态")]
    [Description("不同的外观形态（如幼年，成年），所有外貌应关联至同一个主条目，角色名后可标注形态类型")]
    Form = 2001,

    [NameCn("改编")]
    [Description("角色的改编版本，如影视作品中的角色")]
    [ViceVersa<PersonCharacterRelationType>(Prototype)]
    Adaptation = 2020,

    [NameCn("原型")]
    [Description("角色的原型或灵感来源，如历史人物、神话传说等")]
    [ViceVersa<PersonCharacterRelationType>(Adaptation)]
    Prototype = 2021,

    [NameCn("朋友")]
    Friend = 2002,

    [NameCn("亲属")]
    [Description("父母子女、祖孙等直系亲属及其兄弟姐妹关系。涵盖生物学亲属、法律收养亲属以及再婚家庭形成的亲属关系")]
    Relative = 2006,

    [NameCn("亲戚")]
    [Description("除亲属外的其他亲戚")]
    DistantRelative = 2024,

    [NameCn("恋人")]
    [Description("双方都已确认恋爱关系")]
    Lover = 2003,

    [NameCn("配偶")]
    SpouseC = 2017,

    [NameCn("对手")]
    Rival = 2005,

    [NameCn("创始人")]
    [Description("组织、偶像组合等团体的创始人")]
    FounderC = 2008,

    [NameCn("成员")]
    [Description("组织、偶像组合等团体的成员")]
    MemberC = 2007,

    [NameCn("驾驶员")]
    [Description("机体的驾驶员，如高达系列、EVA 系列等")]
    Pilot = 2015,

    [NameCn("进化")]
    [Description("角色进化或升级后的形态")]
    [ViceVersa<PersonCharacterRelationType>(PreEvolution)]
    Evolution = 2027,

    [NameCn("前身")]
    [Description("角色进化或升级前的形态")]
    [ViceVersa<PersonCharacterRelationType>(Evolution)]
    PreEvolution = 2028,

    [NameCn("主人")]
    [Description("侍从的主人")]
    [ViceVersa<PersonCharacterRelationType>(Servant)]
    Master = 2009,

    [NameCn("侍从")]
    [Description("主人的侍从，如女仆、管家等")]
    [ViceVersa<PersonCharacterRelationType>(Master)]
    Servant = 2016,

    [NameCn("饲主")]
    [Description("宠物的主人")]
    [ViceVersa<PersonCharacterRelationType>(Pet)]
    Owner = 2029,

    [NameCn("宠物")]
    [Description("被主人所驯养的动物，如宝可梦、数码宝贝等")]
    [ViceVersa<PersonCharacterRelationType>(Owner)]
    Pet = 2010,

    [NameCn("老师")]
    [ViceVersa<PersonCharacterRelationType>(TeacherC)]
    TeacherC = 2012,

    [NameCn("学生")]
    [ViceVersa<PersonCharacterRelationType>(StudentC)]
    StudentC = 2011,

    [NameCn("上司")]
    [ViceVersa<PersonCharacterRelationType>(Subordinate)]
    Superior = 2019,

    [NameCn("下属")]
    [ViceVersa<PersonCharacterRelationType>(Superior)]
    Subordinate = 2018,

    [NameCn("同学")]
    Classmate = 2025,

    [NameCn("同事")]
    ColleagueC = 2023,

    [NameCn("同门")]
    FellowDisciple = 2026,

    [NameCn("单恋")]
    [SkipViceVersa]
    UnrequitedLove = 2004,

    //[NameCn("暧昧")]
    //AmbiguousLove = 2013,

    [NameCn("前任")]
    Ex = 2022,

    //[NameCn("离异")]
    //DivorcedC = 2014,

    [NameCn("其他")]
    Other = 2099,
}
