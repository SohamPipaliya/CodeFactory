using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Utf8Json.JsonSerializer;

namespace CodeFactoryWeb.Extra
{
    public static class Addons
    {
        public static Uri HostUrl =>
            new("https://localhost:44354/api/");

        public static string GetImage(string name, string directory = "Questions") =>
            name is null ? null : $"https://localhost:44354/files/images/{directory}/" + name;

        #region ParseToHttpContent
        public static Task<StringContent> ParseToStringContentAsync<T>
            (this T value, string type = "application/json") => Task.Run(() =>
                new StringContent(ToJsonString(value), Encoding.UTF8, type));
        #endregion

        #region LogException
        public static Task LogAsync(this Exception value) => Task.Run(() =>
        {
            try
            {
                var x = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Logs");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Errors.log");

                using StreamWriter writer = new(path, true) { AutoFlush = true };

                writer.WriteLine($"Date:-   {DateTime.Now}");
                writer.WriteLine($"Exception:-   {value.Message}");
                if (value.InnerException is not null)
                    writer.WriteLine($"Inner Exception:-   {value.InnerException.Message}");
                if (value.StackTrace is not null)
                    writer.WriteLine($"StackTrace:-   {value.StackTrace.Split("   ")[^1]}");
                writer.WriteLine();

                writer.Flush();
                writer.Close();
            }
            catch { }
        });
        #endregion

        #region GetDataFromAPI
        public async static Task<T?> GetDataAsync<T>(this HttpClient client, APIName url, object? id = null) where T : class
        {
            T? t = null;
            try
            {
                var data = await client.GetStringAsync(id == null ? url.ToString() : url + "/" + id).ConfigureAwait(false);
                t = Deserialize<T>(data);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
            }
            return t;
        }
        #endregion

        #region ForEachDispose
        public static void ForEachDispose<T>(this IEnumerable<T> collection) where T : IDisposable
        {
            foreach (var item in collection)
                item.Dispose();
        }
        #endregion
    }

    public enum APIName
    {
        QuestionsAPI,
        TagsAPI,
        UsersAPI
    }
}