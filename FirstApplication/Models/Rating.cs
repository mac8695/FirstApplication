namespace FirstApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string RatingId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AspNetUser User{ get; set; }

        [Required]
        [StringLength(128)]
        public string GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game{ get; set; }

        public decimal Rank { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;
    }
}
