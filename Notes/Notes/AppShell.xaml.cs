namespace Notes;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(Vistas.NotePage), typeof(Vistas.NotePage));
    }
}
