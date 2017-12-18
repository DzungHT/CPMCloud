namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permission()
        {
            ActionLogs = new HashSet<ActionLog>();
            Roles = new HashSet<Role>();
        }

        public int PermissionID { get; set; }

        public int? ResourceID { get; set; }

        public int? OperationID { get; set; }

        [StringLength(200)]
        public string Code { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActionLog> ActionLogs { get; set; }

        public virtual Operation Operation { get; set; }

        public virtual Resource Resource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Roles { get; set; }

        internal static bool HasPermission(object sEARCH)
        {
            throw new NotImplementedException();
        }
    }
}
