using GamesLib.DataAccess.Model.Base;

namespace GamesLib.DataAccess.Model;

public class Game : Entity
{
    //public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
        
    public int Rating { get; set; }
    public Dev Dev { get; set; }
        
    public Publisher Publisher { get; set; }
}