﻿using CodeFactoryAPI.Extra;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

        #region GetDataFromAPI
        public async static Task<T?> GetDataAsync<T>(this HttpClient client, APIName url, object? id = null) where T : class
        {
            T? t = null;
            try
            {
                string? data = await client.GetStringAsync(id == null ? url.ToString() : url + "/" + id).ConfigureAwait(false);
                t = Deserialize<T>(data, StandardResolver.ExcludeNull);
            }
            catch { }
            return t;
        }
        #endregion

        #region ToIActionResult
        public static async Task<IActionResult> ToActionResult<T>(this Controller controller, T data)
        {
            try
            {
                return data is null ? controller.NotFound() : controller.View(data);
            }
            catch (Exception ex)
            {
                await ex.LogAsync().ConfigureAwait(false);
                return controller.RedirectToAction("Error", "Error");
            }
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

        public static string GetImage(string? name, string directory = "Questions") =>
            $"https://localhost:44354/files/images/{directory}/{name}";
        #endregion

        #region PostorPutAsFormData
        public static async Task<HttpResponseMessage?> PostAsFormDataAsync<T>(this HttpClient client, string apiName, T value, IFormFile[]? files)
        {
            var data = await GetDataContent(value, files).ConfigureAwait(false);
            try
            {
                using var multipartFormDataContent = data.multipartFormDataContent;
                return await client.PostAsync(apiName, multipartFormDataContent).ConfigureAwait(false);
            }
            finally
            {
                data.StringContent.Dispose();
                data.streams?.ForEachDispose();
                data.streamContents?.ForEachDispose();
            }
        }

        private async static Task<(MultipartFormDataContent multipartFormDataContent, Stream[] streams, StreamContent[] streamContents, StringContent StringContent)> GetDataContent<T>(T value, IFormFile[]? files, string parameterName = null)
        {
            Stream[] streams = null;
            StreamContent[] streamContents = null;

            var data = new MultipartFormDataContent();
            var stringContent = await value.ParseToStringContentAsync().ConfigureAwait(false);
            data.Add(stringContent, parameterName ?? value.GetType().Name);

            if (files is not null && files.Length > 0)
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
            return (data, streams, streamContents, stringContent);
        }

        public static async Task<HttpResponseMessage?> PutAsFormDataAsync<T>(this HttpClient client, string apiName, T value, IFormFile[]? files)
        {
            var data = await GetDataContent(value, files).ConfigureAwait(false);
            try
            {
                using var multipartFormDataContent = data.multipartFormDataContent;
                return await client.PutAsync(apiName, multipartFormDataContent).ConfigureAwait(false);
            }
            finally
            {
                data.StringContent.Dispose();
                data.streams.ForEachDispose();
                data.streamContents.ForEachDispose();
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
    #endregion
}