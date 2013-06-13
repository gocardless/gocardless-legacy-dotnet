using System;

namespace GoCardlessSdk.WebHooks
{
    /// <summary>
    /// GoCardless - StringTuple
    /// </summary>
    internal class StringTuple : IComparable<StringTuple>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringTuple"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public StringTuple(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other.
        /// </returns>
        public int CompareTo(StringTuple other)
        {
            var delta = this.Key.CompareTo(other.Key);
            return delta == 0 ? this.Value.CompareTo(other.Value) : delta;
        }
    }
}
