using System.ComponentModel.DataAnnotations;
using GamesLib.DataAccess.Model.Base;

namespace GamesLib.DataAccess.Model
{
    public class Publisher : Entity
    {
        [MaxLength(50)]
        public string Title { get; set; }
        
        [MaxLength(300)]
        public string Description { get; set; }
    }
}