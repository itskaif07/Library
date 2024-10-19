using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Library.Models
{
    public class Api
    {
        [Key]
        public string ExternalId { get; set; } // Google Books unique ID

        public VolumeInfo VolumeInfo { get; set; } // Include VolumeInfo in your Api model
    }

    public class VolumeInfo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required.")]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Display(Name = "Author(s)")]
        public List<string> Authors { get; set; }

        [Display(Name = "Book Description")]
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }

        public ImageLinks ImageLinks { get; set; } // Include ImageLinks for book cover
    }

    public class ImageLinks
    {
        [Key] // Marking this property as the primary key
        public int Id { get; set; }
        public string Thumbnail { get; set; } // Add property for thumbnail image
    }
}
