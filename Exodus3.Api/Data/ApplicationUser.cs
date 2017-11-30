using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Exodus3.Api.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<ApplicationRole> Roles { get; } = new List<ApplicationRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; } = new List<IdentityUserClaim<Guid>>();
        public ApplicationUser()
        {
        }
    }
}
