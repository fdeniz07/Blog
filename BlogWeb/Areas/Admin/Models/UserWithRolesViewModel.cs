using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BlogWeb.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        //Bu bölüm, yetkisiz kullanicilarin yetkisi olmayan bölümleri görmemesini saglar
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
