using ComponentB.Common;

namespace ComponentB.Models
{
    /// <summary>
    /// Represents a estimated fee for a transaction.
    /// </summary>
    public class FeeEstimationModel
    {
        /// <summary>
        /// Gets or sets the name for transaction execution unit.
        /// </summary>
        public string ExecutionUnitName { get; set; }

        /// <summary>
        /// Gets or sets the fee estimation for <see cref="ExecutionUnitName"/>.
        /// </summary>
        public BigDecimal FeePerExecutionUnit { get; set; }

        /// <summary>
        /// Gets or sets the fee estimation for a transaction.
        /// </summary>
        public BigDecimal StandardTransactionFeeEstimation { get; set; }
    }
}