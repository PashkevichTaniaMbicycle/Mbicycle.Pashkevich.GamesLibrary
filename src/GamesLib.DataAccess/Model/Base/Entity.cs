using System.ComponentModel.DataAnnotations;

namespace GamesLib.DataAccess.Model.Base;

public abstract class Entity
{
    public int Id { get; set; }
        
    [MaxLength(50)]
    public string Title { get; set; }
        
    [MaxLength(300)]
    public string Description { get; set; }
}