using Microsoft.AspNetCore.Identity;

namespace Casino.Api.Domain.Entities
{
    public class Player : IdentityUser
    {
        public Wallet Wallet { get; set; }
        public int WalletId { get; set; }
    }
}
