using System.Text.Json.Serialization; // استخدم هذه المكتبة المدمجة بدلاً من Newtonsoft

namespace project.Models;

public class Book
{
    [JsonPropertyName("_id")] 
    public string Id { get; set; }
    public string Title { get; set; }
    public string CategoryName { get; set; }
    public string StatusName { get; set; }
}