using project.Models; 
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace project.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    private const string BaseUrl = "http://192.168.100.20:5000/api/";

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    // --- منطق الكتب (Books Logic) ---

    public async Task<List<Book>> GetBooksAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Book>>($"{BaseUrl}books");
        }
        catch (Exception) { return new List<Book>(); }
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}books", book);
        return response.IsSuccessStatusCode;
    }

    // --- منطق التصنيفات والحالات (Pickers Logic) ---

    public async Task<List<Category>> GetCategoriesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>($"{BaseUrl}categories");
        }
        catch { return new List<Category>(); }
    }
        
    public async Task<List<ReadingStatus>> GetStatusesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ReadingStatus>>($"{BaseUrl}statuses");
        }
        catch { return new List<ReadingStatus>(); }
    }

    // --- منطق الملاحظات (Notes Logic) ---

    public async Task<List<Note>> GetNotesByBookIdAsync(string bookId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Note>>($"{BaseUrl}notes/{bookId}");
        }
        catch { return new List<Note>(); }
    }

    // --- إضافة دالة حفظ الملاحظة المفقودة ---
    public async Task<bool> AddNoteAsync(Note note)
    {
        try
        {
            
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}notes", note);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding note: {ex.Message}");
            return false;
        }
    }
} 
