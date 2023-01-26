using System.Collections.ObjectModel;
using FireSharp.Config;
using FireSharp;
using Microsoft.Maui.Controls.Xaml;
using FireSharp.Response;
using FireSharp.Extensions;
using Newtonsoft.Json;

namespace Notes.Modelos;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    FirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "xTL6rRreLSb83PVV0eSznrlxh5nIOBE77GRudgis",
        BasePath = "https://layo-firebase.firebaseio.com/"

    };
    FirebaseClient client;

    public AllNotes() =>
        LoadNotes2();

    public void LoadNotes2()
    {
        Notes.Clear();

        try
        {
            client = new FirebaseClient(config);
            FireSharp.Response.FirebaseResponse resp = client.Get("Notas");

            Dictionary<string, Note> lista = new Dictionary<string, Note>();
            lista = JsonConvert.DeserializeObject<Dictionary<string, Note>>(resp.Body);

            List<Note> Listado = new List<Note>();

            foreach (KeyValuePair<string, Note> elemento in lista)
            {
                //agrega las notas
                Notes.Add(new Note()
                {
                    Date = elemento.Value.Date,
                    Titulo = elemento.Value.Titulo,
                    Text = elemento.Value.Text
                    //Filename = elemento.Value.Filename
                });
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public void LoadNotesx()
    {
        Notes.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<Note> notes = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.notes.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new Note()
                                    {
                                        //Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetCreationTime(filename)
                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (Note note in notes)
            Notes.Add(note);
    }
}
