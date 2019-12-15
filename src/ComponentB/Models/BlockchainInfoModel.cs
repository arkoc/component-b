using System;

namespace ComponentB.Models
{
    /// <summary>
    /// Represents Info Model.
    /// </summary>
    public class BlockchainInfoModel
    {
        /// <summary>
        /// Gets or sets the underlying node endpoint.
        /// </summary>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// Gets or sets last synced block number.
        /// </summary>
        public long LastBlockHeight { get; set; }
    }
}