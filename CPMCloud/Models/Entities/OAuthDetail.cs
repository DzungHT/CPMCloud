namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OAuthDetail
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Key]
        [StringLength(50)]
        public string ClientId { get; set; }

        [Required]
        [StringLength(50)]
        public string IpAccess { get; set; }

        public virtual OAuthClientDetail OAuthClientDetail { get; set; }
    }
}
