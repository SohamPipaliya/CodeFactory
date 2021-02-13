using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;
using static System.IO.File;

namespace CodeFactoryAPI.Extra
{
    public static class Addons
    {
        #region GetImagePath
        public static string ImagePath(string name, string directory = "Users") =>
                 Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", directory, name);
        #endregion

        #region SetColumnState
        public static void SetUpdatedColumns<T>(this EntityEntry<T> value, params string[] columnNames) where T : class
        {
            foreach (var item in columnNames)
                value.Property(item).IsModified = true;
        }
        #endregion

        #region Triple DES
        #region Encryption
        public static Task<string> EncryptAsync(this string toEncrypt) => Task.Run(() =>
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            string key = "sohampatel81281";

            using MD5CryptoServiceProvider md = new();
            var keyArray = md.ComputeHash(Encoding.UTF8.GetBytes(key));
            md.Clear();

            using TripleDESCryptoServiceProvider tdes = new();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        });
        #endregion

        #region Decryption
        public static Task<string> DecryptAsync(this string cipherString) => Task.Run(() =>
        {
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            string key = "sohampatel81281";

            using MD5CryptoServiceProvider md = new();
            var keyArray = md.ComputeHash(Encoding.UTF8.GetBytes(key));

            using TripleDESCryptoServiceProvider tdes = new();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        });
        #endregion
        #endregion

        #region Filter Value
        private static List<string> BadWords => new()
        {
            "fuck",
            "ass",
            "fucker",
            "bitch"
        };

        public static Task<string> FilterStringAsync(this string? value, char ch = ' ') => Task.Run(() =>
          {
              var sb = new StringBuilder(value ??= string.Empty);

              foreach (var item in value.Split(ch))
                  if (BadWords.Contains(item.ToLower()))
                      sb.Replace(item, String.Empty);

              return sb.ToString();
          });
        #endregion

        #region SetUpdatedColumn
        public async static Task SetColumnsWithImages<T>(this T value, IFormFile[] files) where T : class
        {
            var arr = new string[5];
            var extension = ".png";

            Question question = value as Question;
            if (question is not null)
            {
                if (files.Length is 1)
                {
                    arr[0] = question.Image1 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 2)
                {
                    arr[0] = question.Image1 = Guid.NewGuid() + extension;
                    arr[1] = question.Image2 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 3)
                {
                    arr[0] = question.Image1 = Guid.NewGuid() + extension;
                    arr[1] = question.Image2 = Guid.NewGuid() + extension;
                    arr[2] = question.Image3 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 4)
                {
                    arr[0] = question.Image1 = Guid.NewGuid() + extension;
                    arr[1] = question.Image2 = Guid.NewGuid() + extension;
                    arr[2] = question.Image3 = Guid.NewGuid() + extension;
                    arr[3] = question.Image4 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 5)
                {
                    arr[0] = question.Image1 = Guid.NewGuid() + extension;
                    arr[1] = question.Image2 = Guid.NewGuid() + extension;
                    arr[2] = question.Image3 = Guid.NewGuid() + extension;
                    arr[3] = question.Image4 = Guid.NewGuid() + extension;
                    arr[4] = question.Image5 = Guid.NewGuid() + extension;
                }
            }

            var reply = value as Reply;
            if (reply is not null)
            {
                if (files.Length is 1)
                {
                    arr[0] = reply.Image1 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 2)
                {
                    arr[0] = reply.Image1 = Guid.NewGuid() + extension;
                    arr[1] = reply.Image2 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 3)
                {
                    arr[0] = reply.Image1 = Guid.NewGuid() + extension;
                    arr[1] = reply.Image2 = Guid.NewGuid() + extension;
                    arr[2] = reply.Image3 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 4)
                {
                    arr[0] = reply.Image1 = Guid.NewGuid() + extension;
                    arr[1] = reply.Image2 = Guid.NewGuid() + extension;
                    arr[2] = reply.Image3 = Guid.NewGuid() + extension;
                    arr[3] = reply.Image4 = Guid.NewGuid() + extension;
                }
                else if (files.Length is 5)
                {
                    arr[0] = reply.Image1 = Guid.NewGuid() + extension;
                    arr[1] = reply.Image2 = Guid.NewGuid() + extension;
                    arr[2] = reply.Image3 = Guid.NewGuid() + extension;
                    arr[3] = reply.Image4 = Guid.NewGuid() + extension;
                    arr[4] = reply.Image5 = Guid.NewGuid() + extension;
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                using var fs = new FileStream(ImagePath(name: arr[i], directory: "Questions"), FileMode.Create);
                await files[i].CopyToAsync(fs).ConfigureAwait(false);
                await fs.FlushAsync().ConfigureAwait(false);
            }
        }
        #endregion

        #region SetMetaData
        public static Task<IEnumerable<T>> SetMetaDataAsync<T>(this IEnumerable<T> data, params Action<T>[] actions)
            where T : class => Task.Run(() =>
        {
            if (data is null)
                throw new NullReferenceException();

            foreach (var item in data)
                foreach (var action in actions)
                    action(item);

            return data;
        });

        public static IEnumerable<T> SetMetaData<T>(this IEnumerable<T> data, params Action<T>[] actions) where T : class
        {
            if (data is null)
                throw new NullReferenceException();

            foreach (var item in data)
                foreach (var action in actions)
                    action(item);

            return data;
        }

        public static void SetUserState(this User user) =>
            (user.Password, user.RegistrationDate) = (null, null);

        public static Task<T> SetMetaDataAsync<T>(this T data, params Action<T>[] actions)
            where T : class => Task.Run(() =>
        {
            if (data is null)
                throw new NullReferenceException();

            foreach (var action in actions)
                action(data);

            return data;
        });
        #endregion

        #region DeleteImages
        public static void DeleteImages<T>(this T value)
        {
            string path;
            Question question;
            if ((question = value as Question) is not null)
            {
                if (question.Image1 is not null)
                {
                    path = ImagePath(question.Image1, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (question.Image2 is not null)
                {
                    path = ImagePath(question.Image2, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (question.Image3 is not null)
                {
                    path = ImagePath(question.Image3, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (question.Image4 is not null)
                {
                    path = ImagePath(question.Image4, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (question.Image5 is not null)
                {
                    path = ImagePath(question.Image5, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
            }

            Reply reply;
            if ((reply = value as Reply) is not null)
            {
                if (reply.Image1 is not null)
                {
                    path = ImagePath(reply.Image1, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (reply.Image2 is not null)
                {
                    path = ImagePath(reply.Image2, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (reply.Image3 is not null)
                {
                    path = ImagePath(reply.Image3, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (reply.Image4 is not null)
                {
                    path = ImagePath(reply.Image4, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
                if (reply.Image5 is not null)
                {
                    path = ImagePath(reply.Image5, "Questions");
                    if (Exists(path))
                        Delete(path);
                }
            }
        }
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

        #region SerializeToJson
        public static string SerializeToJson<T>(T data/*, IJsonFormatterResolver jsonFormatterResolver = null*/) =>
            JsonSerializer.ToJsonString(data, /*jsonFormatterResolver ?? */StandardResolver.ExcludeNull);
        #endregion
    }

    public class FormDataModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bindingContext = bindingContext ??
                throw new ArgumentNullException();

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject(valueAsString, bindingContext.ModelType);
                if (result != null)
                    bindingContext.Result = ModelBindingResult.Success(result);
            }
            return Task.CompletedTask;
        }
    }
}