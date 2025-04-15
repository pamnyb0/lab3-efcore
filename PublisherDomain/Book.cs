using System.ComponentModel.DataAnnotations;

namespace PublisherDomain;

/// <summary>
/// Represents a book in the publishing system
/// </summary>
public class Book
{
    /// <summary>
    /// The unique identifier for the book
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// The title of the book
    /// </summary>
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; }

    /// <summary>
    /// The publication date of the book
    /// </summary>
    [Required(ErrorMessage = "Publication date is required")]
    public DateOnly PublishDate { get; set; }

    /// <summary>
    /// The base price of the book
    /// </summary>
    [Required(ErrorMessage = "Base price is required")]
    [Range(0.01, 1000.00, ErrorMessage = "Price must be between $0.01 and $1000.00")]
    public decimal BasePrice { get; set; }

    /// <summary>
    /// The author of the book
    /// </summary>
    public Author Author { get; set; }

    /// <summary>
    /// The ID of the book's author
    /// </summary>
    [Required(ErrorMessage = "Author ID is required")]
    public int AuthorId { get; set; }

    /// <summary>
    /// The cover design of the book
    /// </summary>
    public Cover Cover { get; set; }
}
