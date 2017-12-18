namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OAuthClientDetail
    {
        [Key]
        [StringLength(50)]
        public string ClientId { get; set; }

        [StringLength(50)]
        public string ResourceIds { get; set; }

        [StringLength(50)]
        public string ClientSecret { get; set; }

        [StringLength(50)]
        public string Scope { get; set; }

        [StringLength(50)]
        public string GrantTypes { get; set; }

        [StringLength(50)]
        public string WebServerRedirectUri { get; set; }

        [StringLength(50)]
        public string Authorities { get; set; }

        public int? AccessTokenValidity { get; set; }

        public int? RefreshTokenValidity { get; set; }

        public virtual OAuthDetail OAuthDetail { get; set; }
    }
}
