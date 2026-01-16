using project.Models;
using project.Services;

namespace project.Pages;

public partial class BookDetailsPage : ContentPage
{
    // Service responsable de la communication avec l’API Node.js
    private readonly ApiService _apiService;

    // Livre actuellement sélectionné (celui dont on affiche les détails)
    private readonly Book _currentBook;

    // Constructeur : appelé lors de l’ouverture de la page
    public BookDetailsPage(Book book)
    {
        InitializeComponent();

        _apiService = new ApiService();
        _currentBook = book;

        // Afficher les informations principales du livre dans l’interface
        TitleLabel.Text = _currentBook.Title;
        StatusLabel.Text = _currentBook.StatusName;
    }

    // Cette méthode est appelée automatiquement lorsque la page apparaît
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadNotes(); // Charger les notes liées au livre
    }

    // Charger les notes depuis la base de données
    private async Task LoadNotes()
    {
        try
        {
            // Récupérer les notes associées à l’ID du livre depuis MongoDB
            var notes = await _apiService.GetNotesByBookIdAsync(_currentBook.Id);

            // Afficher les notes dans la CollectionView
            NotesCollection.ItemsSource = notes;
        }
        catch (Exception)
        {
            await DisplayAlert(
                "Erreur",
                "Échec du chargement des notes",
                "OK"
            );
        }
    }

    // Méthode appelée lorsque l’utilisateur clique sur "Ajouter la note"
    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        // Vérifier que l’utilisateur a écrit quelque chose
        if (string.IsNullOrWhiteSpace(NewNoteEditor.Text))
            return;

        // Créer un objet Note avec les données saisies
        var note = new Note
        {
            Content = NewNoteEditor.Text,
            BookId = _currentBook.Id
        };

        // Envoyer la note au serveur Node.js pour l’enregistrer
        bool success = await _apiService.AddNoteAsync(note);

        if (success)
        {
            // Vider le champ après l’ajout
            NewNoteEditor.Text = string.Empty;

            // Recharger la liste pour afficher la nouvelle note
            await LoadNotes();
        }
        else
        {
            await DisplayAlert(
                "Erreur",
                "La note n’a pas été enregistrée. Vérifiez la connexion au serveur",
                "OK"
            );
        }
    }
}
