using System;

namespace ComponentB.Models
{
    /// <summary>
    /// Represents a blockchain health model.
    /// </summary>
    public class BlockchainHealthModel
    {
        /// <summary>
        /// Gets or sets health status.
        /// </summary>
        public BlockchainHealthStatus HealthStatus { get; set; }

        /// <summary>
        /// Gets or sets last synced block number.
        /// </summary>
        public long LastBlockHeight { get; set; }

        /// <summary>
        /// Gets or sets last synced block time.
        /// </summary>
        public DateTimeOffset LastBlockTime { get; set; }
    }
}