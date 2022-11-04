namespace GamesLib.BusinessLogic.Dtos;

public class GameDto
{
    public int Id { get; set; }

    public string Title { get; set; }
        
    public string Description { get; set; }

    public DateTime ReleaseDate { get; set; }

    public int Rating { get; set; }
    
    public string DevName { get; set; }
        
    public string PublisherName { get; set; }
}