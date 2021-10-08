using EntityLayer.Dtos;

namespace BlogWeb.Areas.Admin.Models
{
    public class UserRoleAssignAjaxViewModel
    {
        public UserRoleAssignDto UserRoleAssignDto { get; set; }

        public string RoleAssignPartial { get; set; }

        public UserDto UserDto { get; set; }
    }
}
