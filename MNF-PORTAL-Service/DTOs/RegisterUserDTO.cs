namespace MNF_PORTAL_Service.DTOs
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string User_Name { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
