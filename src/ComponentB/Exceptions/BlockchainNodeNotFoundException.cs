using System;

namespace ComponentB.Exceptions
{
    public class BlockchainNodeNotFoundException : Exception
    {
        public BlockchainNodeNotFoundException(string blockchain) : base($"Blockchain node for {blockchain} blockchain not found in blockchain nodes store.")
        {
        }
    }
}
