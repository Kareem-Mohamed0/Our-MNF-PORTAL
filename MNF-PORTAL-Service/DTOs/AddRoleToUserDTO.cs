using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.DTOs
{
    public class AddRoleToUserDTO
    {
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
    }
}
