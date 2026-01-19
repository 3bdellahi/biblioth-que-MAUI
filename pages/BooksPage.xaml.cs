using project.Models;
using project.Services;
using System.Collections.ObjectModel;

namespace project.Pages;

public partial class BooksPage : ContentPage
{
    // الخدمة المسؤولة عن الاتصال بـ Node.js
    private readonly ApiService _apiService;

    // القائمة "الخزنة" التي تحفظ النسخة الأصلية للبحث فيها محلياً
    private List<Book> _allBooks = new List<Book>();

    public BooksPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    // يتم استدعاء هذه الدالة عند ظهور الصفحة (لتحديث البيانات)
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadBooks();
    }

    // جلب قائمة الكتب من السيرفر
    private async Task LoadBooks()
    {
        try
        {
            // جلب البيانات من MongoDB عبر API
            var books = await _apiService.GetBooksAsync();

            if (books != null)
            {
                // حفظ النسخة الأصلية للبحث
                _allBooks = books.ToList();

                // عرض البيانات في الواجهة
                BooksCollection.ItemsSource = _allBooks;
            }
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

    // ميزة البحث التلقائي: تعمل مع كل حرف يكتبه المستخدم
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        // تحويل النص المكتوب إلى حروف صغيرة لتسهيل البحث
        string searchTerm = e.NewTextValue?.ToLower() ?? "";

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            // إذا كان البحث فارغاً، نعيد عرض كل الكتب
            BooksCollection.ItemsSource = _allBooks;
        }
        else
        {
            // فلترة القائمة الأصلية وعرض النتائج التي تحتوي على نص البحث
            var filtered = _allBooks.Where(b =>
                (b.Title != null && b.Title.ToLower().Contains(searchTerm)) ||
                (b.CategoryName != null && b.CategoryName.ToLower().Contains(searchTerm))
            ).ToList();

            BooksCollection.ItemsSource = filtered;
        }
    }

    // الانتقال لصفحة إضافة كتاب جديد
    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddBookPage());
    }

    // الانتقال لصفحة التفاصيل عند الضغط على كتاب
    private async void OnBookSelected(object sender, EventArgs e)
    {
        // التعديل: نستخدم VisualElement ليكون الكود مرناً ويقبل أي عنصر (Layout أو Border)
        var view = sender as VisualElement;

        // استخراج الكتاب المرتبط بالعنصر الذي تم الضغط عليه
        var book = view?.BindingContext as Book;

        if (book != null)
        {
            // الانتقال لصفحة التفاصيل وتمرير بيانات الكتاب
            await Navigation.PushAsync(new BookDetailsPage(book));
        }
    } 
}