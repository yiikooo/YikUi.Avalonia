using YikUi.Common.Helper;

namespace YikUi.Common;

public class Data
{
    private static Data? _instance;

    public static Data Instance
    {
        get { return _instance ??= new Data(); }
    }

    public static DesktopType DesktopType
    {
        get
        {
            if (field == DesktopType.Undefine) field = Platform.DetectPlatform();
            return field;
        }
    } = DesktopType.Undefine;
}