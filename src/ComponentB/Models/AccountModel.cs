namespace ComponentB.Models
{
    /// <summary>
    /// An account class with basic information to use it.
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Gets or sets the account's address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the account's public key.
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the account's private key.
        /// </summary>
        public string PrivateKey { get; set; }
    }
}