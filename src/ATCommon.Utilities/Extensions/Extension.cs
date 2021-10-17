using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace ATCommon.Utilities.Extensions
{
    public static class Extension
    {
        private static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T XmlToObject<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        private static ColumnMapperAttribute GetColumnMapperAttribute(PropertyInfo propertyInfo)
        {
            var mapperAttribute = propertyInfo
                .GetCustomAttributes(typeof(ColumnMapperAttribute), true)
                .Select(o => o as ColumnMapperAttribute).FirstOrDefault();

            return mapperAttribute;
        }
        public static List<T> ConvertDataTableToList<T>(this object dataTable) where T : class, new()
        {
            var dt = dataTable as DataTable;
            var arr = new List<T>();
            var entityType = typeof(T);

            var properties = entityType.GetProperties();

            if (dt == null)
            {
                return arr;
            }
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new T();
                foreach (var property in properties)
                {
                    var mapperAttribute = GetColumnMapperAttribute(property);
                    var propertyName = mapperAttribute == null ? property.Name : mapperAttribute.HeaderName;

                    dynamic value = dt.Rows[i][propertyName].ToString();
                    if (i >= 0 && dt.Rows.Count > i && dt.Rows[i][propertyName].ToString() != "")
                    {
                        var converter = TypeDescriptor.GetConverter(property.PropertyType);
                        var result = converter.ConvertFrom(value);
                        obj.GetType().GetProperty(property.Name)?.SetValue(obj, result, null);
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        obj.GetType().GetProperty(property.Name)?.SetValue(obj, "", null);
                    }
                }

                arr.Add(obj);
            }

            return arr;
        }

        /// <summary>
        /// DataTable'ı ObjectEe çevirir
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static T ConvertDataTableToSingle<T>(this object dataTable) where T : class, new()
        {
            var dt = dataTable as DataTable;
            var entityType = typeof(T);

            var properties = entityType.GetProperties();
            var obj = new T();
            if (dt == null)
            {
                return obj;
            }
            foreach (var property in properties)
            {
                var mapperAttribute = GetColumnMapperAttribute(property);
                var propertyName = mapperAttribute == null ? property.Name : mapperAttribute.HeaderName;
                dynamic value = dt.Rows[0][propertyName].ToString();

                var converter = TypeDescriptor.GetConverter(property.PropertyType);
                var result = converter.ConvertFrom(value);

                obj.GetType().GetProperty(property.Name)?.SetValue(obj, result, null);
            }

            return obj;
        }

        /// <summary>
        ///   Boş değeri Db yazdırmak için kullanıyoruz
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static object ToDbNull(this object objValue)
        {
            if (objValue == null)
            {
                return DBNull.Value;
            }

            if (string.IsNullOrWhiteSpace(objValue.ToString()))
            {
                return DBNull.Value;
            }

            return objValue;
        }

        /// <summary>
        /// Gelen Boş değeri ekrana yazdırmak için kullanıyoruz
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static object ToEmpty(this object strValue)
        {
            return ReferenceEquals(strValue, DBNull.Value) ? null : strValue;
        }

        /// <summary>
        /// Cümledeki Kelimelerin ilk harflerini büyük hale getirir
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FirstUpperAllLower(this string text)
        {
            text = text.TrimEnd();
            text = text.TrimStart();
            text = text.Replace("_", " ");
            var newString = string.Empty;

            if (text == "") return newString;
            var textArr = text.Split(' ');
            var clearTextArr = new List<string>();

            if (textArr.Length > 1)
            {
                clearTextArr.AddRange(textArr.Where(item => !string.IsNullOrWhiteSpace(item)));

                newString = clearTextArr
                    .Select(item => item.ToLower())
                    .Select(tempString => char.ToUpper(tempString[0]) + tempString[1..]).
                    Aggregate(newString, (current, tempString) => current == string.Empty ? tempString : $"{current} {tempString}");
            }
            else
            {
                text = text.ToLower();
                text = char.ToUpper(text[0]) + text[1..];
                newString = text;
            }

            return newString;
        }

        /// <summary>
        /// Gönderilen text içerisindeki _ karakterini temizler ve baş harfleri büyük yapar
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceColumnName(this string text)
        {
            text = text.TrimEnd();
            text = text.TrimStart();
            text = text.Replace("_", " ");
            text = text.Replace(".", " ");
            var newString = string.Empty;

            if (text != "")
            {
                var textArr = text.Split(' ');
                var clearTextArr = new List<string>();

                if (textArr.Length > 1)
                {
                    clearTextArr.AddRange(textArr.Where(item => !string.IsNullOrWhiteSpace(item)));

                    newString = clearTextArr
                        .Select(item => item.ToLower())
                        .Select(tempString => char.ToUpper(tempString[0]) + tempString[1..])
                        .Aggregate(newString, (current, tempString) => current == string.Empty ? tempString : $"{current} {tempString}");
                }
                else
                {
                    text = text.ToLower();
                    text = char.ToUpper(text[0]) + text.Substring(1);
                    newString = text;
                }
            }

            newString = newString.Replace(" ", string.Empty);
            newString = ToEnglishCharacter(newString);
            return newString;
        }


        /// <summary>
        /// Türkçe karakterleri ingilizce karakterlere çevirir.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string ToEnglishCharacter(this string word)
        {
            var wordIn = word.ToCharArray();
            var result = new StringBuilder();
            for (var i = 0; i < wordIn.Length; i++)
            {
                wordIn[i] = wordIn[i] switch
                {
                    'ç' => 'c',
                    'ğ' => 'g',
                    'ı' => 'i',
                    'ö' => 'o',
                    'ş' => 's',
                    'ü' => 'u',
                    'Ç' => 'C',
                    'Ğ' => 'G',
                    'İ' => 'I',
                    'Ö' => 'O',
                    'Ş' => 'S',
                    'Ü' => 'U',
                    _ => wordIn[i]
                };

                result.AppendLine(wordIn[i].ToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// Html taglarını siler
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string html)
        {
            var value = Regex.Replace(html, "<[^>]*>", string.Empty);
            return value;
        }

        /// <summary>
        /// verilen string değerin belirli bir kısmını alır.
        /// </summary>
        /// <param name="stringToShorten"></param>
        /// <param name="newLength"></param>
        /// <returns></returns>
        public static string ShortenString(this string stringToShorten, int newLength)
        {
            if (newLength > stringToShorten.Length) return stringToShorten;

            var cutOffPoint = stringToShorten.IndexOf(" ", newLength - 1,StringComparison.InvariantCulture);

            if (cutOffPoint <= 0)
                cutOffPoint = stringToShorten.Length;

            return stringToShorten[..cutOffPoint];
        }

        public static string GetDataType(string dataType)
        {
            return dataType switch
            {
                "VARCHAR2" => "string",
                "NUMBER" => "decimal",
                "DATE" => "DateTime",
                "RAW" => "string",
                _ => "string"
            };
        }

        public static string Base64Encode(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string Base64Decode(string text)
        {
            var decodedText = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(decodedText);
        }

        private static string GetFolderPath(string folder)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);
        }
        public static void WriteTextFile(FileMode fileMode, string message, string folder, string fileName)
        {
            var folderPath = GetFolderPath(folder);
            var filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fs = new FileStream(filePath, fileMode, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static string ReadTextFile(string folder, string fileName)
        {
            var folderPath = GetFolderPath(folder);
            var filePath = Path.Combine(folderPath, fileName);
            if (!File.Exists(filePath))
            {
                return "";
            }

            var sr = new StreamReader(filePath);
            var text = sr.ReadToEnd();
            if (string.IsNullOrWhiteSpace(text.Trim()))
                return "";

            text = text.Trim();
            sr.Close();
            return text;
        }

        public static bool IsIn(object value, params object[] parameters)
        {
            return parameters.Any(item => string.Equals(item.ToString(), value.ToString(), StringComparison.OrdinalIgnoreCase));
        }
    }
}