

using System.ComponentModel.DataAnnotations;

namespace MNF_PORTAL_Service.DTOs
{
    public class UpdateRoleDTO
    {
        [Required(ErrorMessage ="Please enter an old role")]
        [MinLength(3, ErrorMessage = "Old role must be at least 3 characters long")]
        public string OldRole { get; set; }
        [Required(ErrorMessage = "Please enter an new role")]
        [MinLength(3, ErrorMessage = "New role must be at least 3 characters long")]
        public string NewRole { get; set; }
    }
}
