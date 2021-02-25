using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBase.Entities
{
    public class Book  
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name  { get; set; }
        public int PagesCount { get; set; } 
        public string About { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        
    }
}
