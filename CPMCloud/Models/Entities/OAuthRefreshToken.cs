namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OAuthRefreshToken")]
    public partial class OAuthRefreshToken
    {
        [Key]
        [StringLength(500)]
        public string TokenId { get; set; }

        [StringLength(500)]
        public string Token { get; set; }

        [StringLength(500)]
        public string Authentication { get; set; }
    }
}
