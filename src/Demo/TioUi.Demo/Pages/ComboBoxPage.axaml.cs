using System.Collections;
using System.Collections.Generic;
using Avalonia.Controls;

namespace TioUi.Demo.Pages;

public partial class ComboBoxPage : UserControl
{
    public ComboBoxPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public IEnumerable Items { get; set; } = new List<string> { "Apple", "Banana", "Orange", "Pear", "Grape", };
}