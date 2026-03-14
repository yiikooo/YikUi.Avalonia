using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Pages;

public partial class TreeDataGridPage : PageModelBase
{
    public TreeDataGridPage()
    {
        InitializeComponent();
        DataContext = new TreeDataGridDemoViewModel();
    }

    private void SelectedPath_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && DataContext is TreeDataGridDemoViewModel vm)
        {
            vm.FilesContext.SelectedPath = (sender as TextBox)?.Text;
        }
    }
}

public class TreeDataGridDemoViewModel : ObservableObject
{
    public SongsPageViewModel SongsContext { get; } = new();
    public FilesPageViewModel FilesContext { get; } = new();
}

public class SongsPageViewModel : ObservableObject
{
    public SongsPageViewModel()
    {
        var songs = new ObservableCollection<SongViewModel>(Song.Songs.Select(a => new SongViewModel()
        {
            Title = a.Title,
            Artist = a.Artist,
            Album = a.Album,
            CountOfComment = a.CountOfComment,
            IsSelected = false
        }));

        Songs = new FlatTreeDataGridSource<SongViewModel>(songs)
        {
            Columns =
            {
                new CheckBoxColumn<SongViewModel>(
                    "IsSelected",
                    a => a.IsSelected,
                    (model, b) => { model.IsSelected = b; },
                    new GridLength(128, GridUnitType.Pixel)),
                new TextColumn<SongViewModel, string>(
                    "Title",
                    a => a.Title,
                    (o, a) => o.Title = a,
                    new GridLength(6, GridUnitType.Star)),
                new TextColumn<SongViewModel, string>("Artist",
                    a => a.Artist,
                    (o, a) => o.Artist = a,
                    new GridLength(6, GridUnitType.Star)),
                new TemplateColumn<SongViewModel>("Album",
                    "AlbumCell",
                    "AlbumEditCell",
                    new GridLength(6, GridUnitType.Star)),
                new TemplateColumn<SongViewModel>(
                    "Comments",
                    "CommentsCell",
                    "CommentsEditCell",
                    new GridLength(6, GridUnitType.Star)),
            }
        };
    }

    public FlatTreeDataGridSource<SongViewModel> Songs { get; }
}

public partial class FilesPageViewModel : ObservableObject
{
    [ObservableProperty] private FileNodeViewModel? _root;
    [ObservableProperty] private string _selectedDrive;
    private string? _selectedPath;

    public FilesPageViewModel()
    {
        Drives = DriveInfo.GetDrives().Select(x => x.Name).ToList();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            SelectedDrive = @"C:\";
        }
        else
        {
            SelectedDrive = Drives.FirstOrDefault() ?? "/";
        }

        Source = new HierarchicalTreeDataGridSource<FileNodeViewModel>([])
        {
            Columns =
            {
                new CheckBoxColumn<FileNodeViewModel>(
                    null,
                    x => x.IsChecked,
                    (o, v) => o.IsChecked = v,
                    options: new CheckBoxColumnOptions<FileNodeViewModel>
                    {
                        CanUserResizeColumn = false,
                    }),
                new HierarchicalExpanderColumn<FileNodeViewModel>(
                    new TemplateColumn<FileNodeViewModel>(
                        "Name",
                        "FileNameCell",
                        "FileNameEditCell",
                        new GridLength(1, GridUnitType.Star),
                        new TemplateColumnOptions<FileNodeViewModel>
                        {
                            CompareAscending = FileNodeViewModel.SortAscending(vm => vm.Name),
                            CompareDescending = FileNodeViewModel.SortDescending(vm => vm.Name),
                            IsTextSearchEnabled = true,
                            TextSearchValueSelector = vm => vm.Name
                        }),
                    vm => vm.Children,
                    vm => vm.HasChildren,
                    vm => vm.IsExpanded),
                new TextColumn<FileNodeViewModel, long?>(
                    "Size",
                    vm => vm.Size,
                    options: new TextColumnOptions<FileNodeViewModel>
                    {
                        CompareAscending = FileNodeViewModel.SortAscending(x => x.Size),
                        CompareDescending = FileNodeViewModel.SortDescending(x => x.Size),
                    }),
                new TextColumn<FileNodeViewModel, DateTimeOffset?>(
                    "Modified",
                    x => x.Modified,
                    options: new TextColumnOptions<FileNodeViewModel>
                    {
                        CompareAscending = FileNodeViewModel.SortAscending(x => x.Modified),
                        CompareDescending = FileNodeViewModel.SortDescending(x => x.Modified),
                    }),
            }
        };
        Source.RowSelection!.SingleSelect = false;
        Source.RowSelection.SelectionChanged += SelectionChanged;
    }

    public IList<string> Drives { get; }
    public HierarchicalTreeDataGridSource<FileNodeViewModel> Source { get; }

    public string? SelectedPath
    {
        get => _selectedPath;
        set => SetSelectedPath(value);
    }

    partial void OnSelectedDriveChanged(string value)
    {
        Root = new FileNodeViewModel(value, true, true);
        if (Source is not null)
        {
            Source.Items = [Root];
        }
    }

    private void SelectionChanged(object? sender, TreeSelectionModelSelectionChangedEventArgs<FileNodeViewModel> e)
    {
        var selectedPath = Source.RowSelection?.SelectedItem?.Path;
        this.SetProperty(ref _selectedPath, selectedPath, nameof(SelectedPath));

        foreach (var i in e.DeselectedItems)
            Trace.WriteLine($"Deselected '{i?.Path}'");
        foreach (var i in e.SelectedItems)
            Trace.WriteLine($"Selected '{i?.Path}'");
    }

    private void SetSelectedPath(string? path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Source.RowSelection!.Clear();
            return;
        }

        var components = new Stack<string>();
        DirectoryInfo? d = null;

        if (File.Exists(path))
        {
            var f = new FileInfo(path);
            components.Push(f.Name);
            d = f.Directory;
        }
        else if (Directory.Exists(path))
        {
            d = new DirectoryInfo(path);
        }

        while (d is not null)
        {
            components.Push(d.Name);
            d = d.Parent;
        }

        var index = IndexPath.Unselected;

        if (components.Count > 0)
        {
            var drive = components.Pop();
            var driveIndex = Drives.FindIndex(x => string.Equals(x, drive, StringComparison.OrdinalIgnoreCase));

            if (driveIndex >= 0)
                SelectedDrive = Drives[driveIndex];

            var node = Root;
            index = new IndexPath(0);

            while (node is not null && components.Count > 0)
            {
                node.IsExpanded = true;

                var component = components.Pop();
                var i = node.Children.FindIndex(x =>
                    string.Equals(x.Name, component, StringComparison.OrdinalIgnoreCase));
                node = i >= 0 ? node.Children[i] : null;
                index = i >= 0 ? index.Append(i) : default;
            }
        }

        Source.Items = [Root!];
        Source.RowSelection!.SelectedIndex = index;
    }
}

public partial class FileNodeViewModel : ObservableObject, IEditableObject
{
    private ObservableCollection<FileNodeViewModel>? _children;
    [ObservableProperty] private bool _hasChildren = true;
    [ObservableProperty] private bool _isExpanded;
    [ObservableProperty] private DateTimeOffset? _modified;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _path;
    [ObservableProperty] private long? _size;
    private string? _undoName;
    private FileSystemWatcher? _watcher;

    public FileNodeViewModel(string path, bool isDirectory, bool isRoot = false)
    {
        Path = path;
        Name = isRoot ? path : System.IO.Path.GetFileName(Path);
        IsExpanded = isRoot;
        IsDirectory = isDirectory;
        HasChildren = isDirectory;

        if (!isDirectory)
        {
            var info = new FileInfo(path);
            Size = info.Length;
            Modified = info.LastWriteTimeUtc;
        }
    }

    public bool IsChecked { get; set; }
    public bool IsDirectory { get; }
    public IReadOnlyList<FileNodeViewModel> Children => _children ??= LoadChildren();

    void IEditableObject.BeginEdit() => _undoName = Name;
    void IEditableObject.CancelEdit() => Name = _undoName ?? string.Empty;
    void IEditableObject.EndEdit() => _undoName = null;

    private ObservableCollection<FileNodeViewModel> LoadChildren()
    {
        var result = new ObservableCollection<FileNodeViewModel>();
        if (!IsDirectory)
        {
            HasChildren = false;
            return result;
        }

        try
        {
            var options = new EnumerationOptions { IgnoreInaccessible = true };


            foreach (var d in Directory.EnumerateDirectories(Path, "*", options))
            {
                result.Add(new FileNodeViewModel(d, true));
            }

            foreach (var f in Directory.EnumerateFiles(Path, "*", options))
            {
                result.Add(new FileNodeViewModel(f, false));
            }

            _watcher = new FileSystemWatcher
            {
                Path = Path,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.LastWrite,
            };

            _watcher.Changed += OnChanged;
            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.EnableRaisingEvents = true;

            if (result.Count == 0)
                HasChildren = false;

            return result;
        }
        catch
        {
            HasChildren = false;
            return result;
        }
    }

    public static Comparison<FileNodeViewModel?> SortAscending<T>(Func<FileNodeViewModel, T> selector)
    {
        return (x, y) =>
        {
            if (x is null && y is null)
                return 0;
            else if (x is null)
                return -1;
            else if (y is null)
                return 1;
            if (x.IsDirectory == y.IsDirectory)
                return Comparer<T>.Default.Compare(selector(x), selector(y));
            else if (x.IsDirectory)
                return -1;
            else
                return 1;
        };
    }

    public static Comparison<FileNodeViewModel?> SortDescending<T>(Func<FileNodeViewModel, T> selector)
    {
        return (x, y) =>
        {
            if (x is null && y is null)
                return 0;
            else if (x is null)
                return 1;
            else if (y is null)
                return -1;
            if (x.IsDirectory == y.IsDirectory)
                return Comparer<T>.Default.Compare(selector(y), selector(x));
            else if (x.IsDirectory)
                return -1;
            else
                return 1;
        };
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType == WatcherChangeTypes.Changed && File.Exists(e.FullPath))
        {
            Dispatcher.UIThread.Post(() =>
            {
                foreach (var child in _children!)
                {
                    if (child.Path == e.FullPath)
                    {
                        if (!child.IsDirectory)
                        {
                            var info = new FileInfo(e.FullPath);
                            child.Size = info.Length;
                            child.Modified = info.LastWriteTimeUtc;
                        }

                        break;
                    }
                }
            });
        }
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var node = new FileNodeViewModel(
                e.FullPath,
                File.GetAttributes(e.FullPath).HasFlag(FileAttributes.Directory));
            _children!.Add(node);
        });
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            for (var i = 0; i < _children!.Count; ++i)
            {
                if (_children[i].Path == e.FullPath)
                {
                    _children.RemoveAt(i);
                    Debug.WriteLine($"Removed {e.FullPath}");
                    break;
                }
            }
        });
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            foreach (var child in _children!)
            {
                if (child.Path == e.OldFullPath)
                {
                    child.Path = e.FullPath;
                    child.Name = e.Name ?? string.Empty;
                    break;
                }
            }
        });
    }
}

internal static class ListExtensions
{
    public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        int i = 0;
        foreach (var item in source)
        {
            if (predicate(item))
                return i;
            i++;
        }

        return -1;
    }
}