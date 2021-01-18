using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Utf8Json.JsonSerializer;

namespace Extra
{
    public static class Addons
    {
        private static readonly object o = new();

        public static Uri HostUrl =>
            new("https://localhost:44354/api/");

        #region ParseToHttpContent
        public static Task<StringContent> ParseToStringContentAsync<T>
            (this T value, Encoding encoding = null, string type = "application/json") => Task.Run(() =>
            {
                lock (o)
                {
                    return new StringContent(ToJsonString(value), encoding ?? Encoding.UTF8, type);
                }
            });
        #endregion

        #region LogException
        public static Task LogAsync(this Exception value) => Task.Run(() =>
        {
            try
            {
                lock (o)
                {
                    var x = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Logs");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Errors.log");
                    using (var writer = new StreamWriter(path, true) { AutoFlush = true })
                    {
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
                }
            }
            catch { }
        });
        #endregion

        #region GetDataFromAPI
        public static Task<T?> GetDataAsync<T>(this HttpClient client, string url) where T : class => Task.Run(() =>
        {
            T? t = null;
            try
            {
                lock (o)
                {
                    using (var response = client.GetAsync(url).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var data = client.GetStringAsync(url).Result;
                            t = Deserialize<T>(data);
                        }
                    }
                }
            }
            catch { }
            return t;
        });
        #endregion
    }
}