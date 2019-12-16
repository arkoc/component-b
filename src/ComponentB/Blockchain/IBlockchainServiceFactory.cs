using System.Threading.Tasks;

namespace ComponentB.Blockchain
{
    /// <summary>
    /// Represents interface for blockchain service factory.
    /// </summary>
    public interface IBlockchainServiceFactory
    {
        /// <summary>
        /// Creates concrete <see cref="IBlockchainService"/> for specified currencyId.
        /// </summary>
        /// <param name="assetId">The asset Id.</param>
        /// <returns>The concrete <see cref="IBlockchainService"/> for specified currencyId.</returns>
        Task<IBlockchainService> CreateAsync(uint assetId);
    }
}
