using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace TioUi.Demo.Pages;

public partial class AutoCompleteBoxPage : UserControl
{
    public AutoCompleteBoxPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ObservableCollection<Data> DataList { get; set; } =
    [
        new("Apple", "A"), new("Anchor", "A"), new("Abyss", "A"), new("Arctic", "A"), new("Agency", "A"),
        new("Aspect", "A"), new("Amount", "A"), new("Action", "A"), new("Artist", "A"), new("Advice", "A"),

        new("Banana", "B"), new("Bridge", "B"), new("Bottle", "B"),
        new("Cactus", "C"), new("Camera", "C"), new("Castle", "C"), new("Coffee", "C"), new("Crystal", "C"),
        new("Danger", "D"), new("Desert", "D"),
        new("Eagle", "E"), new("Engine", "E"), new("Energy", "E"), new("Editor", "E"),
        new("Forest", "F"), new("Factor", "F"), new("Flavor", "F"),
        new("Garden", "G"), new("Galaxy", "G"), new("Guitar", "G"), new("Gently", "G"), new("Garage", "G"),
        new("Hammer", "H"), new("Height", "H"), new("Health", "H"),
        new("Island", "I"), new("Impact", "I"), new("Income", "I"), new("Injury", "I"),
        new("Jungle", "J"), new("Junior", "J"), new("Journey", "J"),
        new("Knight", "K"),
        new("Laptop", "L"), new("Legend", "L"), new("Liquid", "L"), new("Luxury", "L"), new("Lyrics", "L"),
        new("Market", "M"), new("Memory", "M"), new("Museum", "M"), new("Mirror", "M"),
        new("Nature", "N"), new("Notice", "N"), new("Number", "N"),
        new("Object", "O"), new("Office", "O"), new("Option", "O"), new("Oxygen", "O"),
        new("Parent", "P"), new("Planet", "P"), new("Player", "P"), new("Police", "P"), new("Public", "P"),
        new("Purple", "P"),
        new("Quartz", "Q"), new("Quench", "Q"),
        new("Rabbit", "R"), new("Random", "R"), new("Record", "R"), new("Repair", "R"), new("Report", "R"),
        new("School", "S"), new("Series", "S"), new("Silver", "S"), new("Simple", "S"), new("Social", "S"),
        new("Status", "S"), new("System", "S"),
        new("Target", "T"), new("Theory", "T"), new("Ticket", "T"), new("Travel", "T"),
        new("Unique", "U"), new("Update", "U"), new("Urgent", "U"),
        new("Valley", "V"), new("Vector", "V"), new("Visual", "V"),
        new("Window", "W"), new("Worker", "W"), new("Writer", "W"), new("Weight", "W"),
        new("Xenon", "X"), new("Xylophone", "X"),
        new("Yellow", "Y"), new("Yearly", "Y"),
        new("Zodiac", "Z"), new("Zenith", "Z"), new("Zigzag", "Z")
    ];
}

public class Data(string name, string data)
{
    public string Name { get; private set; } = name;
    public string Tag { get; private set; } = data;
}