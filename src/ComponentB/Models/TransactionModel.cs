using System.Collections.Generic;

namespace ComponentB.Models
{
    /// <summary>
    /// A transaction class, which represents a transaction.
    /// </summary>
    public class TransactionModel
    {
        /// <summary>
        /// Gets or sets the id of the transaction.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the height of the transaction.
        /// </summary>
        public long Height { get; set; }

        /// <summary>
        /// Gets or sets the status of the transaction.
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the sources list of the transaction.
        /// </summary>
        public List<AddressAmountPair> Sources { get; set; } = new List<AddressAmountPair>();

        /// <summary>
        /// Gets or sets the recipient list of the transaction.
        /// </summary>
        public List<AddressAmountPair> Recipients { get; set; } = new List<AddressAmountPair>();
    }
}