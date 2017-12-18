namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActionLog")]
    public partial class ActionLog
    {
        public int ActionLogID { get; set; }

        public DateTime? ActionTime { get; set; }

        [StringLength(250)]
        public string ActionType { get; set; }

        [StringLength(50)]
        public string IPClientAdress { get; set; }

        [StringLength(50)]
        public string HostClientAdress { get; set; }

        [StringLength(250)]
        public string ServerUrl { get; set; }

        [StringLength(250)]
        public string Status { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? UserID { get; set; }

        public int? RoleID { get; set; }

        public int? PermissionID { get; set; }

        public int? DomainDataID { get; set; }

        public virtual DomainData DomainData { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
