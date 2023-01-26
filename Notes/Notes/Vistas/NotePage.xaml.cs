//using static Android.Content.ClipData;
namespace Notes.Vistas;

using FireSharp.Config;
using FireSharp;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    public string ItemId
    {
        set { LoadNote(value); }
    }

    FirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "xTL6rRreLSb83PVV0eSznrlxh5nIOBE77GRudgis",
        BasePath = "https://layo-firebase.firebaseio.com/"

    };
    FirebaseClient client;

    string titulo = "";

    public NotePage()
	{
		InitializeComponent();

        //string appDataPath = FileSystem.AppDataDirectory;
        //string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        //LoadNote(Path.Combine(appDataPath, randomFileName));
        LoadNote("");
    }

    private void LoadNote(string title)
    {
        titulo= title;   //esto se sabra para cuando se modifique la nota
        Modelos.Note noteModel = new Modelos.Note();

        //si trae algo, se leera la nota (se puede mejorar leyendo el objeto Notes
        if (title == null || title!="") {
            noteModel.Titulo = title;

            client = new FirebaseClient(config);
            FireSharp.Response.FirebaseResponse resp = client.Get("Notas/" + title);
            if (resp.Body != "null")
            {
                Modelos.Note obj= resp.ResultAs<Modelos.Note>();
                noteModel.Text= obj.Text;
                noteModel.Date= obj.Date;
            }

        }

        // se quito esto, era cuando leia los archivos del dispositivo
        //if (File.Exists(fileName))
        //{
        //    noteModel.Date = File.GetCreationTime(fileName);
        //    noteModel.Text = File.ReadAllText(fileName);

        //}
        //else
        //    noteModel.Date= DateTime.Now;

        BindingContext = noteModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        
        if (BindingContext is Modelos.Note note)
        {
            note.Date= DateTime.Now;  //actualizarle la fecha

            client = new FirebaseClient(config);
            await client.SetAsync("Notas/"+note.Titulo, note);

            if (note.Titulo != titulo && titulo!="")
            {
                await client.DeleteAsync("Notas/" + titulo);
            }

            //File.WriteAllText(note.Filename, TextEditor.Text);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Modelos.Note note)
        {
            // Delete the file.
            //if (File.Exists(note.Filename))
            //    File.Delete(note.Filename);

            client = new FirebaseClient(config);
            await client.DeleteAsync("Notas/" + titulo);
        }

        await Shell.Current.GoToAsync("..");
    }
}