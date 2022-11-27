//criacao das classes que serviram como dependecia.

using Microsoft.AspNetCore.Identity;

namespace Doiman
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
    }
}

