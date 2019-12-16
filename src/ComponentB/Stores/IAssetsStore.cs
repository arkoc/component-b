using ComponentB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComponentB.Stores
{
    public interface IAssetsStore
    {
        Task<Asset> GetAssetAsync(uint assetId);
    }
}
