using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utf8Json.Resolvers;
using static Utf8Json.JsonSerializer;

namespace CodeFactoryWeb.Extra
{
    public static class Addons
    {
        #region HostURL
        public static Uri HostUrl =>
           new("https://localhost:44354/api/");
        #endregion

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
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Logs");
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
                t = Deserialize<T>(data, StandardResolver.ExcludeNull);
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

        #region CustomTagHelper
        public static IHtmlContent Link(string name, ControllerName controllerName, ActionName actionName, Guid? id = null) =>
            new HtmlString($"<a href=\"/{controllerName}/{actionName}/{id}\">{name}<a>");

        public static string GetImage(string name, string directory = "Questions") =>
            name is null ? null : $"https://localhost:44354/files/images/{directory}/{name}";
        #endregion

        #region PostorPutAsFormData
        public static async Task<HttpResponseMessage?> AsFormDataAsync<T>(this HttpClient client, APIName aPIName, MethodName methodName, T value, IFormFile[]? files = null, Guid? id = default)
        {
            Stream[]? streams = null;
            StreamContent[]? streamContents = null;
            try
            {
                using var data = new MultipartFormDataContent();
                using var stringContent = await value.ParseToStringContentAsync().ConfigureAwait(false);
                data.Add(stringContent, "question");

                if (files is not null && files.Length > 0 && files.Length < 6)
                {
                    streams = new Stream[files.Length];
                    streamContents = new StreamContent[files.Length];

                    for (int i = 0; i < files.Length; i++)
                    {
                        streams[i] = files[i].OpenReadStream();
                        streamContents[i] = new StreamContent((streams[i]));
                        data.Add(streamContents[i], "files", files[i].FileName);
                    }
                }

                return methodName == MethodName.PostAsync
                     ? await client.PostAsync(aPIName.ToString(), data).ConfigureAwait(false)
                     : await client.PutAsync(aPIName.ToString() + id, data).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return null;
            }
            finally
            {
                streams?.ForEachDispose();
                streamContents?.ForEachDispose();
            }
        }
        #endregion
    }

    #region Enums
    public enum APIName
    {
        QuestionsAPI,
        TagsAPI,
        UsersAPI,
        MessagesAPI,
        RepliesAPI
    }

    public enum ControllerName
    {
        Messages,
        Questions,
        Tags,
        Users,
        Replies
    }

    public enum ActionName
    {
        Index,
        Create,
        Edit,
        Delete,
        Details
    }

    public enum MethodName
    {
        PostAsync,
        PutAsync
    }
    #endregion
}