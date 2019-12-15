using ComponentB.Common;

namespace ComponentB.Models
{
    /// <summary>
    /// A recipient class, which represents a single (address, value) tuple.
    /// </summary>
    public class TransactionRecipientModel
    {
        /// <summary>
        /// Gets or sets the address of the recipient.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the sent amount to the recipient.
        /// </summary>
        public BigDecimal Amount { get; set; }
    }
}