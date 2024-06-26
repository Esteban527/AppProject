﻿using System.ComponentModel.DataAnnotations;

namespace AppProject.Web.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
