namespace ComponentB.Models
{
    /// <summary>
    /// Represents the status of the transaction.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Indicates that the transaction failed
        /// </summary>
        Failed,

        /// <summary>
        /// Indicates that the transaction is pending
        /// </summary>
        Pending,

        /// <summary>
        /// Indicates that the transaction was not validated
        /// </summary>
        Unvalidated,

        /// <summary>
        /// Indicates that the transaction succeded
        /// </summary>
        Succeeded,
    }
}
