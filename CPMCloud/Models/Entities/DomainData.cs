namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DomainData")]
    public partial class DomainData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DomainData()
        {
            ActionLogs = new HashSet<ActionLog>();
            UserRoleDatas = new HashSet<UserRoleData>();
        }

        public int DomainDataID { get; set; }

        public int? DataID { get; set; }

        [StringLength(50)]
        public string DataCode { get; set; }

        [StringLength(200)]
        public string DataName { get; set; }

        public int? ParentID { get; set; }

        [StringLength(100)]
        public string Path { get; set; }

        [StringLength(1000)]
        public string FullPath { get; set; }

        public int? Status { get; set; }

        public int? DomainTypeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public int? PathLevel { get; set; }

        public int? IsActive { get; set; }

        public int? MarketCompanyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActionLog> ActionLogs { get; set; }

        public virtual DomainType DomainType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleData> UserRoleDatas { get; set; }
    }
}
