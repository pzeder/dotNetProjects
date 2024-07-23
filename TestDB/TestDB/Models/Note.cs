﻿using System.ComponentModel.DataAnnotations;
namespace TestDB.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Boolean IsImportant { get; set; }
    }
}
