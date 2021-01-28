﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.IO.File;

namespace CodeFactoryAPI.Extra
{
    public static class Addons
    {
        public static string ImagePath(string name, string directory = "Users") =>
                 Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", directory, name);

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

        public static Task<string> FilterStringAsync(this string? value) => Task.Run(() =>
        {
            var sb = new StringBuilder(value ??= string.Empty);

            foreach (var item in value.Split(' '))
                if (BadWords.Contains(item.ToLower()))
                    sb.Replace(item, String.Empty);

            return sb.ToString();
        });
        #endregion

        #region SetUpdatedColumn
        public async static Task SetColumnsWithImages(this Question question, IFormFile[] files)
        {
            var list = new List<string>();
            var extension = ".png";

            if (files.Length is 1)
            {
                list.Add(question.Image = Guid.NewGuid() + extension);
            }
            else if (files.Length is 2)
            {
                list.Add(question.Image = Guid.NewGuid() + extension);
                list.Add(question.Image2 = Guid.NewGuid() + extension);
            }
            else if (files.Length is 3)
            {
                list.Add(question.Image = Guid.NewGuid() + extension);
                list.Add(question.Image2 = Guid.NewGuid() + extension);
                list.Add(question.Image3 = Guid.NewGuid() + extension);
            }
            else if (files.Length is 4)
            {
                list.Add(question.Image = Guid.NewGuid() + extension);
                list.Add(question.Image2 = Guid.NewGuid() + extension);
                list.Add(question.Image3 = Guid.NewGuid() + extension);
                list.Add(question.Image4 = Guid.NewGuid() + extension);
            }
            else if (files.Length is 5)
            {
                list.Add(question.Image = Guid.NewGuid() + extension);
                list.Add(question.Image2 = Guid.NewGuid() + extension);
                list.Add(question.Image3 = Guid.NewGuid() + extension);
                list.Add(question.Image4 = Guid.NewGuid() + extension);
                list.Add(question.Image5 = Guid.NewGuid() + extension);
            }

            for (int i = 0; i < list.Count; i++)
            {
                using (var fs = new FileStream(ImagePath(name: list[i], directory: "Questions"), FileMode.Create))
                {
                    await files[i].CopyToAsync(fs).ConfigureAwait(false);
                    await fs.FlushAsync().ConfigureAwait(false);
                }
            }
        }
        #endregion

        #region SetMetaData
        public static Task<IEnumerable<T>> SetMetaData<T>(this IEnumerable<T> data, params Action<T>[] actions)
            where T : class => Task.Run(() =>
        {
            if (data is null)
                throw new NullReferenceException();

            foreach (var item in data)
                foreach (var action in actions)
                    action(item);

            return data;
        });

        public static Task<T> SetMetaData<T>(this T data, params Action<T>[] actions)
            where T : class => Task.Run(() =>
        {
            if (data is null)
                throw new NullReferenceException();

            foreach (var action in actions)
                action(data);

            return data;
        });
        #endregion

        #region DeleteQuestionImages
        public static void DeleteImages(this Question question)
        {
            string path;
            if (question.Image is not null)
            {
                path = ImagePath(question.Image, "Questions");
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
        #endregion

        #region SingleRecord
        public static Task<T?> FirstOneAsync<T>(this IEnumerable<T> data, Predicate<T> predicate) where T : class => Task.Run(() =>
        {
            foreach (var item in data)
                if (predicate(item))
                    return item;

            return null;
        });
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