using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ATCommon.Utilities.Extensions
{
    public static class Extension
    {
        public static Stream ToStream(this string @this)
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
            var reader = XmlReader.Create(@this.Trim().ToStream(),
                new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        public static List<T> ConvertDataTableToList<T>(this object dataTable) where T : class, new()
        {
            DataTable dt = dataTable as DataTable;
            List<T> arr = new List<T>();
            Type entityType = typeof(T);

            PropertyInfo[] properties = entityType.GetProperties();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T obj = new T();
                foreach (PropertyInfo property in properties)
                {
                    ColumnMapperAttribute mapperAttribute = property
                        .GetCustomAttributes(typeof(ColumnMapperAttribute), true)
                        .Select((o) => o as ColumnMapperAttribute).FirstOrDefault();
                    string propertyName = mapperAttribute == null ? property.Name : mapperAttribute.HeaderName;

                    dynamic value = dt.Rows[i][propertyName].ToString();
                    if (i >= 0 && dt.Rows.Count > i && dt.Rows[i][propertyName].ToString() != "")
                    {
                        var converter = TypeDescriptor.GetConverter(property.PropertyType);
                        var result = converter.ConvertFrom(value);
                        obj.GetType().GetProperty(property.Name).SetValue(obj, result, null);
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        obj.GetType().GetProperty(property.Name).SetValue(obj, "", null);
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
            DataTable dt = dataTable as DataTable;
            Type entityType = typeof(T);

            PropertyInfo[] properties = entityType.GetProperties();
            T obj = new T();

            foreach (PropertyInfo property in properties)
            {
                ColumnMapperAttribute mapperAttribute = property
                    .GetCustomAttributes(typeof(ColumnMapperAttribute), true).Select((x) => x as ColumnMapperAttribute)
                    .FirstOrDefault();
                string propertyName = mapperAttribute == null ? property.Name : mapperAttribute.HeaderName;
                dynamic value = dt.Rows[0][propertyName].ToString();

                var converter = TypeDescriptor.GetConverter(property.PropertyType);
                var result = converter.ConvertFrom(value);

                obj.GetType().GetProperty(property.Name).SetValue(obj, result, null);
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
            else if (string.IsNullOrWhiteSpace(objValue.ToString()))
            {
                return DBNull.Value;
            }
            else
            {
                return objValue;
            }
        }

        /// <summary>
        /// Gelen Boş değeri ekrana yazdırmak için kullanıyoruz
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static object ToEmpty(this object strValue)
        {
            if (ReferenceEquals(strValue, DBNull.Value))
            {
                return null;
            }

            return strValue;
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
            List<string> clearTextArr = new List<string>();

            if (textArr.Length > 1)
            {
                clearTextArr.AddRange(textArr.Where(item => !string.IsNullOrWhiteSpace(item)));

                foreach (var item in clearTextArr)
                {
                    var tempString = string.Empty;
                    tempString = item.ToLower();
                    tempString = char.ToUpper(tempString[0]) + tempString.Substring(1);
                    if (newString == string.Empty)
                    {
                        newString = tempString;
                    }
                    else
                    {
                        newString = $"{newString} {tempString}";
                    }
                }
            }
            else
            {
                text = text.ToLower();
                text = char.ToUpper(text[0]) + text.Substring(1);
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
            string newString = string.Empty;

            if (text != "")
            {
                var textArr = text.Split(' ');
                var clearTextArr = new List<string>();

                if (textArr.Length > 1)
                {
                    clearTextArr.AddRange(textArr.Where(item => !string.IsNullOrWhiteSpace(item)));

                    foreach (var item in clearTextArr)
                    {
                        var tempString = string.Empty;
                        tempString = item.ToLower();
                        tempString = char.ToUpper(tempString[0]) + tempString.Substring(1);
                        if (newString == string.Empty)
                        {
                            newString = tempString;
                        }
                        else
                        {
                            newString = $"{newString} {tempString}";
                        }
                    }
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
            var wordin = word.ToCharArray();
            var result = new StringBuilder();
            for (int i = 0; i < wordin.Length; i++)
            {
                switch (wordin[i])
                {
                    case 'ç':
                        wordin[i] = 'c';
                        break;
                    case 'ğ':
                        wordin[i] = 'g';
                        break;
                    case 'ı':
                        wordin[i] = 'i';
                        break;
                    case 'ö':
                        wordin[i] = 'o';
                        break;
                    case 'ş':
                        wordin[i] = 's';
                        break;
                    case 'ü':
                        wordin[i] = 'u';
                        break;
                    case 'Ç':
                        wordin[i] = 'C';
                        break;
                    case 'Ğ':
                        wordin[i] = 'G';
                        break;
                    case 'İ':
                        wordin[i] = 'I';
                        break;
                    case 'Ö':
                        wordin[i] = 'O';
                        break;
                    case 'Ş':
                        wordin[i] = 'S';
                        break;
                    case 'Ü':
                        wordin[i] = 'U';
                        break;
                    default:
                        break;
                }

                result.AppendLine(wordin[i].ToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// Html taglarını siler
        /// </summary>
        /// <param name="Html"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string Html)
        {
            var value = System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
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

            int cutOffPoint = stringToShorten.IndexOf(" ", newLength - 1);

            if (cutOffPoint <= 0)
                cutOffPoint = stringToShorten.Length;

            return stringToShorten.Substring(0, cutOffPoint);
        }

        public static string GetDataType(string dataType)
        {
            if (dataType == "VARCHAR2")
            {
                return "string";
            }
            else if (dataType == "NUMBER")
            {
                return "decimal";
            }
            else if (dataType == "DATE")
            {
                return "DateTime";
            }
            else if (dataType == "RAW")
            {
                return "string";
            }

            return "string";
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

        public static void WriteTextFile(FileMode fileMode, string message, string folder, string fileName)
        {
            var folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);
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
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);
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