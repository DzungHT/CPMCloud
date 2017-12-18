namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OAuthAccessToken")]
    public partial class OAuthAccessToken
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(500)]
        public string TokenId { get; set; }

        [StringLength(500)]
        public string Token { get; set; }

        [StringLength(500)]
        public string AuthenticationId { get; set; }

        [StringLength(500)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string ClientId { get; set; }

        [StringLength(500)]
        public string Authentication { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string RefreshToken { get; set; }
    }
}
