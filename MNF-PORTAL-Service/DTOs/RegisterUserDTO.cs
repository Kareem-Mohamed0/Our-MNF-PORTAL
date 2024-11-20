using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.DTOs
{
    public class RegisterUserDTO
    {
        public string Full_Name { get; set; }
        public string User_Name { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
}
