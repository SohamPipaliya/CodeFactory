using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Extra
{
    public static class Addons
    {
        private static readonly object o = new();

        public static string ImagePath(string name, string directory = "Users")
        {
            lock (o)
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", directory, name);
            }
        }

        #region SetColumnState
        public static void SetUpdatedColumns<T>(this EntityEntry<T> value, params string[] columnNames) where T : class
        {
            lock (o)
            {
                //unit.GetUser.context.Attach(user);
                foreach (var item in columnNames)
                    value.Property(item).IsModified = true;
            }
        }
        #endregion

        #region Triple DES
        #region Encryption
        public static Task<string> EncryptAsync(this string toEncrypt) => Task.Run(() =>
        {
            lock (o)
            {
                byte[] resultArray;
                byte[] keyArray;
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

                string key = "sohampatel81281";
                using (MD5CryptoServiceProvider md = new())
                {
                    keyArray = md.ComputeHash(Encoding.UTF8.GetBytes(key));
                    md.Clear();
                }

                using (TripleDESCryptoServiceProvider tdes = new())
                {
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform cTransform = tdes.CreateEncryptor())
                    {
                        resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    }
                    tdes.Clear();
                }
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        });
        #endregion

        #region Decryption
        public static Task<string> DecryptAsync(this string cipherString) => Task.Run(() =>
        {
            lock (o)
            {
                byte[] keyArray;
                byte[] resultArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                string key = "sohampatel81281";
                using (MD5CryptoServiceProvider md = new())
                {
                    keyArray = md.ComputeHash(Encoding.UTF8.GetBytes(key));
                    md.Clear();
                }

                using (TripleDESCryptoServiceProvider tdes = new())
                {
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform cTransform = tdes.CreateDecryptor())
                    {
                        resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    }
                    tdes.Clear();
                }
                return Encoding.UTF8.GetString(resultArray);
            }
        });
        #endregion
        #endregion

        #region Filter Value
        private static List<string> badWords => new()
        {
            "fuck",
            "ass",
            "fucker",
            "bitch"
        };

        public static Task<string> FilterStringAsync(this string value) => Task.Run(() =>
        {
            value = value ?? string.Empty;
            foreach (var item in value.Split(' '))
            {
                if (badWords.Contains(item.ToLower()))
                    value = value.Replace(item, String.Empty);
            }
            return value;
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