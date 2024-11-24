﻿namespace MNF_PORTAL_Service.DTOs
{
    public class DetailsUserDTO
    {
        public string User_Id { get; set; }
        public string Full_Name { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public bool IsActive { get; set; }
    }
}
