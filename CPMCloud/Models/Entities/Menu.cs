namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            RoleMenus = new HashSet<RoleMenu>();
        }

        public int MenuID { get; set; }

        public int? MenuPID { get; set; }

        public int? ApplicationID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string FontIcon { get; set; }

        [StringLength(500)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        [StringLength(50)]
        public string Controller { get; set; }

        public int? Sort_Order { get; set; }

        [StringLength(100)]
        public string Path { get; set; }

        public int? Status { get; set; }

        public virtual Application Application { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
