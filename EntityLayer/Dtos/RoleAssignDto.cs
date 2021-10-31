using CoreLayer.Entities.Abstract;

namespace EntityLayer.Dtos
{
    public class RoleAssignDto : IDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool HasRole { get; set; }

    }
}
