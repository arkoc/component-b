using System;
using System.Threading.Tasks;

namespace ComponentB.Stores
{
    public interface IBlockchainNodesStore
    {
        Task<Uri[]> GetNodesAsync(string blockchain);
    }
}
