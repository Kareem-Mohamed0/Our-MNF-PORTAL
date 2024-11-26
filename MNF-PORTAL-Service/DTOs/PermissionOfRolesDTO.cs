namespace MNF_PORTAL_Service.DTOs
{
    public class PermissionOfRolesDTO
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionDTO> AllPermissions { get; set; }

    }
}
