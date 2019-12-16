using System;

namespace ComponentB.Exceptions
{
    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException(uint assetId) : base($"Asset with {assetId} not found in assets store.")
        {

        }
    }
}
