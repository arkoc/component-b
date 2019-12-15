namespace ComponentB.Models
{
    /// <summary>
    /// Represents a status for blockchain's health.
    /// </summary>
    public enum BlockchainHealthStatus
    {
        /// <summary>
        /// The blockchain is healthy.
        /// </summary>
        Healthy,

        /// <summary>
        /// Can't sync to the blockchain.
        /// </summary>
        BadSync,

        /// <summary>
        /// Problem occured while connecting to the responsible party (node or wallet).
        /// </summary>
        BadConnection,
    }
}