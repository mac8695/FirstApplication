namespace FirstApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string GameId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = " Name")]

        public string Name { get; set; }

        [Display(Name = "Is Multiplayer")]
        public bool IsMultiplayer { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Genres")]
        [InverseProperty("Game")]
        public virtual ICollection<GameGenre> Genres { get; set; } = new HashSet<GameGenre>();

        [Display(Name = "Ratings")]
        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
