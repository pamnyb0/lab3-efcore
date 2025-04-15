using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PubAPI.Endpoints;

/// <summary>
/// Endpoints for managing books in the publishing system
/// </summary>
public static class BookEndpoints
{
    /// <summary>
    /// Maps all book-related endpoints
    /// </summary>
    /// <param name="routes">The route builder to add endpoints to</param>
    public static void MapBookEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/book").WithTags(nameof(Book));

        group.MapGet("/", GetAllBooks)
            .WithName("GetAllBooks")
            .WithDescription("Retrieves a list of all books with their author information");

        group.MapGet("/{id}", GetBookById)
            .WithName("GetBookById")
            .WithDescription("Retrieves a specific book by its ID with author information");

        group.MapPost("/", CreateBook)
            .WithName("CreateBook")
            .WithDescription("Creates a new book with the specified details");

        group.MapPut("/{id}", UpdateBook)
            .WithName("UpdateBook")
            .WithDescription("Updates an existing book with the specified details");

        group.MapDelete("/{id}", DeleteBook)
            .WithName("DeleteBook")
            .WithDescription("Deletes a specific book by its ID");
    }

    /// <summary>
    /// Gets all books with their authors
    /// </summary>
    private static async Task<IResult> GetAllBooks(PubContext db)
    {
        return Results.Ok(await db.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .ToListAsync());
    }

    /// <summary>
    /// Gets a specific book by ID
    /// </summary>
    private static async Task<IResult> GetBookById(int id, PubContext db)
    {
        return await db.Books
            .Include(b => b.Author)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BookId == id)
            is Book book
            ? Results.Ok(book)
            : Results.NotFound();
    }

    /// <summary>
    /// Creates a new book
    /// </summary>
    private static async Task<IResult> CreateBook(Book book, PubContext db)
    {
        if (!await db.Authors.AnyAsync(a => a.AuthorId == book.AuthorId))
            return Results.BadRequest("Invalid AuthorId - Author does not exist");

        db.Books.Add(book);
        await db.SaveChangesAsync();

        // Reload the book with author included
        var createdBook = await db.Books
            .Include(b => b.Author)
            .FirstAsync(b => b.BookId == book.BookId);

        return Results.Created($"/api/book/{book.BookId}", createdBook);
    }

    /// <summary>
    /// Updates an existing book
    /// </summary>
    private static async Task<IResult> UpdateBook(int id, Book book, PubContext db)
    {
        if (id != book.BookId)
            return Results.BadRequest("ID mismatch");

        if (!await db.Authors.AnyAsync(a => a.AuthorId == book.AuthorId))
            return Results.BadRequest("Invalid AuthorId - Author does not exist");

        db.Entry(book).State = EntityState.Modified;

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await db.Books.AnyAsync(b => b.BookId == id))
                return Results.NotFound();
            throw;
        }

        return Results.NoContent();
    }

    /// <summary>
    /// Deletes a specific book
    /// </summary>
    private static async Task<IResult> DeleteBook(int id, PubContext db)
    {
        var book = await db.Books.FindAsync(id);
        if (book == null)
            return Results.NotFound();

        db.Books.Remove(book);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
} 