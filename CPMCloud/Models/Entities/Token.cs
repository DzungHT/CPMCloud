namespace CPMCloud.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Token
    {
        public int TokenId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string AuthToken { get; set; }

        public DateTime IssuesOn { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
