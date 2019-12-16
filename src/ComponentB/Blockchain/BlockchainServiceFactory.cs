using ComponentB.Exceptions;
using ComponentB.Stores;
using System;
using System.Threading.Tasks;

namespace ComponentB.Blockchain
{
    public class BlockchainServiceFactory : IBlockchainServiceFactory
    {
        private readonly IAssetsStore _assetsStore;
        private readonly IBlockchainNodesStore _blockchainNodesStore;

        public BlockchainServiceFactory(
            IAssetsStore assetsStore,
            IBlockchainNodesStore blockchainNodesStore)
        {
            _assetsStore = assetsStore ?? throw new ArgumentNullException(nameof(assetsStore));
            _blockchainNodesStore = blockchainNodesStore ?? throw new ArgumentNullException(nameof(blockchainNodesStore));
        }

        public async Task<IBlockchainService> CreateAsync(
            uint assetId)
        {
            var asset = await _assetsStore.GetAssetAsync(assetId);
            if (asset == null)
            {
                throw new AssetNotFoundException(assetId);
            }

            var nodes = await _blockchainNodesStore.GetNodesAsync(asset.Blockchain);
            if (nodes == null)
            {
                throw new AssetNotFoundException(assetId);
            }

            throw new NotImplementedException();
        }
    }
}
