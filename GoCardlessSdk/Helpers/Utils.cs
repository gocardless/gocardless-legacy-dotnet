using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GoCardlessSdk.Helpers
{
    internal static class Utils
    {
        /// <summary>
        /// Generate a random base64-encoded string
        /// </summary>
        /// <returns>a randomly generated string</returns>
        internal static string GenerateNonce()
        {
            const int size = 60;
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890+/".ToCharArray();
            var crypto = new RNGCryptoServiceProvider();
            var data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        internal class HashParams : List<KeyValuePair<string, object>>
        {
            internal IEnumerable<object> this[string key]
            {
                get { return this.Where(x => x.Key == key).Select(x => x.Value); }
            }

            internal void Add(string propertyName, object value)
            {
                this.Add(new KeyValuePair<string, object>(propertyName, value));
            }
        }

        /// <summary>
        /// Percent encode a string according to RFC 5849 (section 3.6)
        /// </summary>
        /// <param name="s">the string to encode</param>
        /// <returns>the encoded string</returns>
        internal static string PercentEncode(this string s)
        {
            // NOTE: HttpUtility.UrlEncode(s) or HttpUtility.UrlPathEncode(s) don't quite do what we need :(
            const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            var encoded = new StringBuilder();
            foreach (var c in s)
            {
                if (unreservedChars.Contains(c))
                {
                    encoded.Append(c);
                }
                else
                {
                    encoded.Append('%');
                    encoded.Append(String.Format("{0:X2}", (int) c));
                }
            }
            return encoded.ToString();
        }

        /// <summary>
        /// Format a Time object according to ISO 8601, and convert to UTC.
        /// </summary>
        /// <param name="time">the time object to format</param>
        /// <returns>the ISO-formatted time</returns>
        internal static string IsoFormatTime(this DateTimeOffset time)
        {
            return time.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        /// <summary>
        /// Converts PascalCase and camelCase to underscore_case. Leaves underscore_case unaffected.
        /// </summary>
        /// <param name="s">the string to convert</param>
        /// <returns>the underscore_cased string</returns>
        internal static string ToUnderscoreCase(this string s)
        {
            return Regex.Replace(s, @"(\p{Ll})(\p{Lu})", "$1_$2").ToLower();
        }

        internal static string ToUrlString(this object o)
        {
            if (o is DateTimeOffset)
            {
                return ((DateTimeOffset)o).IsoFormatTime().PercentEncode();
            }
            if (o is decimal)
            {
                return ((decimal) o).ToString("0.00", CultureInfo.InvariantCulture);
            }
            return o.ToString().PercentEncode();
        }



        internal static HashParams ToHashParams(
            this object queryStringable, HashParams hash = null, string prefix = null)
        {
            Func<object, bool> isOfSimpleType = o =>
            {
                var type = o.GetType();
                return type.IsPrimitive
                       || type == typeof(string)
                       || type == typeof(decimal)
                       || type == typeof(DateTimeOffset)
                    ;
            };

            PropertyInfo[] propertyInfos = queryStringable.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance);

            hash = hash ?? new HashParams();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetSetMethod(true) == null)
                {
                    continue;
                }
                var value = propertyInfo.GetValue(queryStringable, null);
                if (value != null)
                {
                    var propertyName = (prefix != null)
                                           ? prefix + "[" + propertyInfo.Name.ToUnderscoreCase() + "]"
                                           : propertyInfo.Name.ToUnderscoreCase();
                    if (value is Array)
                    {
                        foreach (var innerValue in (Array)value)
                        {
                            if (isOfSimpleType(innerValue))
                            {
                                if (innerValue is Boolean)
                                {
                                    hash.Add(propertyName + "[]", Convert.ToInt16(innerValue));
                                }
                                else
                                {
                                    hash.Add(propertyName + "[]", innerValue);
                                }
                            }
                            else
                            {
                                innerValue.ToHashParams(hash, propertyName + "[]");
                            }
                        }
                    }
                    else if (isOfSimpleType(value))
                    {
                        if (value is Boolean)
                        {
                            hash.Add(propertyName, Convert.ToInt16(value));
                        }
                        else
                        {
                            hash.Add(propertyName, value);
                        }
                    }
                    else
                    {
                        value.ToHashParams(hash, propertyName);
                    }
                }
            }
            return hash;
        }

        internal static string ToQueryString(this object queryStringable)
        {
            return queryStringable.ToHashParams().ToQueryString();
        }

        internal static string ToQueryString(this HashParams hash)
        {
            var s = new StringBuilder();
            foreach (string key in hash.Select(x => x.Key).Distinct().OrderBy(x => x))
            {
                string key1 = key;
                foreach (var value in hash[key1].OrderBy(x => x.ToString()))
                {
                    s.Append("&");
                    s.Append(key.ToUrlString());
                    s.Append("=");
                    s.Append(value.ToUrlString());
                }
            }

            // TODO: percent_encoding

            // cut off the first &
            return s.ToString().Substring(1);
        }

        /// <summary>
        /// Given a Hash of parameters, normalize them (flatten and convert to a
        /// string), then generate the HMAC-SHA-256 signature using the provided key.
        /// </summary>
        /// <param name="params">the parameters to sign</param>
        /// <param name="key">the key to sign the params with</param>
        /// <returns>the resulting signature</returns>
        internal static string GetSignatureForParams(HashParams @params, string key)
        {
            Byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Byte[] paramsBytes = Encoding.UTF8.GetBytes(@params.ToQueryString());
            Byte[] hashedBytes = new HMACSHA256(keyBytes).ComputeHash(paramsBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}