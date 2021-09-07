using System;

namespace CoreLayer.Entities.Abstract
{
    public abstract class EntityBase
    {
        // Base sinifimiz, tablolarimizdaki ortak alanlari tutar ve miras alan siniflar degisiklik isterlerse override anahtari ile degisiklik yapabilirler.

        public EntityBase()
        {
            CreatedDate = ModifiedDate = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }

        public virtual int Id { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime ModifiedDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual string CreatedByName { get; set; } = "Admin";

        public virtual string ModifiedByName { get; set; } = "Admin";

        public virtual string Note { get; set; }
    }
}
