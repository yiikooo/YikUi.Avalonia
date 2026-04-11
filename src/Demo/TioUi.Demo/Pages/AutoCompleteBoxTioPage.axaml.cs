using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace TioUi.Demo.Pages;

public partial class AutoCompleteBoxTioPage : UserControl
{
    public AutoCompleteBoxTioPage()
    {
        InitializeComponent();
        DataContext = this;
        Controls = new ObservableCollection<ControlData>(GetControlData());
    }

    public ObservableCollection<ControlData> Controls { get; set; }

    private static ControlData[] GetControlData()
    {
        return
        [
            new ControlData { MenuHeader = "Button Group", Chinese = "按钮组" },
            new ControlData { MenuHeader = "Icon Button", Chinese = "图标按钮" },
            new ControlData { MenuHeader = "AutoCompleteBox", Chinese = "自动完成框" },
            new ControlData { MenuHeader = "Class Input", Chinese = "类输入框" },
            new ControlData { MenuHeader = "Enum Selector", Chinese = "枚举选择器" },
            new ControlData { MenuHeader = "Form", Chinese = "表单" },
            new ControlData { MenuHeader = "KeyGestureInput", Chinese = "快捷键输入" },
            new ControlData { MenuHeader = "IPv4Box", Chinese = "IPv4输入框" },
            new ControlData { MenuHeader = "MultiComboBox", Chinese = "多选组合框" },
            new ControlData { MenuHeader = "Multi AutoCompleteBox", Chinese = "多项自动完成框" },
            new ControlData { MenuHeader = "Numeric UpDown", Chinese = "数字上下调节" },
            new ControlData { MenuHeader = "NumPad", Chinese = "数字键盘" },
            new ControlData { MenuHeader = "PathPicker", Chinese = "路径选择器" },
            new ControlData { MenuHeader = "PinCode", Chinese = "密码输入" },
            new ControlData { MenuHeader = "RangeSlider", Chinese = "范围滑块" },
            new ControlData { MenuHeader = "Rating", Chinese = "评分" },
            new ControlData { MenuHeader = "Selection List", Chinese = "选择列表" },
            new ControlData { MenuHeader = "TagInput", Chinese = "标签输入" },
            new ControlData { MenuHeader = "Theme Toggler", Chinese = "主题切换" },
            new ControlData { MenuHeader = "TreeComboBox", Chinese = "树形组合框" },
            new ControlData { MenuHeader = "Dialog", Chinese = "对话框" },
            new ControlData { MenuHeader = "Drawer", Chinese = "抽屉" },
            new ControlData { MenuHeader = "Loading", Chinese = "加载" },
            new ControlData { MenuHeader = "Message Box", Chinese = "消息框" },
            new ControlData { MenuHeader = "Notification", Chinese = "通知" },
            new ControlData { MenuHeader = "PopConfirm", Chinese = "气泡确认" },
            new ControlData { MenuHeader = "Toast", Chinese = "吐司" },
            new ControlData { MenuHeader = "Skeleton", Chinese = "骨架屏" },
            new ControlData { MenuHeader = "Date Picker", Chinese = "日期选择器" },
            new ControlData { MenuHeader = "Date Range Picker", Chinese = "日期范围选择器" },
            new ControlData { MenuHeader = "Date Time Picker", Chinese = "日期时间选择器" },
            new ControlData { MenuHeader = "Time Box", Chinese = "时间输入框" },
            new ControlData { MenuHeader = "Time Picker", Chinese = "时间选择器" },
            new ControlData { MenuHeader = "Time Range Picker", Chinese = "时间范围选择器" },
            new ControlData { MenuHeader = "Clock", Chinese = "时钟" },
            new ControlData { MenuHeader = "Anchor", Chinese = "锚点" },
            new ControlData { MenuHeader = "Breadcrumb", Chinese = "面包屑" },
            new ControlData { MenuHeader = "Nav Menu", Chinese = "导航菜单" },
            new ControlData { MenuHeader = "Pagination", Chinese = "分页" },
            new ControlData { MenuHeader = "ToolBar", Chinese = "工具栏" },
            new ControlData { MenuHeader = "AspectRatioLayout", Chinese = "宽高比布局" },
            new ControlData { MenuHeader = "Avatar", Chinese = "头像" },
            new ControlData { MenuHeader = "Badge", Chinese = "徽章" },
            new ControlData { MenuHeader = "Banner", Chinese = "横幅" },
            new ControlData { MenuHeader = "Disable Container", Chinese = "禁用容器" },
            new ControlData { MenuHeader = "Divider", Chinese = "分割线" },
            new ControlData { MenuHeader = "DualBadge", Chinese = "双徽章" },
            new ControlData { MenuHeader = "ImageViewer", Chinese = "图片查看器" },
            new ControlData { MenuHeader = "ElasticWrapPanel", Chinese = "弹性换行面板" },
            new ControlData { MenuHeader = "Marquee", Chinese = "跑马灯" },
            new ControlData { MenuHeader = "Number Displayer", Chinese = "数字显示器" },
            new ControlData { MenuHeader = "Scroll To", Chinese = "滚动到按钮" },
            new ControlData { MenuHeader = "Timeline", Chinese = "时间轴" },
            new ControlData { MenuHeader = "TwoTonePathIcon", Chinese = "双色路径图标" }
        ];
    }
}

public class ControlData
{
    public required string MenuHeader { get; init; }
    public required string Chinese { get; init; }
}