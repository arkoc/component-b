using ComponentB.Models;
using System.Threading.Tasks;

namespace ComponentB.Blockchain
{
    /// <summary>
    /// Represents a blockchain service.
    /// </summary>
    public interface IBlockchainService
    {
        /// <summary>
        /// Checkes whetver passed address is valid blockchain account or not.
        /// </summary>
        /// <param name="address">The address to validate.</param>
        /// <returns>True if address is valid, otherwise false.</returns>
        Task<bool> IsValidAddressAsync(string address);

        /// <summary>
        /// Creates new account.
        /// </summary>
        /// <returns>The newly created account.</returns>
        Task<AccountModel> NewAddressAsync();

        /// <summary>
        /// Gets blocks by block no.
        /// </summary>
        /// <param name="blockNo">The block no.</param>
        /// <returns>The requested block.</returns>
        Task<BlockModel> GetBlockAsync(long blockNo);

        /// <summary>
        /// Publishes the spcified signed raw trnasaction to blockchain network.
        /// </summary>
        /// <param name="rawSignedTransaction">The raw transaction to publish.</param>
        /// <returns>The published transaction.</returns>
        Task<TransactionModel> PublishTransactionAsync(string rawSignedTransaction);

        /// <summary>
        /// Checks whether the underlying blockchain is healthy.
        /// </summary>
        /// <returns>The status of the blockchain's health.</returns>
        Task<BlockchainHealthModel> CheckHealthAsync();

        /// <summary>
        /// Gets the estimation for transactions' fees.
        /// </summary>
        /// <param name="confirmationTarget">Confirmation target in blocks (1 - 1008).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<FeeEstimationModel> GetFeeEstimationAsync(int? confirmationTarget = null);

        /// <summary>
        /// Gets the general information about node.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<BlockchainInfoModel> GetInfoAsync();
    }
}
