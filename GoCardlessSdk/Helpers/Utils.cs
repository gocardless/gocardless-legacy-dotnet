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
    /// <summary>
    /// GoCardless - Utils
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Generate a random base64-encoded string
        /// </summary>
        /// <returns>
        /// a randomly generated string
        /// </returns>
        internal static string GenerateNonce()
        {
            const int SIZE = 60;
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890+/".ToCharArray();
            var crypto = new RNGCryptoServiceProvider();
            var data = new byte[SIZE];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(SIZE);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Percent encode a string according to RFC 5849 (section 3.6)
        /// </summary>
        /// <param name="s">the string to encode</param>
        /// <returns>the encoded string</returns>
        internal static string PercentEncode(this string s)
        {
            // NOTE: HttpUtility.UrlEncode(s) or HttpUtility.UrlPathEncode(s) don't quite do what we need :(
            const string UNRESERVED_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            var encoded = new StringBuilder();

            foreach (var c in s)
            {
                if (UNRESERVED_CHARS.Contains(c))
                {
                    encoded.Append(c);
                }
                else
                {
                    encoded.Append('%');
                    encoded.Append(string.Format("{0:X2}", (int)c));
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

        /// <summary>
        /// Toes the URL string.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>string url</returns>
        internal static string ToUrlString(this object o)
        {
            if (o is DateTimeOffset)
            {
                return ((DateTimeOffset)o).IsoFormatTime().PercentEncode();
            }

            if (o is decimal)
            {
                return ((decimal)o).ToString("0.00", CultureInfo.InvariantCulture);
            }

            return o.ToString().PercentEncode();
        }

        /// <summary>
        /// Toes the hash params.
        /// </summary>
        /// <param name="queryStringable">The query stringable.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>HashParams object</returns>
        internal static HashParams ToHashParams(
            this object queryStringable, HashParams hash = null, string prefix = null)
        {
            Func<object, bool> isOfSimpleType = o =>
                                                    {
                                                        var type = o.GetType();
                                                        return type.IsPrimitive
                                                               || type == typeof(string)
                                                               || type == typeof(decimal)
                                                               || type == typeof(DateTimeOffset);
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
                                hash.Add(propertyName + "[]", innerValue);
                            }
                            else
                            {
                                innerValue.ToHashParams(hash, propertyName + "[]");
                            }
                        }
                    }
                    else if (isOfSimpleType(value))
                    {
                        hash.Add(propertyName, value);
                    }
                    else
                    {
                        value.ToHashParams(hash, propertyName);
                    }
                }
            }

            return hash;
        }

        /// <summary>
        /// Toes the query string.
        /// </summary>
        /// <param name="queryStringable">The query stringable.</param>
        /// <returns>Query String</returns>
        internal static string ToQueryString(this object queryStringable)
        {
            return queryStringable.ToHashParams().ToQueryString();
        }

        /// <summary>
        /// Toes the query string.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>Query String</returns>
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
            byte[] keybytes = Encoding.UTF8.GetBytes(key);
            byte[] paramsbytes = Encoding.UTF8.GetBytes(@params.ToQueryString());
            byte[] hashedbytes = new HMACSHA256(keybytes).ComputeHash(paramsbytes);
            return BitConverter.ToString(hashedbytes).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// GoCardless - HashParams
        /// </summary>
        internal class HashParams : List<KeyValuePair<string, object>>
        {
            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            /// <param name="key">The key</param>
            /// <returns>The element at the specified index.</returns>
            /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than 0.-or-index is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count"></see>. </exception>
            internal IEnumerable<object> this[string key]
            {
                get { return this.Where(x => x.Key == key).Select(x => x.Value); }
            }

            /// <summary>
            /// Adds the specified property name.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <param name="value">The value.</param>
            internal void Add(string propertyName, object value)
            {
                this.Add(new KeyValuePair<string, object>(propertyName, value));
            }
        }
    }
}