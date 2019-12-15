using System.Collections.Generic;

namespace ComponentB.Models
{
    /// <summary>
    /// Represents immutable single block model.
    /// </summary>
    public class BlockModel
    {
        /// <summary>
        /// Gets or sets the height of the block.
        /// </summary>
        public long Height { get; set; }

        /// <summary>
        /// Gets or sets the confirmation number of the block.
        /// </summary>
        public int Confirmation { get; set; }

        /// <summary>
        /// Gets or sets the UNIX time of the block in milliseconds.
        /// </summary>
        public long TimestampInMs { get; set; }

        /// <summary>
        /// Gets or sets the transaction list of the block.
        /// </summary>
        public List<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
    }
}