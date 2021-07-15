using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDS.Data
{
    public class IDSConfig
    {
        public List<Client> Clients { get; set; }
        public List<IdentityResource> IdentityResources { get; set; }
    }
}
