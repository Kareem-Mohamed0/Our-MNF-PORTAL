using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.DTOs
{
    public class ResetPasswordDTO
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}
