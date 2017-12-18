namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ActionLogs = new HashSet<ActionLog>();
            UserRoles = new HashSet<UserRole>();
            UserRoleDatas = new HashSet<UserRoleData>();
        }

        public int UserID { get; set; }

        [StringLength(50)]
        public string LoginName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? NeedChangePassword { get; set; }

        public DateTime? ChangePasswordTime { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        public int? Status { get; set; }

        [StringLength(100)]
        public string ActiveCode { get; set; }

        public DateTime? RequestDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActionLog> ActionLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleData> UserRoleDatas { get; set; }
    }
}
