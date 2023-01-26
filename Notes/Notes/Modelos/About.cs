namespace Notes.Modelos;

internal class About
{
    public string Title => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string MoreInfoUrl => "https://aka.ms/maui";
    public string Message => "Esta App es escrita con XAML y C# con .NET MAUI.";
}
