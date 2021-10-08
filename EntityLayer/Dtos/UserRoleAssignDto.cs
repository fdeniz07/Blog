using System.Collections.Generic;

namespace EntityLayer.Dtos
{
    public class UserRoleAssignDto
    {
        public UserRoleAssignDto()
        {
            RoleAssignDtos = new List<RoleAssignDto>(); //Listeyi interface atmak sorun olmaz. Ama interface icerisindeki bir listeye tek tek eleman atilmaya kalkildiginda hata verir. O yüzden constructor olarak atama yapiyoruz.
        }
        public int UserId { get; set; }

        public string UserName { get; set; }

        public IList<RoleAssignDto> RoleAssignDtos { get; set; }
    }
}
