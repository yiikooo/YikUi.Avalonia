using System.Collections;
using System.Collections.Generic;
using Avalonia.Controls;

namespace YikUi.Demo.Pages;

public partial class ListBoxPage : UserControl
{
    public ListBoxPage()
    {
        InitializeComponent();
    }

    public IEnumerable Items { get; set; } = new List<string> { "Ding", "Otter", "Husky", "Mr.17", "Cass", };
}