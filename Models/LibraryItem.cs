using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class LibraryItem
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Book title is required.")]
        [DisplayName("Book Title")]
        public string BookTitle { get; set; }

        [DisplayName("Book Content")]
        [Required(ErrorMessage = "Book Content is required.")]
        public string BookContent { get; set; }

        public string? BookCoverImage { get; set; }

        [NotMapped]
        public IFormFile? BookCoverFile { get; set; }

        [DisplayName("Author Name")]
        [MaxLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public string? AuthorName { get; set; }

        [DisplayName("Book Description")]
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        public string? Description { get; set; }

        public string ExternalId { get; set; }


    }
}
