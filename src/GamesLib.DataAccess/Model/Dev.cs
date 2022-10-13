using System;
using System.ComponentModel.DataAnnotations;

namespace GamesLib.DataAccess.Model 
{
    public class Dev : Base.Model
    {
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
    }
}

