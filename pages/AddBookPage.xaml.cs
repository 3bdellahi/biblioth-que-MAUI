using project.Models;
using project.Services;

namespace project.Pages;

public partial class AddBookPage : ContentPage
{
    private readonly ApiService _apiService;

    public AddBookPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    // Cette méthode est appelée lorsque la page apparaît
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    // Méthode appelée lorsqu'on clique sur le bouton "Enregistrer"
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // 1. Lire les textes saisis par l'utilisateur dans les champs (Entry)
        string bookTitle = TitleEntry.Text;
        string bookCategory = CategoryEntry.Text;
        string bookStatus = StatusEntry.Text;

        // 2. Vérifier que les champs obligatoires ne sont pas vides
        if (string.IsNullOrWhiteSpace(bookTitle) || string.IsNullOrWhiteSpace(bookCategory))
        {
            await DisplayAlert(
                "Attention",
                "Veuillez saisir le titre du livre et sa catégorie",
                "OK"
            );
            return;
        }

        // 3. Créer un objet Book avec les données saisies
        var newBook = new Book
        {
            Title = bookTitle,
            CategoryName = bookCategory,
            StatusName = string.IsNullOrWhiteSpace(bookStatus)
                ? "En cours de lecture"
                : bookStatus
        };

        // 4. Envoyer les données au serveur (API Node.js)
        try
        {
            bool success = await _apiService.AddBookAsync(newBook);

            if (success)
            {
                await DisplayAlert(
                    "Succès",
                    "Le livre a été ajouté avec succès à la base de données",
                    "OK"
                );

                // Retourner à la page précédente
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(
                    "Erreur",
                    "Échec de l'enregistrement du livre. Vérifiez la connexion avec MongoDB",
                    "OK"
                );
            }
        }
        catch (Exception ex)
        {
            // Affiche l'erreur exacte (timeout, connexion refusée, etc.)
            await DisplayAlert(
                "Erreur technique",
                $"Détail : {ex.Message}",
                "OK"
            );
        }
    }
}
