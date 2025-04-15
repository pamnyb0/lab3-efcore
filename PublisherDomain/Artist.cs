namespace PublisherDomain;

public class Artist
{
    public int ArtistId { get; set; }
    public required string FirstName { get; set;}
    public required string LastName { get; set;}
    public List<Cover> Covers
      { get; set; } = new();
}
