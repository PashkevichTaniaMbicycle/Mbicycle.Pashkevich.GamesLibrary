using GamesLib.DataAccess.Model.Base;

namespace GamesLib.DataAccess.Model
{
    public class Game : Entity
    {
        //public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Platforms { get; set; }
        
        public string Genres { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public int Rating { get; set; }

        public int DevId { get; set; }
        
        public int PublisherId { get; set; }

        public Dev Dev { get; set; }
        
        public Publisher Publisher { get; set; }
    }
}