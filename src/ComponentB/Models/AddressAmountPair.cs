using ComponentB.Common;

namespace ComponentB.Models
{
    /// <summary>
    /// A class for address amount pair
    /// </summary>
    public class AddressAmountPair
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public BigDecimal Amount { get; set; }
    }
}