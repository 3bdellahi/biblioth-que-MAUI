using project.Models;
using project.Services;

namespace project.Pages;

public partial class BooksPage : ContentPage
{
    // Service responsable de la communication avec l’API
    private readonly ApiService _apiService;

    public BooksPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    // Cette méthode est appelée à chaque apparition de la page
    // (permet de rafraîchir automatiquement les données)
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadBooks();
    }

    // Charger la liste des livres depuis le serveur
    private async Task LoadBooks()
    {
        try
        {
            // Récupérer les livres depuis MongoDB via l’API Node.js
            var books = await _apiService.GetBooksAsync();

            // Lier les données à la CollectionView
            BooksCollection.ItemsSource = books;
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Erreur",
                "Échec du chargement des données : " + ex.Message,
                "OK"
            );
        }
    }

    // Navigation vers la page d’ajout d’un nouveau livre
    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddBookPage());
    }

    // Navigation vers la page des détails lorsqu’un livre est sélectionné
    private async void OnBookSelected(object sender, EventArgs e)
    {
        // Identifier l’élément (Frame) qui a été cliqué
        var frame = sender as Frame;

        // Récupérer le livre associé à ce Frame
        var book = frame?.BindingContext as Book;

        if (book != null)
        {
            // Aller à la page de détails du livre sélectionné
            await Navigation.PushAsync(new BookDetailsPage(book));
        }
    }
}
