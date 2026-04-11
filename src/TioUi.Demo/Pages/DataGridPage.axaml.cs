using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Collections;

namespace TioUi.Demo.Pages;

public partial class DataGridPage : PageModelBase
{
    public DataGridPage()
    {
        InitializeComponent();
        GridData1 = new ObservableCollection<Song>(Song.Songs);
        GridData2 = new DataGridCollectionView(Song.Songs);
        GridData2.GroupDescriptions.Add(new DataGridPathGroupDescription("Album"));
        GridData3 = new ObservableCollection<SongViewModel>(Song.Songs.Take(10).Select(a => new SongViewModel()
        {
            Title = a.Title,
            Artist = a.Artist,
            Album = a.Album,
            CountOfComment = a.CountOfComment,
            IsSelected = false
        }));
        AddCommand = new ActionCommand(Add);
        DataContext = this;
    }

    public ObservableCollection<Song> GridData1 { get; set; }

    public DataGridCollectionView GridData2 { get; set; }

    public ObservableCollection<SongViewModel> GridData3 { get; set; }

    public ActionCommand AddCommand { get; set; }

    public void Add()
    {
        GridData3.Add(new SongViewModel());
    }
}

public class Song
{
    public Song(string title, string artist, int m, int s, string album, int countOfComment, int netEaseId)
    {
        Title = title;
        Artist = artist;
        Duration = new TimeSpan(0, m, s);
        Album = album;
        CountOfComment = countOfComment;
        Url = $"https://music.163.com/song?id={netEaseId}";
    }

    public string? Title { get; set; }
    public string? Artist { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? Album { get; set; }
    public int CountOfComment { get; set; }
    public string Url { get; set; }

    public static List<string> Albums =>
    [
        "天龙八部之宿敌",
        "素颜",
        "不如吃茶去",
        "自定义",
        "许嵩单曲集",
        "梦游计",
        "寻雾启示",
        "2023-2024湖南卫视芒果TV跨年晚会现场",
        "我的音乐你听吗 第1期",
        "半城烟沙",
        "如果当时2020",
        "苏格拉没有底",
        "青年晚报",
        "寻宝游戏",
        "热门华语233",
        "如谜",
        "温泉",
        "雨幕",
        "千古",
        "乐酷",
        "呼吸之野",
        "书香年华",
        "断桥残雪",
        "曼陀山庄",
        "合拍"
    ];

    public static List<Song> Songs =>
    [
        new("天龙八部之宿敌", "许嵩", 4, 23, "天龙八部之宿敌", 0, 167691),
        new("素颜", "许嵩/何曼婷", 3, 58, "素颜", 0, 167827),
        new("惊鸿一面", "许嵩/黄龄", 4, 16, "不如吃茶去", 0, 28854182),
        new("清明雨上", "许嵩", 3, 39, "自定义", 0, 167882),
        new("如果当时", "许嵩", 5, 16, "自定义", 0, 167870),
        new("散场电影", "许嵩", 3, 35, "许嵩单曲集", 0, 27646698),
        new("有何不可", "许嵩", 4, 1, "自定义", 0, 167876),
        new("幻听", "许嵩", 4, 33, "梦游计", 0, 167655),
        new("庐州月", "许嵩", 4, 15, "寻雾启示", 0, 167850),
        new("有何不可 (2023-2024湖南卫视芒果TV跨年晚会现场)", "许嵩/何炅", 2, 16, "2023-2024湖南卫视芒果TV跨年晚会现场", 0, 2114424070),
        new("多余的解释", "许嵩", 4, 37, "自定义", 0, 167873),
        new("雅俗共赏", "许嵩", 4, 9, "青年晚报", 0, 411214279),
        new("雅俗共赏(Live)", "许嵩", 4, 8, "我的音乐你听吗 第1期", 0, 1873375675),
        new("断桥残雪", "许嵩", 3, 47, "许嵩单曲集", 0, 27646693),
        new("玫瑰花的葬礼", "许嵩", 4, 20, "许嵩单曲集", 0, 27646687),
        new("南山忆", "许嵩", 3, 19, "半城烟沙", 0, 167786),
        new("城府", "许嵩", 3, 19, "自定义", 0, 167885),
        new("如果当时2020", "许嵩/朱婷婷", 5, 6, "如果当时2020", 0, 1488737309),
        new("灰色头像", "许嵩", 4, 49, "寻雾启示", 0, 167844),
        new("千百度", "许嵩", 3, 46, "苏格拉没有底", 0, 167732),
        new("山水之间", "许嵩", 4, 36, "不如吃茶去", 0, 28802028),
        new("叹服", "许嵩", 4, 21, "寻雾启示", 0, 167841),
        new("如约而至", "许嵩", 4, 15, "寻宝游戏", 0, 573384240),
        new("想象之中", "许嵩", 4, 6, "苏格拉没有底", 0, 167705),
        new("拆东墙", "许嵩", 4, 18, "苏格拉没有底", 0, 167712),
        new("认错", "许嵩", 4, 35, "自定义", 0, 167888),
        new("你若成风", "许嵩/莫诗旎", 3, 41, "许嵩单曲集", 0, 167929),
        new("半城烟沙", "许嵩", 4, 52, "半城烟沙", 0, 167744),
        new("惟爱你", "许嵩", 3, 25, "热门华语233", 0, 28936085),
        new("又小雪", "许嵩", 1, 51, "半城烟沙", 0, 167792),
        new("亲情式的爱情", "许嵩", 4, 44, "梦游计", 0, 167680),
        new("最佳歌手", "许嵩", 4, 27, "青年晚报", 0, 412902950),
        new("如谜", "许嵩", 4, 6, "如谜", 0, 2077483068),
        new("温泉", "许嵩/刘美麟", 4, 43, "温泉", 0, 1449406576),
        new("燕归巢", "许嵩", 4, 54, "青年晚报", 0, 402073807),
        new("内线", "许嵩", 4, 7, "自定义", 0, 167891),
        new("我想牵着你的手", "许嵩", 2, 47, "许嵩单曲集", 0, 27646702),
        new("雨幕", "许嵩", 4, 0, "雨幕", 0, 1397105439),
        new("千古", "许嵩", 3, 41, "千古", 0, 34040693),
        new("河山大好", "许嵩", 3, 16, "苏格拉没有底", 0, 167709),
        new("星座书上", "许嵩", 3, 58, "自定义", 0, 167894),
        new("浅唱", "许嵩", 4, 7, "许嵩单曲集", 0, 27646697),
        new("单人旅途", "许嵩", 4, 48, "寻雾启示", 0, 167860),
        new("曼陀山庄", "许嵩", 4, 16, "曼陀山庄", 0, 1992391270),
        new("合拍", "许嵩", 3, 3, "合拍", 0, 2016442586),
        new("千古(云音乐特别版)", "许嵩", 3, 40, "千古", 0, 34057977),
        new("你若成风", "许嵩/莫诗旎", 3, 41, "乐酷", 0, 5255987),
        new("七号公园", "许嵩", 3, 34, "许嵩单曲集", 0, 27646696),
        new("乌鸦", "许嵩", 5, 29, "呼吸之野", 0, 1842784921),
        new("毁人不倦", "许嵩", 3, 15, "苏格拉没有底", 0, 167720),
        new("胡萝卜须", "许嵩", 3, 51, "梦游计", 0, 167651),
        new("书香年华", "许嵩/孙涛", 3, 9, "书香年华", 0, 404182479),
        new("断桥残雪", "许嵩", 3, 47, "断桥残雪", 0, 167937),
        new("粉色信笺", "许嵩", 5, 8, "许嵩单曲集", 0, 27646692),
        new("白马非马", "许嵩", 4, 44, "寻雾启示", 0, 167858),
        new("医生", "许嵩", 4, 3, "苏格拉没有底", 0, 167715),
        new("全球变冷", "许嵩", 4, 12, "梦游计", 0, 167679),
        new("留香", "许嵩", 4, 7, "留香", 0, 1913262091),
        new("梧桐灯", "许嵩", 4, 37, "不如吃茶去", 0, 28987656),
        new("明智之举", "许嵩", 4, 27, "寻宝游戏", 0, 862099032)
    ];
}

public partial class SongViewModel : ModelBase
{
    public string? Title
    {
        get;
        set => SetField(ref field, value);
    }

    public string? Artist
    {
        get;
        set => SetField(ref field, value);
    }

    public string? Album
    {
        get;
        set => SetField(ref field, value);
    }

    public int CountOfComment
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsSelected
    {
        get;
        set => SetField(ref field, value);
    }
}