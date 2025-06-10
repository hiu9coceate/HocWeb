using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hoc_web.Models.Extensisons
{
    public static class SessisonExtension
    {
        public static void SaveObjectAsjson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
