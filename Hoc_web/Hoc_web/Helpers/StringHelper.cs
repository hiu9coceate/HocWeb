using System.Globalization;
using System.Text;

namespace Hoc_web.Helpers
{
    // Changed the class to static to fix CS1106
    public static class StringHelper
    {
        // Hàm loại bỏ dấu tiếng Việt
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in text)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // Chuyển lại về dạng C và thay thế chữ 'Đ'/'đ'
            return sb.ToString()
                     .Normalize(NormalizationForm.FormC)
                     .Replace('Đ', 'D')
                     .Replace('đ', 'd');
        }

        // Hàm mở rộng để tìm kiếm không dấu và không hoa thường
        public static bool ContainsIgnoreCaseAndDiacritics(this string source, string value)
        {
            if (source == null || value == null) return false;

            string sourceNormalized = RemoveDiacritics(source).ToLowerInvariant();
            string valueNormalized = RemoveDiacritics(value).ToLowerInvariant();

            return sourceNormalized.Contains(valueNormalized);
        }
    }
}
