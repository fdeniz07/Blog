using System;

namespace CoreLayer.Entities.Abstract
{
    public abstract class UserBase
    {
        // UserBase sinifimiz, login olacak kullanici tablolarindaki ortak alanlari tutar ve miras alan siniflar degisiklik isterlerse override anahtari ile degisiklik yapabilirler.

        public UserBase()
        {
            CreatedDate = ModifiedDate = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
            IsArchived = false;
        }

        public virtual int Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual byte[] PasswordHash { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime ModifiedDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual bool IsArchived { get; set; }

        public virtual string CreatedByName { get; set; } = "Admin";

        public virtual string ModifiedByName { get; set; } = "Admin";

        public virtual string Note { get; set; }
    }
}
