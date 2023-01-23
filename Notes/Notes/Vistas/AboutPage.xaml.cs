using Notes.Modelos;

namespace Notes.Vistas;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private async Task LearnMore_ClickedAsync(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://aka.ms/maui");
    }

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Modelos.About about)
    {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
        }
    }
}