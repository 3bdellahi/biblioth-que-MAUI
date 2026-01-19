using System.Text.Json.Serialization;

namespace project.Models;

public class Book
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; }

    [JsonPropertyName("statusName")]
    public string StatusName { get; set; }

    // هذه الخاصية تُستخدم فقط للعرض في الواجهة (UI)
    // نستخدم JsonIgnore لكي لا يحاول التطبيق إرسالها إلى قاعدة البيانات في Node.js
    [JsonIgnore]
    public string BookImage
    {
        get
        {
            if (string.IsNullOrEmpty(CategoryName))
                return "other.png";

            string category = CategoryName.ToLower();

            // 1. إذا كان الكتاب برمجياً أو تقنياً
            if (category.Contains("prog") || category.Contains("tech") || category.Contains("برمج"))
                return "code.png";

            // 2. إذا كان رواية أو قصة أو تاريخ
            if (category.Contains("roman") || category.Contains("hist") || category.Contains("قصة"))
                return "novel.png";

            // 3. أي نوع آخر يظهر بالصورة الافتراضية
            return "other.png";
        } 
    }
}