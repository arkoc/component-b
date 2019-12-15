using System;
using System.Globalization;
using System.Numerics;

namespace ComponentB.Common
{
    /// <summary>
    /// Represents an arbitrarily large signed decimal.
    /// </summary>
    public partial struct BigDecimal : IComparable, IComparable<BigDecimal>, IEquatable<BigDecimal>
    {
        private const int _precision = 50;
        private BigInteger _mantissa;
        private int _exponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
        /// </summary>
        /// <param name="mantissa">The mantissa of decimal.</param>
        public BigDecimal(BigInteger mantissa)
            : this(mantissa, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
        /// </summary>
        /// <param name="mantissa">The mantissa of decimal.</param>
        /// <param name="exponent">The exponent of decimal.</param>
        public BigDecimal(BigInteger mantissa, int exponent)
            : this()
        {
            _mantissa = mantissa;
            _exponent = exponent;
            Normalize();
            Truncate(_precision);
        }

        /// <summary>
        /// Gets a value that represents the number one (1).
        /// </summary>
        public static BigDecimal One => new BigDecimal(BigInteger.One, 0);

        /// <summary>
        /// Gets a value that represents the number negative one (-1).
        /// </summary>
        public static BigDecimal MinusOne => new BigDecimal(BigInteger.MinusOne, 0);

        /// <summary>
        /// Gets a value that represents the number zero (0).
        /// </summary>
        public static BigDecimal Zero => new BigDecimal(BigInteger.Zero, 0);

        /// <summary>
        /// Gets specifies the decimal separator symbol.
        /// </summary>
        public static string DecimalSeparator => CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        /// <summary>
        ///  Converts the string representation of a number to its <see cref="BigDecimal"/> equivalent.
        /// </summary>
        /// <param name="value">A string that contains the number to convert.</param>
        /// <returns>A value that is equivalent to the number specified in the value parameter.</returns>
        public static BigDecimal Parse(string value)
        {
            var bigInt = BigInteger.Parse(value.Replace(DecimalSeparator, string.Empty));
            var decimals = 0;

            if (value.Contains(DecimalSeparator))
            {
                decimals = value.Length - value.IndexOf(DecimalSeparator) - 1;
            }

            var exponent = decimals * -1;
            return new BigDecimal(bigInt, exponent);
        }

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="BigDecimal"/> equivalent, and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">The string representation of a number.</param>
        /// <param name="result">The result value.</param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string value, out BigDecimal result)
        {
            try
            {
                result = Parse(value);
                return true;
            }
            catch
            {
                result = Zero;
                return false;
            }
        }

        /// <summary>
        /// Truncate the number to the given precision by removing the least significant digits.
        /// </summary>
        /// <param name="precision">Precision for trucating.</param>
        /// <returns>The truncated number.</returns>
        public BigDecimal Truncate(int precision)
        {
            // copy this instance (remember it's a struct)
            var shortened = this;

            // save some time because the number of digits is not needed to remove trailing zeros
            shortened.Normalize();

            // remove the least significant digits, as long as the number of digits is higher than the given Precision
            while (NumberOfDigits(shortened._mantissa) > precision)
            {
                shortened._mantissa /= 10;
                shortened._exponent++;
            }

            // normalize again to make sure there are no trailing zeros left
            shortened.Normalize();
            return shortened;
        }

        /// <summary>
        /// Floors and the current BigDecimal value.
        /// </summary>
        /// <returns>The floored BigDecimal value.</returns>
        public BigDecimal Floor()
        {
            return Truncate(NumberOfDigits(_mantissa) + _exponent);
        }

        /// <summary>
        /// Converts the numeric value of the current System.Numerics.BigInteger object to its equivalent string representation by using the specified format.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <returns>The string representation of the current <see cref="BigDecimal"/> value in the format specified by the format parameter.</returns>
        public string ToString(string format)
        {
            if (format.Equals("E", StringComparison.OrdinalIgnoreCase))
            {
                return string.Concat(_mantissa.ToString(), "E", _exponent);
            }
            else
            {
                return ToString();
            }
        }

        /// <summary>
        /// Converts the numeric value of the current <see cref="BigDecimal"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the current System.Numerics.BigInteger value.</returns>
        public override string ToString()
        {
            var mantissaWithoutSign = BigInteger.Abs(_mantissa).ToString();

            var decimalCount = _exponent * -1;
            if (decimalCount > 0)
            {
                mantissaWithoutSign = mantissaWithoutSign.PadLeft(decimalCount + 1, '0');
                mantissaWithoutSign = mantissaWithoutSign.Insert(mantissaWithoutSign.Length - decimalCount, DecimalSeparator);
            }
            else
            {
                mantissaWithoutSign = mantissaWithoutSign.PadRight(mantissaWithoutSign.Length + _exponent, '0');
            }

            if (_mantissa.Sign < 0)
            {
                mantissaWithoutSign = "-" + mantissaWithoutSign;
            }

            return mantissaWithoutSign;
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance and a specified System.Numerics.BigInteger have the same value.
        /// </summary>
        /// <param name="other">The object to compare.</param>
        /// <returns>true if this System.Numerics.BigInteger object and other have the same value otherwise, false.</returns>
        public bool Equals(BigDecimal other)
        {
            return other._mantissa.Equals(_mantissa) && other._exponent == _exponent;
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance and a specified System.Numerics.BigInteger have the same value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>true if this System.Numerics.BigInteger object and other have the same value otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is BigDecimal && Equals((BigDecimal)obj);
        }

        /// <summary>
        /// Returns the hash code for the current System.Numerics.BigInteger object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (_mantissa.GetHashCode() * 397) ^ _exponent;
            }
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null || !(obj is BigDecimal))
            {
                throw new ArgumentException();
            }

            return CompareTo((BigDecimal)obj);
        }

        /// <inheritdoc />
        public int CompareTo(BigDecimal other)
        {
            return this < other ? -1 : (this > other ? 1 : 0);
        }

        private static int NumberOfDigits(BigInteger value)
        {
            return (int)Math.Ceiling(BigInteger.Log10(value * value.Sign));
        }

        private static BigDecimal Add(BigDecimal left, BigDecimal right)
        {
            return left._exponent > right._exponent
                ? new BigDecimal(AlignExponent(left, right) + right._mantissa, right._exponent)
                : new BigDecimal(AlignExponent(right, left) + left._mantissa, left._exponent);
        }

        private static BigInteger AlignExponent(BigDecimal value, BigDecimal reference)
        {
            return value._mantissa * BigInteger.Pow(10, value._exponent - reference._exponent);
        }

        private void Normalize()
        {
            if (_mantissa.IsZero)
            {
                _exponent = 0;
            }
            else
            {
                BigInteger remainder = 0;
                while (remainder == 0)
                {
                    var shortened = BigInteger.DivRem(_mantissa, 10, out remainder);
                    if (remainder == 0)
                    {
                        _mantissa = shortened;
                        _exponent++;
                    }
                }

                if (_exponent > 0)
                {
                    _mantissa *= BigInteger.Pow(10, _exponent);
                    _exponent = 0;
                }
            }
        }

        public static implicit operator BigDecimal(int value)
        {
            return new BigDecimal(value, 0);
        }

        public static implicit operator BigDecimal(double value)
        {
            var mantissa = (BigInteger)value;
            var exponent = 0;
            double scaleFactor = 1;
            while (Math.Abs((value * scaleFactor) - (double)mantissa) > 0)
            {
                exponent -= 1;
                scaleFactor *= 10;
                mantissa = (BigInteger)(value * scaleFactor);
            }

            return new BigDecimal(mantissa, exponent);
        }

        public static implicit operator BigDecimal(decimal value)
        {
            var mantissa = (BigInteger)value;
            var exponent = 0;
            decimal scaleFactor = 1;
            while ((decimal)mantissa != value * scaleFactor)
            {
                exponent -= 1;
                scaleFactor *= 10;
                mantissa = (BigInteger)(value * scaleFactor);
            }

            return new BigDecimal(mantissa, exponent);
        }

        public static implicit operator BigDecimal(BigInteger value)
        {
            return new BigDecimal(value);
        }

        public static implicit operator BigDecimal(long value)
        {
            return new BigDecimal(value, 0);
        }

        public static implicit operator BigDecimal(ulong value)
        {
            return new BigDecimal(value, 0);
        }

        public static explicit operator double(BigDecimal value)
        {
            return double.Parse(value.ToString(), CultureInfo.InvariantCulture);
        }

        public static explicit operator float(BigDecimal value)
        {
            return float.Parse(value.ToString(), CultureInfo.InvariantCulture);
        }

        public static explicit operator decimal(BigDecimal value)
        {
            return decimal.Parse(value.ToString(), CultureInfo.InvariantCulture);
        }

        public static explicit operator int(BigDecimal value)
        {
            return Convert.ToInt32((decimal)value);
        }

        public static explicit operator uint(BigDecimal value)
        {
            return Convert.ToUInt32((decimal)value);
        }

        public static explicit operator ulong(BigDecimal value)
        {
            return Convert.ToUInt64((decimal)value);
        }

        public static explicit operator BigInteger(BigDecimal value)
        {
            return BigInteger.Parse(value.ToString());
        }

        /// <summary>
        /// <see cref="BigDecimal"/> value to the power of a specified value.
        /// </summary>
        /// <param name="basis">The basis.</param>
        /// <param name="exponent">The exponent to raise value by.</param>
        /// <returns>he result of raising value to the exponent power.</returns>
        public static BigDecimal Pow(double basis, double exponent)
        {
            var tmp = (BigDecimal)1;
            while (Math.Abs(exponent) > 100)
            {
                var diff = exponent > 0 ? 100 : -100;
                tmp *= Math.Pow(basis, diff);
                exponent -= diff;
            }

            return tmp * Math.Pow(basis, exponent);
        }

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="exponent">A number specifying a power.</param>
        /// <returns>The number e raised to the power d.</returns>
        public static BigDecimal Exp(double exponent)
        {
            var tmp = (BigDecimal)1;
            while (Math.Abs(exponent) > 100)
            {
                var diff = exponent > 0 ? 100 : -100;
                tmp *= Math.Exp(diff);
                exponent -= diff;
            }

            return tmp * Math.Exp(exponent);
        }

        public static BigDecimal operator +(BigDecimal value)
        {
            return value;
        }

        public static BigDecimal operator -(BigDecimal value)
        {
            value._mantissa *= -1;
            return value;
        }

        public static BigDecimal operator ++(BigDecimal value)
        {
            return value + 1;
        }

        public static BigDecimal operator --(BigDecimal value)
        {
            return value - 1;
        }

        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            return Add(left, right);
        }

        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            return Add(left, -right);
        }

        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            return new BigDecimal(left._mantissa * right._mantissa, left._exponent + right._exponent);
        }

        public static BigDecimal operator /(BigDecimal dividend, BigDecimal divisor)
        {
            var exponentChange = _precision - (NumberOfDigits(dividend._mantissa) - NumberOfDigits(divisor._mantissa));
            if (exponentChange < 0)
            {
                exponentChange = 0;
            }

            dividend._mantissa *= BigInteger.Pow(10, exponentChange);
            return new BigDecimal(dividend._mantissa / divisor._mantissa, dividend._exponent - divisor._exponent - exponentChange);
        }

        public static BigDecimal operator %(BigDecimal left, BigDecimal right)
        {
            return left - (right * (left / right).Floor());
        }

        public static bool operator ==(BigDecimal left, BigDecimal right)
        {
            return left._exponent == right._exponent && left._mantissa == right._mantissa;
        }

        public static bool operator !=(BigDecimal left, BigDecimal right)
        {
            return left._exponent != right._exponent || left._mantissa != right._mantissa;
        }

        public static bool operator <(BigDecimal left, BigDecimal right)
        {
            return left._exponent > right._exponent ? AlignExponent(left, right) < right._mantissa : left._mantissa < AlignExponent(right, left);
        }

        public static bool operator >(BigDecimal left, BigDecimal right)
        {
            return left._exponent > right._exponent ? AlignExponent(left, right) > right._mantissa : left._mantissa > AlignExponent(right, left);
        }

        public static bool operator <=(BigDecimal left, BigDecimal right)
        {
            return left._exponent > right._exponent ? AlignExponent(left, right) <= right._mantissa : left._mantissa <= AlignExponent(right, left);
        }

        public static bool operator >=(BigDecimal left, BigDecimal right)
        {
            return left._exponent > right._exponent ? AlignExponent(left, right) >= right._mantissa : left._mantissa >= AlignExponent(right, left);
        }
    }
}
