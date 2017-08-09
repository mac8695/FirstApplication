namespace FirstApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameGenre
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string GameGenreId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Genre")]
        public string GenreId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Game")]
        public string GameId { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("GameId")]
        [Display(Name ="Game Name")]
        public virtual Game Game { get; set; }

        [ForeignKey("GenreId")]
        [Display(Name = "Genre Name")]
        public virtual Genre Genre { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Game.Name, Genre.Name);
        }
    }
}
